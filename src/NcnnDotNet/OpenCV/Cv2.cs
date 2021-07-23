using System;
using System.IO;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    /// <summary>
    /// Provides the methods of OpenCV.
    /// </summary>
    public static partial class Cv2
    {

        #region Fields

        public static readonly int CV_CN_MAX = 512;
        public static readonly int CV_CN_SHIFT = 3;
        public static readonly int CV_DEPTH_MAX = 1 << CV_CN_SHIFT;

        public static readonly int CV_8U = 0;
        public static readonly int CV_8S = 1;
        public static readonly int CV_16U = 2;
        public static readonly int CV_16S = 3;
        public static readonly int CV_32S = 4;
        public static readonly int CV_32F = 5;
        public static readonly int CV_64F = 6;
        public static readonly int CV_USRTYPE1 = 7;

        public static readonly int CV_MAT_DEPTH_MASK = (CV_DEPTH_MAX - 1);

        public static readonly int CV_8UC1 = CV_MAKETYPE(CV_8U, 1);
        public static readonly int CV_8UC2 = CV_MAKETYPE(CV_8U, 2);
        public static readonly int CV_8UC3 = CV_MAKETYPE(CV_8U, 3);
        public static readonly int CV_8UC4 = CV_MAKETYPE(CV_8U, 4);

        public static readonly int CV_8SC1 = CV_MAKETYPE(CV_8S, 1);
        public static readonly int CV_8SC2 = CV_MAKETYPE(CV_8S, 2);
        public static readonly int CV_8SC3 = CV_MAKETYPE(CV_8S, 3);
        public static readonly int CV_8SC4 = CV_MAKETYPE(CV_8S, 4);

        public static readonly int CV_16UC1 = CV_MAKETYPE(CV_16U, 1);
        public static readonly int CV_16UC2 = CV_MAKETYPE(CV_16U, 2);
        public static readonly int CV_16UC3 = CV_MAKETYPE(CV_16U, 3);
        public static readonly int CV_16UC4 = CV_MAKETYPE(CV_16U, 4);

        public static readonly int CV_16SC1 = CV_MAKETYPE(CV_16S, 1);
        public static readonly int CV_16SC2 = CV_MAKETYPE(CV_16S, 2);
        public static readonly int CV_16SC3 = CV_MAKETYPE(CV_16S, 3);
        public static readonly int CV_16SC4 = CV_MAKETYPE(CV_16S, 4);

        public static readonly int CV_32SC1 = CV_MAKETYPE(CV_32S, 1);
        public static readonly int CV_32SC2 = CV_MAKETYPE(CV_32S, 2);
        public static readonly int CV_32SC3 = CV_MAKETYPE(CV_32S, 3);
        public static readonly int CV_32SC4 = CV_MAKETYPE(CV_32S, 4);

        public static readonly int CV_32FC1 = CV_MAKETYPE(CV_32F, 1);
        public static readonly int CV_32FC2 = CV_MAKETYPE(CV_32F, 2);
        public static readonly int CV_32FC3 = CV_MAKETYPE(CV_32F, 3);
        public static readonly int CV_32FC4 = CV_MAKETYPE(CV_32F, 4);

        public static readonly int CV_64FC1 = CV_MAKETYPE(CV_64F, 1);
        public static readonly int CV_64FC2 = CV_MAKETYPE(CV_64F, 2);
        public static readonly int CV_64FC3 = CV_MAKETYPE(CV_64F, 3);
        public static readonly int CV_64FC4 = CV_MAKETYPE(CV_64F, 4);

        #region Helpers

        private static int CV_MAT_DEPTH(int flags)
        {
            return flags & CV_MAT_DEPTH_MASK;
        }

        private static int CV_MAKETYPE(int depth, int cn)
        {
            return CV_MAT_DEPTH(depth) + ((cn - 1) << CV_CN_SHIFT);
        }

        #endregion

        #endregion

        #region Methods

        public static void Circle(Mat mat,
                                  Point<int> center,
                                  int radius,
                                  Scalar<double> scalar,
                                  int thickness = 1,
                                  CvLineTypes lineType = CvLineTypes.Line8,
                                  int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativeCenter = center.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_circle_int32_t(mat.NativePtr,
                                                    nativeCenter.NativePtr,
                                                    radius,
                                                    nativeScalar.NativePtr,
                                                    thickness,
                                                    lineType,
                                                    shift);
            }
        }

        public static void Circle(Mat mat,
                                  Point<float> center,
                                  int radius,
                                  Scalar<double> scalar,
                                  int thickness = 1,
                                  CvLineTypes lineType = CvLineTypes.Line8,
                                  int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativeCenter = center.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_circle_float(mat.NativePtr,
                                                  nativeCenter.NativePtr,
                                                  radius,
                                                  nativeScalar.NativePtr,
                                                  thickness,
                                                  lineType,
                                                  shift);
            }
        }

        public static Size<int> GetTextSize(string text, CvHersheyFonts fontFace, double fontScale, int thickness, ref int baseLine)
        {
            var str = Ncnn.Encoding.GetBytes(text ?? "");
            var error = NativeMethods.opencv_getTextSize(str,
                                                         str.Length,
                                                         fontFace,
                                                         fontScale,
                                                         thickness,
                                                         ref baseLine,
                                                         out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Size<int>(ret);
        }

        public static Mat ImDecode(byte[] buffer, CvLoadImage flags = CvLoadImage.Color)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            var error = NativeMethods.opencv_imdecode(buffer, buffer.Length, (int)flags, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Mat(ret);
        }

        public static Mat ImRead(string fileName, CvLoadImage flags = CvLoadImage.Color)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException("The specified file does not exist.", fileName);

            var str = Ncnn.Encoding.GetBytes(fileName);
            var error = NativeMethods.opencv_imread(str, str.Length, (int)flags, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Mat(ret);
        }

        public static void ImShow(string winName, Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            
            mat.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(winName ?? "");
            NativeMethods.opencv_imshow(str, str.Length, mat.NativePtr);
        }

        public static void ImWrite(string fileName, Mat mat)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(fileName);
            var error = NativeMethods.opencv_imwrite(str, str.Length, mat.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public static void Line(Mat mat,
                                Point<int> p1,
                                Point<int> p2,
                                Scalar<double> scalar,
                                int thickness = 1,
                                CvLineTypes lineType = CvLineTypes.Line8,
                                int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativePoint1 = p1.ToNative())
            using (var nativePoint2 = p2.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_line_int32_t(mat.NativePtr,
                                                  nativePoint1.NativePtr,
                                                  nativePoint2.NativePtr,
                                                  nativeScalar.NativePtr,
                                                  thickness,
                                                  lineType,
                                                  shift);
            }
        }

        public static void Line(Mat mat,
                                Point<float> p1,
                                Point<float> p2,
                                Scalar<double> scalar,
                                int thickness = 1,
                                CvLineTypes lineType = CvLineTypes.Line8,
                                int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativePoint1 = p1.ToNative())
            using (var nativePoint2 = p2.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_line_float(mat.NativePtr,
                                                nativePoint1.NativePtr,
                                                nativePoint2.NativePtr,
                                                nativeScalar.NativePtr,
                                                thickness,
                                                lineType,
                                                shift);
            }
        }
        
        public static void PutText(Mat mat,
                                   string text,
                                   Point<int> point,
                                   CvHersheyFonts fontFace,
                                   double fontScale,
                                   Scalar<double> scalar,
                                   int thickness = 1,
                                   CvLineTypes lineType = CvLineTypes.Line8,
                                   bool bottomLeftOrigin = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(text ?? "");
            using (var nativePoint = point.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_putText_int32_t(mat.NativePtr,
                                                     str,
                                                     str.Length,
                                                     nativePoint.NativePtr,
                                                     fontFace,
                                                     fontScale,
                                                     nativeScalar.NativePtr,
                                                     thickness,
                                                     lineType,
                                                     bottomLeftOrigin);
            }
        }

        public static void PutText(Mat mat,
                                   string text,
                                   Point<long> point,
                                   CvHersheyFonts fontFace,
                                   double fontScale,
                                   Scalar<double> scalar,
                                   int thickness = 1,
                                   CvLineTypes lineType = CvLineTypes.Line8,
                                   bool bottomLeftOrigin = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(text ?? "");
            using (var nativePoint = point.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_putText_int64_t(mat.NativePtr,
                                                     str,
                                                     str.Length,
                                                     nativePoint.NativePtr,
                                                     fontFace,
                                                     fontScale,
                                                     nativeScalar.NativePtr,
                                                     thickness,
                                                     lineType,
                                                     bottomLeftOrigin);
            }
        }

        public static void PutText(Mat mat,
                                   string text,
                                   Point<float> point,
                                   CvHersheyFonts fontFace,
                                   double fontScale,
                                   Scalar<double> scalar,
                                   int thickness = 1,
                                   CvLineTypes lineType = CvLineTypes.Line8,
                                   bool bottomLeftOrigin = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(text ?? "");
            using (var nativePoint = point.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_putText_float(mat.NativePtr,
                                                   str,
                                                   str.Length,
                                                   nativePoint.NativePtr,
                                                   fontFace,
                                                   fontScale,
                                                   nativeScalar.NativePtr,
                                                   thickness,
                                                   lineType,
                                                   bottomLeftOrigin);
            }
        }

        public static void PutText(Mat mat,
                                   string text,
                                   Point<double> point,
                                   CvHersheyFonts fontFace,
                                   double fontScale,
                                   Scalar<double> scalar,
                                   int thickness = 1,
                                   CvLineTypes lineType = CvLineTypes.Line8,
                                   bool bottomLeftOrigin = false)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(text ?? "");
            using (var nativePoint = point.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_putText_double(mat.NativePtr,
                                                    str,
                                                    str.Length,
                                                    nativePoint.NativePtr,
                                                    fontFace,
                                                    fontScale,
                                                    nativeScalar.NativePtr,
                                                    thickness,
                                                    lineType,
                                                    bottomLeftOrigin);
            }
        }

        public static void Rectangle(Mat mat,
                                     Point<int> pt1,
                                     Point<int> pt2,
                                     Scalar<double> scalar,
                                     int thickness = 1,
                                     CvLineTypes lineType = CvLineTypes.Line8,
                                     int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativePt1 = pt1.ToNative())
            using (var nativePt2 = pt2.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_rectangle2_int32_t(mat.NativePtr,
                                                        nativePt1.NativePtr,
                                                        nativePt2.NativePtr,
                                                        nativeScalar.NativePtr,
                                                        thickness,
                                                        lineType,
                                                        shift);
            }
        }

        public static void Rectangle(Mat mat,
                                     Point<float> pt1,
                                     Point<float> pt2,
                                     Scalar<double> scalar,
                                     int thickness = 1,
                                     CvLineTypes lineType = CvLineTypes.Line8,
                                     int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativePt1 = pt1.ToNative())
            using (var nativePt2 = pt2.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_rectangle2_float(mat.NativePtr,
                                                      nativePt1.NativePtr,
                                                      nativePt2.NativePtr,
                                                      nativeScalar.NativePtr,
                                                      thickness,
                                                      lineType,
                                                      shift);
            }
        }

        public static void Rectangle(Mat mat,
                                     Rect<int> rect,
                                     Scalar<double> scalar,
                                     int thickness = 1,
                                     CvLineTypes lineType = CvLineTypes.Line8,
                                     int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativeRect = rect.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_rectangle_int32_t(mat.NativePtr,
                                                       nativeRect.NativePtr,
                                                       nativeScalar.NativePtr,
                                                       thickness,
                                                       lineType,
                                                       shift);
            }
        }

        public static void Rectangle(Mat mat,
                                     Rect<float> rect,
                                     Scalar<double> scalar,
                                     int thickness = 1,
                                     CvLineTypes lineType = CvLineTypes.Line8,
                                     int shift = 0)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            using (var nativeRect = rect.ToNative())
            using (var nativeScalar = scalar.ToNative())
            {
                NativeMethods.opencv_rectangle_float(mat.NativePtr,
                                                     nativeRect.NativePtr,
                                                     nativeScalar.NativePtr,
                                                     thickness,
                                                     lineType,
                                                     shift);
            }
        }

        public static int WaitKey(int delay = 0)
        {
            var error = NativeMethods.opencv_waitKey(delay, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        #endregion

    }

}