using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType command_VkTransfer_new(IntPtr vkdev, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void command_VkTransfer_delete(IntPtr transfer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType command_VkTransfer_submit_and_wait(IntPtr transfer, out int returnValue);

    }

}