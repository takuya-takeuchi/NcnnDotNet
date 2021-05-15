#ifndef _CPP_LAYER_LAYERS_REQUANTIZE_H_
#define _CPP_LAYER_LAYERS_REQUANTIZE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/requantize.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Requantize_new(ncnn::Requantize** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Requantize();

    return error;
}

DLLEXPORT void layer_layers_Requantize_delete(ncnn::Requantize* layer)
{
    if (layer != nullptr) delete layer;
}

#endif