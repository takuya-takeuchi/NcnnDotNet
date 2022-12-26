using System;
using System.Collections.Generic;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace SqueezeNetSSD
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(SqueezeNetSSD)} [imagepath]");
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

                var objects = new List<Object>();
                DetectSqueezeNet(m, objects);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                DrawObjects(m, objects);
            }

            return 0;
        }

        #region Helpers

        private static int DetectSqueezeNet(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            using (var squeezenet = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    squeezenet.Opt.UseVulkanCompute = true;

                // original pretrained model from https://github.com/chuanqi305/SqueezeNet-SSD
                // squeezenet_ssd_voc_deploy.prototxt
                // https://drive.google.com/open?id=0B3gersZ2cHIxdGpyZlZnbEQ5Snc
                // the ncnn model https://github.com/nihui/ncnn-assets/tree/master/models
                squeezenet.LoadParam("squeezenet_ssd_voc.param");
                squeezenet.LoadModel("squeezenet_ssd_voc.bin");

                const int targetSize = 300;

                var imgW = bgr.Cols;
                var imgH = bgr.Rows;

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, bgr.Cols, bgr.Rows, targetSize, targetSize);
                var meanVals = new[] { 104f, 117f, 123f };
                @in.SubstractMeanNormalize(meanVals, null);

                using var ex = squeezenet.CreateExtractor();
                ex.SetNumThreads(4);

                ex.Input("data", @in);

                using var @out = new Mat();
                ex.Extract("detection_out", @out);

                //     printf("%d %d %d\n", out.w, out.h, out.c);
                objects.Clear();
                for (var i = 0; i < @out.H; i++)
                {
                    var values = @out.Row(i);

                    var @object = new Object();
                    @object.Label = (int)values[0];
                    @object.Prob = values[1];
                    @object.Rect.X = values[2] * imgW;
                    @object.Rect.Y = values[3] * imgH;
                    @object.Rect.Width = values[4] * imgW - @object.Rect.X;
                    @object.Rect.Height = values[5] * imgH - @object.Rect.Y;

                    objects.Add(@object);
                }
            }

            return 0;
        }

        private static void DrawObjects(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            string[] classNames =
            {
                "background",
                "aeroplane",
                "bicycle",
                "bird",
                "boat",
                "bottle",
                "bus",
                "car",
                "cat",
                "chair",
                "cow",
                "diningtable",
                "dog",
                "horse",
                "motorbike",
                "person",
                "pottedplant",
                "sheep",
                "sofa",
                "train",
                "tvmonitor"
            };

            using var image = bgr.Clone();
            for (var i = 0; i < objects.Count; i++)
            {
                var obj = objects[i];

                Console.WriteLine($"{obj.Label} = {obj.Prob:f5} at {obj.Rect.X:f2} {obj.Rect.Y:f2} {obj.Rect.Width:f2} {obj.Rect.Height:f2}");

                Cv2.Rectangle(image, obj.Rect, new Scalar<double>(255, 0, 0));

                var text = $"{classNames[obj.Label]} {(obj.Prob * 100):f1}";

                var baseLine = 0;
                var labelSize = Cv2.GetTextSize(text, CvHersheyFonts.HersheySimplex, 0.5, 1, ref baseLine);

                var x = (int)obj.Rect.X;
                var y = (int)(obj.Rect.Y - labelSize.Height - baseLine);
                if (y < 0)
                    y = 0;
                if (x + labelSize.Width > image.Cols)
                    x = image.Cols - labelSize.Width;

                Cv2.Rectangle(image, new Rect<int>(new Point<int>(x, y),
                                                   new Size<int>(labelSize.Width, labelSize.Height + baseLine)),
                              new Scalar<double>(255, 255, 255), -1);
                
                Cv2.PutText(image, text, new Point<int>(x, y + labelSize.Height),
                            CvHersheyFonts.HersheySimplex, 0.5, new Scalar<double>(0, 0, 0));
            }

            Cv2.ImShow("image", image);
            Cv2.WaitKey(0);
        }

        #endregion

        #endregion

    }

}