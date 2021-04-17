#ifndef _CPP_LAYER_LAYERS_SELU_H_
#define _CPP_LAYER_LAYERS_SELU_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/selu.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_SELU_new(ncnn::SELU** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::SELU();

    return error;
}

DLLEXPORT void layer_layers_SELU_delete(ncnn::SELU* layer)
{
    if (layer != nullptr) delete layer;
}

#endif