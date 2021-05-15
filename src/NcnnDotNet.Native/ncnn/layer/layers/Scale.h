#ifndef _CPP_LAYER_LAYERS_SCALE_H_
#define _CPP_LAYER_LAYERS_SCALE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/scale.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Scale_new(ncnn::Scale** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Scale();

    return error;
}

DLLEXPORT void layer_layers_Scale_delete(ncnn::Scale* layer)
{
    if (layer != nullptr) delete layer;
}

#endif