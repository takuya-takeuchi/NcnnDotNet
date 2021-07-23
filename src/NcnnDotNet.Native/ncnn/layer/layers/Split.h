#ifndef _CPP_LAYER_LAYERS_SPLIT_H_
#define _CPP_LAYER_LAYERS_SPLIT_H_

#include "../../export.h"
#include <layer.h>
#include <layer/split.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Split_new(ncnn::Split** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Split();

    return error;
}

DLLEXPORT void layer_layers_Split_delete(ncnn::Split* layer)
{
    if (layer != nullptr) delete layer;
}

#endif