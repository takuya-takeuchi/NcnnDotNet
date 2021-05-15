#ifndef _CPP_LAYER_LAYERS_FLATTEN_H_
#define _CPP_LAYER_LAYERS_FLATTEN_H_

#include "../../export.h"
#include <layer.h>
#include <layer/flatten.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Flatten_new(ncnn::Flatten** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Flatten();

    return error;
}

DLLEXPORT void layer_layers_Flatten_delete(ncnn::Flatten* layer)
{
    if (layer != nullptr) delete layer;
}

#endif