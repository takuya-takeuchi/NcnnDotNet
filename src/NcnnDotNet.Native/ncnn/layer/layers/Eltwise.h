#ifndef _CPP_LAYER_LAYERS_ELTWISE_H_
#define _CPP_LAYER_LAYERS_ELTWISE_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/eltwise.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Eltwise_new(ncnn::Eltwise** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Eltwise();

    return error;
}

DLLEXPORT void layer_layers_Eltwise_delete(ncnn::Eltwise* layer)
{
    if (layer != nullptr) delete layer;
}

#endif