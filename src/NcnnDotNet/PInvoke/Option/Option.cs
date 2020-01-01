using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType option_Option_new(out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_delete(IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_vulkan_compute(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_vulkan_compute(IntPtr option, bool value);

    }

}