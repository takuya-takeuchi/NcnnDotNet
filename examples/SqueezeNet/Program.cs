using System;
using System.Collections.Generic;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace SqueezeNet
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(SqueezeNet)} [imagepath]");
                return -1;
            }

            var imagepath = args[0];

            using (var m = Cv2.ImRead(imagepath, CvLoadImage.Grayscale))
            {
                if (m.IsEmpty)
                {
                    Console.WriteLine($"Cv2.ImRead {imagepath} failed");
                    return -1;
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.CreateGpuInstance();

                var clsScores = new List<float>();
                DetectSqueezeNet(m, clsScores);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                PrintTopK(clsScores, 3);
            }

            return 0;
        }

        #region Helpers

        private static int DetectSqueezeNet(NcnnDotNet.OpenCV.Mat bgr, List<float> clsScores)
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

            return 0;
        }

        private static int PrintTopK(IList<float> clsScores, int topK)
        {
            // partial sort topk with index
            var size = clsScores.Count;
            var vec = new List<Tuple<float, int>>(size);
            for (var i = 0; i < size; i++)
                vec.Add(new Tuple<float, int>(clsScores[i], i));

            vec.Sort((tuple, tuple1) => tuple1.Item1.CompareTo(tuple.Item1));

            // print topk and score
            for (var i = 0; i < topK; i++)
            {
                var score = vec[i].Item1;
                var index = vec[i].Item2;
                Console.WriteLine($"{index} = {score}");
            }

            return 0;
        }

        #endregion

        #endregion

    }

}
