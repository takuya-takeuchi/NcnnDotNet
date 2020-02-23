#ifndef _CPP_LAYER_LAYERS_ARGMAX_H_
#define _CPP_LAYER_LAYERS_ARGMAX_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/argmax.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ArgMax_new(ncnn::ArgMax** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ArgMax();

    return error;
}

DLLEXPORT void layer_layers_ArgMax_delete(ncnn::ArgMax* layer)
{
    if (layer != nullptr) delete layer;
}

#endif