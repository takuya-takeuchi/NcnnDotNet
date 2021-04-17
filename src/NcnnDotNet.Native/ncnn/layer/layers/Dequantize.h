#ifndef _CPP_LAYER_LAYERS_DEQUANTIZE_H_
#define _CPP_LAYER_LAYERS_DEQUANTIZE_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/dequantize.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Dequantize_new(ncnn::Dequantize** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Dequantize();

    return error;
}

DLLEXPORT void layer_layers_Dequantize_delete(ncnn::Dequantize* layer)
{
    if (layer != nullptr) delete layer;
}

#endif