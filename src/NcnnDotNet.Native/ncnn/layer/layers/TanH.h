#ifndef _CPP_LAYER_LAYERS_TANH_H_
#define _CPP_LAYER_LAYERS_TANH_H_

#include "../../export.h"
#include <layer.h>
#include <layer/tanh.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_TanH_new(ncnn::TanH** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::TanH();

    return error;
}

DLLEXPORT void layer_layers_TanH_delete(ncnn::TanH* layer)
{
    if (layer != nullptr) delete layer;
}

#endif