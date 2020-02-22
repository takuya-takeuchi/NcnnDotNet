using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_cast_float16_to_float32(IntPtr src,
                                                                   IntPtr dst,
                                                                   IntPtr opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_cast_float32_to_float16(IntPtr src,
                                                                   IntPtr dst,
                                                                   IntPtr opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_convert_packing(IntPtr src,
                                                           IntPtr dst,
                                                           int elemPack,
                                                           IntPtr opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_resize_bicubic(IntPtr src,
                                                          IntPtr dst,
                                                          int w,
                                                          int h,
                                                          IntPtr opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_resize_bilinear(IntPtr src,
                                                           IntPtr dst,
                                                           int w,
                                                           int h,
                                                           IntPtr opt);

    }

}