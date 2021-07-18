using System;
using System.Collections.Generic;
using System.IO;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using YoloV3.Models;
using YoloV3.Services.Interfaces;
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

        public DetectResult Detect(string file)
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

            return new DetectResult(m.Cols, m.Rows, objects);
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

        #endregion

        #endregion

    }

}
