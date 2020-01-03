using System;
using System.Collections.Generic;
using System.Linq;

namespace NcnnDotNet.Native.DotNetFramework.Tests
{

    internal class Program
    {

        #region Methods

        private static void Main()
        {
            const string image = "goldfish.jpg";
            using (var m = NcnnDotNet.OpenCV.Cv2.ImRead(image, NcnnDotNet.OpenCV.CvLoadImage.Color))
            {
                if (m.IsEmpty)
                {
                    Assert.Fail($"Failed to load {image}.");
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.CreateGpuInstance();

                var clsScores = new List<float>();
                DetectSqueezeNet(m, clsScores);

                if (!clsScores.Any())
                {
                    Assert.Fail($"Failed to classify {image}. Reason: No classification");
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                var top = PrintTop(clsScores);
                if (top.Item1 < 0.9)
                    Assert.Fail($"Failed to classify {image}. Reason: Low Score");
                if (top.Item2 != 1)
                    Assert.Fail($"Failed to classify {image}. Reason: Wrong class");
            }
        }
        
        #region Helpers

        private static void DetectSqueezeNet(NcnnDotNet.OpenCV.Mat bgr, List<float> clsScores)
        {
            using (var squeezeNet = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    squeezeNet.Opt.UseVulkanCompute = true;

                // the ncnn model https://github.com/nihui/ncnn-assets/tree/master/models
                squeezeNet.LoadParam("squeezenet_v1.1.param");
                squeezeNet.LoadModel("squeezenet_v1.1.bin");

                using (var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, bgr.Cols, bgr.Rows, 227, 227))
                {
                    var meanVals = new[] { 104f, 117f, 123f };
                    @in.SubstractMeanNormalize(meanVals, null);

                    using (var ex = squeezeNet.CreateExtractor())
                    {
                        ex.Input("data", @in);

                        using (var @out = new Mat())
                        {
                            ex.Extract("prob", @out);

                            clsScores.Capacity = @out.W;
                            for (var j = 0; j < @out.W; j++)
                                clsScores.Add(@out[j]);
                        }
                    }
                }
            }
        }

        private static Tuple<float, int> PrintTop(IList<float> clsScores)
        {
            // partial sort topk with index
            var size = clsScores.Count;
            var vec = new List<Tuple<float, int>>(size);
            for (var i = 0; i < size; i++)
                vec.Add(new Tuple<float, int>(clsScores[i], i));

            vec.Sort((tuple, tuple1) => tuple1.Item1.CompareTo(tuple.Item1));
            return vec[0];
        }

        #endregion

        #endregion

        private static class Assert
        {

            public static void Fail(string message)
            {
                throw new ApplicationException(message);
            }

        }

    }

}
