#ifndef _CPP_NET_NET_H_
#define _CPP_NET_NET_H_

#include "../export.h"
#include <ncnn/net.h>
#include "../shared.h"

DLLEXPORT int net_Net_new(ncnn::Net** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Net();

    return error;
}

DLLEXPORT void net_Net_delete(ncnn::Net* net)
{
    if (net != nullptr) delete net;
}

#if NCNN_VULKAN

DLLEXPORT int net_Net_set_vulkan_device(ncnn::Net* net, const int32_t device_index)
{
    int32_t error = ERR_OK;

    net->set_vulkan_device(device_index);

    return error;
}

DLLEXPORT int net_Net_set_vulkan_device2(ncnn::Net* net, const ncnn::VulkanDevice* vkdev)
{
    int32_t error = ERR_OK;

    net->set_vulkan_device(vkdev);

    return error;
}

DLLEXPORT int net_Net_get_vulkan_device(ncnn::Net* net, const ncnn::VulkanDevice** returnValue)
{
    int32_t error = ERR_OK;

    auto ret = net->vulkan_device();
    *returnValue = ret;

    return error;
}

#endif

DLLEXPORT int net_Net_create_extractor(ncnn::Net* net, ncnn::Extractor** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = net->create_extractor();
    *returnValue = new ncnn::Extractor(ret);

    return error;
}

#pragma region load_param

DLLEXPORT int net_Net_load_param_filepath(ncnn::Net* net, const char* protopath, const int32_t protopath_len)
{
    int32_t error = ERR_OK;

    std::string path(protopath, protopath_len);
    const auto ret = net->load_param(path.c_str());
    if (ret != 0)
        return ERR_GENERAL_ERROR;

    return error;
}

DLLEXPORT int net_Net_load_model_filepath(ncnn::Net* net, const char* modelpath, const int32_t modelpath_len)
{
    int32_t error = ERR_OK;

    std::string path(modelpath, modelpath_len);
    const auto ret = net->load_model(path.c_str());
    if (ret != 0)
        return ERR_GENERAL_ERROR;

    return error;
}

DLLEXPORT int net_Net_load_model_datareader(ncnn::Net* net, ncnn::DataReader* reader)
{
    int32_t error = ERR_OK;

    const auto& dr = *reader;
    const auto ret = net->load_model(dr);
    if (ret != 0)
        return ERR_GENERAL_ERROR;

    return error;
}

#pragma endregion load_param

DLLEXPORT void net_Net_get_opt(ncnn::Net* net, ncnn::Option** returnValue)
{
    *returnValue = &(net->opt);
}

DLLEXPORT int32_t net_Net_set_opt(ncnn::Net* net, ncnn::Option* option)
{
    int32_t error = ERR_OK;

    const auto& o = *option;
    net->opt = o;

    return error;
}

#endif