#ifndef _CPP_COMMAND_VKTRANSFER_H_
#define _CPP_COMMAND_VKTRANSFER_H_

#include "../export.h"
#include <ncnn/command.h>
#include "../shared.h"

#if NCNN_VULKAN

DLLEXPORT int32_t command_VkTransfer_new(const ncnn::VulkanDevice* vkdev, ncnn::VkTransfer** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::VkTransfer(vkdev);

    return error;
}

DLLEXPORT void command_VkTransfer_delete(ncnn::VkTransfer * transfer)
{
    if (transfer != nullptr) delete transfer;
}

DLLEXPORT int32_t command_VkTransfer_submit_and_wait(ncnn::VkTransfer* transfer, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = transfer->submit_and_wait();

    return error;
}

DLLEXPORT void command_VkTransfer_get_weight_vkallocator(ncnn::VkTransfer* transfer, const ncnn::VkAllocator** returnValue)
{
    *returnValue = transfer->weight_vkallocator;
}

DLLEXPORT void command_VkTransfer_set_weight_vkallocator(ncnn::VkTransfer* transfer, ncnn::VkAllocator* value)
{
    transfer->weight_vkallocator = value;
}

DLLEXPORT void command_VkTransfer_get_staging_vkallocator(ncnn::VkTransfer* transfer, const ncnn::VkAllocator** returnValue)
{
    *returnValue = transfer->staging_vkallocator;
}

DLLEXPORT void command_VkTransfer_set_staging_vkallocator(ncnn::VkTransfer* transfer, ncnn::VkAllocator* value)
{
    transfer->staging_vkallocator = value;
}

#endif

#endif