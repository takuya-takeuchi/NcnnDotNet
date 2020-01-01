#ifndef _CPP_LAYER_LAYER_H_
#define _CPP_LAYER_LAYER_H_

#include "../export.h"
#include <ncnn/layer.h>
#include "../shared.h"

DLLEXPORT void layer_Layer_delete(ncnn::Layer* layer)
{
    if (layer != nullptr) delete layer;
}

DLLEXPORT int32_t layer_Layer_load_param(ncnn::Layer* layer, ncnn::ParamDict* pd, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    const auto& p = *pd;
    *returnValue = layer->load_param(p);

    return error;
}

DLLEXPORT int32_t layer_Layer_forward_inplace(ncnn::Layer* layer, ncnn::Mat* mat, ncnn::Option* opt, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    auto& m = *mat;
    const auto& o = *opt;
    *returnValue = layer->forward_inplace(m, o);

    return error;
}

DLLEXPORT int32_t layer_create_layer(const char* type, const int32_t type_len, ncnn::Layer** returnValue)
{
    int32_t error = ERR_OK;

    std::string t(type, type_len);
    *returnValue = ncnn::create_layer(t.c_str());

    return error;
}

#endif