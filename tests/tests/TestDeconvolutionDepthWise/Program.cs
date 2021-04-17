/*
 * This example program is ported by C# from tests/test_deconvolutiondepthwise.cpp.
*/

using System;
using NcnnDotNet;
using NcnnDotNet.Extensions;
using NcnnDotNet.Layers;

namespace TestDeconvolutionDepthWise
{

    internal class Program
    {

        #region Methods

        private static int Main()
        {
            TestUtil.TestUtil.SRAND(7767517);

            return TestDeconvolutionDepthWise0();
        }

        #region Helpers

        private static int TestDeconvolutionDepthWise0()
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
                    | TestDeconvolutionDepthWise(13, 11, 1, 1, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 1, false)
                    | TestDeconvolutionDepthWise(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 1, false)
                    | TestDeconvolutionDepthWise(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, false)
                    | TestDeconvolutionDepthWise(13, 11, 3, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 3, false)
                    | TestDeconvolutionDepthWise(13, 11, 4, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, false)
                    | TestDeconvolutionDepthWise(13, 11, 4, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 4, false)
                    | TestDeconvolutionDepthWise(13, 11, 7, 7, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 7, false)
                    | TestDeconvolutionDepthWise(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, false)
                    | TestDeconvolutionDepthWise(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 8, false)
                    | TestDeconvolutionDepthWise(13, 11, 12, 12, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 4, false)
                    | TestDeconvolutionDepthWise(13, 11, 15, 15, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 15, false)
                    | TestDeconvolutionDepthWise(13, 11, 16, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, false)
                    | TestDeconvolutionDepthWise(13, 11, 16, 16, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 16, false)

                    | TestDeconvolutionDepthWise(13, 11, 1, 1, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 1, true)
                    | TestDeconvolutionDepthWise(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 1, true)
                    | TestDeconvolutionDepthWise(13, 11, 2, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, true)
                    | TestDeconvolutionDepthWise(13, 11, 3, 3, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 3, true)
                    | TestDeconvolutionDepthWise(13, 11, 4, 2, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, true)
                    | TestDeconvolutionDepthWise(13, 11, 4, 4, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 4, true)
                    | TestDeconvolutionDepthWise(13, 11, 7, 7, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 7, true)
                    | TestDeconvolutionDepthWise(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, true)
                    | TestDeconvolutionDepthWise(13, 11, 8, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 8, true)
                    | TestDeconvolutionDepthWise(13, 11, 12, 12, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 4, true)
                    | TestDeconvolutionDepthWise(13, 11, 15, 15, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 15, true)
                    | TestDeconvolutionDepthWise(13, 11, 16, 8, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 2, true)
                    | TestDeconvolutionDepthWise(13, 11, 16, 16, kdsp[i][0], kdsp[i][1], kdsp[i][2], kdsp[i][3], 1, 16, true)
                    ;

                if (ret != 0)
                    return -1;
            }

            return 0;
        }

        private static int TestDeconvolutionDepthWise(int w, int h, int c, int outch, int kernel, int dilation, int stride, int pad, int bias, int group, bool usePackingLayout)
        {
            using var a = TestUtil.TestUtil.RandomMat(w, h, c);

            using var pd = new ParamDict();
            pd.Set(0, outch);// num_output
            pd.Set(1, kernel);// kernel_w
            pd.Set(2, dilation);// dilation_w
            pd.Set(3, stride);// stride_w
            pd.Set(4, pad);// pad_w
            pd.Set(5, bias);// bias_term
            pd.Set(6, outch / group * c / group * kernel * kernel * group);
            pd.Set(7, group);

            var weights = new Mat[2];
            weights[0] = TestUtil.TestUtil.RandomMat(outch / group * c / group * kernel * kernel * group);
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

            var ret = TestUtil.TestUtil.TestLayer<DeconvolutionDepthWise>("DeconvolutionDepthWise", pd, mb, opt, a);
            if (ret != 0)
            {
                Console.Error.WriteLine($"test_deconvolutiondepthwise failed w={w} h={h} c={c} outch={outch} kernel={kernel} dilation={dilation} stride={stride} pad={pad} bias={bias} group={group} use_packing_layout={usePackingLayout}");
            }

            weights?.DisposeElement();

            return ret;
        }

        #endregion

        #endregion

    }

}