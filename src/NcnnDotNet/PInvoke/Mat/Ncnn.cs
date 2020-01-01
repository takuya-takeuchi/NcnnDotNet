using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern ErrorType mat_resize_bicubic(IntPtr src,
                                                          IntPtr dst,
                                                          int w,
                                                          int h,
                                                          IntPtr opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern ErrorType mat_resize_bilinear(IntPtr src,
                                                           IntPtr dst,
                                                           int w,
                                                           int h,
                                                           IntPtr opt);

    }

}