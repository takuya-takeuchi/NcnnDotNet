#ifndef _CPP_COMMAND_VKCOMPUTE_H_
#define _CPP_COMMAND_VKCOMPUTE_H_

#include "../export.h"
#include <command.h>
#include "../shared.h"

#ifdef NCNN_VULKAN

DLLEXPORT int32_t command_VkCompute_new(const ncnn::VulkanDevice* vkdev, ncnn::VkCompute** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::VkCompute(vkdev);

    return error;
}

DLLEXPORT void command_VkCompute_delete(ncnn::VkCompute * compute)
{
    if (compute != nullptr) delete compute;
}

DLLEXPORT int32_t command_VkCompute_record_upload(ncnn::VkCompute* compute,
                                                  ncnn::Mat* mat,
                                                  ncnn::VkMat* dst,
                                                  ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& m = *mat;
    auto& d = *dst;
    const auto& o = *opt;
    compute->record_upload(m, d, o);

    return error;
}

DLLEXPORT int32_t command_VkCompute_record_download(ncnn::VkCompute* compute,
                                                    ncnn::VkMat* mat,
                                                    ncnn::Mat* dst,
                                                    ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& m = *mat;
    auto& d = *dst;
    const auto& o = *opt;
    compute->record_download(m, d, o);

    return error;
}

DLLEXPORT int32_t command_VkCompute_submit_and_wait(ncnn::VkCompute* compute, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = compute->submit_and_wait();

    return error;
}

#endif

#endif