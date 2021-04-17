#ifndef _CPP_LAYER_LAYERS_POWER_H_
#define _CPP_LAYER_LAYERS_POWER_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/power.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Power_new(ncnn::Power** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Power();

    return error;
}

DLLEXPORT void layer_layers_Power_delete(ncnn::Power* layer)
{
    if (layer != nullptr) delete layer;
}

#endif