#ifndef _CPP_LAYER_LAYERS_RESHAPE_H_
#define _CPP_LAYER_LAYERS_RESHAPE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/reshape.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Reshape_new(ncnn::Reshape** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Reshape();

    return error;
}

DLLEXPORT void layer_layers_Reshape_delete(ncnn::Reshape* layer)
{
    if (layer != nullptr) delete layer;
}

#endif