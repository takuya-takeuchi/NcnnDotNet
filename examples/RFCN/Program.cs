using System;
using System.Collections.Generic;
using System.Linq;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace RFCN
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(RFCN)} [imagepath]");
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
                DetectRFCN(m, objects);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                DrawObjects(m, objects);
            }

            return 0;
        }

        #region Helpers

        private static int DetectRFCN(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            using (var rfcn = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    rfcn.Opt.UseVulkanCompute = true;

                // original pretrained model from https://github.com/YuwenXiong/py-R-FCN
                // https://github.com/YuwenXiong/py-R-FCN/blob/master/models/pascal_voc/ResNet-50/rfcn_end2end/test_agnostic.prototxt
                // https://1drv.ms/u/s!AoN7vygOjLIQqUWHpY67oaC7mopf
                // resnet50_rfcn_final.caffemodel
                rfcn.LoadParam("rfcn_end2end.param");
                rfcn.LoadModel("rfcn_end2end.bin");

                const int targetSize = 224;
                
                const int maxPerImage = 100;
                const float confidenceThresh = 0.6f; // CONF_THRESH

                const float nmsThreshold = 0.3f; // NMS_THRESH

                // scale to target detect size
                var w = bgr.Cols;
                var h = bgr.Rows;
                float scale;
                if (w < h)
                {
                    scale = (float)targetSize / w;
                    w = targetSize;
                    h = (int)(h * scale);
                }
                else
                {
                    scale = (float)targetSize / h;
                    h = targetSize;
                    w = (int)(w * scale);
                }

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, bgr.Cols, bgr.Rows, w, h);

                var meanVals = new[] { 102.9801f, 115.9465f, 122.7717f };
                @in.SubstractMeanNormalize(meanVals, null);

                using var im_info = new Mat(3);
                im_info[0] = h;
                im_info[1] = w;
                im_info[2] = scale;

                // step1, extract feature and all rois
                using var ex1 = rfcn.CreateExtractor();

                ex1.Input("data", @in);
                ex1.Input("im_info", im_info);

                using var rfcnCls = new Mat();
                using var rfcnBBox = new Mat();
                using var rois = new Mat();// all rois
                ex1.Extract("rfcn_cls", rfcnCls);
                ex1.Extract("rfcn_bbox", rfcnBBox);
                ex1.Extract("rois", rois);

                // step2, extract bbox and score for each roi
                var classCandidates = new List<List<Object>>();
                for (var i = 0; i < rois.C; i++)
                {
                    using var ex2 = rfcn.CreateExtractor();

                    using var roi = rois.Channel(i); // get single roi
                    ex2.Input("rfcn_cls", rfcnCls);
                    ex2.Input("rfcn_bbox", rfcnBBox);
                    ex2.Input("rois", roi);


                    using var bboxPred = new Mat();
                    using var clsProb = new Mat();
                    ex2.Extract("bbox_pred", bboxPred);
                    ex2.Extract("cls_prob", clsProb);

                    var numClass = clsProb.Width;

                    // There is no equivalent to std::vector::resize  in C#
                    Resize(classCandidates, numClass);

                    // find class id with highest score
                    var label = 0;
                    var score = 0.0f;
                    for (var j = 0; j < numClass; j++)
                    {
                        var classScore = clsProb[j];
                        if (classScore > score)
                        {
                            label = j;
                            score = classScore;
                        }
                    }

                    // ignore background or low score
                    if (label == 0 || score <= confidenceThresh)
                        continue;

                    //         fprintf(stderr, "%d = %f\n", label, score);

                    // unscale to image size
                    var x1 = roi[0] / scale;
                    var y1 = roi[1] / scale;
                    var x2 = roi[2] / scale;
                    var y2 = roi[3] / scale;

                    var pbW = x2 - x1 + 1;
                    var pbH = y2 - y1 + 1;

                    // apply bbox regression
                    var dx = bboxPred[4];
                    var dy = bboxPred[4 + 1];
                    var dw = bboxPred[4 + 2];
                    var dh = bboxPred[4 + 3];

                    var cx = x1 + pbW * 0.5f;
                    var cy = y1 + pbH * 0.5f;

                    var objCx = cx + pbW * dx;
                    var objCy = cy + pbH * dy;

                    var objW = pbW * Math.Exp(dw);
                    var objH = pbH * Math.Exp(dh);

                    var objX1 = (float)(objCx - objW * 0.5f);
                    var objY1 = (float)(objCy - objH * 0.5f);
                    var objX2 = (float)(objCx + objW * 0.5f);
                    var objY2 = (float)(objCy + objH * 0.5f);

                    // clip
                    objX1 = Math.Max(Math.Min(objX1, bgr.Cols - 1), 0.0f);
                    objY1 = Math.Max(Math.Min(objY1, bgr.Rows - 1), 0.0f);
                    objX2 = Math.Max(Math.Min(objX2, bgr.Cols - 1), 0.0f);
                    objY2 = Math.Max(Math.Min(objY2, bgr.Rows - 1), 0.0f);

                    // append object
                    var obj = new Object
                    {
                        Rect = new Rect<float>(objX1, objY1, objX2 - objX1 + 1, objY2 - objY1 + 1),
                        Label = label,
                        Prob = score
                    };

                    classCandidates[label].Add(obj);
                }


                // post process
                objects.Clear();
                for (var i = 0; i < (int)classCandidates.Count; i++)
                {
                    var candidates = classCandidates[i];

                    QsortDescentInplace(candidates);

                    var picked = new List<int>();
                    NmsSortedBBoxes(candidates, picked, nmsThreshold);

                    for (var j = 0; j < picked.Count; j++)
                    {
                        var z = picked[j];
                        objects.Add(candidates[z]);
                    }
                }

                QsortDescentInplace(objects);

                if (maxPerImage > 0 && maxPerImage < objects.Count)
                {
                    Resize(objects, maxPerImage);
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

        private static float IntersectionArea(Object a, Object b)
        {
            var inter = a.Rect & b.Rect;
            return inter.Area;
        }

        private static void NmsSortedBBoxes(IList<Object> objects, IList<int> picked, float nmsThreshold)
        {
            picked.Clear();

            var n = objects.Count;

            var areas = new float[n];
            for (var i = 0; i < n; i++)
                areas[i] = objects[i].Rect.Area;

            for (var i = 0; i < n; i++)
            {
                Object a = objects[i];

                var keep = 1;
                for (var j = 0; j < picked.Count; j++)
                {
                    var b = objects[picked[j]];

                    // intersection over union
                    var interArea = IntersectionArea(a, b);
                    var unionArea = areas[i] + areas[picked[j]] - interArea;
                    //             float IoU = inter_area / union_area
                    if (interArea / unionArea > nmsThreshold)
                        keep = 0;
                }

                if (keep > 0)
                    picked.Add(i);
            }
        }

        private static void QsortDescentInplace(IList<Object> objects, int left, int right)
        {
            var i = left;
            var j = right;
            float p = objects[(left + right) / 2].Prob;

            while (i <= j)
            {
                while (objects[i].Prob > p)
                    i++;

                while (objects[j].Prob < p)
                    j--;

                if (i <= j)
                {
                    // swap
                    var tmp = objects[i];
                    objects[i] = objects[j];
                    objects[j] = tmp;

                    i++;
                    j--;
                }
            }

            if (left < j)
                QsortDescentInplace(objects, left, j);
            if (i < right)
                QsortDescentInplace(objects, i, right);
        }

        private static void QsortDescentInplace(IList<Object> objects)
        {
            if (!objects.Any())
                return;

            QsortDescentInplace(objects, 0, objects.Count - 1);
        }

        private static void Resize<T>(List<T> list, int size)
            where T : new()
        {
            var count = list.Count;
            if (size < count)
                list.RemoveRange(size, count - size);
            else if (size > count)
            {
                if (size > list.Capacity)
                    list.Capacity = size;
                list.AddRange(Enumerable.Repeat(new T(), size - count));
            }
        }

        #endregion

        #endregion

    }

}