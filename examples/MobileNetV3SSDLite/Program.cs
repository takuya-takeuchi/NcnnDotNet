using System;
using System.Collections.Generic;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace MobileNetV3SSDLite
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(MobileNetV3SSDLite)} [imagepath]");
                return -1;
            }

            var imagepath = args[0];

            using (var m = Cv2.ImRead(imagepath, CvLoadImage.AnyColor))
            {
                if (m.IsEmpty)
                {
                    Console.WriteLine($"Cv2.ImRead {imagepath} failed");
                    return -1;
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.CreateGpuInstance();

                var objects = new List<Object>();
                DetectMobileNetV3(m, objects);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                DrawObjects(m, objects);
            }

            return 0;
        }

        #region Helpers

        private static float Clamp(float v, float lo, float hi)
        {
            return v < lo ? lo : hi < v ? hi : v;
        }

        private static int DetectMobileNetV3(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            using (var mobilenetV3 = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    mobilenetV3.Opt.UseVulkanCompute = true;

                // converted ncnn model from https://github.com/ujsyehao/mobilenetv3-ssd
                mobilenetV3.LoadParam("mobilenetv3_ssdlite_voc.param");
                mobilenetV3.LoadModel("mobilenetv3_ssdlite_voc.bin");

                const int targetSize = 300;

                var imgW = bgr.Cols;
                var imgH = bgr.Rows;

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr2Rgb, bgr.Cols, bgr.Rows, targetSize, targetSize);
                var meanVals = new[] { 123.675f, 116.28f, 103.53f };
                var normVals = new[] { 1.0f, 1.0f, 1.0f };
                @in.SubstractMeanNormalize(meanVals, normVals);

                using var ex = mobilenetV3.CreateExtractor();
                ex.SetLiteMode(true);
                ex.SetNumThreads(4);

                ex.Input("input", @in);

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

                    // filter out cross-boundary
                    var x1 = Clamp(values[2] * targetSize, 0.0f, targetSize - 1) / targetSize * imgW;
                    var y1 = Clamp(values[3] * targetSize, 0.0f, targetSize - 1) / targetSize * imgH;
                    var x2 = Clamp(values[4] * targetSize, 0.0f, targetSize - 1) / targetSize * imgW;
                    var y2 = Clamp(values[5] * targetSize, 0.0f, targetSize - 1) / targetSize * imgH;

                    @object.Rect.X = x1;
                    @object.Rect.Y = y1;
                    @object.Rect.Width = x2 - x1;
                    @object.Rect.Height = y2 - y1;

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
                if (objects[i].Prob > 0.6)
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
            }

            Cv2.ImShow("image", image);
            Cv2.WaitKey(0);
        }

        #endregion

        #endregion

    }

}