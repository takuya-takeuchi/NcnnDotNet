#ifndef _CPP_OPTION_OPTION_H_
#define _CPP_OPTION_OPTION_H_

#include "../export.h"
#include <ncnn/option.h>
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

DLLEXPORT void option_Option_get_use_vulkan_compute(ncnn::Option* option, bool* returnValue)
{
    *returnValue = option->use_vulkan_compute;
}

DLLEXPORT void option_Option_set_use_vulkan_compute(ncnn::Option* option, bool value)
{
    option->use_vulkan_compute = value;
}

#endif