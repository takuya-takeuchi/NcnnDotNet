using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_new(out IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void net_Net_delete(IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_set_vulkan_device(IntPtr net, int deviceIndex);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_set_vulkan_device2(IntPtr net, IntPtr vulkanDevice);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_get_vulkan_device(IntPtr net, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_register_custom_layer(IntPtr net, IntPtr type, IntPtr creator, IntPtr destroyer, IntPtr userData);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_register_custom_layer2(IntPtr net, int index, IntPtr creator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_create_extractor(IntPtr net, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_load_param_mem(IntPtr net, byte[] mem);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_load_param_filepath(IntPtr net, byte[] protoPath, int protoPathLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_load_param_datareader(IntPtr net, IntPtr reader);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_load_model_filepath(IntPtr net, byte[] modelPath, int modelPathLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_load_model_datareader(IntPtr net, IntPtr reader);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void net_Net_get_opt(IntPtr net, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Net_set_opt(IntPtr net, IntPtr option);

    }

}