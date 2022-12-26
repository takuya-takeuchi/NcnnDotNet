#ifndef _CPP_LAYER_LAYERS_GRIDSAMPLE_H_
#define _CPP_LAYER_LAYERS_GRIDSAMPLE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/gridsample.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_GridSample_new(ncnn::GridSample** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::GridSample();

    return error;
}

DLLEXPORT void layer_layers_GridSample_delete(ncnn::GridSample* layer)
{
    if (layer != nullptr) delete layer;
}

#endif