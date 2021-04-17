#ifndef _CPP_LAYER_LAYERS_INTERP_H_
#define _CPP_LAYER_LAYERS_INTERP_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/interp.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Interp_new(ncnn::Interp** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Interp();

    return error;
}

DLLEXPORT void layer_layers_Interp_delete(ncnn::Interp* layer)
{
    if (layer != nullptr) delete layer;
}

#endif