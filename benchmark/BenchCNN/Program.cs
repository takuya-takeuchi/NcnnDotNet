using System;
using System.Threading;
using NcnnDotNet;

namespace BenchCNN
{

    internal class Program
    {

        #region Fields

        private static readonly GlobalGpuInstance GlobalGpuInstance;

        private static int g_warmup_loop_count = 8;

        private static int g_loop_count = 4;

        private static UnlockedPoolAllocator g_blob_pool_allocator = new UnlockedPoolAllocator();

        private static PoolAllocator g_workspace_pool_allocator = new PoolAllocator();

        private static VulkanDevice g_vkdev = null;

        private static VkAllocator g_blob_vkallocator = null;

        private static VkAllocator g_staging_vkallocator = null;

        #endregion

        #region Constructors

        static Program()
        {
            GlobalGpuInstance = Ncnn.IsSupportVulkan ? new GlobalGpuInstance() : null;
        }

        #endregion

        #region Methods

        private static int Main(string[] args)
        {
            var loopCount = 4;
            var numThreads = Ncnn.GetGpuCount();
            var powerSave = 0;
            var gpuDevice = -1;

            if (args.Length >= 1)
                loopCount = int.Parse(args[0]);
            if (args.Length >= 2)
                numThreads = int.Parse(args[1]);
            if (args.Length >= 3)
                powerSave = int.Parse(args[2]);
            if (args.Length >= 4)
                gpuDevice = int.Parse(args[3]);

            var useVulkanCompute = gpuDevice != -1;

            g_loop_count = loopCount;

            g_blob_pool_allocator.SetSizeCompareRatio(0.0f);
            g_workspace_pool_allocator.SetSizeCompareRatio(0.5f);

            if (useVulkanCompute)
            {
                g_warmup_loop_count = 10;
                g_vkdev = Ncnn.GetGpuDevice(gpuDevice);
                g_blob_vkallocator = new VkBlobBufferAllocator(g_vkdev);
                g_staging_vkallocator = new VkStagingBufferAllocator(g_vkdev);
            }

            // default option
            using var opt = new Option();
            opt.LightMode = true;
            opt.NumThreads = numThreads;
            opt.BlobAllocator = g_blob_pool_allocator;
            opt.WorkspaceAllocator = g_workspace_pool_allocator;
            if (Ncnn.IsSupportVulkan)
            {
                opt.BlobVkAllocator = g_blob_vkallocator;
                opt.WorkspaceVkAllocator = g_blob_vkallocator;
                opt.StagingVkAllocator = g_staging_vkallocator;
            }
            opt.UseWinogradConvolution = true;
            opt.UseSgemmConvolution = true;
            opt.UseInt8Inference = true;
            opt.UseVulkanCompute = useVulkanCompute;
            opt.UseFP16Packed = true;
            opt.UseFP16Storage = true;
            opt.UseFP16Arithmetic = true;
            opt.UseInt8Storage = true;
            opt.UseInt8Arithmetic = true;
            opt.UsePackingLayout = true;

            Ncnn.SetCpuPowerSave((PowerSave)powerSave);

            Ncnn.SetOmpDynamic(0);
            Ncnn.SetOmpNumThreads(numThreads);

            Console.WriteLine($"loop_count = {loopCount}");
            Console.WriteLine($"num_threads = {numThreads}");
            Console.WriteLine($"powersave = {(int)Ncnn.SetCpuPowerSave()}");
            Console.WriteLine($"gpu_device = {gpuDevice}");


            // run
            Benchmark("squeezenet", new Mat(227, 227, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("squeezenet_int8", new Mat(227, 227, 3), opt);
                opt.UsePackingLayout = true;
            }

            Benchmark("mobilenet", new Mat(224, 224, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("mobilenet_int8", new Mat(224, 224, 3), opt);
                opt.UsePackingLayout = false;
            }

            Benchmark("mobilenet_v2", new Mat(224, 224, 3), opt);

            //if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            //{
            //    Benchmark("mobilenet_v2_int8", new Mat(224, 224, 3), opt);
            //}

            Benchmark("mobilenet_v3", new Mat(224, 224, 3), opt);

            Benchmark("shufflenet", new Mat(224, 224, 3), opt);

            Benchmark("shufflenet_v2", new Mat(224, 224, 3), opt);

            Benchmark("mnasnet", new Mat(224, 224, 3), opt);

            Benchmark("proxylessnasnet", new Mat(224, 224, 3), opt);

            Benchmark("googlenet", new Mat(224, 224, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("googlenet_int8", new Mat(224, 224, 3), opt);
                opt.UsePackingLayout = false;
            }

            Benchmark("resnet18", new Mat(224, 224, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("resnet18_int8", new Mat(224, 224, 3), opt);
                opt.UsePackingLayout = false;
            }

            Benchmark("alexnet", new Mat(227, 227, 3), opt);

            Benchmark("vgg16", new Mat(224, 224, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("vgg16_int8", new Mat(224, 224, 3), opt);
                opt.UsePackingLayout = false;
            }

            Benchmark("resnet50", new Mat(224, 224, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("resnet50_int8", new Mat(224, 224, 3), opt);
                opt.UsePackingLayout = false;
            }

            Benchmark("squeezenet_ssd", new Mat(300, 300, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("squeezenet_ssd_int8", new Mat(300, 300, 3), opt);
                opt.UsePackingLayout = false;
            }

            Benchmark("mobilenet_ssd", new Mat(300, 300, 3), opt);

            if (!Ncnn.IsSupportVulkan || !useVulkanCompute)
            {
                opt.UsePackingLayout = false;
                Benchmark("mobilenet_ssd_int8", new Mat(300, 300, 3), opt);
                opt.UsePackingLayout = false;
            }

            Benchmark("mobilenet_yolo", new Mat(416, 416, 3), opt);

            Benchmark("mobilenetv2_yolov3", new Mat(352, 352, 3), opt);

            if (Ncnn.IsSupportVulkan)
            {
                g_blob_vkallocator?.Dispose();
                g_staging_vkallocator?.Dispose();
            }

            return 0;
        }

        #region Helpers

        private static void Benchmark(string comment, Mat @in, Option opt)
        {
            @in.Fill(0.01f);

            using var net = new Net();
            net.Opt = opt;


            if (Ncnn.IsSupportVulkan)
            {
                if (net.Opt.UseVulkanCompute)
                {
                    net.SetVulkanDevice(g_vkdev);
                }
            }

            net.LoadParam($"{comment}.param");

            using var dr = new DataReaderFromEmpty();
            net.LoadModel(dr);

            g_blob_pool_allocator.Clear();
            g_workspace_pool_allocator.Clear();

            if (net.Opt.UseVulkanCompute)
            {
                g_blob_vkallocator.Clear();
                g_staging_vkallocator.Clear();
            }

            Thread.Sleep(10 * 1000);

            using var @out = new Mat();

            // warm up
            for (var i = 0; i < g_warmup_loop_count; i++)
            {
                using var ex = net.CreateExtractor();
                ex.Input("data", @in);
                ex.Extract("output", @out);
            }

            var timeMin = double.MaxValue;
            var timeMax = -double.MaxValue;
            var timeAvg = 0d;

            for (var i = 0; i < g_loop_count; i++)
            {
                double start = Ncnn.GetCurrentTime();

                {
                    using var ex = net.CreateExtractor();
                    ex.Input("data", @in);
                    ex.Extract("output", @out);
                }

                double end = Ncnn.GetCurrentTime();

                var time = end - start;

                timeMin = Math.Min(timeMin, time);
                timeMax = Math.Max(timeMax, time);
                timeAvg += time;
            }

            timeAvg /= g_loop_count;

            var com = comment.PadLeft(20, ' ');
            var min = $"{timeMin:F2}".PadLeft(7, ' ');
            var max = $"{timeMax:F2}".PadLeft(7, ' ');
            var avg = $"{timeAvg:F2}".PadLeft(7, ' ');
            Console.WriteLine($"{com}  min = {min}  max = {max}  avg = {avg}");
        }

        #endregion

        #endregion

    }

}