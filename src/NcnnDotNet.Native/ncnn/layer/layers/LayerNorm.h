#ifndef _CPP_LAYER_LAYERNORM_LOG_H_
#define _CPP_LAYER_LAYERNORM_LOG_H_

#include "../../export.h"
#include <layer.h>
#include <layer/layernorm.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_LayerNorm_new(ncnn::LayerNorm** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::LayerNorm();

    return error;
}

DLLEXPORT void layer_layers_LayerNorm_delete(ncnn::LayerNorm* layer)
{
    if (layer != nullptr) delete layer;
}

#endif