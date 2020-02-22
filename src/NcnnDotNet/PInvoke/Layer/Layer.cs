using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_new(out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_delete(IntPtr layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_load_param(IntPtr layer, IntPtr pd, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_load_model(IntPtr layer, IntPtr mb, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_create_pipeline(IntPtr layer, IntPtr opt, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_destroy_pipeline(IntPtr layer, IntPtr opt, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_forward2(IntPtr layer,
                                                            IntPtr bottomBlob,
                                                            IntPtr topBlob,
                                                            IntPtr opt,
                                                            out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_forward2_vkmat(IntPtr layer,
                                                                  IntPtr bottomBlob,
                                                                  IntPtr topBlob,
                                                                  IntPtr cmd,
                                                                  IntPtr opt,
                                                                  out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_forward_inplace(IntPtr layer, IntPtr mat, IntPtr opt, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_Layer_upload_model(IntPtr layer, IntPtr cmd, IntPtr opt, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_get_one_blob_only(IntPtr layer, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_set_one_blob_only(IntPtr layer, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_get_support_packing(IntPtr layer, out bool returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_set_support_packing(IntPtr layer, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_get_vkdev(IntPtr layer, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_Layer_set_vkdev(IntPtr layer, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_layer_to_index(byte[] type, int typeLength, out int returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_create_layer(byte[] type, int typeLength, out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_create_layer2(int index, out IntPtr returnValue);

    }

}