using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using YoloV3.Services.Interfaces;

using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;
using Object = YoloV3.Models.Object;

namespace YoloV3.Services
{

    public sealed class DetectService : IDetectService
    {

        #region Constructors

        public DetectService()
        {
        }

        #endregion

        #region IDetectService Members

        public IEnumerable<Object> Detect(string file)
        {
            using var m = Cv2.ImRead(file, CvLoadImage.Grayscale);
            if (m.IsEmpty)
                throw new NotSupportedException("This file is not supported!!");

            if (Ncnn.IsSupportVulkan)
                Ncnn.CreateGpuInstance();

            var directory = Path.GetDirectoryName(file);

            var objects = new List<Object>();
            DetectYoloV3(directory, m, objects);

            if (Ncnn.IsSupportVulkan)
                Ncnn.DestroyGpuInstance();

            //DrawObjects(m, objects);

            return objects;
        }


        #region Helpers

        private static int DetectYoloV3(string directory, NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            var param = Path.Combine(directory, "mobilenetv2_yolov3.param");
            var model = Path.Combine(directory, "mobilenetv2_yolov3.bin");
            if (!File.Exists(param))
                throw new FileNotFoundException("param file is missing");
            if (!File.Exists(model))
                throw new FileNotFoundException("model file is missing");

            using (var yolov3 = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    yolov3.Opt.UseVulkanCompute = true;

                // original pretrained model from https://github.com/eric612/MobileNet-YOLO
                // param : https://drive.google.com/open?id=1V9oKHP6G6XvXZqhZbzNKL6FI_clRWdC-
                // bin : https://drive.google.com/open?id=1DBcuFCr-856z3FRQznWL_S5h-Aj3RawA
                // the ncnn model https://github.com/nihui/ncnn-assets/tree/master/models
                yolov3.LoadParam(param);
                yolov3.LoadModel(model);

                const int targetSize = 352;

                var imgW = bgr.Cols;
                var imgH = bgr.Rows;

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, bgr.Cols, bgr.Rows, targetSize, targetSize);
                var meanVals = new[] { 127.5f, 127.5f, 127.5f };
                var normVals = new[] { 0.007843f, 0.007843f, 0.007843f };
                @in.SubstractMeanNormalize(meanVals, normVals);

                using var ex = yolov3.CreateExtractor();
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

            //Cv2.ImShow("image", image);
            //Cv2.WaitKey(0);
        }

        #endregion

        #endregion

    }

}