#ifndef _CPP_LAYER_LAYERS_HARDSIGMOID_H_
#define _CPP_LAYER_LAYERS_HARDSIGMOID_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/hardsigmoid.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_HardSigmoid_new(ncnn::HardSigmoid** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::HardSigmoid();

    return error;
}

DLLEXPORT void layer_layers_HardSigmoid_delete(ncnn::HardSigmoid* layer)
{
    if (layer != nullptr) delete layer;
}

#endif