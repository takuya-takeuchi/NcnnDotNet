using System;
using System.Collections.Generic;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace SimplePose
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(SimplePose)} [imagepath]");
                return -1;
            }

            var imagepath = args[0];

            using (var m = Cv2.ImRead(imagepath, CvLoadImage.Grayscale))
            {
                if (m.IsEmpty)
                {
                    Console.WriteLine($"cv::imread {imagepath} failed");
                    return -1;
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.CreateGpuInstance();

                var keyPoints = new List<KeyPoint>();
                DetectPoseNet(m, keyPoints);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                DrawPose(m, keyPoints);
            }

            return 0;
        }

        #region Helpers

        private static int DetectPoseNet(NcnnDotNet.OpenCV.Mat bgr, List<KeyPoint> keyPoints)
        {
            using (var poseNet = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    poseNet.Opt.UseVulkanCompute = true;

                // the simple baseline human pose estimation from gluon-cv
                // https://gluon-cv.mxnet.io/build/examples_pose/demo_simple_pose.html
                // mxnet model exported via
                //      pose_net.hybridize()
                //      pose_net.export('pose')
                // then mxnet2ncnn
                // the ncnn model https://github.com/nihui/ncnn-assets/tree/master/models
                poseNet.LoadParam("pose.param");
                poseNet.LoadModel("pose.bin");

                var w = bgr.Cols;
                var h = bgr.Rows;

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr2Rgb, bgr.Cols, bgr.Rows, 192, 256);

                // transforms.ToTensor(),
                // transforms.Normalize((0.485, 0.456, 0.406), (0.229, 0.224, 0.225)),
                // R' = (R / 255 - 0.485) / 0.229 = (R - 0.485 * 255) / 0.229 / 255
                // G' = (G / 255 - 0.456) / 0.224 = (G - 0.456 * 255) / 0.224 / 255
                // B' = (B / 255 - 0.406) / 0.225 = (B - 0.406 * 255) / 0.225 / 255
                var meanVals = new[] { 0.485f * 255.0f, 0.456f * 255.0f, 0.406f * 255.0f };
                var normVals = new[] { 1 / 0.229f / 255.0f, 1 / 0.224f / 255.0f, 1 / 0.225f / 255.0f };
                @in.SubstractMeanNormalize(meanVals, normVals);

                using var ex = poseNet.CreateExtractor();

                ex.Input("data", @in);

                using var @out = new Mat();
                ex.Extract("conv3_fwd", @out);

                // resolve point from heatmap
                keyPoints.Clear();
                for (var p = 0; p < @out.H; p++)
                {
                    using var m = @out.Channel(p);

                    var maxProb = 0f;
                    var maxX = 0;
                    var maxY = 0;
                    for (var y = 0; y < @out.H; y++)
                    {
                        var ptr = m.Row(y);
                        for (var x = 0; x < @out.W; x++)
                        {
                            var prob = ptr[x];
                            if (prob > maxProb)
                            {
                                maxProb = prob;
                                maxX = x;
                                maxY = y;
                            }
                        }
                    }

                    var keyPoint = new KeyPoint
                    {
                        P = new Point<float>(maxX * w / (float) @out.W, maxY * h / (float) @out.H),
                        Prob = maxProb
                    };
                    keyPoints.Add(keyPoint);
                }
            }

            return 0;
        }

        private static void DrawPose(NcnnDotNet.OpenCV.Mat bgr, List<KeyPoint> keyPoints)
        {
            using var image = bgr.Clone();

            // draw bone
            var jointParts = new[]
            {
                new[] {0, 1},
                new[] {1, 3},
                new[] {0, 2},
                new[] {2, 4},
                new[] {5, 6},
                new[] {5, 7},
                new[] {7, 9},
                new[] {6, 8},
                new[] {8, 10},
                new[] {5, 11},
                new[] {6, 12},
                new[] {11, 12},
                new[] {11, 13},
                new[] {12, 14},
                new[] {13, 15},
                new[] {14, 16}
            };

            for (var i = 0; i < 16; i++)
            {
                var p1 = keyPoints[jointParts[i][0]];
                var p2 = keyPoints[jointParts[i][1]];

                if (p1.Prob < 0.2f || p2.Prob < 0.2f)
                    continue;

                Cv2.Line(image, p1.P, p2.P, new Scalar<double>(255, 0, 0), 2);
            }

            // draw joint
            for (var i = 0; i < keyPoints.Count; i++)
            {
                var keyPoint = keyPoints[i];

                Console.WriteLine($"{keyPoint.P.X:f2} {keyPoint.P.Y:f2} = {keyPoint.Prob:f5}");

                if (keyPoint.Prob < 0.2f)
                    continue;

                Cv2.Circle(image, keyPoint.P, 3, new Scalar<double>(0, 255, 0), -1);
            }

            Cv2.ImShow("image", image);
            Cv2.WaitKey(0);
        }

        #endregion

        #endregion

    }

}