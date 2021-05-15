#ifndef _CPP_LAYER_LAYERS_REDUCTION_H_
#define _CPP_LAYER_LAYERS_REDUCTION_H_

#include "../../export.h"
#include <layer.h>
#include <layer/reduction.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Reduction_new(ncnn::Reduction** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Reduction();

    return error;
}

DLLEXPORT void layer_layers_Reduction_delete(ncnn::Reduction* layer)
{
    if (layer != nullptr) delete layer;
}

#endif