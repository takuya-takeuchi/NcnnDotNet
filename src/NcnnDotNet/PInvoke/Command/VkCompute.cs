using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType command_VkCompute_new(IntPtr vkdev, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void command_VkCompute_delete(IntPtr compute);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType command_VkCompute_submit_and_wait(IntPtr compute, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType command_VkCompute_record_upload(IntPtr compute, IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType command_VkCompute_record_download(IntPtr compute, IntPtr mat);

    }

}