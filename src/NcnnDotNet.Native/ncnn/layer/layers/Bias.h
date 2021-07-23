#ifndef _CPP_LAYER_LAYERS_BIAS_H_
#define _CPP_LAYER_LAYERS_BIAS_H_

#include "../../export.h"
#include <layer.h>
#include <layer/bias.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Bias_new(ncnn::Bias** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Bias();

    return error;
}

DLLEXPORT void layer_layers_Bias_delete(ncnn::Bias* layer)
{
    if (layer != nullptr) delete layer;
}

#endif