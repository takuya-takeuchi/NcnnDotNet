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
        public static extern ErrorType mat_Mat_new2(int w, long elemsize, IntPtr allocator, out IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_Mat_delete(IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_reshape(IntPtr mat, int w, IntPtr allocator, out IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_reshape2(IntPtr mat, int w, int h, IntPtr allocator, out IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_reshape3(IntPtr mat, int w, int h, int c, IntPtr allocator, out IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_create(IntPtr mat, int w, long elemsize, IntPtr allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_create2(IntPtr mat, int w, int h, long elemsize, IntPtr allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mat_Mat_empty(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mat_Mat_channel(IntPtr mat, int c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mat_Mat_channel_range(IntPtr mat, int c, int channels);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mat_Mat_row(IntPtr mat, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_w(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_h(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_c(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern long mat_Mat_get_elemsize(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_elempack(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int mat_Mat_get_dims(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mat_Mat_get_data(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_get_operator_indexer(IntPtr mat, int index, out float returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_set_operator_indexer(IntPtr mat, int index, float value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_substract_mean_normalize(IntPtr mat, float[] meanVals, float[] normVals);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_from_pixels(IntPtr pixels,
                                                           PixelType type,
                                                           int w,
                                                           int h,
                                                           IntPtr allocator,
                                                           out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_from_pixels2(IntPtr pixels,
                                                            PixelType type,
                                                            int w,
                                                            int h,
                                                            int stride,
                                                            IntPtr allocator,
                                                            out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_from_pixels_resize(IntPtr pixels,
                                                                  PixelType type,
                                                                  int w,
                                                                  int h,
                                                                  int targetWidth,
                                                                  int targetHeight,
                                                                  IntPtr allocator,
                                                                  out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_Mat_from_pixels_resize2(IntPtr pixels,
                                                                   PixelType type,
                                                                   int w,
                                                                   int h,
                                                                   int stride,
                                                                   int targetWidth,
                                                                   int targetHeight,
                                                                   IntPtr allocator,
                                                                   out IntPtr returnValue);

    }

}