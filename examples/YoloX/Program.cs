using System;
using System.Collections.Generic;
using System.Linq;
using NcnnDotNet;
using NcnnDotNet.OpenCV;
using Mat = NcnnDotNet.Mat;

namespace YoloX
{

    internal class Program
    {

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
                Console.WriteLine($"Usage: {nameof(YoloX)} [imagepath]");
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

                //if (Ncnn.IsSupportVulkan)
                //    Ncnn.CreateGpuInstance();

                var objects = new List<Object>();
                DetectYoloX(m, objects);

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
        
        private static void GenerateGridsAndStride(int targetW, int targetH, IList<int> strides, IList<GridAndStride> gridAndStrides)
        {
            var count = strides.Count;
            for (var i = 0; i < count; i++)
            {
                var stride = strides[i];
                var numGridW = targetW / stride;
                var numGridH = targetH / stride;
                for (var g1 = 0; g1 < numGridH; g1++)
                {
                    for (var g0 = 0; g0 < numGridW; g0++)
                    {
                        var gs = new GridAndStride
                        {
                            Grid0 = g0,
                            Grid1 = g1,
                            Stride = stride
                        };
                        gridAndStrides.Add(gs);
                    }
                }
            }
        }

        private static unsafe void GenerateYoloXProposals(IList<GridAndStride> gridStrides, Mat featBlob, float probThreshold, IList<Object> objects)
        {
            var numGrid = featBlob.H;
            var numClass = featBlob.W - 5;
            var numAnchors = gridStrides.Count;
            
            using var tmp = featBlob.Channel(0);
            var featPtr = (float*)tmp.Data;
            for (var anchorIdx = 0; anchorIdx < numAnchors; anchorIdx++)
            {
                var grid0 = gridStrides[anchorIdx].Grid0;
                var grid1 = gridStrides[anchorIdx].Grid1;
                var stride = gridStrides[anchorIdx].Stride;

                // yolox/models/yolo_head.py decode logic
                //  outputs[..., :2] = (outputs[..., :2] + grids) * strides
                //  outputs[..., 2:4] = torch.exp(outputs[..., 2:4]) * strides
                var xCenter = (featPtr[0] + grid0) * stride;
                var yCenter = (featPtr[1] + grid1) * stride;
                var w = (float)Math.Exp(featPtr[2]) * stride;
                var h = (float)Math.Exp(featPtr[3]) * stride;
                var x0 = xCenter - w * 0.5f;
                var y0 = yCenter - h * 0.5f;

                var boxObjectness = featPtr[4];
                for (var classIdx = 0; classIdx < numClass; classIdx++)
                {
                    var boxClsScore = featPtr[5 + classIdx];
                    var boxProb = boxObjectness * boxClsScore;
                    if (boxProb > probThreshold)
                    {
                        var obj = new Object
                        {
                            Rect =
                            {
                                X = x0,
                                Y = y0,
                                Width = w,
                                Height = h
                            },
                            Label = classIdx,
                            Prob = boxProb
                        };

                        objects.Add(obj);
                    }

                }

                featPtr += featBlob.W;
            }
        }

        private static void DetectYoloX(NcnnDotNet.OpenCV.Mat bgr, List<Object> objects)
        {
            using (var yolox = new Net())
            {
                if (Ncnn.IsSupportVulkan)
                    yolox.Opt.UseVulkanCompute = true;
                // yolov5.Opt.UseBf16Storage = true;

                // Focus in yolov5
                using var reg = new CustomLayerRegister("YoloV5Focus", YoloV5FocusLayerCreator);
                yolox.RegisterCustomLayer(reg);

                // original pretrained model from https://github.com/Megvii-BaseDetection/YOLOX
                // ncnn model param: https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_s_ncnn.tar.gz
                // NOTE that newest version YOLOX remove normalization of model (minus mean and then div by std),
                // which might cause your model outputs becoming a total mess, plz check carefully.
                if (!yolox.LoadParam("yolox.param"))
                    Environment.Exit(-1);
                if (!yolox.LoadModel("yolox.bin"))
                    Environment.Exit(-1);

                const int targetSize = 640;
                const float probThreshold = 0.25f;
                const float nmsThreshold = 0.45f;

                var imgW = bgr.Cols;
                var imgH = bgr.Rows;
                
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
                
                using var @in = Mat.FromPixelsResize(bgr.Data, PixelType.Bgr, imgW, imgH, w, h);

                // pad to targetSize rectangle
                var wPad = (w + 31) / 32 * 32 - w;
                var hPad = (h + 31) / 32 * 32 - h;
                
                using var inPad = new Mat();
                // different from yolov5, yolox only pad on bottom and right side,
                // which means users don't need to extra padding info to decode boxes coordinate.
                Ncnn.CopyMakeBorder(@in, inPad, 0, hPad, 0, wPad, BorderType.Constant, 114.0f);

                using var ex = yolox.CreateExtractor();

                ex.Input("images", inPad);

                var proposals = new List<Object>();
                
                {
                    using var @out = new Mat();
                    ex.Extract("output", @out);

                    var strideArray = new []{ 8, 16, 32 }; // might have stride=64 in YOLOX
                    var gridStrides = new List<GridAndStride>();
                    GenerateGridsAndStride(inPad.W, inPad.H, strideArray, gridStrides);
                    GenerateYoloXProposals(gridStrides, @out, probThreshold, proposals);
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
                    var x0 = (objects[i].Rect.X) / scale;
                    var y0 = (objects[i].Rect.Y) / scale;
                    var x1 = (objects[i].Rect.X + objects[i].Rect.Width) / scale;
                    var y1 = (objects[i].Rect.Y + objects[i].Rect.Height) / scale;

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