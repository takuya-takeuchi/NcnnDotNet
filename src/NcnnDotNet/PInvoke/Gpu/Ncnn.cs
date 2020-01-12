using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int gpu_create_gpu_instance();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern void gpu_destroy_gpu_instance();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int gpu_get_default_gpu_index();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int gpu_get_gpu_count();

    }

}