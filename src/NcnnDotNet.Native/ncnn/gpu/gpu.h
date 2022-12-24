#ifndef _CPP_GPU_GPU_H_
#define _CPP_GPU_GPU_H_

#include "../export.h"
#include <gpu.h>
#include "../shared.h"

#ifdef NCNN_VULKAN

DLLEXPORT int32_t gpu_create_gpu_instance()
{
    return ncnn::create_gpu_instance();
}

DLLEXPORT void gpu_destroy_gpu_instance()
{
    ncnn::destroy_gpu_instance();
}

DLLEXPORT int32_t gpu_get_default_gpu_index()
{
    return ncnn::get_default_gpu_index();
}

DLLEXPORT int32_t gpu_get_gpu_count()
{
    return ncnn::get_gpu_count();
}

DLLEXPORT ncnn::VulkanDevice* gpu_get_gpu_device(const int device_index)
{
    return ncnn::get_gpu_device(device_index);
}

#endif

#endif