/*
 * This example program is ported by C# from tests/test_deconvolution.cpp.
*/

using System;
using NcnnDotNet;
using NcnnDotNet.Extensions;
using NcnnDotNet.Layers;

namespace TestDeconvolution
{

    internal class Program
    {

        #region Methods

        private static int Main()
        {
            TestUtil.TestUtil.SRAND(7767517);

            return TestDeconvolution0() | TestDeconvolution1();
        }

        #region Helpers

        private static int TestDeconvolution0()
        {
            var kdsp = new[]
            {
                new []{1, 1, 1, 0},
                new []{1, 1, 2, 0},
                new []{2, 1, 1, 1},
                new []{2, 1, 2, 1},
                new []{2, 2, 1, 1},
                new []{2, 2, 2, 1},
                new []{3, 1, 1, 1},
                new []{3, 1, 2, 1},
                new []{3, 2, 1, 1},
                new []{3, 2, 2, 1},
                new []{4, 1, 1, 2},
                new []{4, 1, 2, 2},
                new []{4, 2, 1, 2},
                new []{4, 2, 2, 2},
                new []{5, 1, 1, 2},
                new []{5, 1, 2, 2},
                new []{5, 2, 1, 2},
                new []{5, 2, 2, 2},
                new []{7, 1, 1, 3},
                new []{7, 1, 2, 3},
                new []{7, 1, 3, 3},
                new []{7, 2, 1, 3},
                new []{7, 2, 2, 3},
                new []{7, 2, 3, 3},
            };

            for (var i = 0; i < 24; i++)
            {
                var ret = 0
                          | TestDeconvolution(13, 11, 1, 1, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)
                          | TestDeconvolution(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)
                          | TestDeconvolution(13, 11, 3, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)
                          | TestDeconvolution(13, 11, 4, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)
                          | TestDeconvolution(13, 11, 7, 7, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)
                          | TestDeconvolution(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)
                          | TestDeconvolution(13, 11, 15, 15, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)
                          | TestDeconvolution(13, 11, 16, 16, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, false)

                          | TestDeconvolution(13, 11, 1, 1, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 3, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 3, 12, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 4, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 8, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 16, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                          | TestDeconvolution(13, 11, 16, 16, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, true)
                    ;

                if (ret != 0)
                    return -1;
            }

            return 0;
        }

        private static int TestDeconvolution1()
        {
            var kdsp = new[]
            {
                new []{1, 1, 1, 0},
                new []{1, 1, 2, 0},
                new []{2, 1, 1, 1},
                new []{2, 1, 2, 1},
                new []{2, 2, 1, 1},
                new []{2, 2, 2, 1},
                new []{3, 1, 1, 1},
                new []{3, 1, 2, 1},
                new []{3, 2, 1, 1},
                new []{3, 2, 2, 1},
                new []{4, 1, 1, 2},
                new []{4, 1, 2, 2},
                new []{4, 2, 1, 2},
                new []{4, 2, 2, 2},
                new []{5, 1, 1, 2},
                new []{5, 1, 2, 2},
                new []{5, 2, 1, 2},
                new []{5, 2, 2, 2},
                new []{7, 1, 1, 3},
                new []{7, 1, 2, 3},
                new []{7, 1, 3, 3},
                new []{7, 2, 1, 3},
                new []{7, 2, 2, 3},
                new []{7, 2, 3, 3},
            };

            for (var i = 0; i < 24; i++)
            {
                var ret = 0
                          | TestDeconvolutionInt8(13, 11, 1, 1, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 3, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 4, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 7, 7, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 15, 15, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 16, 16, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)

                          | TestDeconvolutionInt8(13, 11, 1, 1, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 3, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 3, 12, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 4, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 8, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 16, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                          | TestDeconvolutionInt8(13, 11, 16, 16, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1)
                    ;

                if (ret != 0)
                    return -1;
            }

            return 0;
        }

        private static int TestDeconvolution(int w, int h, int c, int outch, int kernel, int dilation, int stride, int pad, int bias, bool usePackingLayout)
        {
            using var a = TestUtil.TestUtil.RandomMat(w, h, c);

            using var pd = new ParamDict();
            pd.Set(0, outch);// num_output
            pd.Set(1, kernel);// kernel_w
            pd.Set(2, dilation);// dilation_w
            pd.Set(3, stride);// stride_w
            pd.Set(4, pad);// pad_w
            pd.Set(5, bias);// bias_term
            pd.Set(6, outch * c * kernel * kernel);

            var weights = new Mat[2];
            weights[0] = TestUtil.TestUtil.RandomMat(outch * c * kernel * kernel);
            weights[1] = TestUtil.TestUtil.RandomMat(outch);
            using var vector = new StdVector<Mat>(weights);
            using var mb = new ModelBinFromMatArray(vector);

            using var opt = new Option
            {
                NumThreads = 1,
                UseVulkanCompute = true,
                UseFP16Packed = false,
                UseFP16Storage = false,
                UseFP16Arithmetic = false,
                UseInt8Storage = false,
                UseInt8Arithmetic = false,
                UsePackingLayout = usePackingLayout
            };

            var ret = TestUtil.TestUtil.TestLayer<Deconvolution>("Deconvolution", pd, mb, opt, a);
            if (ret != 0)
            {
                Console.Error.WriteLine($"test_deconvolution failed w={w} h={h} c={c} outch={outch} kernel={kernel} dilation={dilation} stride={stride} pad={pad} bias={bias} use_packing_layout={usePackingLayout}");
            }

            weights?.DisposeElement();

            return ret;
        }

        private static int TestDeconvolutionInt8(int w, int h, int c, int outch, int kernel, int dilation, int stride, int pad, int bias)
        {
            using var a = TestUtil.TestUtil.RandomMat(w, h, c);

            using var pd = new ParamDict();
            pd.Set(0, outch);// num_output
            pd.Set(1, kernel);// kernel_w
            pd.Set(2, dilation);// dilation_w
            pd.Set(3, stride);// stride_w
            pd.Set(4, pad);// pad_w
            pd.Set(5, bias);// bias_term
            pd.Set(6, outch * c * kernel * kernel);
            pd.Set(8, 1);// int8_scale_term

            var weights = new Mat[bias > 0 ? 4 : 3];
            weights[0] = TestUtil.TestUtil.RandomMat(outch * c * kernel * kernel);
            if (bias > 0)
            {
                weights[1] = TestUtil.TestUtil.RandomMat(outch);
                weights[2] = TestUtil.TestUtil.RandomMat(outch);
                weights[3] = TestUtil.TestUtil.RandomMat(1);
            }
            else
            {
                weights[1] = TestUtil.TestUtil.RandomMat(outch);
                weights[2] = TestUtil.TestUtil.RandomMat(1);
            }
            using var vector = new StdVector<Mat>(weights);
            using var mb = new ModelBinFromMatArray(vector);

            using var opt = new Option
            {
                NumThreads = 1,
                UseVulkanCompute = false,
                UseInt8Inference = true,
                UseFP16Packed = false,
                UseFP16Storage = false,
                UseFP16Arithmetic = false,
                UseInt8Storage = false,
                UseInt8Arithmetic = false,
                UsePackingLayout = false
            };

            var ret = TestUtil.TestUtil.TestLayer<Deconvolution>("Deconvolution", pd, mb, opt, a);
            if (ret != 0)
            {
                Console.Error.WriteLine($"test_convolution failed w={w} h={h} c={c} outch={outch} kernel={kernel} dilation={dilation} stride={stride} pad={pad} bias={bias}");
            }

            return ret;
        }

        #endregion

        #endregion

    }

}
