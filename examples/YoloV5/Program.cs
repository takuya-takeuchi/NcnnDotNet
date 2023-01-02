using System;
using System.Collections.Generic;
using System.Linq;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace YoloV5
{

    internal class Program
    {

        #region Fields

#if YOLOV5_V60 || YOLOV5_V62
        private const int MaxStride = 64;
#else
        private const int MaxStride = 32;
#endif

        #endregion

        private sealed class YoloV5Focus : CustomLayer
        {

            public YoloV5Focus()
            {
                this.OneBlobOnly = true;
            }

            protected override unsafe int OnForward(Mat bottomBlob, Mat topBlob, Option opt)
            {
                var w = bottomBlob.W;
                var h = bottomBlob.H;
                var channels = bottomBlob.C;

                var outW = w / 2;
                var outH = h / 2;
                var outC = channels * 4;

                topBlob.Create(outW, outH, outC, 4u, 1, opt.BlobAllocator);
                if (topBlob.IsEmpty)
                    return -100;

                // ToDo: parallel
                for (var p = 0; p < outC; p++)
                {
                    using var bottom = bottomBlob.Channel(p % channels);
                    using var top = topBlob.Channel(p);
                    var ptr = (float*)(bottom.Row((p / channels) % 2).Data) + ((p / channels) / 2);
                    var outPtr = (float*)top.Data;

                    for (var i = 0; i < outH; i++)
                    {
                        for (var j = 0; j < outW; j++)
                        {
                            *outPtr = *ptr;

                            outPtr += 1;
                            ptr += 2;
                        }

                        ptr += w;
                    }
                }

                return 0;
            }

        }

        private static YoloV5Focus YoloV5FocusLayerCreator(IntPtr userData)
        {
            return new YoloV5Focus();
        }

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Usage: {nameof(YoloV5)} [imagepath]");
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

                //if (Ncnn.IsSupportVulkan)
                //    Ncnn.CreateGpuInstance();

                var objects = new List<Object>();
                DetectYoloV5(m, objects);

                //if (Ncnn.IsSupportVulkan)
                //    Ncnn.DestroyGpuInstance();

                DrawObjects(m, objects);
            }

            return 0;
        }

        #region Helpers
        
        private static float IntersectionArea(Object a, Object b)
        {
            var inter = a.Rect & b.Rect;
            return inter.Area;
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

        private static void GenerateProposals(Mat anchors, int stride, Mat inPad, Mat featBlob, float probThreshold, IList<Object> objects)
        {
            var numGrid = featBlob.H;

            int numGridX;
            int numGridY;
            if (inPad.W > inPad.H)
            {
                numGridX = inPad.W / stride;
                numGridY = numGrid / numGridX;
            }
            else
            {
                numGridY = inPad.H / stride;
                numGridX = numGrid / numGridY;
            }

            var numClass = featBlob.W - 5;

            var numAnchors = anchors.W / 2;

            for (var q = 0; q < numAnchors; q++)
            {
                var anchorW = anchors[q * 2];
                var anchorH = anchors[q * 2 + 1];

                using var feat = featBlob.Channel(q);

                for (var i = 0; i < numGridY; i++)
                {
                    for (var j = 0; j < numGridX; j++)
                    {
                        var featPtr = feat.Row(i * numGridX + j);
                        var boxConfidence = Sigmoid(featPtr[4]);
                        if (boxConfidence >= probThreshold)
                        {
                            // find class index with max class score
                            var classIndex = 0;
                            var classScore = -float.MaxValue;
                            for (var k = 0; k < numClass; k++)
                            {
                                var score = featPtr[5 + k];
                                if (score > classScore)
                                {
                                    classIndex = k;
                                    classScore = score;
                                }
                            }

                            var confidence = boxConfidence * Sigmoid(classScore);
                            if (confidence >= probThreshold)
                            {
                                // yolov5/models/yolo.py Detect forward
                                // y = x[i].Sigmoid()
                                // y[..., 0:2] = (y[..., 0:2] * 2. - 0.5 + self.grid[i].to(x[i].device)) * self.stride[i]  # xy
                                // y[..., 2:4] = (y[..., 2:4] * 2) ** 2 * self.anchor_grid[i]  # wh

                                var dx = Sigmoid(featPtr[0]);
                                var dy = Sigmoid(featPtr[1]);
                                var dw = Sigmoid(featPtr[2]);
                                var dh = Sigmoid(featPtr[3]);

                                var pbCx = (dx * 2.0f - 0.5f + j) * stride;
                                var pbCy = (dy * 2.0f - 0.5f + i) * stride;

                                var pbW = (float)Math.Pow(dw * 2.0f, 2) * anchorW;
                                var pbH = (float)Math.Pow(dh * 2.0f, 2) * anchorH;

                                var x0 = pbCx - pbW * 0.5f;
                                var y0 = pbCy - pbH * 0.5f;
                                var x1 = pbCx + pbW * 0.5f;
                                var y1 = pbCy + pbH * 0.5f;

                                var obj = new Object
                                {
                                    Rect =
                                {
                                    X = x0,
                                    Y = y0,
                                    Width = x1 - x0,
                                    Height = y1 - y0
                                },
                                    Label = classIndex,
                                    Prob = confidence
                                };

                                objects.Add(obj);
                            }
                        }
                    }
                }
            }
        }

        private static void DetectYoloV5(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            using (var yolov5 = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    yolov5.Opt.UseVulkanCompute = true;
                // yolov5.Opt.UseBf16Storage = true;

                // original pretrained model from https://github.com/ultralytics/yolov5
                // the ncnn model https://github.com/nihui/ncnn-assets/tree/master/models
#if YOLOV5_V62
                yolov5.LoadParam("yolov5s_6.2.param");
                yolov5.LoadModel("yolov5s_6.2.bin");
#elif YOLOV5_V60
                yolov5.LoadParam("yolov5s_6.0.param");
                yolov5.LoadModel("yolov5s_6.0.bin");
#else
                using var reg = new CustomLayerRegister("YoloV5Focus", YoloV5FocusLayerCreator);
                yolov5.RegisterCustomLayer(reg);

                yolov5.LoadParam("yolov5s.param");
                yolov5.LoadModel("yolov5s.bin");
#endif

                const int targetSize = 640;
                const float probThreshold = 0.25f;
                const float nmsThreshold = 0.45f;

                var imgW = bgr.Cols;
                var imgH = bgr.Rows;

                // letterbox pad to multiple of MaxStride
                var w = imgW;
                var h = imgH;
                float scale;
                if (w > h)
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
                
                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr2Rgb, imgW, imgH, w, h);

                // pad to targetSize rectangle
                // yolov5/utils/datasets.py letterbox
                var wPad = (w + MaxStride - 1) / MaxStride * MaxStride - w;
                var hPad = (h + MaxStride - 1) / MaxStride * MaxStride - h;
                using var inPad = new Mat();
                Ncnn.CopyMakeBorder(@in, inPad, hPad / 2, hPad - hPad / 2, wPad / 2, wPad - wPad / 2, BorderType.Constant, 114.0f);
                
                var normVals = new[] { 1 / 255.0f, 1 / 255.0f, 1 / 255.0f };
                inPad.SubstractMeanNormalize(null, normVals);

                using var ex = yolov5.CreateExtractor();

                ex.Input("images", inPad);

                var proposals = new List<Object>();

                // anchor setting from yolov5/models/yolov5s.yaml

                // stride 8
                {
                    using var @out = new Mat();
                    ex.Extract("output", @out);

                    using var anchors = new Mat(6);
                    anchors[0] = 10.0f;
                    anchors[1] = 13.0f;
                    anchors[2] = 16.0f;
                    anchors[3] = 30.0f;
                    anchors[4] = 33.0f;
                    anchors[5] = 23.0f;
                    
                    var objects8 = new List<Object>();
                    GenerateProposals(anchors, 8, inPad, @out, probThreshold, objects8);
                    
                    proposals.AddRange(objects8);
                }

                // stride 16
                {
                    using var @out = new Mat();
#if YOLOV5_V62
                    ex.Extract("353", @out);
#elif YOLOV5_V60
                    ex.Extract("376", @out);
#else
                    ex.Extract("781", @out);
#endif

                    using var anchors = new Mat(6);
                    anchors[0] = 30.0f;
                    anchors[1] = 61.0f;
                    anchors[2] = 62.0f;
                    anchors[3] = 45.0f;
                    anchors[4] = 59.0f;
                    anchors[5] = 119.0f;
                    
                    var objects16 = new List<Object>();
                    GenerateProposals(anchors, 16, inPad, @out, probThreshold, objects16);
                    
                    proposals.AddRange(objects16);
                }

                // stride 32
                {
                    using var @out = new Mat();
#if YOLOV5_V62
                    ex.Extract("367", @out);
#elif YOLOV5_V60
                    ex.Extract("401", @out);
#else
                    ex.Extract("801", @out);
#endif
                    using var anchors = new Mat(6);
                    anchors[0] = 116.0f;
                    anchors[1] = 90.0f;
                    anchors[2] = 156.0f;
                    anchors[3] = 198.0f;
                    anchors[4] = 373.0f;
                    anchors[5] = 326.0f;

                    var objects32 = new List<Object>();
                    GenerateProposals(anchors, 32, inPad, @out, probThreshold, objects32);

                    proposals.AddRange(objects32);
                }

                // sort all proposals by score from highest to lowest
                QsortDescentInplace(proposals);

                // apply nms with nmsThreshold
                var picked = new List<int>();
                NmsSortedBBoxes(proposals, picked, nmsThreshold);

                var count = picked.Count;

                objects.Clear();
                objects.AddRange(new Object[count]);
                for (var i = 0; i < count; i++)
                {
                    objects[i] = proposals[picked[i]];

                    // adjust offset to original unpadded
                    var x0 = (objects[i].Rect.X - (wPad / 2)) / scale;
                    var y0 = (objects[i].Rect.Y - (hPad / 2)) / scale;
                    var x1 = (objects[i].Rect.X + objects[i].Rect.Width - (wPad / 2)) / scale;
                    var y1 = (objects[i].Rect.Y + objects[i].Rect.Height - (hPad / 2)) / scale;

                    // clip
                    x0 = Math.Max(Math.Min(x0, imgW - 1), 0.0f);
                    y0 = Math.Max(Math.Min(y0, imgH - 1), 0.0f);
                    x1 = Math.Max(Math.Min(x1, imgW - 1), 0.0f);
                    y1 = Math.Max(Math.Min(y1, imgH - 1), 0.0f);

                    objects[i].Rect.X = x0;
                    objects[i].Rect.Y = y0;
                    objects[i].Rect.Width = x1 - x0;
                    objects[i].Rect.Height = y1 - y0;
                }
            }

        }

        private static void DrawObjects(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            string[] classNames =
            {
                "person",
                "bicycle",
                "car",
                "motorcycle",
                "airplane",
                "bus",
                "train",
                "truck",
                "boat",
                "traffic light",
                "fire hydrant",
                "stop sign",
                "parking meter",
                "bench",
                "bird",
                "cat",
                "dog",
                "horse",
                "sheep",
                "cow",
                "elephant",
                "bear",
                "zebra",
                "giraffe",
                "backpack",
                "umbrella",
                "handbag",
                "tie",
                "suitcase",
                "frisbee",
                "skis",
                "snowboard",
                "sports ball",
                "kite",
                "baseball bat",
                "baseball glove",
                "skateboard",
                "surfboard",
                "tennis racket",
                "bottle",
                "wine glass",
                "cup",
                "fork",
                "knife",
                "spoon",
                "bowl",
                "banana",
                "apple",
                "sandwich",
                "orange",
                "broccoli",
                "carrot",
                "hot dog",
                "pizza",
                "donut",
                "cake",
                "chair",
                "couch",
                "potted plant",
                "bed",
                "dining table",
                "toilet",
                "tv",
                "laptop",
                "mouse",
                "remote",
                "keyboard",
                "cell phone",
                "microwave",
                "oven",
                "toaster",
                "sink",
                "refrigerator",
                "book",
                "clock",
                "vase",
                "scissors",
                "teddy bear",
                "hair drier",
                "toothbrush"
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