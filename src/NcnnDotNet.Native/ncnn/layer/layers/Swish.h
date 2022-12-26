#ifndef _CPP_LAYER_LAYERS_SWISH_H_
#define _CPP_LAYER_LAYERS_SWISH_H_

#include "../../export.h"
#include <layer.h>
#include <layer/swish.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Swish_new(ncnn::Swish** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Swish();

    return error;
}

DLLEXPORT void layer_layers_Swish_delete(ncnn::Swish* layer)
{
    if (layer != nullptr) delete layer;
}

#endif