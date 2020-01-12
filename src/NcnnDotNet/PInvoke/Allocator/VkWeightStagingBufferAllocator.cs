using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType allocator_VkWeightStagingBufferAllocator_new(IntPtr vulkanDevice, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void allocator_VkWeightStagingBufferAllocator_delete(IntPtr allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void allocator_VkWeightStagingBufferAllocator_clear(IntPtr allocator);

    }

}