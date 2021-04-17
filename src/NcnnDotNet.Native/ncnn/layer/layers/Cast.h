#ifndef _CPP_LAYER_LAYERS_CAST_H_
#define _CPP_LAYER_LAYERS_CAST_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/cast.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Cast_new(ncnn::Cast** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Cast();

    return error;
}

DLLEXPORT void layer_layers_Cast_delete(ncnn::Cast* layer)
{
    if (layer != nullptr) delete layer;
}

#endif