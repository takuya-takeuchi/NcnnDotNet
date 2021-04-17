#ifndef _CPP_LAYER_LAYERS_RELU_H_
#define _CPP_LAYER_LAYERS_RELU_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/relu.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ReLU_new(ncnn::ReLU** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ReLU();

    return error;
}

DLLEXPORT void layer_layers_ReLU_delete(ncnn::ReLU* layer)
{
    if (layer != nullptr) delete layer;
}

#endif