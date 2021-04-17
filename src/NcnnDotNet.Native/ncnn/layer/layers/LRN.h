#ifndef _CPP_LAYER_LAYERS_LRN_H_
#define _CPP_LAYER_LAYERS_LRN_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/lrn.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_LRN_new(ncnn::LRN** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::LRN();

    return error;
}

DLLEXPORT void layer_layers_LRN_delete(ncnn::LRN* layer)
{
    if (layer != nullptr) delete layer;
}

#endif