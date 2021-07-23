using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void gpu_GpuInfo_get_support_fp16_packed(IntPtr info, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void gpu_GpuInfo_get_support_fp16_storage(IntPtr info, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void gpu_GpuInfo_get_support_fp16_arithmetic(IntPtr info, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void gpu_GpuInfo_get_support_int8_storage(IntPtr info, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void gpu_GpuInfo_get_support_int8_arithmetic(IntPtr info, out bool returnValue);

    }

}