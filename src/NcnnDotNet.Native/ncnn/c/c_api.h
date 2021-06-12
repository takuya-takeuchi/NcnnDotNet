#ifndef _CPP_C_CAPI_H_
#define _CPP_C_CAPI_H_

#include "../export.h"
#include <c_api.h>
#include "../shared.h"

DLLEXPORT const char* c_ncnn_version()
{
    return ::ncnn_version();
}

#pragma region allocator

DLLEXPORT const ncnn_allocator_t c_ncnn_allocator_create_pool_allocator()
{
    return ::ncnn_allocator_create_pool_allocator();
}

DLLEXPORT const ncnn_allocator_t c_ncnn_allocator_create_unlocked_pool_allocator()
{
    return ::ncnn_allocator_create_unlocked_pool_allocator();
}

DLLEXPORT void c_ncnn_allocator_destroy(ncnn_allocator_t allocator)
{
    ::ncnn_allocator_destroy(allocator);
}

#pragma endregion allocator

#pragma region option

DLLEXPORT const ncnn_option_t c_ncnn_option_create()
{
    return ::ncnn_option_create();
}

DLLEXPORT void c_ncnn_option_destroy(ncnn_option_t opt)
{
    ::ncnn_option_destroy(opt);
}


DLLEXPORT const int c_ncnn_option_get_num_threads(const ncnn_option_t opt)
{
    return ::ncnn_option_get_num_threads(opt);
}

DLLEXPORT void c_ncnn_option_set_num_threads(ncnn_option_t opt, const int num_threads)
{
    ::ncnn_option_set_num_threads(opt, num_threads);
}

DLLEXPORT const int c_ncnn_option_get_use_vulkan_compute(const ncnn_option_t opt)
{
    return ::ncnn_option_get_use_vulkan_compute(opt);
}

DLLEXPORT void c_ncnn_option_set_use_vulkan_compute(ncnn_option_t opt, const int use_vulkan_compute)
{
    ::ncnn_option_set_use_vulkan_compute(opt, use_vulkan_compute);
}

#pragma endregion option

#pragma region mat

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_1d(const int w, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_1d(w, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_2d(const int w, const int h, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_2d(w, h, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_3d(const int w, const int h, const int c, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_3d(w, h, c, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_external_1d(const int w, void* data, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_external_1d(w, data, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_external_2d(const int w, const int h, void* data, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_external_2d(w, h, data, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_external_3d(const int w, const int h, const int c, void* data, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_external_3d(w, h, c, data, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_1d_elem(const int w, size_t elemsize, const int elempack, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_1d_elem(w, elemsize, elempack, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_2d_elem(const int w, const int h, size_t elemsize, const int elempack, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_2d_elem(w, h, elemsize, elempack, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_3d_elem(const int w, const int h, const int c, size_t elemsize, const int elempack, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_3d_elem(w, h, c, elemsize, elempack, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_external_1d_elem(const int w, void* data, size_t elemsize, const int elempack, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_external_1d_elem(w, data, elemsize, elempack, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_external_2d_elem(const int w, const int h, void* data, size_t elemsize, const int elempack, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_external_2d_elem(w, h, data, elemsize, elempack, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_create_external_3d_elem(const int w, const int h, const int c, void* data, size_t elemsize, const int elempack, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_create_external_3d_elem(w, h, c, data, elemsize, elempack, allocator);
}

DLLEXPORT void c_ncnn_mat_destroy(ncnn_mat_t mat)
{
    ::ncnn_mat_destroy(mat);
}

DLLEXPORT void c_ncnn_mat_fill_float(ncnn_mat_t mat, const float v)
{
    ::ncnn_mat_fill_float(mat, v);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_clone(const ncnn_mat_t mat, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_clone(mat, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_reshape_1d(const ncnn_mat_t mat, const int w, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_reshape_1d(mat, w, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_reshape_2d(const ncnn_mat_t mat, const int w, const int h, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_reshape_2d(mat, w, h, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_reshape_3d(const ncnn_mat_t mat, const int w, const int h, const int c, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_reshape_3d(mat, w, h, c, allocator);
}

DLLEXPORT const int c_ncnn_mat_get_dims(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_dims(mat);
}

DLLEXPORT const int c_ncnn_mat_get_w(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_w(mat);
}

DLLEXPORT const int c_ncnn_mat_get_h(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_h(mat);
}

DLLEXPORT const int c_ncnn_mat_get_c(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_c(mat);
}

DLLEXPORT const size_t c_ncnn_mat_get_elemsize(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_elemsize(mat);
}

DLLEXPORT const int c_ncnn_mat_get_elempack(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_elempack(mat);
}

DLLEXPORT const size_t c_ncnn_mat_get_cstep(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_cstep(mat);
}

DLLEXPORT const void* c_ncnn_mat_get_data(const ncnn_mat_t mat)
{
    return ::ncnn_mat_get_data(mat);
}

DLLEXPORT const void* c_ncnn_mat_get_channel_data(const ncnn_mat_t mat, const int c)
{
    return ::ncnn_mat_get_channel_data(mat, c);
}

#pragma endregion mat

#pragma region mat pixel

DLLEXPORT const ncnn_mat_t c_ncnn_mat_from_pixels(const unsigned char* pixels, const int type, const int w, const int h, const int stride, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_from_pixels(pixels, type, w, h, stride, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_from_pixels_resize(const unsigned char* pixels, const int type, const int w, const int h, const int stride, const int target_width, const int target_height, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_from_pixels_resize(pixels, type, w, h, stride, target_width, target_height, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_from_pixels_roi(const unsigned char* pixels, const int type, const int w, const int h, const int stride, const int roix, const int roiy, const int roiw, const int roih, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_from_pixels_roi(pixels, type, w, h, stride, roix, roiy, roiw, roih, allocator);
}

DLLEXPORT const ncnn_mat_t c_ncnn_mat_from_pixels_roi_resize(const unsigned char* pixels, const int type, const int w, const int h, const int stride, const int roix, const int roiy, const int roiw, const int roih, const int target_width, const int target_height, ncnn_allocator_t allocator)
{
    return ::ncnn_mat_from_pixels_roi_resize(pixels, type, w, h, stride, roix, roiy, roiw, roih, target_width, target_height, allocator);
}

DLLEXPORT void c_ncnn_mat_to_pixels(const ncnn_mat_t mat, unsigned char* pixels, const int type, const int stride)
{
    ::ncnn_mat_to_pixels(mat, pixels, type, stride);
}

DLLEXPORT void c_ncnn_mat_to_pixels_resize(const ncnn_mat_t mat, unsigned char* pixels, const int type, const int target_width, const int target_height, const int target_stride)
{
    ::ncnn_mat_to_pixels_resize(mat, pixels, type, target_width, target_height, target_stride);
}

DLLEXPORT void c_ncnn_mat_substract_mean_normalize(ncnn_mat_t mat, const float* mean_vals, const float* norm_vals)
{
    ::ncnn_mat_substract_mean_normalize(mat, mean_vals, norm_vals);
}

DLLEXPORT void c_ncnn_convert_packing(const ncnn_mat_t src, ncnn_mat_t* dst, const int elempack, const ncnn_option_t opt)
{
    ::ncnn_convert_packing(src, dst, elempack, opt);
}

DLLEXPORT void c_ncnn_flatten(const ncnn_mat_t src, ncnn_mat_t* dst, const ncnn_option_t opt)
{
    ::ncnn_flatten(src, dst, opt);
}

#pragma endregion mat pixel

#pragma region blob

DLLEXPORT const char* c_ncnn_blob_get_name(const ncnn_blob_t blob)
{
    return ::ncnn_blob_get_name(blob);
}

DLLEXPORT const int c_ncnn_blob_get_producer(const ncnn_blob_t blob)
{
    return ::ncnn_blob_get_producer(blob);
}

DLLEXPORT const int c_ncnn_blob_get_consumer(const ncnn_blob_t blob)
{
    return ::ncnn_blob_get_consumer(blob);
}

DLLEXPORT void c_ncnn_blob_get_shape(const ncnn_blob_t blob, int* dims, int* w, int* h, int* c)
{
    ::ncnn_blob_get_shape(blob, dims, w, h, c);
}

#pragma endregion blob

#pragma region parameter

DLLEXPORT ncnn_paramdict_t c_ncnn_paramdict_create()
{
    return ::ncnn_paramdict_create();
}

DLLEXPORT void c_ncnn_paramdict_destroy(ncnn_paramdict_t pd)
{
    ::ncnn_paramdict_destroy(pd);
}

DLLEXPORT const int c_ncnn_paramdict_get_type(const ncnn_paramdict_t pd, const int id)
{
    return ::ncnn_paramdict_get_type(pd, id);
}

DLLEXPORT const int c_ncnn_paramdict_get_int(const ncnn_paramdict_t pd, const int id, const int def)
{
    return ::ncnn_paramdict_get_int(pd, id, def);
}

DLLEXPORT const float c_ncnn_paramdict_get_float(const ncnn_paramdict_t pd, const int id, const float def)
{
    return ::ncnn_paramdict_get_float(pd, id, def);
}

DLLEXPORT const ncnn_mat_t c_ncnn_paramdict_get_array(const ncnn_paramdict_t pd, const int id, const ncnn_mat_t def)
{
    return ::ncnn_paramdict_get_array(pd, id, def);
}

DLLEXPORT void c_ncnn_paramdict_set_int(ncnn_paramdict_t pd, const int id, const int i)
{
    ::ncnn_paramdict_set_int(pd, id, i);
}

DLLEXPORT void c_ncnn_paramdict_set_float(ncnn_paramdict_t pd, const int id, const float f)
{
    ::ncnn_paramdict_set_float(pd, id, f);
}

DLLEXPORT void c_ncnn_paramdict_set_array(ncnn_paramdict_t pd, const int id, const ncnn_mat_t v)
{
    ::ncnn_paramdict_set_array(pd, id, v);
}

#pragma endregion parameter

#pragma region datareader

DLLEXPORT const ncnn_datareader_t c_ncnn_datareader_create()
{
    return ::ncnn_datareader_create();
}

DLLEXPORT const ncnn_datareader_t c_ncnn_datareader_create_from_stdio(FILE* fp)
{
    return ::ncnn_datareader_create_from_stdio(fp);
}

DLLEXPORT const ncnn_datareader_t c_ncnn_datareader_create_from_memory(const unsigned char** mem)
{
    return ::ncnn_datareader_create_from_memory(mem);
}

DLLEXPORT void c_ncnn_datareader_destroy(ncnn_datareader_t dr)
{
    ::ncnn_datareader_destroy(dr);
}

#pragma endregion datareader

#pragma region modelbin

DLLEXPORT const ncnn_modelbin_t c_ncnn_modelbin_create_from_datareader(const ncnn_datareader_t dr)
{
    return ::ncnn_modelbin_create_from_datareader(dr);
}

DLLEXPORT const ncnn_modelbin_t c_ncnn_modelbin_create_from_mat_array(const ncnn_mat_t* weights, const int n)
{
    return ::ncnn_modelbin_create_from_mat_array(weights, n);
}

DLLEXPORT void c_ncnn_modelbin_destroy(ncnn_modelbin_t mb)
{
    ::ncnn_modelbin_destroy(mb);
}

#pragma endregion modelbin

#pragma region layer

DLLEXPORT const ncnn_layer_t c_ncnn_layer_create()
{
    return ::ncnn_layer_create();
}

DLLEXPORT const ncnn_layer_t c_ncnn_layer_create_by_typeindex(const int typeindex)
{
    return ::ncnn_layer_create_by_typeindex(typeindex);
}

DLLEXPORT const ncnn_layer_t c_ncnn_layer_create_by_type(const char* type)
{
    return ::ncnn_layer_create_by_type(type);
}

DLLEXPORT void c_ncnn_layer_destroy(ncnn_layer_t layer)
{
    ::ncnn_layer_destroy(layer);
}

DLLEXPORT const char* c_ncnn_layer_get_name(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_name(layer);
}

DLLEXPORT const int c_ncnn_layer_get_typeindex(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_typeindex(layer);
}

DLLEXPORT const char* c_ncnn_layer_get_type(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_type(layer);
}

DLLEXPORT const int c_ncnn_layer_get_one_blob_only(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_one_blob_only(layer);
}

DLLEXPORT const int c_ncnn_layer_get_support_inplace(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_support_inplace(layer);
}

DLLEXPORT const int c_ncnn_layer_get_support_vulkan(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_support_vulkan(layer);
}

DLLEXPORT const int c_ncnn_layer_get_support_packing(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_support_packing(layer);
}

DLLEXPORT const int c_ncnn_layer_get_support_bf16_storage(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_support_bf16_storage(layer);
}

DLLEXPORT const int c_ncnn_layer_get_support_fp16_storage(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_support_fp16_storage(layer);
}

DLLEXPORT const int c_ncnn_layer_get_support_image_storage(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_support_image_storage(layer);
}

DLLEXPORT void c_ncnn_layer_set_one_blob_only(ncnn_layer_t layer, const int enable)
{
    ::ncnn_layer_set_one_blob_only(layer, enable);
}

DLLEXPORT void c_ncnn_layer_set_support_inplace(ncnn_layer_t layer, const int enable)
{
    ::ncnn_layer_set_support_inplace(layer, enable);
}

DLLEXPORT void c_ncnn_layer_set_support_vulkan(ncnn_layer_t layer, const int enable)
{
    ::ncnn_layer_set_support_vulkan(layer, enable);
}

DLLEXPORT void c_ncnn_layer_set_support_packing(ncnn_layer_t layer, const int enable)
{
    ::ncnn_layer_set_support_packing(layer, enable);
}

DLLEXPORT void c_ncnn_layer_set_support_bf16_storage(ncnn_layer_t layer, const int enable)
{
    ::ncnn_layer_set_support_bf16_storage(layer, enable);
}

DLLEXPORT void c_ncnn_layer_set_support_fp16_storage(ncnn_layer_t layer, const int enable)
{
    ::ncnn_layer_set_support_fp16_storage(layer, enable);
}

DLLEXPORT void c_ncnn_layer_set_support_image_storage(ncnn_layer_t layer, const int enable)
{
    ::ncnn_layer_set_support_image_storage(layer, enable);
}

DLLEXPORT const int c_ncnn_layer_get_bottom_count(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_bottom_count(layer);
}

DLLEXPORT const int c_ncnn_layer_get_bottom(const ncnn_layer_t layer, const int i)
{
    return ::ncnn_layer_get_bottom(layer, i);
}

DLLEXPORT const int c_ncnn_layer_get_top_count(const ncnn_layer_t layer)
{
    return ::ncnn_layer_get_top_count(layer);
}

DLLEXPORT const int c_ncnn_layer_get_top(const ncnn_layer_t layer, const int i)
{
    return ::ncnn_layer_get_top(layer, i);
}

DLLEXPORT void c_ncnn_blob_get_bottom_shape(const ncnn_layer_t layer, const int i, int* dims, int* w, int* h, int* c)
{
    ::ncnn_blob_get_bottom_shape(layer, i, dims, w, h, c);
}

DLLEXPORT void c_ncnn_blob_get_top_shape(const ncnn_layer_t layer, const int i, int* dims, int* w, int* h, int* c)
{
    ::ncnn_blob_get_top_shape(layer, i, dims, w, h, c);
}

#pragma endregion layer

#pragma region layer factory

#pragma endregion layer factory

#pragma region net

DLLEXPORT const ncnn_net_t c_ncnn_net_create()
{
    return ::ncnn_net_create();
}

DLLEXPORT void c_ncnn_net_destroy(ncnn_net_t net)
{
    ::ncnn_net_destroy(net);
}

DLLEXPORT void c_ncnn_net_set_option(ncnn_net_t net, ncnn_option_t opt)
{
    ::ncnn_net_set_option(net, opt);
}

DLLEXPORT void c_ncnn_net_register_custom_layer_by_type(ncnn_net_t net, const char* type, ncnn_layer_creator_t creator, ncnn_layer_destroyer_t destroyer, void* userdata)
{
    ::ncnn_net_register_custom_layer_by_type(net, type, creator, destroyer, userdata);
}

DLLEXPORT void c_ncnn_net_register_custom_layer_by_typeindex(ncnn_net_t net, const int typeindex, ncnn_layer_creator_t creator, ncnn_layer_destroyer_t destroyer, void* userdata)
{
    ::ncnn_net_register_custom_layer_by_typeindex(net, typeindex, creator, destroyer, userdata);
}

DLLEXPORT const int c_ncnn_net_load_param(ncnn_net_t net, const char* path)
{
    return ::ncnn_net_load_param(net, path);
}

DLLEXPORT const int c_ncnn_net_load_param_bin(ncnn_net_t net, const char* path)
{
    return ::ncnn_net_load_param_bin(net, path);
}

DLLEXPORT const int c_ncnn_net_load_model(ncnn_net_t net, const char* path)
{
    return ::ncnn_net_load_model(net, path);
}

DLLEXPORT const int c_ncnn_net_load_param_memory(ncnn_net_t net, const char* mem)
{
    return ::ncnn_net_load_param_memory(net, mem);
}

DLLEXPORT const int c_ncnn_net_load_param_bin_memory(ncnn_net_t net, const unsigned char* mem)
{
    return ::ncnn_net_load_param_bin_memory(net, mem);
}

DLLEXPORT const int c_ncnn_net_load_model_memory(ncnn_net_t net, const unsigned char* mem)
{
    return ::ncnn_net_load_model_memory(net, mem);
}

DLLEXPORT const int c_ncnn_net_load_param_datareader(ncnn_net_t net, const ncnn_datareader_t dr)
{
    return ::ncnn_net_load_param_datareader(net, dr);
}

DLLEXPORT const int c_ncnn_net_load_param_bin_datareader(ncnn_net_t net, const ncnn_datareader_t dr)
{
    return ::ncnn_net_load_param_bin_datareader(net, dr);
}

DLLEXPORT const int c_ncnn_net_load_model_datareader(ncnn_net_t net, const ncnn_datareader_t dr)
{
    return ::ncnn_net_load_model_datareader(net, dr);
}

DLLEXPORT void c_ncnn_net_clear(ncnn_net_t net)
{
    ncnn_net_clear(net);
}

#pragma endregion net

#pragma region extractor

DLLEXPORT const ncnn_extractor_t c_ncnn_extractor_create(ncnn_net_t net)
{
    return ::ncnn_extractor_create(net);
}

DLLEXPORT void c_ncnn_extractor_destroy(ncnn_extractor_t ex)
{
    ::ncnn_extractor_destroy(ex);
}

DLLEXPORT void c_ncnn_extractor_set_option(ncnn_extractor_t ex, const ncnn_option_t opt)
{
    ::ncnn_extractor_set_option(ex, opt);
}

DLLEXPORT const int c_ncnn_extractor_input(ncnn_extractor_t ex, const char* name, const ncnn_mat_t mat)
{
    return ::ncnn_extractor_input(ex, name, mat);
}

DLLEXPORT const int c_ncnn_extractor_extract(ncnn_extractor_t ex, const char* name, ncnn_mat_t* mat)
{
    return ::ncnn_extractor_extract(ex, name, mat);
}

DLLEXPORT const int c_ncnn_extractor_input_index(ncnn_extractor_t ex, const int index, const ncnn_mat_t mat)
{
    return ::ncnn_extractor_input_index(ex, index, mat);
}

DLLEXPORT const int c_ncnn_extractor_extract_index(ncnn_extractor_t ex, const int index, ncnn_mat_t* mat)
{
    return ::ncnn_extractor_extract_index(ex, index, mat);
}

#pragma endregion extractor

#endif