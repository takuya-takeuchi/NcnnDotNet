#ifndef _CPP_OPTION_OPTION_H_
#define _CPP_OPTION_OPTION_H_

#include "../export.h"
#include <option.h>
#include "../shared.h"

DLLEXPORT int option_Option_new(ncnn::Option** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Option();

    return error;
}

DLLEXPORT void option_Option_delete(ncnn::Option* option)
{
    if (option != nullptr) delete option;
}

DLLEXPORT void option_Option_get_lightmode(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->lightmode;
}

DLLEXPORT void option_Option_set_lightmode(ncnn::Option* option, bool value)
{
    option->lightmode = value;
}

DLLEXPORT void option_Option_get_num_threads(ncnn::Option* option, int32_t* returnValue)
{
    *returnValue = option->num_threads;
}

DLLEXPORT void option_Option_set_num_threads(ncnn::Option* option, int32_t value)
{
    option->num_threads = value;
}

DLLEXPORT void option_Option_get_use_fp16_arithmetic(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_fp16_arithmetic;
}

DLLEXPORT void option_Option_set_use_fp16_arithmetic(ncnn::Option* option, bool value)
{
    option->use_fp16_arithmetic = value;
}

DLLEXPORT void option_Option_get_use_fp16_packed(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_fp16_packed;
}

DLLEXPORT void option_Option_set_use_fp16_packed(ncnn::Option* option, bool value)
{
    option->use_fp16_packed = value;
}

DLLEXPORT void option_Option_get_use_fp16_storage(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_fp16_storage;
}

DLLEXPORT void option_Option_set_use_fp16_storage(ncnn::Option* option, bool value)
{
    option->use_fp16_storage = value;
}

DLLEXPORT void option_Option_get_use_int8_arithmetic(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_int8_arithmetic;
}

DLLEXPORT void option_Option_set_use_int8_arithmetic(ncnn::Option* option, bool value)
{
    option->use_int8_arithmetic = value;
}

DLLEXPORT void option_Option_get_use_int8_inference(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_int8_inference;
}

DLLEXPORT void option_Option_set_use_int8_inference(ncnn::Option* option, bool value)
{
    option->use_int8_inference = value;
}

DLLEXPORT void option_Option_get_use_int8_storage(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_int8_storage;
}

DLLEXPORT void option_Option_set_use_int8_storage(ncnn::Option* option, bool value)
{
    option->use_int8_storage = value;
}

DLLEXPORT void option_Option_get_use_packing_layout(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_packing_layout;
}

DLLEXPORT void option_Option_set_use_packing_layout(ncnn::Option* option, bool value)
{
    option->use_packing_layout = value;
}

DLLEXPORT void option_Option_get_use_sgemm_convolution(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_sgemm_convolution;
}

DLLEXPORT void option_Option_set_use_sgemm_convolution(ncnn::Option* option, bool value)
{
    option->use_sgemm_convolution = value;
}

DLLEXPORT void option_Option_get_use_vulkan_compute(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_vulkan_compute;
}

DLLEXPORT void option_Option_set_use_vulkan_compute(ncnn::Option* option, bool value)
{
    option->use_vulkan_compute = value;
}

DLLEXPORT void option_Option_get_use_winograd_convolution(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_winograd_convolution;
}

DLLEXPORT void option_Option_set_use_winograd_convolution(ncnn::Option* option, bool value)
{
    option->use_winograd_convolution = value;
}

DLLEXPORT void option_Option_get_blob_allocator(ncnn::Option* option, ncnn::Allocator** returnValue)
{
    *returnValue = option->blob_allocator;
}

DLLEXPORT void option_Option_set_blob_allocator(ncnn::Option* option, ncnn::Allocator* value)
{
    option->blob_allocator = value;
}

DLLEXPORT void option_Option_get_workspace_allocator(ncnn::Option* option, ncnn::Allocator** returnValue)
{
    *returnValue = option->workspace_allocator;
}

DLLEXPORT void option_Option_set_workspace_allocator(ncnn::Option* option, ncnn::Allocator* value)
{
    option->workspace_allocator = value;
}

#if NCNN_VULKAN

DLLEXPORT void option_Option_get_blob_vkallocator(ncnn::Option* option, ncnn::VkAllocator** returnValue)
{
    *returnValue = option->blob_vkallocator;
}

DLLEXPORT void option_Option_set_blob_vkallocator(ncnn::Option* option, ncnn::VkAllocator* value)
{
    option->blob_vkallocator = value;
}

DLLEXPORT void option_Option_get_staging_vkallocator(ncnn::Option* option, ncnn::VkAllocator** returnValue)
{
    *returnValue = option->staging_vkallocator;
}

DLLEXPORT void option_Option_set_staging_vkallocator(ncnn::Option* option, ncnn::VkAllocator* value)
{
    option->staging_vkallocator = value;
}

DLLEXPORT void option_Option_get_workspace_vkallocator(ncnn::Option* option, ncnn::VkAllocator** returnValue)
{
    *returnValue = option->workspace_vkallocator;
}

DLLEXPORT void option_Option_set_workspace_vkallocator(ncnn::Option* option, ncnn::VkAllocator* value)
{
    option->workspace_vkallocator = value;
}

#endif

#endif