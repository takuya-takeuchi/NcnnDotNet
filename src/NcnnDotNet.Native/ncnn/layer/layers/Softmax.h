#ifndef _CPP_LAYER_LAYERS_SOFTMAX_H_
#define _CPP_LAYER_LAYERS_SOFTMAX_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/softmax.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Softmax_new(ncnn::Softmax** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Softmax();

    return error;
}

DLLEXPORT void layer_layers_Softmax_delete(ncnn::Softmax* layer)
{
    if (layer != nullptr) delete layer;
}

#endif