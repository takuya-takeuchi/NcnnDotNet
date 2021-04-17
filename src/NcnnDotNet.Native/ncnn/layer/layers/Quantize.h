#ifndef _CPP_LAYER_LAYERS_QUANTIZE_H_
#define _CPP_LAYER_LAYERS_QUANTIZE_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/quantize.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Quantize_new(ncnn::Quantize** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Quantize();

    return error;
}

DLLEXPORT void layer_layers_Quantize_delete(ncnn::Quantize* layer)
{
    if (layer != nullptr) delete layer;
}

#endif