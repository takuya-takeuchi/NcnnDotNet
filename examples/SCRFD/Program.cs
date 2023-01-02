using System;
using System.Collections.Generic;
using System.Linq;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace SCRFD
{

    internal class Program
    {

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine($"Usage: {nameof(SCRFD)} [imagePath] [modelType] [probThreshold] [nmsThreshold]");
                return -1;
            }

            var imagePath = args[0];
            var modelType = args[1];
            var probThreshold = float.Parse(args[2]);
            var nmsThreshold = float.Parse(args[3]);

            var modelTypes = new[]
            {
                "500m",
                "500m_kps",
                "1g",
                "2.5g",
                "2.5g_kps",
                "10g",
                "10g_kps",
                "34g"
            };

            if (!modelTypes.Contains(modelType))
            {
                Console.WriteLine($"Usage: modelType must be {string.Join(',', modelTypes.Take(modelTypes.Length - 1))} or {modelTypes.Last()}");
                return -1;
            }

            using (var m = Cv2.ImRead(imagePath, CvLoadImage.AnyColor))
            {
                if (m.IsEmpty)
                {
                    Console.WriteLine($"Cv2.ImRead {imagePath} failed");
                    return -1;
                }

                if (Ncnn.IsSupportVulkan)
                    Ncnn.CreateGpuInstance();

                var faceObjects = new List<FaceObject>();

                Ncnn.SetCpuPowerSave(PowerSave.OnlyBigClustersEnabled);
                //Ncnn.SetOmpNumThreads(Ncnn.GetBigCpuCount());

                Detect(m, modelType, faceObjects, probThreshold, nmsThreshold);

                if (Ncnn.IsSupportVulkan)
                    Ncnn.DestroyGpuInstance();

                Draw(m, modelType, faceObjects);
            }

            return 0;
        }

        #region Helpers
        
        private static float IntersectionArea(FaceObject a, FaceObject b)
        {
            var inter = a.Rect & b.Rect;
            return inter.Area;
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
                    (objects[i], objects[j]) = (objects[j], objects[i]);

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
                    // float IoU = inter_area / union_area
                    if (interArea / unionArea > nmsThreshold)
                        keep = 0;
                }

                if (keep > 0)
                    picked.Add(i);
            }
        }

        private static float Sigmoid(float x)
        {
            return (float)(1.0f / (1.0f + Math.Exp(-x)));
        }

        private static unsafe Mat GenerateAnchors(int baseSize, Mat ratios, Mat scales)
        {
            var numRatio = ratios.W;
            var numScale = scales.W;

            var anchors = new Mat();
            anchors.Create(4, numRatio * numScale);

            const float cx = 0;
            const float cy = 0;

            for (var i = 0; i < numRatio; i++)
            {
                var ar = ratios[i];

                var rW = Math.Round(baseSize / Math.Sqrt(ar));
                var rH = Math.Round(rW * ar); //round(base_size * sqrt(ar));

                for (var j = 0; j < numScale; j++)
                {
                    var scale = scales[j];

                    var rsW = rW * scale;
                    var rsH = rH * scale;

                    var anchor = (float*)anchors.Row(i * numScale + j).Data;

                    anchor[0] = (float)(cx - rsW * 0.5f);
                    anchor[1] = (float)(cy - rsH * 0.5f);
                    anchor[2] = (float)(cx + rsW * 0.5f);
                    anchor[3] = (float)(cy + rsH * 0.5f);
                }
            }

            return anchors;
        }

        private static unsafe void GenerateProposals(Mat anchors,
                                                     int featStride,
                                                     Mat scoreBlob,
                                                     Mat bboxBlob,
                                                     Mat kpsBlob,
                                                     float probThreshold,
                                                     IList<FaceObject> faceObjects)
        {
            var w = scoreBlob.W;
            var h = scoreBlob.H;

            // generate face proposal from bbox deltas and shifted anchors
            var num_anchors = anchors.H;

            for (var q = 0; q < num_anchors; q++)
            {
                var anchor = (float*)anchors.Row(q).Data;

                using var score = scoreBlob.Channel(q);
                using var bbox = bboxBlob.ChannelRange(q * 4, 4);

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
                            // insightface/detection/scrfd/mmdet/models/dense_heads/scrfd_head.py _get_bboxes_single()
                            var dx = bbox.Channel(0)[index] * featStride;
                            var dy = bbox.Channel(1)[index] * featStride;
                            var dw = bbox.Channel(2)[index] * featStride;
                            var dh = bbox.Channel(3)[index] * featStride;

                            // insightface/detection/scrfd/mmdet/core/bbox/transforms.py distance2bbox()
                            var cx = anchorX + anchorW * 0.5f;
                            var cy = anchorY + anchorH * 0.5f;

                            var x0 = cx - dx;
                            var y0 = cy - dy;
                            var x1 = cx + dw;
                            var y1 = cy + dh;

                            var obj = new FaceObject
                            {
                                Rect = new Rect<float>
                                {
                                    X = x0,
                                    Y = y0,
                                    Width = x1 - x0 + 1,
                                    Height = y1 - y0 + 1
                                },
                                Prob = prob,
                                Landmark = Enumerable.Range(0, 5).Select(i => new Point<float>()).ToArray()
                            };

                            if (!kpsBlob.IsEmpty)
                            {
                                using var kps = kpsBlob.ChannelRange(q * 10, 10);
                                
                                obj.Landmark[0].X = cx + kps.Channel(0)[index] * featStride;
                                obj.Landmark[0].Y = cy + kps.Channel(1)[index] * featStride;
                                obj.Landmark[1].X = cx + kps.Channel(2)[index] * featStride;
                                obj.Landmark[1].Y = cy + kps.Channel(3)[index] * featStride;
                                obj.Landmark[2].X = cx + kps.Channel(4)[index] * featStride;
                                obj.Landmark[2].Y = cy + kps.Channel(5)[index] * featStride;
                                obj.Landmark[3].X = cx + kps.Channel(6)[index] * featStride;
                                obj.Landmark[3].Y = cy + kps.Channel(7)[index] * featStride;
                                obj.Landmark[4].X = cx + kps.Channel(8)[index] * featStride;
                                obj.Landmark[4].Y = cy + kps.Channel(9)[index] * featStride;
                            }

                            faceObjects.Add(obj);
                        }

                        anchorX += featStride;
                    }

                    anchorY += featStride;
                }
            }
        }

        private static void Detect(NcnnDotNet.OpenCV.Mat bgr, string modelType, List<FaceObject> faceObjects, float probThreshold, float nmsThreshold)
        {
            using (var scrfd = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    scrfd.Opt.UseVulkanCompute = true;

                //scrfd.Opt.NumThreads = Ncnn.GetBigCpuCount();

                // https://github.com/nihui/ncnn-android-scrfd/blob/master/app/src/main/jni/scrfd.cpp
                Console.WriteLine($"scrfd_{modelType}-opt2.param");
                Console.WriteLine($"scrfd_{modelType}-opt2.bin");

                scrfd.LoadParam($"scrfd_{modelType}-opt2.param");
                scrfd.LoadModel($"scrfd_{modelType}-opt2.bin");

                var hasKps = modelType.Contains("_kps");

                var width = bgr.Cols;
                var height = bgr.Rows;

                // insightface/detection/scrfd/configs/scrfd/scrfd_500m.py
                const int targetSize = 640;

                // pad to multiple of 32
                var w = width;
                var h = height;
                var scale = 1.0f;
                if (w > h)
                {
                    scale = (float)targetSize / w;
                    w = targetSize;
                    h =(int)(h * scale);
                }
                else
                {
                    scale = (float)targetSize / h;
                    h = targetSize;
                    w = (int)(w * scale);
                }

                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Rgb, width, height, w, h);

                // pad to target_size rectangle
                var wPad = (w + 31) / 32 * 32 - w;
                var hPad = (h + 31) / 32 * 32 - h;
                using var inPad = new Mat();
                Ncnn.CopyMakeBorder(@in, inPad, hPad / 2, hPad - hPad / 2, wPad / 2, wPad - wPad / 2, BorderType.Constant, 0.0f);

                
                var meanVals = new[] { 127.5f, 127.5f, 127.5f };
                var normVals = new[] { 1 / 128.0f, 1 / 128.0f, 1 / 128.0f };
                inPad.SubstractMeanNormalize(meanVals, normVals);

                using var ex = scrfd.CreateExtractor();

                ex.Input("input.1", inPad);

                var faceProposals = new List<FaceObject>();
                

                // stride 8
                {
                    using var scoreBlob = new Mat();
                    using var bboxBlob = new Mat();
                    using var kpsBlob = new Mat();
                    ex.Extract("score_8", scoreBlob);
                    ex.Extract("bbox_8", bboxBlob);

                    if (hasKps)
                        ex.Extract("kps_8", kpsBlob);

                    const int baseSize = 16;
                    const int featStride = 8;
                    using var ratios = new Mat(1);
                    ratios[0] = 1.0f;
                    using var scales = new Mat(1);
                    scales[0] = 1.0f;
                    scales[1] = 2.0f;
                    using var anchors = GenerateAnchors(baseSize, ratios, scales);

                    var faceObjects32 = new List<FaceObject>();
                    GenerateProposals(anchors, featStride, scoreBlob, bboxBlob, kpsBlob, probThreshold, faceObjects32);

                    faceProposals.AddRange(faceObjects32);
                }

                // stride 16
                {
                    using var scoreBlob = new Mat();
                    using var bboxBlob = new Mat();
                    using var kpsBlob = new Mat();
                    ex.Extract("score_16", scoreBlob);
                    ex.Extract("bbox_16", bboxBlob);

                    if (hasKps)
                        ex.Extract("kps_16", kpsBlob);

                    const int baseSize = 64;
                    const int featStride = 16;
                    using var ratios = new Mat(1);
                    ratios[0] = 1.0f;
                    using var scales = new Mat(1);
                    scales[0] = 1.0f;
                    scales[1] = 2.0f;
                    using var anchors = GenerateAnchors(baseSize, ratios, scales);

                    var faceObjects16 = new List<FaceObject>();
                    GenerateProposals(anchors, featStride, scoreBlob, bboxBlob, kpsBlob, probThreshold, faceObjects16);

                    faceProposals.AddRange(faceObjects16);
                }

                // stride 32
                {
                    using var scoreBlob = new Mat();
                    using var bboxBlob = new Mat();
                    using var kpsBlob = new Mat();
                    ex.Extract("score_32", scoreBlob);
                    ex.Extract("bbox_32", bboxBlob);

                    if (hasKps)
                        ex.Extract("kps_32", kpsBlob);

                    const int baseSize = 256;
                    const int featStride = 32;
                    using var ratios = new Mat(1);
                    ratios[0] = 1.0f;
                    using var scales = new Mat(1);
                    scales[0] = 1.0f;
                    scales[1] = 2.0f;
                    using var anchors = GenerateAnchors(baseSize, ratios, scales);

                    var faceObjects8 = new List<FaceObject>();
                    GenerateProposals(anchors, featStride, scoreBlob, bboxBlob, kpsBlob, probThreshold, faceObjects8);

                    faceProposals.AddRange(faceObjects8);
                }

                // sort all proposals by score from highest to lowest
                QsortDescentInplace(faceProposals);

                // apply nms with nmsThreshold
                var picked = new List<int>();
                NmsSortedBBoxes(faceProposals, picked, nmsThreshold);

                var faceCount = picked.Count;

                faceObjects.Clear();
                faceObjects.AddRange(new FaceObject[faceCount]);
                for (var i = 0; i < faceCount; i++)
                {
                    faceObjects[i] = faceProposals[picked[i]];

                    // adjust offset to original unpadded
                    {
                        var x0 = (faceObjects[i].Rect.X - (wPad / 2)) / scale;
                        var y0 = (faceObjects[i].Rect.Y - (hPad / 2)) / scale;
                        var x1 = (faceObjects[i].Rect.X + faceObjects[i].Rect.Width - (wPad / 2)) / scale;
                        var y1 = (faceObjects[i].Rect.Y + faceObjects[i].Rect.Height - (hPad / 2)) / scale;

                        x0 = Math.Max(Math.Min(x0, width - 1), 0.0f);
                        y0 = Math.Max(Math.Min(y0, height - 1), 0.0f);
                        x1 = Math.Max(Math.Min(x1, width - 1), 0.0f);
                        y1 = Math.Max(Math.Min(y1, height - 1), 0.0f);

                        faceObjects[i].Rect.X = x0;
                        faceObjects[i].Rect.Y = y0;
                        faceObjects[i].Rect.Width = x1 - x0;
                        faceObjects[i].Rect.Height = y1 - y0;
                    }

                    if (hasKps)
                    {
                        var x0 = (faceObjects[i].Landmark[0].X - (wPad / 2)) / scale;
                        var y0 = (faceObjects[i].Landmark[0].Y - (hPad / 2)) / scale;
                        var x1 = (faceObjects[i].Landmark[1].X - (wPad / 2)) / scale;
                        var y1 = (faceObjects[i].Landmark[1].Y - (hPad / 2)) / scale;
                        var x2 = (faceObjects[i].Landmark[2].X - (wPad / 2)) / scale;
                        var y2 = (faceObjects[i].Landmark[2].Y - (hPad / 2)) / scale;
                        var x3 = (faceObjects[i].Landmark[3].X - (wPad / 2)) / scale;
                        var y3 = (faceObjects[i].Landmark[3].Y - (hPad / 2)) / scale;
                        var x4 = (faceObjects[i].Landmark[4].X - (wPad / 2)) / scale;
                        var y4 = (faceObjects[i].Landmark[4].Y - (hPad / 2)) / scale;

                        faceObjects[i].Landmark[0].X = Math.Max(Math.Min(x0, (float)width - 1), 0.0f);
                        faceObjects[i].Landmark[0].Y = Math.Max(Math.Min(y0, (float)height - 1), 0.0f);
                        faceObjects[i].Landmark[1].X = Math.Max(Math.Min(x1, (float)width - 1), 0.0f);
                        faceObjects[i].Landmark[1].Y = Math.Max(Math.Min(y1, (float)height - 1), 0.0f);
                        faceObjects[i].Landmark[2].X = Math.Max(Math.Min(x2, (float)width - 1), 0.0f);
                        faceObjects[i].Landmark[2].Y = Math.Max(Math.Min(y2, (float)height - 1), 0.0f);
                        faceObjects[i].Landmark[3].X = Math.Max(Math.Min(x3, (float)width - 1), 0.0f);
                        faceObjects[i].Landmark[3].Y = Math.Max(Math.Min(y3, (float)height - 1), 0.0f);
                        faceObjects[i].Landmark[4].X = Math.Max(Math.Min(x4, (float)width - 1), 0.0f);
                        faceObjects[i].Landmark[4].Y = Math.Max(Math.Min(y4, (float)height - 1), 0.0f);
                    }
                }
            }

        }

        private static void Draw(NcnnDotNet.OpenCV.Mat rgb, string modelType, IReadOnlyList<FaceObject> faceObjects)
        {
            var hasKps = modelType.Contains("_kps");

            for (var i = 0; i < faceObjects.Count; i++)
            {
                var obj = faceObjects[i];

                Cv2.Rectangle(rgb, obj.Rect, new Scalar<double>(0, 255, 0));

                if (hasKps)
                {
                    Cv2.Circle(rgb, obj.Landmark[0], 2, new Scalar<double>(255, 255, 0), -1);
                    Cv2.Circle(rgb, obj.Landmark[1], 2, new Scalar<double>(255, 255, 0), -1);
                    Cv2.Circle(rgb, obj.Landmark[2], 2, new Scalar<double>(255, 255, 0), -1);
                    Cv2.Circle(rgb, obj.Landmark[3], 2, new Scalar<double>(255, 255, 0), -1);
                    Cv2.Circle(rgb, obj.Landmark[4], 2, new Scalar<double>(255, 255, 0), -1);
                }
                
                var text = $"{(obj.Prob * 100):f1}%";

                var baseLine = 0;
                var labelSize = Cv2.GetTextSize(text, CvHersheyFonts.HersheySimplex, 0.5, 1, ref baseLine);

                var x = (int)obj.Rect.X;
                var y = (int)(obj.Rect.Y - labelSize.Height - baseLine);
                if (y < 0)
                    y = 0;
                if (x + labelSize.Width > rgb.Cols)
                    x = rgb.Cols - labelSize.Width;

                Cv2.Rectangle(rgb, new Rect<int>(new Point<int>(x, y),
                                                 new Size<int>(labelSize.Width, labelSize.Height + baseLine)),
                              new Scalar<double>(255, 255, 255), -1);
                
                Cv2.PutText(rgb, text, new Point<int>(x, y + labelSize.Height),
                            CvHersheyFonts.HersheySimplex, 0.5, new Scalar<double>(0, 0, 0));
            }

            Cv2.ImShow("image", rgb);
            Cv2.WaitKey(0);
        }

        #endregion

        #endregion

    }

}