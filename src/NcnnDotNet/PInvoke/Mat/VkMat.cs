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
        public static extern IntPtr mat_VkMat_get_allocator(IntPtr mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_create_like_mat(IntPtr vkmat,
                                                                 IntPtr mat,
                                                                 IntPtr allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_VkMat_create_like_vkmat(IntPtr vkmat,
                                                                   IntPtr mat,
                                                                   IntPtr allocator);

    }

}