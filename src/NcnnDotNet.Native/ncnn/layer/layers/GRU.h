#ifndef _CPP_LAYER_LAYERS_GRU_H_
#define _CPP_LAYER_LAYERS_GRU_H_

#include "../../export.h"
#include <layer.h>
#include <layer/gru.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_GRU_new(ncnn::GRU** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::GRU();

    return error;
}

DLLEXPORT void layer_layers_GRU_delete(ncnn::GRU* layer)
{
    if (layer != nullptr) delete layer;
}

#endif