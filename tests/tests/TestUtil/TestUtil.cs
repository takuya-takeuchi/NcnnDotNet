using System;
using NcnnDotNet;

namespace TestUtil
{

    public static class TestUtil
    {

        #region Fields

        private static readonly GlobalGpuInstance _GlobalGpuInstance;

        private static readonly IntPtr _PRngRandState;

        #endregion

        #region Constructors

        static TestUtil()
        {
            _GlobalGpuInstance = new GlobalGpuInstance();

            NativeMethods.prng_prng_rand_t_new(out var ret);
            _PRngRandState = ret;
        }

        #endregion

        #region Methods

        public static void SRAND(ulong seed)
        {
            NativeMethods.prng_prng_srand(seed, _PRngRandState);
        }

        public static ulong RAND()
        {
            NativeMethods.prng_prng_rand(_PRngRandState, out var ret);
            return ret;
        }

        private static int CheckMember<T>(string member, T am, T bm)
        {
            if (!am.Equals(bm))
            {
                Console.Error.WriteLine($"{member} not match {am} {bm}");
                return -1;
            }

            return 0;
        }

        public static int CompareMat(Mat a, Mat b, float epsilon = 0.001f)
        {
            if (CheckMember(nameof(Mat.Dims), a.Dims, b.Dims) != 0) return -1;
            if (CheckMember(nameof(Mat.W), a.W, b.W) != 0) return -1;
            if (CheckMember(nameof(Mat.H), a.H, b.H) != 0) return -1;
            if (CheckMember(nameof(Mat.C), a.C, b.C) != 0) return -1;
            if (CheckMember(nameof(Mat.ElemSize), a.ElemSize, b.ElemSize) != 0) return -1;
            if (CheckMember(nameof(Mat.ElemPack), a.ElemPack, b.ElemPack) != 0) return -1;

            for (int q = 0, c = a.C; q < c; q++)
            {
                using (var ma = a.Channel(q))
                using (var mb = b.Channel(q))
                {
                    for (int i = 0, h = a.H; i < h; i++)
                    {
                        var pa = ma.Row(i);
                        var pb = mb.Row(i);
                        for (int j = 0, w = a.W; j < w; j++)
                        {
                            if(!FloatNearlyEqual(pa[j], pb[j], epsilon))
                            {
                                Console.Error.WriteLine($"value not match at {q} {i} {j}    {pa[j]} {pb[j]}");
                                return -1;
                            }
                        }
                    }
                }
            }

            return 0;
        }

        public static bool FloatNearlyEqual(float a, float b, float epsilon)
        {
            if (Math.Abs(a - b) < float.Epsilon)
                return true;

            var diff = Math.Abs(a - b);
            if (diff <= epsilon)
                return true;

            // relative error
            return diff < epsilon * Math.Max(Math.Abs(a), Math.Abs(b));
        }

        public static float RandomFloat(float a = -2, float b = 2)
        {
            var random = RAND() / (float)unchecked((ulong)(-1)); //RAND_MAX;
            var diff = b - a;
            var r = random * diff;
            return a + r;
        }

        public static void Randomize(Mat m)
        {
            for (var i = 0ul; i < m.Total; i++)
                m[i] = RandomFloat();
        }

        public static Mat RandomMat(int w)
        {
            var m = new Mat(w);
            Randomize(m);
            return m;
        }

        public static Mat RandomMat(int w, int h)
        {
            var m = new Mat(w, h);
            Randomize(m);
            return m;
        }

        public static Mat RandomMat(int w, int h, int c)
        {
            var m = new Mat(w, h, c);
            Randomize(m);
            return m;
        }

        public static int TestLayer<T>(string layerType, ParamDict pd, ModelBin mb, Option opt, Mat a, float epsilon = 0.001f)
            where T : Layer, new()
        {
            return TestLayer<T>(Ncnn.LayerToIndex(layerType), pd, mb, opt, a, epsilon);
        }

        public static int TestLayer<T>(int typeIndex, ParamDict pd, ModelBin mb, Option opt, Mat a, float epsilon = 0.001f)
            where T : Layer, new()
        {
            using (var op = Ncnn.CreateLayer<T>(typeIndex))
            {
                if (!op.SupportPacking)
                    opt.UsePackingLayout = false;

                VulkanDevice vkDev = null;
                VkWeightBufferAllocator gWeightVkAllocator = null;
                VkWeightStagingBufferAllocator gWeightStagingVkAllocator = null;
                VkBlobBufferAllocator gBlobVkAllocator = null;
                VkStagingBufferAllocator gStagingVkAllocator = null;

                if (Ncnn.IsSupportVulkan)
                {
                    vkDev = Ncnn.GetGpuDevice();

                    gWeightVkAllocator = new VkWeightBufferAllocator(vkDev);
                    gWeightStagingVkAllocator = new VkWeightStagingBufferAllocator(vkDev);

                    gBlobVkAllocator = new VkBlobBufferAllocator(vkDev);
                    gStagingVkAllocator = new VkStagingBufferAllocator(vkDev);

                    opt.BlobVkAllocator = gBlobVkAllocator;
                    opt.WorkspaceVkAllocator = gBlobVkAllocator;
                    opt.StagingVkAllocator = gStagingVkAllocator;

                    if (!vkDev.Info.SupportFP16Storage) opt.UseFP16Storage = false;
                    if (!vkDev.Info.SupportFP16Packed) opt.UseFP16Packed = false;

                    op.VkDev = vkDev;
                }

                op.LoadParam(pd);

                op.LoadModel(mb);

                op.CreatePipeline(opt);

                if (Ncnn.IsSupportVulkan)
                {
                    if (opt.UseVulkanCompute)
                    {
                        using var cmd = new VkTransfer(vkDev)
                        {
                            WeightVkAllocator = gWeightVkAllocator,
                            StagingVkAllocator = gWeightStagingVkAllocator
                        };

                        op.UploadModel(cmd, opt);

                        cmd.SubmitAndWait();

                        gWeightStagingVkAllocator?.Clear();
                    }
                }

                using var b = new Mat();
                ((T)op).Forward(a, b, opt);

                var c = new Mat();
                {
                    Mat a4;
                    if (opt.UsePackingLayout)
                    {
                        a4 = new Mat();
                        Ncnn.ConvertPacking(a, a4, 4, opt);
                    }
                    else
                    {
                        a4 = a;
                    }

                    var c4 = new Mat();
                    op.Forward(a4, c4, opt);

                    if (opt.UsePackingLayout)
                    {
                        Ncnn.ConvertPacking(c4, c, 1, opt);
                        c4.Dispose();
                    }
                    else
                    {
                        c?.Dispose();
                        c = c4;
                    }
                }

                Mat d = null;

                try
                {
                    if (Ncnn.IsSupportVulkan)
                    {
                        d = new Mat();

                        if (opt.UseVulkanCompute)
                        {
                            using var a4 = new Mat();
                            Mat a4_fp16 = null;

                            try
                            {
                                // pack
                                Ncnn.ConvertPacking(a, a4, 4, opt);

                                // fp16
                                if (opt.UseFP16Storage || a4.ElemPack == 4 && opt.UseFP16Packed)
                                {
                                    a4_fp16 = new Mat();
                                    Ncnn.CastFloat32ToFloat16(a4, a4_fp16, opt);
                                }
                                else
                                {
                                    a4_fp16 = a4;
                                }

                                // upload
                                using var a4_fp16_gpu = new VkMat();
                                a4_fp16_gpu.CreateLike(a4_fp16, gBlobVkAllocator, gStagingVkAllocator);
                                a4_fp16_gpu.PrepareStagingBuffer();
                                a4_fp16_gpu.Upload(a4_fp16);

                                // forward
                                using var cmd = new VkCompute(vkDev);

                                cmd.RecordUpload(a4_fp16_gpu);

                                using var d4_fp16_gpu = new VkMat();
                                op.Forward(a4_fp16_gpu, d4_fp16_gpu, cmd, opt);

                                d4_fp16_gpu.PrepareStagingBuffer();

                                cmd.RecordDownload(d4_fp16_gpu);

                                cmd.SubmitAndWait();

                                // download
                                using var d4_fp16 = new Mat();
                                d4_fp16.CreateLike(d4_fp16_gpu);
                                d4_fp16_gpu.Download(d4_fp16);

                                // fp32
                                Mat d4 = null;

                                try
                                {
                                    if (opt.UseFP16Storage || d4_fp16.ElemPack == 4 && opt.UseFP16Packed)
                                    {
                                        d4 = new Mat();
                                        Ncnn.CastFloat16ToFloat32(d4_fp16, d4, opt);
                                    }
                                    else
                                    {
                                        d4 = d4_fp16;
                                    }

                                    // unpack
                                    Ncnn.ConvertPacking(d4, d, 1, opt);
                                }
                                finally
                                {
                                    d4?.Dispose();
                                }
                            }
                            finally
                            {
                                a4_fp16?.Dispose();
                            }
                        }
                    }

                    op.DestroyPipeline(opt);

                    // Must dispose here!!
                    op.Dispose();

                    if (Ncnn.IsSupportVulkan)
                    {
                        gBlobVkAllocator.Clear();
                        gStagingVkAllocator.Clear();
                        gWeightVkAllocator.Clear();

                        gBlobVkAllocator?.Dispose();
                        gStagingVkAllocator?.Dispose();
                        gWeightVkAllocator?.Dispose();
                        gWeightStagingVkAllocator?.Dispose();
                    }

                    if (CompareMat(b, c, epsilon) != 0)
                    {
                        Console.Error.WriteLine("test_layer failed cpu");
                        return -1;
                    }

                    if (Ncnn.IsSupportVulkan)
                    {
                        if (opt.UseVulkanCompute && CompareMat(b, d, epsilon) != 0)
                        {
                            Console.Error.WriteLine("test_layer failed gpu");
                            return -1;
                        }
                    }
                }
                finally
                {
                    c?.Dispose();
                    d?.Dispose();
                }
            }

            return 0;
        }

        #endregion

    }

}
