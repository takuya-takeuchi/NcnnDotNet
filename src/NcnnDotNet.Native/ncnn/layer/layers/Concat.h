#ifndef _CPP_LAYER_LAYERS_CONCAT_H_
#define _CPP_LAYER_LAYERS_CONCAT_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/concat.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Concat_new(ncnn::Concat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Concat();

    return error;
}

DLLEXPORT void layer_layers_Concat_delete(ncnn::Concat* layer)
{
    if (layer != nullptr) delete layer;
}

#endif