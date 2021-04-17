#ifndef _CPP_LAYER_LAYERS_SPP_H_
#define _CPP_LAYER_LAYERS_SPP_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/spp.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_SPP_new(ncnn::SPP** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::SPP();

    return error;
}

DLLEXPORT void layer_layers_SPP_delete(ncnn::SPP* layer)
{
    if (layer != nullptr) delete layer;
}

#endif