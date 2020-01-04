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