#ifndef _CPP_COMMAND_VKTRANSFER_H_
#define _CPP_COMMAND_VKTRANSFER_H_

#include "../export.h"
#include <command.h>
#include "../shared.h"

#ifdef NCNN_VULKAN

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

#endif

#endif