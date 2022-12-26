using System;
using System.Collections.Generic;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace ShuffleNetV2
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(ShuffleNetV2)} [imagepath]");
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
                DetectShuffleNetV2(m, clsScores);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                PrintTopK(clsScores, 3);
            }

            return 0;
        }

        #region Helpers

        private static int DetectShuffleNetV2(NcnnDotNet.OpenCV.Mat bgr, List<float> clsScores)
        {
            using (var shuffleNetV2 = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    shuffleNetV2.Opt.UseVulkanCompute = true;

                // https://github.com/miaow1988/ShuffleNet_V2_pytorch_caffe
                // models can be downloaded from https://github.com/miaow1988/ShuffleNet_V2_pytorch_caffe/releases
                shuffleNetV2.LoadParam("shufflenet_v2_x0.5.param");
                shuffleNetV2.LoadModel("shufflenet_v2_x0.5.bin");

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, bgr.Cols, bgr.Rows, 224, 224);
                var normVals = new[] { 1 / 255.0f, 1 / 255.0f, 1 / 255.0f };
                @in.SubstractMeanNormalize(null, normVals);

                using var ex = shuffleNetV2.CreateExtractor();
                ex.Input("data", @in);

                using var @out = new Mat();
                ex.Extract("fc", @out);

                // manually call softmax on the fc output
                // convert result into probability
                // skip if your model already has softmax operation
                {
                    using var softmax = Ncnn.CreateLayer("Softmax");

                    using var pd = new ParamDict();
                    softmax.LoadParam(pd);

                    softmax.ForwardInplace(@out, shuffleNetV2.Opt);
                }

                using var @out2 = @out.Reshape(@out.W * @out.H * @out.C);

                clsScores.Capacity = @out2.W;
                for (var j = 0; j < @out2.W; j++)
                    clsScores.Add(@out2[j]);
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
