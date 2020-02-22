#ifndef _CPP_COMMAND_VKCOMPUTE_H_
#define _CPP_COMMAND_VKCOMPUTE_H_

#include "../export.h"
#include <ncnn/command.h>
#include "../shared.h"

#if NCNN_VULKAN

DLLEXPORT int32_t command_Command_new(const ncnn::VulkanDevice* vkdev,
                                      const uint32_t queue_family_index,
                                      ncnn::Command** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Command(vkdev, queue_family_index);

    return error;
}

DLLEXPORT void command_Command_delete(ncnn::Command * command)
{
    if (command != nullptr) delete command;
}

#endif

#endif