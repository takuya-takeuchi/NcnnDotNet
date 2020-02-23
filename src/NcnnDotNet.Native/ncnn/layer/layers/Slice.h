#ifndef _CPP_LAYER_LAYERS_SLICE_H_
#define _CPP_LAYER_LAYERS_SLICE_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/slice.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Slice_new(ncnn::Slice** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Slice();

    return error;
}

DLLEXPORT void layer_layers_Slice_delete(ncnn::Slice* layer)
{
    if (layer != nullptr) delete layer;
}

#endif