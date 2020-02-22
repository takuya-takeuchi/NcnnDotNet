using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_new(out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_VkMat_delete(IntPtr vkmat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_create_like_mat(IntPtr vkmat,
                                                                 IntPtr mat,
                                                                 IntPtr allocator,
                                                                 IntPtr stagingAllocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_create_like_vkmat(IntPtr vkmat,
                                                                   IntPtr mat,
                                                                   IntPtr allocator,
                                                                   IntPtr stagingAllocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_discard_staging_buffer(IntPtr vkmat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_download(IntPtr vkmat, IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_prepare_staging_buffer(IntPtr vkmat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_upload(IntPtr vkmat, IntPtr mat);

    }

}