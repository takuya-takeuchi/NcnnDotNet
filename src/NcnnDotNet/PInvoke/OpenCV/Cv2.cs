using System;
using System.Runtime.InteropServices;
using NcnnDotNet.OpenCV;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_getTextSize(byte[] text,
                                                          int textLength,
                                                          CvHersheyFonts fontFace,
                                                          double fontScale,
                                                          int thickness,
                                                          ref int baseLine,
                                                          out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_imread(byte[] filename, int filenameLength, int flags, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_imdecode(byte[] buf, int buf_len, int flags, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_imwrite(byte[] filename, int filenameLength, IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_imshow(byte[] winName, int winNameLength, IntPtr mat);

        #region circle

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_circle_int32_t(IntPtr mat,
                                                             IntPtr center,
                                                             int radius,
                                                             IntPtr scalar,
                                                             int thickness,
                                                             CvLineTypes lineType,
                                                             int shift);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_circle_float(IntPtr mat,
                                                           IntPtr center,
                                                           int radius,
                                                           IntPtr scalar,
                                                           int thickness,
                                                           CvLineTypes lineType,
                                                           int shift);

        #endregion

        #region line

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_line_int32_t(IntPtr mat,
                                                           IntPtr p1,
                                                           IntPtr p2,
                                                           IntPtr scalar,
                                                           int thickness,
                                                           CvLineTypes lineType,
                                                           int shift);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_line_float(IntPtr mat,
                                                         IntPtr p1,
                                                         IntPtr p2,
                                                         IntPtr scalar,
                                                         int thickness,
                                                         CvLineTypes lineType,
                                                         int shift);

        #endregion

        #region putText

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_putText_int32_t(IntPtr mat,
                                                              byte[] text,
                                                              int textLength,
                                                              IntPtr point,
                                                              CvHersheyFonts fontFace,
                                                              double fontScale,
                                                              IntPtr scalar,
                                                              int thickness,
                                                              CvLineTypes lineType,
                                                              bool bottomLeftOrigin);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_putText_int64_t(IntPtr mat,
                                                              byte[] text,
                                                              int textLength,
                                                              IntPtr point,
                                                              CvHersheyFonts fontFace,
                                                              double fontScale,
                                                              IntPtr scalar,
                                                              int thickness,
                                                              CvLineTypes lineType,
                                                              bool bottomLeftOrigin);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_putText_float(IntPtr mat,
                                                            byte[] text,
                                                            int textLength,
                                                            IntPtr point,
                                                            CvHersheyFonts fontFace,
                                                            double fontScale,
                                                            IntPtr scalar,
                                                            int thickness,
                                                            CvLineTypes lineType,
                                                            bool bottomLeftOrigin);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_putText_double(IntPtr mat,
                                                             byte[] text,
                                                             int textLength,
                                                             IntPtr point,
                                                             CvHersheyFonts fontFace,
                                                             double fontScale,
                                                             IntPtr scalar,
                                                             int thickness,
                                                             CvLineTypes lineType,
                                                             bool bottomLeftOrigin);

        #endregion

        #region rectangle

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_rectangle_int32_t(IntPtr mat,
                                                                IntPtr rect,
                                                                IntPtr scalar,
                                                                int thickness,
                                                                CvLineTypes lineType,
                                                                int shift);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_rectangle_float(IntPtr mat,
                                                              IntPtr rect,
                                                              IntPtr scalar,
                                                              int thickness,
                                                              CvLineTypes lineType,
                                                              int shift);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_rectangle2_int32_t(IntPtr mat,
                                                                 IntPtr pt1,
                                                                 IntPtr pt2,
                                                                 IntPtr scalar,
                                                                 int thickness,
                                                                 CvLineTypes lineType,
                                                                 int shift);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_rectangle2_float(IntPtr mat,
                                                               IntPtr pt1,
                                                               IntPtr pt2,
                                                               IntPtr scalar,
                                                               int thickness,
                                                               CvLineTypes lineType,
                                                               int shift);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_waitKey(int delay, out int returnValue);

    }

}