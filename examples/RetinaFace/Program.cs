using System;
using System.Collections.Generic;
using System.Linq;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace RetinaFace
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(RetinaFace)} [imagepath]");
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

                var keyPoints = new List<FaceObject>();
                DetectRetinaFace(m, keyPoints);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                DrawFaceObject(m, keyPoints);
            }

            return 0;
        }

        #region Helpers

        private static int DetectRetinaFace(NcnnDotNet.OpenCV.Mat bgr, List<FaceObject> faceObjects)
        {
            using (var retinaFace = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    retinaFace.Opt.UseVulkanCompute = true;

                // model is converted from
                // https://github.com/deepinsight/insightface/tree/master/RetinaFace#retinaface-pretrained-models
                // https://github.com/deepinsight/insightface/issues/669
                // the ncnn model https://github.com/nihui/ncnn-assets/tree/master/models
                // retinaface.load_param("retinaface-R50.param");
                // retinaface.load_model("retinaface-R50.bin");
                retinaFace.LoadParam("mnet.25-opt.param");
                retinaFace.LoadModel("mnet.25-opt.bin");

                const float probThreshold = 0.8f;
                const float nmsThreshold = 0.4f;

                var imgW = bgr.Cols;
                var imgH = bgr.Rows;

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr2Rgb, bgr.Cols, bgr.Rows, imgW, imgH);

                using var ex = retinaFace.CreateExtractor();

                ex.Input("data", @in);

                var faceProposals = new List<FaceObject>();

                // stride 32
                {
                    using var scoreBlob = new Mat();
                    using var bboxBlob = new Mat();
                    using var landmarkBlob = new Mat();
                    ex.Extract("face_rpn_cls_prob_reshape_stride32", scoreBlob);
                    ex.Extract("face_rpn_bbox_pred_stride32", bboxBlob);
                    ex.Extract("face_rpn_landmark_pred_stride32", landmarkBlob);

                    const int baseSize = 16;
                    const int featStride = 32;
                    using var ratios = new Mat(1);
                    ratios[0] = 1.0f;
                    using var scales = new Mat(2);
                    scales[0] = 32.0f;
                    scales[1] = 16.0f;
                    using var anchors = GenerateAnchors(baseSize, ratios, scales);

                    var faceObjects32 = new List<FaceObject>();
                    GenerateProposals(anchors, featStride, scoreBlob, bboxBlob, landmarkBlob, probThreshold, faceObjects32);

                    faceProposals.AddRange(faceObjects32);
                }

                // stride 16
                {
                    using var scoreBlob = new Mat();
                    using var bboxBlob = new Mat();
                    using var landmarkBlob = new Mat();
                    ex.Extract("face_rpn_cls_prob_reshape_stride16", scoreBlob);
                    ex.Extract("face_rpn_bbox_pred_stride16", bboxBlob);
                    ex.Extract("face_rpn_landmark_pred_stride16", landmarkBlob);

                    const int baseSize = 16;
                    const int featStride = 16;
                    using var ratios = new Mat(1);
                    ratios[0] = 1.0f;
                    using var scales = new Mat(2);
                    scales[0] = 8.0f;
                    scales[1] = 4.0f;
                    using var anchors = GenerateAnchors(baseSize, ratios, scales);

                    var faceObjects16 = new List<FaceObject>();
                    GenerateProposals(anchors, featStride, scoreBlob, bboxBlob, landmarkBlob, probThreshold, faceObjects16);

                    faceProposals.AddRange(faceObjects16);
                }

                // stride 8
                {
                    using var scoreBlob = new Mat();
                    using var bboxBlob = new Mat();
                    using var landmarkBlob = new Mat();
                    ex.Extract("face_rpn_cls_prob_reshape_stride8", scoreBlob);
                    ex.Extract("face_rpn_bbox_pred_stride8", bboxBlob);
                    ex.Extract("face_rpn_landmark_pred_stride8", landmarkBlob);

                    const int baseSize = 16;
                    const int featStride = 8;
                    using var ratios = new Mat(1);
                    ratios[0] = 1.0f;
                    using var scales = new Mat(2);
                    scales[0] = 2.0f;
                    scales[1] = 1.0f;
                    using var anchors = GenerateAnchors(baseSize, ratios, scales);

                    var faceObjects8 = new List<FaceObject>();
                    GenerateProposals(anchors, featStride, scoreBlob, bboxBlob, landmarkBlob, probThreshold, faceObjects8);

                    faceProposals.AddRange(faceObjects8);
                }
                
                // sort all proposals by score from highest to lowest
                QsortDescentInplace(faceProposals);

                // apply nms with nms_threshold
                var picked = new List<int>();
                NmsSortedBBoxes(faceProposals, picked, nmsThreshold);

                var faceCount = picked.Count;

                // resolve point from heatmap
                faceObjects.AddRange(new FaceObject[faceCount]);
                for (var i = 0; i < faceCount; i++)
                {
                    faceObjects[i] = faceProposals[picked[i]];

                    // clip to image size
                    var x0 = faceProposals[i].Rect.X;
                    var y0 = faceProposals[i].Rect.Y;
                    var x1 = x0 + faceProposals[i].Rect.Width;
                    var y1 = y0 + faceProposals[i].Rect.Height;

                    x0 = Math.Max(Math.Min(x0, (float)imgW - 1), 0.0f);
                    y0 = Math.Max(Math.Min(y0, (float)imgH - 1), 0.0f);
                    x1 = Math.Max(Math.Min(x1, (float)imgW - 1), 0.0f);
                    y1 = Math.Max(Math.Min(y1, (float)imgH - 1), 0.0f);

                    faceObjects[i].Rect.X = x0;
                    faceObjects[i].Rect.Y = y0;
                    faceObjects[i].Rect.Width = x1 - x0;
                    faceObjects[i].Rect.Height = y1 - y0;
                }
            }

            return 0;
        }

        private static void DrawFaceObject(NcnnDotNet.OpenCV.Mat bgr, List<FaceObject> faceObjects)
        {
            using var image = bgr.Clone();

            for (var i = 0; i < faceObjects.Count; i++)
            {
                var obj = faceObjects[i];

                Console.WriteLine($"{obj.Prob:f5} at {obj.Rect.X:f2} {obj.Rect.Y:f2} {obj.Rect.Width:f2} x {obj.Rect.Height:f2}");
                
                Cv2.Rectangle(image, obj.Rect, new Scalar<double>(0, 255, 0));

                Cv2.Circle(image, obj.Landmark[0], 2, new Scalar<double>(0, 255, 255), -1);
                Cv2.Circle(image, obj.Landmark[1], 2, new Scalar<double>(0, 255, 255), -1);
                Cv2.Circle(image, obj.Landmark[2], 2, new Scalar<double>(0, 255, 255), -1);
                Cv2.Circle(image, obj.Landmark[3], 2, new Scalar<double>(0, 255, 255), -1);
                Cv2.Circle(image, obj.Landmark[4], 2, new Scalar<double>(0, 255, 255), -1);

                var text = $"{obj.Prob * 100:f1}%";

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

        // copy from src/layer/proposal.cpp
        private static Mat GenerateAnchors(int baseSize, Mat ratios, Mat scales)
        {
            var numRatio = ratios.W;
            var numScale = scales.W;

            var anchors = new Mat();
            anchors.Create(4, numRatio * numScale);

            var cx = baseSize * 0.5f;
            var cy = baseSize * 0.5f;

            for (var i = 0; i < numRatio; i++)
            {
                var ar = ratios[i];

                var rW = (int)Math.Round(baseSize / Math.Sqrt(ar));
                var rH = (int)Math.Round(rW * ar); //round(baseSize * sqrt(ar));

                for (var j = 0; j < numScale; j++)
                {
                    var scale = scales[j];

                    var rsW = rW * scale;
                    var rsH = rH * scale;

                    var anchor = anchors.Row(i * numScale + j);

                    anchor[0] = cx - rsW * 0.5f;
                    anchor[1] = cy - rsH * 0.5f;
                    anchor[2] = cx + rsW * 0.5f;
                    anchor[3] = cy + rsH * 0.5f;
                }
            }

            return anchors;
        }

        private static void GenerateProposals(Mat anchors, int featStride, Mat scoreBlob, Mat bboxBlob, Mat landmarkBlob, float probThreshold, IList<FaceObject> faceObjects)
        {
            var w = scoreBlob.W;
            var h = scoreBlob.H;

            // generate face proposal from bbox deltas and shifted anchors
            var numAnchors = anchors.H;

            for (var q = 0; q < numAnchors; q++)
            {
                var anchor = anchors.Row(q);

                using var score = scoreBlob.Channel(q + numAnchors);
                using var bbox = bboxBlob.ChannelRange(q * 4, 4);
                using var landmark = landmarkBlob.ChannelRange(q * 10, 10);

                // shifted anchor
                var anchorY = anchor[1];

                var anchorW = anchor[2] - anchor[0];
                var anchorH = anchor[3] - anchor[1];

                for (var i = 0; i < h; i++)
                {
                    var anchorX = anchor[0];

                    for (var j = 0; j < w; j++)
                    {
                        var index = i * w + j;

                        var prob = score[index];

                        if (prob >= probThreshold)
                        {
                            // apply center size
                            using var mat0 = bbox.Channel(0);
                            using var mat1 = bbox.Channel(1);
                            using var mat2 = bbox.Channel(2);
                            using var mat3 = bbox.Channel(3);
                            var dx = mat0[index];
                            var dy = mat1[index];
                            var dw = mat2[index];
                            var dh = mat3[index];

                            var cx = anchorX + anchorW * 0.5f;
                            var cy = anchorY + anchorH * 0.5f;

                            var pbCx = cx + anchorW * dx;
                            var pbCy = cy + anchorH * dy;

                            var pbW = anchorW * (float)Math.Exp(dw);
                            var pbH = anchorH * (float)Math.Exp(dh);

                            var x0 = pbCx - pbW * 0.5f;
                            var y0 = pbCy - pbH * 0.5f;
                            var x1 = pbCx + pbW * 0.5f;
                            var y1 = pbCy + pbH * 0.5f;

                            var obj = new FaceObject();
                            obj.Rect.X = x0;
                            obj.Rect.Y = y0;
                            obj.Rect.Width = x1 - x0 + 1;
                            obj.Rect.Height = y1 - y0 + 1;
                            using var landmarkMat0 = landmark.Channel(0);
                            using var landmarkMat1 = landmark.Channel(1);
                            using var landmarkMat2 = landmark.Channel(2);
                            using var landmarkMat3 = landmark.Channel(3);
                            using var landmarkMat4 = landmark.Channel(4);
                            using var landmarkMat5 = landmark.Channel(5);
                            using var landmarkMat6 = landmark.Channel(6);
                            using var landmarkMat7 = landmark.Channel(7);
                            using var landmarkMat8 = landmark.Channel(8);
                            using var landmarkMat9 = landmark.Channel(9);
                            obj.Landmark[0].X = cx + (anchorW + 1) * landmarkMat0[index];
                            obj.Landmark[0].Y = cy + (anchorH + 1) * landmarkMat1[index];
                            obj.Landmark[1].X = cx + (anchorW + 1) * landmarkMat2[index];
                            obj.Landmark[1].Y = cy + (anchorH + 1) * landmarkMat3[index];
                            obj.Landmark[2].X = cx + (anchorW + 1) * landmarkMat4[index];
                            obj.Landmark[2].Y = cy + (anchorH + 1) * landmarkMat5[index];
                            obj.Landmark[3].X = cx + (anchorW + 1) * landmarkMat6[index];
                            obj.Landmark[3].Y = cy + (anchorH + 1) * landmarkMat7[index];
                            obj.Landmark[4].X = cx + (anchorW + 1) * landmarkMat8[index];
                            obj.Landmark[4].Y = cy + (anchorH + 1) * landmarkMat9[index];
                            obj.Prob = prob;

                            faceObjects.Add(obj);
                        }

                        anchorX += featStride;
                    }

                    anchorY += featStride;
                }
            }

        }

        private static void NmsSortedBBoxes(IList<FaceObject> objects, IList<int> picked, float nmsThreshold)
        {
            picked.Clear();

            var n = objects.Count;

            var areas = new float[n];
            for (var i = 0; i < n; i++)
                areas[i] = objects[i].Rect.Area;

            for (var i = 0; i < n; i++)
            {
                var a = objects[i];

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

        private static void QsortDescentInplace(IList<FaceObject> objects, int left, int right)
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

        private static void QsortDescentInplace(IList<FaceObject> objects)
        {
            if (!objects.Any())
                return;

            QsortDescentInplace(objects, 0, objects.Count - 1);
        }

        private static float IntersectionArea(FaceObject a, FaceObject b)
        {
            var inter = a.Rect & b.Rect;
            return inter.Area;
        }

        #endregion

        #endregion

    }

}