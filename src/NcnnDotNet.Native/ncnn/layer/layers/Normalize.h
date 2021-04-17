#ifndef _CPP_LAYER_LAYERS_NORMALIZE_H_
#define _CPP_LAYER_LAYERS_NORMALIZE_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/normalize.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Normalize_new(ncnn::Normalize** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Normalize();

    return error;
}

DLLEXPORT void layer_layers_Normalize_delete(ncnn::Normalize* layer)
{
    if (layer != nullptr) delete layer;
}

#endif