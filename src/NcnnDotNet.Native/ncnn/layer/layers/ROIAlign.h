#ifndef _CPP_LAYER_LAYERS_ROIALIGN_H_
#define _CPP_LAYER_LAYERS_ROIALIGN_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/roialign.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ROIAlign_new(ncnn::ROIAlign** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ROIAlign();

    return error;
}

DLLEXPORT void layer_layers_ROIAlign_delete(ncnn::ROIAlign* layer)
{
    if (layer != nullptr) delete layer;
}

#endif