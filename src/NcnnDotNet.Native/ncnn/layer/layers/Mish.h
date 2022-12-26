#ifndef _CPP_LAYER_LAYERS_MISH_H_
#define _CPP_LAYER_LAYERS_MISH_H_

#include "../../export.h"
#include <layer.h>
#include <layer/mish.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Mish_new(ncnn::Mish** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mish();

    return error;
}

DLLEXPORT void layer_layers_Mish_delete(ncnn::Mish* layer)
{
    if (layer != nullptr) delete layer;
}

#endif