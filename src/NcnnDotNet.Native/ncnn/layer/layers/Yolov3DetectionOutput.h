#ifndef _CPP_LAYER_LAYERS_YOLOV3DETECTIONOUTPUT_H_
#define _CPP_LAYER_LAYERS_YOLOV3DETECTIONOUTPUT_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/yolov3detectionoutput.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Yolov3DetectionOutput_new(ncnn::Yolov3DetectionOutput** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Yolov3DetectionOutput();

    return error;
}

DLLEXPORT void layer_layers_Yolov3DetectionOutput_delete(ncnn::Yolov3DetectionOutput* layer)
{
    if (layer != nullptr) delete layer;
}

#endif