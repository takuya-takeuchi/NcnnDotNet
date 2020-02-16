using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_delete(IntPtr layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_load_param(IntPtr layer, IntPtr pd, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_forward_inplace(IntPtr layer, IntPtr mat, IntPtr opt, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_get_one_blob_only(IntPtr layer, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_set_one_blob_only(IntPtr layer, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_get_support_packing(IntPtr layer, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_set_support_packing(IntPtr layer, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_layer_to_index(byte[] type, int typeLength, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_create_layer(byte[] type, int typeLength, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_create_layer2(int index, out IntPtr returnValue);

    }

}