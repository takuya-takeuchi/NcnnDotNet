#ifndef _CPP_LAYER_LAYERS_THRESHOLD_H_
#define _CPP_LAYER_LAYERS_THRESHOLD_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/threshold.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Threshold_new(ncnn::Threshold** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Threshold();

    return error;
}

DLLEXPORT void layer_layers_Threshold_delete(ncnn::Threshold* layer)
{
    if (layer != nullptr) delete layer;
}

#endif