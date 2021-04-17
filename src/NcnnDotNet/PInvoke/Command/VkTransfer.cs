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

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void command_VkTransfer_get_weight_vkallocator(IntPtr transfer, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void command_VkTransfer_set_weight_vkallocator(IntPtr transfer, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void command_VkTransfer_get_staging_vkallocator(IntPtr transfer, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void command_VkTransfer_set_staging_vkallocator(IntPtr transfer, IntPtr value);

    }

}