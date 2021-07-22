using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NcnnDotNet.Native.Tests
{

    public sealed class Program
    {

        private const string VersionKey = "NCNNDOTNET_VERSION";

        private const string VulkanSupportKey = "NCNNDOTNET_VULKAN_SUPPORT";

        [Fact]
        public void CheckNcnnDotNetNativeVersion()
        {
            var values = Environment.GetEnvironmentVariables();
            if (!values.Contains(VersionKey))
                Assert.False(true, $"{VersionKey} is not found.");

            Console.WriteLine($"{VersionKey}: {values[VersionKey]}");
            Assert.Equal(values[VersionKey], NcnnDotNet.Ncnn.GetNativeVersion());
        }

        [Fact]
        public void CheckClassification()
        {
            const string image = "goldfish.jpg";
            using (var m = NcnnDotNet.OpenCV.Cv2.ImRead(image, NcnnDotNet.OpenCV.CvLoadImage.Color))
            {
                if (m.IsEmpty)
                {
                    Assert.False(true, $"Failed to load {image}.");
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.CreateGpuInstance();

                var clsScores = new List<float>();
                DetectSqueezeNet(m, clsScores);
                
                if (!clsScores.Any())
                {
                    Assert.False(true, $"Failed to classify {image}. Reason: No classification");
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                var top = PrintTop(clsScores);
                if (top.Item1 < 0.9)
                    Assert.False(true, $"Failed to classify {image}. Reason: Low Score");
                if (top.Item2 != 1)
                    Assert.False(true, $"Failed to classify {image}. Reason: Wrong class");
            }
        }

        [Fact]
        public void CheckIsSupoortGui()
        {
            var values = Environment.GetEnvironmentVariables();
            if (!values.Contains(VulkanSupportKey))
                Assert.False(true, $"{VulkanSupportKey} is not found.");

            Console.WriteLine($"{VulkanSupportKey}: {values[VulkanSupportKey]}");
            Assert.AreEqual((string)values[VulkanSupportKey] != "0", NcnnDotNet.Ncnn.IsSupportVulkan);
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

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, bgr.Cols, bgr.Rows, 227, 227);
                var meanVals = new[] { 104f, 117f, 123f };
                @in.SubstractMeanNormalize(meanVals, null);

                using var ex = squeezeNet.CreateExtractor();
                ex.Input("data", @in);

                using var @out = new Mat();
                ex.Extract("prob", @out);

                clsScores.Capacity = @out.W;
                for (var j = 0; j < @out.W; j++)
                    clsScores.Add(@out[j]);
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
        
    }

}
