using System;
using System.Collections.Generic;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace PeleeNetSSDSeg
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(PeleeNetSSDSeg)} [imagepath]");
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

                var objects = new List<Object>();
                using var segOut = new NcnnDotNet.Mat();
                DetectPeleeNet(m, objects, segOut);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                DrawObjects(m, objects, segOut);
            }

            return 0;
        }

        #region Helpers

        private static int DetectPeleeNet(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects, NcnnDotNet.Mat resized)
        {
            using (var peleenet = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    peleenet.Opt.UseVulkanCompute = true;

                // model is converted from https://github.com/eric612/MobileNet-YOLO
                // and can be downloaded from https://drive.google.com/open?id=1Wt6jKv13sBRMHgrGAJYlOlRF-o80pC0g
                // the ncnn model https://github.com/nihui/ncnn-assets/tree/master/models
                peleenet.LoadParam("pelee.param");
                peleenet.LoadModel("pelee.bin");

                const int targetSize = 300;

                var imgW = bgr.Cols;
                var imgH = bgr.Rows;

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, bgr.Cols, bgr.Rows, targetSize, targetSize);
                var meanVals = new[] { 103.9f, 116.7f, 123.6f };
                var normVals = new[] { 0.017f, 0.017f, 0.017f };
                @in.SubstractMeanNormalize(meanVals, normVals);

                using var ex = peleenet.CreateExtractor();
                //ex.SetNumThreads(4);

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

                using var segOut = new Mat();
                ex.Extract("sigmoid", segOut);
                Ncnn.ResizeBilinear(segOut, resized, imgW, imgH);
            }

            return 0;
        }

        private static void DrawObjects(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects, NcnnDotNet.Mat map)
        {
            string[] classNames =
            {
                "background",
                "person",
                "rider",
                "car",
                "bus",
                "truck",
                "bike",
                "motor",
                "traffic light",
                "traffic sign",
                "train"
            };

            using var image = bgr.Clone();
            var color = new[] { 128, 255, 128, 244, 35, 232 };
            var colorCount = color.Length;

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

            unsafe
            {
                var width = map.W;
                var height = map.H;
                var size = map.C;
                var imgIndex2 = 0;
                var threshold = 0.45f;
                var ptr2 = (float*)map.Data;
                for (var i = 0; i < height; i++)
                {
                    var ptr1 = (byte*)image.Ptr(i);
                    var imgIndex1 = 0;
                    for (var j = 0; j < width; j++)
                    {
                        var maxImage = threshold;
                        var index = -1;
                        for (var c = 0; c < size; c++)
                        {
                            //const float* ptr3 = map.channel(c);  
                            var ptr3 = ptr2 + c * width * height;
                            if (ptr3[imgIndex2] > maxImage)
                            {
                                maxImage = ptr3[imgIndex2];
                                index = c;
                            }
                        }

                        if (index > -1)
                        {
                            var colorIndex = index * 3;
                            if (colorIndex < colorCount)
                            {
                                var b = color[colorIndex];
                                var g = color[colorIndex + 1];
                                var r = color[colorIndex + 2];
                                ptr1[imgIndex1    ] = (byte)(b / 2 + ptr1[imgIndex1    ] / 2);
                                ptr1[imgIndex1 + 1] = (byte)(g / 2 + ptr1[imgIndex1 + 1] / 2);
                                ptr1[imgIndex1 + 2] = (byte)(r / 2 + ptr1[imgIndex1 + 2] / 2);
                            }
                        }

                        imgIndex1 += 3;
                        imgIndex2++;
                    }
                }
            }

            Cv2.ImShow("image", image);
            Cv2.WaitKey(0);
        }

        #endregion

        #endregion

    }

}