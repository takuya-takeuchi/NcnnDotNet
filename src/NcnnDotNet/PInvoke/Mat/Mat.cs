using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_new(out IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_Mat_delete(IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mat_Mat_empty(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_w(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_h(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_c(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_get_operator_indexer(IntPtr mat, int index, out float returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_substract_mean_normalize(IntPtr mat, float[] meanVals, float[] normVals);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_from_pixels_resize(IntPtr pixels,
                                                                  PixelType type,
                                                                  int w,
                                                                  int h,
                                                                  int targetWidth,
                                                                  int targetHeight,
                                                                  IntPtr allocator,
                                                                  out IntPtr returnValue);

    }

}