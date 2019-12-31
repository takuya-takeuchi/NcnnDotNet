#ifndef _CPP_GPU_GPU_H_
#define _CPP_GPU_GPU_H_

#include "../export.h"
#include <ncnn/gpu.h>
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

#endif

#endif