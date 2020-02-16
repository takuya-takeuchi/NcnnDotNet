using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType gpu_VulkanDevice_new(int deviceIndex, out IntPtr device);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void gpu_VulkanDevice_delete(IntPtr device);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void gpu_VulkanDevice_get_info(IntPtr device, out IntPtr returnValue);

}

}