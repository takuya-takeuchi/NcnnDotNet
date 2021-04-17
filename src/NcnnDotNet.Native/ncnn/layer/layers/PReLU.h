#ifndef _CPP_LAYER_LAYERS_PRELU_H_
#define _CPP_LAYER_LAYERS_PRELU_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/prelu.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_PReLU_new(ncnn::PReLU** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::PReLU();

    return error;
}

DLLEXPORT void layer_layers_PReLU_delete(ncnn::PReLU* layer)
{
    if (layer != nullptr) delete layer;
}

#endif