#ifndef _CPP_LAYER_LAYERS_HARDSWISH_H_
#define _CPP_LAYER_LAYERS_HARDSWISH_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/hardswish.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_HardSwish_new(ncnn::HardSwish** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::HardSwish();

    return error;
}

DLLEXPORT void layer_layers_HardSwish_delete(ncnn::HardSwish* layer)
{
    if (layer != nullptr) delete layer;
}

#endif