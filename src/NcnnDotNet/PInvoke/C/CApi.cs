using System;
using System.Runtime.InteropServices;

using size_t = System.UInt64;

using ncnn_allocator_t = System.IntPtr;
using ncnn_option_t = System.IntPtr;
using ncnn_mat_t = System.IntPtr;
using ncnn_blob_t = System.IntPtr;
using ncnn_paramdict_t = System.IntPtr;
using ncnn_datareader_t = System.IntPtr;
using ncnn_modelbin_t = System.IntPtr;
using ncnn_layer_t = System.IntPtr;
using ncnn_net_t = System.IntPtr;
using ncnn_extractor_t = System.IntPtr;
using ncnn_layer_creator_t = System.IntPtr;
using ncnn_layer_destroyer_t = System.IntPtr;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        #region allocator

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_allocator_t c_ncnn_allocator_create_pool_allocator();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_allocator_t c_ncnn_allocator_create_unlocked_pool_allocator();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_allocator_destroy(ncnn_allocator_t allocator);

        #endregion

        #region option

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_option_t c_ncnn_option_create();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_option_destroy(ncnn_option_t opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_option_get_num_threads(ncnn_option_t opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_option_set_num_threads(ncnn_option_t opt, int num_threads);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_option_get_use_vulkan_compute(ncnn_option_t opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_option_set_use_vulkan_compute(ncnn_option_t opt, int use_vulkan_compute);

        #endregion

        #region mat

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_1d(int w, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_2d(int w, int h, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_3d(int w, int h, int c, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_external_1d(int w, IntPtr data, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_external_2d(int w, int h, IntPtr data, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_external_3d(int w, int h, int c, IntPtr data, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_1d_elem(int w, size_t elemsize, int elempack, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_2d_elem(int w, int h, size_t elemsize, int elempack, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_3d_elem(int w, int h, int c, size_t elemsize, int elempack, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_external_1d_elem(int w, IntPtr data, size_t elemsize, int elempack, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_external_2d_elem(int w, int h, IntPtr data, size_t elemsize, int elempack, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_create_external_3d_elem(int w, int h, int c, IntPtr data, size_t elemsize, int elempack, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_mat_destroy(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_mat_fill_float(ncnn_mat_t mat, float v);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_clone(ncnn_mat_t mat, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_reshape_1d(ncnn_mat_t mat, int w, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_reshape_2d(ncnn_mat_t mat, int w, int h, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_reshape_3d(ncnn_mat_t mat, int w, int h, int c, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_mat_get_dims(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_mat_get_w(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_mat_get_h(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_mat_get_c(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern size_t c_ncnn_mat_get_elemsize(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_mat_get_elempack(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern size_t c_ncnn_mat_get_cstep(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr c_ncnn_mat_get_data(ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr c_ncnn_mat_get_channel_data(ncnn_mat_t mat, int c);

        #endregion

        #region mat pixel

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_from_pixels(byte[] pixels, PixelType type, int w, int h, int stride, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_from_pixels_resize(byte[] pixels, PixelType type, int w, int h, int stride, int target_width, int target_height, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_from_pixels_roi(byte[] pixels, PixelType type, int w, int h, int stride, int roix, int roiy, int roiw, int roih, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_mat_from_pixels_roi_resize(byte[] pixels, PixelType type, int w, int h, int stride, int roix, int roiy, int roiw, int roih, int target_width, int target_height, ncnn_allocator_t allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_mat_to_pixels(ncnn_mat_t mat, byte[] pixels, PixelType type, int stride);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_mat_to_pixels_resize(ncnn_mat_t mat, byte[] pixels, PixelType type, int target_width, int target_height, int target_stride);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_mat_substract_mean_normalize(ncnn_mat_t mat, float[] mean_vals, float[] norm_vals);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_convert_packing(ncnn_mat_t src, out ncnn_mat_t dst, int elempack, ncnn_option_t opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_flatten(ncnn_mat_t src, out ncnn_mat_t dst, ncnn_option_t opt);

        #endregion

        #region blob

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr c_ncnn_blob_get_name(ncnn_blob_t blob);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_blob_get_producer(ncnn_blob_t blob);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_blob_get_consumer(ncnn_blob_t blob);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_blob_get_shape(ncnn_blob_t blob, out int dims, out int w, out int h, out int c);

        #endregion

        #region parameter

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_paramdict_t c_ncnn_paramdict_create();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_paramdict_destroy(ncnn_paramdict_t pd);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_paramdict_get_type(ncnn_paramdict_t pd, int id);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_paramdict_get_int(ncnn_paramdict_t pd, int id, int def);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern float c_ncnn_paramdict_get_float(ncnn_paramdict_t pd, int id, float def);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_mat_t c_ncnn_paramdict_get_array(ncnn_paramdict_t pd, int id, ncnn_mat_t def);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_paramdict_set_int(ncnn_paramdict_t pd, int id, int i);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_paramdict_set_float(ncnn_paramdict_t pd, int id, float f);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_paramdict_set_array(ncnn_paramdict_t pd, int id, ncnn_mat_t v);

        #endregion

        #region datareader

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_datareader_t c_ncnn_datareader_create();

        // ToDo
        // [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        // public static extern ncnn_datareader_t c_ncnn_datareader_create_from_stdio(FILE* fp);

        // [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        // public static extern ncnn_datareader_t c_ncnn_datareader_create_from_memory(unsigned char** mem);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_datareader_destroy(ncnn_datareader_t dr);

        #endregion

        #region modelbin

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_modelbin_t c_ncnn_modelbin_create_from_datareader(ncnn_datareader_t dr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_modelbin_t c_ncnn_modelbin_create_from_mat_array(IntPtr[] weights, int n);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_modelbin_destroy(ncnn_modelbin_t mb);

        #endregion

        #region layer

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_layer_t c_ncnn_layer_create();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_layer_t c_ncnn_layer_create_by_typeindex(int typeindex);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_layer_t c_ncnn_layer_create_by_type(byte[] type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_destroy(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr c_ncnn_layer_get_name(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_typeindex(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr c_ncnn_layer_get_type(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_one_blob_only(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_support_inplace(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_support_vulkan(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_support_packing(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_support_bf16_storage(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_support_fp16_storage(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_support_image_storage(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_set_one_blob_only(ncnn_layer_t layer, int enable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_set_support_inplace(ncnn_layer_t layer, int enable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_set_support_vulkan(ncnn_layer_t layer, int enable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_set_support_packing(ncnn_layer_t layer, int enable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_set_support_bf16_storage(ncnn_layer_t layer, int enable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_set_support_fp16_storage(ncnn_layer_t layer, int enable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_layer_set_support_image_storage(ncnn_layer_t layer, int enable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_bottom_count(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_bottom(ncnn_layer_t layer, int i);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_top_count(ncnn_layer_t layer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_layer_get_top(ncnn_layer_t layer, int i);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_blob_get_bottom_shape(ncnn_layer_t layer, int i, out int dims, out int w, out int h, out int c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_blob_get_top_shape(ncnn_layer_t layer, int i, out int dims, out int w, out int h, out int c);

        #endregion

        #region layer factory
        #endregion

        #region net

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_net_t c_ncnn_net_create();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_net_destroy(ncnn_net_t net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_net_set_option(ncnn_net_t net, ncnn_option_t opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_net_register_custom_layer_by_type(ncnn_net_t net, byte[] type, ncnn_layer_creator_t creator, ncnn_layer_destroyer_t destroyer, IntPtr userdata);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_net_register_custom_layer_by_typeindex(ncnn_net_t net, int typeindex, ncnn_layer_creator_t creator, ncnn_layer_destroyer_t destroyer, IntPtr userdata);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_param(ncnn_net_t net, byte[] path);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_param_bin(ncnn_net_t net, byte[] path);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_model(ncnn_net_t net, byte[] path);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_param_memory(ncnn_net_t net, byte[] mem);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_param_bin_memory(ncnn_net_t net, byte[] mem);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_model_memory(ncnn_net_t net, byte[] mem);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_param_datareader(ncnn_net_t net, ncnn_datareader_t dr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_param_bin_datareader(ncnn_net_t net, ncnn_datareader_t dr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_net_load_model_datareader(ncnn_net_t net, ncnn_datareader_t dr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_net_clear(ncnn_net_t net);

        #endregion

        #region extractor

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ncnn_extractor_t c_ncnn_extractor_create(ncnn_net_t net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_extractor_destroy(ncnn_extractor_t ex);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void c_ncnn_extractor_set_option(ncnn_extractor_t ex, ncnn_option_t opt);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_extractor_input(ncnn_extractor_t ex, byte[] name, ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_extractor_extract(ncnn_extractor_t ex, byte[] name, out ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_extractor_input_index(ncnn_extractor_t ex, int index, ncnn_mat_t mat);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int c_ncnn_extractor_extract_index(ncnn_extractor_t ex, int index, out ncnn_mat_t mat);

        #endregion

    }

}