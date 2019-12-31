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
        public static extern void opencv_imshow(byte[] winName, int winNameLength, IntPtr mat);

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

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType opencv_waitKey(int delay, out int returnValue);

    }

}