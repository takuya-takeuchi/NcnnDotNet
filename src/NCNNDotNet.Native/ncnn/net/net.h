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

DLLEXPORT int net_Net_create_extractor(ncnn::Net* net, ncnn::Extractor** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = net->create_extractor();
    *returnValue = new ncnn::Extractor(ret);

    return error;
}

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

DLLEXPORT void net_Net_get_opt(ncnn::Net* net, ncnn::Option** returnValue)
{
    *returnValue = &(net->opt);
}

#endif