#ifndef _CPP_LAYER_LAYERS_GELU_H_
#define _CPP_LAYER_LAYERS_GELU_H_

#include "../../export.h"
#include <layer.h>
#include <layer/gelu.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_GELU_new(ncnn::GELU** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::GELU();

    return error;
}

DLLEXPORT void layer_layers_GELU_delete(ncnn::GELU* layer)
{
    if (layer != nullptr) delete layer;
}

#endif