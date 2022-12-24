#ifndef _CPP_GPU_GPUINFO_H_
#define _CPP_GPU_GPUINFO_H_

#include "../export.h"
#include <gpu.h>
#include "../shared.h"

#ifdef NCNN_VULKAN

DLLEXPORT void gpu_GpuInfo_get_support_fp16_packed(ncnn::GpuInfo* info, bool* returnValue)
{
    *returnValue = info->support_fp16_packed();
}

DLLEXPORT void gpu_GpuInfo_get_support_fp16_storage(ncnn::GpuInfo* info, bool* returnValue)
{
    *returnValue = info->support_fp16_storage();
}

DLLEXPORT void gpu_GpuInfo_get_support_fp16_arithmetic(ncnn::GpuInfo* info, bool* returnValue)
{
    *returnValue = info->support_fp16_arithmetic();
}

DLLEXPORT void gpu_GpuInfo_get_support_int8_storage(ncnn::GpuInfo* info, bool* returnValue)
{
    *returnValue = info->support_int8_storage();
}

DLLEXPORT void gpu_GpuInfo_get_support_int8_arithmetic(ncnn::GpuInfo* info, bool* returnValue)
{
    *returnValue = info->support_int8_arithmetic();
}

#endif

#endif