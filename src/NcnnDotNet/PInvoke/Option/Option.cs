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
        public static extern void option_Option_get_lightmode(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_lightmode(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_num_threads(IntPtr option, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_num_threads(IntPtr option, int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_fp16_arithmetic(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_fp16_arithmetic(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_fp16_packed(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_fp16_packed(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_fp16_storage(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_fp16_storage(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_int8_arithmetic(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_int8_arithmetic(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_int8_inference(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_int8_inference(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_int8_storage(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_int8_storage(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_packing_layout(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_packing_layout(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_sgemm_convolution(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_sgemm_convolution(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_vulkan_compute(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_vulkan_compute(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_use_winograd_convolution(IntPtr option, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_use_winograd_convolution(IntPtr option, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_blob_allocator(IntPtr option, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_blob_allocator(IntPtr option, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_workspace_allocator(IntPtr option, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_workspace_allocator(IntPtr option, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_blob_vkallocator(IntPtr option, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_blob_vkallocator(IntPtr option, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_staging_vkallocator(IntPtr option, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_staging_vkallocator(IntPtr option, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_get_workspace_vkallocator(IntPtr option, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void option_Option_set_workspace_vkallocator(IntPtr option, IntPtr value);

    }

}