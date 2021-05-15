#ifndef _CPP_LAYER_LAYERS_POOLING_H_
#define _CPP_LAYER_LAYERS_POOLING_H_

#include "../../export.h"
#include <layer.h>
#include <layer/pooling.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Pooling_new(ncnn::Pooling** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Pooling();

    return error;
}

DLLEXPORT void layer_layers_Pooling_delete(ncnn::Pooling* layer)
{
    if (layer != nullptr) delete layer;
}

#endif