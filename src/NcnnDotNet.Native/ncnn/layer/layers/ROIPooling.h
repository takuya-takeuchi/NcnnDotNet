#ifndef _CPP_LAYER_LAYERS_ROIPOOLING_H_
#define _CPP_LAYER_LAYERS_ROIPOOLING_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/roipooling.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ROIPooling_new(ncnn::ROIPooling** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ROIPooling();

    return error;
}

DLLEXPORT void layer_layers_ROIPooling_delete(ncnn::ROIPooling* layer)
{
    if (layer != nullptr) delete layer;
}

#endif