#ifndef _CPP_LAYER_LAYERS_SIGMOID_H_
#define _CPP_LAYER_LAYERS_SIGMOID_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/sigmoid.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Sigmoid_new(ncnn::Sigmoid** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Sigmoid();

    return error;
}

DLLEXPORT void layer_layers_Sigmoid_delete(ncnn::Sigmoid* layer)
{
    if (layer != nullptr) delete layer;
}

#endif