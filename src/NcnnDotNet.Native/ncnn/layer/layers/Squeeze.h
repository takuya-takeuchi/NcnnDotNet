#ifndef _CPP_LAYER_LAYERS_SQUEEZE_H_
#define _CPP_LAYER_LAYERS_SQUEEZE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/squeeze.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Squeeze_new(ncnn::Squeeze** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Squeeze();

    return error;
}

DLLEXPORT void layer_layers_Squeeze_delete(ncnn::Squeeze* layer)
{
    if (layer != nullptr) delete layer;
}

#endif