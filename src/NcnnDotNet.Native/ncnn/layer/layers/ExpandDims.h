#ifndef _CPP_LAYER_LAYERS_EXPANDDIMS_H_
#define _CPP_LAYER_LAYERS_EXPANDDIMS_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/expanddims.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ExpandDims_new(ncnn::ExpandDims** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ExpandDims();

    return error;
}

DLLEXPORT void layer_layers_ExpandDims_delete(ncnn::ExpandDims* layer)
{
    if (layer != nullptr) delete layer;
}

#endif