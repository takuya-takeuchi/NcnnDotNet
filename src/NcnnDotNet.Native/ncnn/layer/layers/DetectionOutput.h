#ifndef _CPP_LAYER_LAYERS_DETECTIONOUTPUT_H_
#define _CPP_LAYER_LAYERS_DETECTIONOUTPUT_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/detectionoutput.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_DetectionOutput_new(ncnn::DetectionOutput** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::DetectionOutput();

    return error;
}

DLLEXPORT void layer_layers_DetectionOutput_delete(ncnn::DetectionOutput* layer)
{
    if (layer != nullptr) delete layer;
}

#endif