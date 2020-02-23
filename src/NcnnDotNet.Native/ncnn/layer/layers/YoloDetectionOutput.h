#ifndef _CPP_LAYER_LAYERS_YOLODETECTIONOUTPUT_H_
#define _CPP_LAYER_LAYERS_YOLODETECTIONOUTPUT_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/yolodetectionoutput.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_YoloDetectionOutput_new(ncnn::YoloDetectionOutput** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::YoloDetectionOutput();

    return error;
}

DLLEXPORT void layer_layers_YoloDetectionOutput_delete(ncnn::YoloDetectionOutput* layer)
{
    if (layer != nullptr) delete layer;
}

#endif