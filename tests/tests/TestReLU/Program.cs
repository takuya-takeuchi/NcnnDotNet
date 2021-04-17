/*
 * This example program is ported by C# from tests/test_relu.cpp.
*/

using System;
using NcnnDotNet;
using NcnnDotNet.Extensions;
using NcnnDotNet.Layers;

namespace TestReLU
{

    internal class Program
    {

        #region Methods

        private static int Main()
        {
            TestUtil.TestUtil.SRAND(7767517);

            return TestReLU0();
        }

        #region Helpers

        private static int TestReLU0()
        {
            return 0
                   | TestReLU(0f, false)
                   | TestReLU(0.1f, false)

                   | TestReLU(0f, true)
                   | TestReLU(0.1f, true)
                ;
        }

        private static int TestReLU(float slope, bool usePackingLayout)
        {
            using var a = TestUtil.TestUtil.RandomMat(6, 7, 8);
            using var pd = new ParamDict();
            pd.Set(0, slope);//slope

            using var tmp = new Mat();
            var weights = new[] { tmp };
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

            var ret = TestUtil.TestUtil.TestLayer<ReLU>("ReLU", pd, mb, opt, a);
            if (ret != 0)
            {
                Console.Error.WriteLine($"test_relu failed slope={slope} use_packing_layout={usePackingLayout}");
            }

            weights?.DisposeElement();

            return ret;
        }

        #endregion

        #endregion

    }

}
