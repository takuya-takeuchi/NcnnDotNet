#ifndef _CPP_LAYER_LAYERS_POOLING1D_H_
#define _CPP_LAYER_LAYERS_POOLING1D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/pooling.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Pooling1D_new(ncnn::Pooling1D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Pooling1D();

    return error;
}

DLLEXPORT void layer_layers_Pooling1D_delete(ncnn::Pooling1D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif