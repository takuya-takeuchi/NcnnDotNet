#ifndef _CPP_LAYER_LAYERS_SOFTPLUS_H_
#define _CPP_LAYER_LAYERS_SOFTPLUS_H_

#include "../../export.h"
#include <layer.h>
#include <layer/softplus.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Softplus_new(ncnn::Softplus** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Softplus();

    return error;
}

DLLEXPORT void layer_layers_Softplus_delete(ncnn::Softplus* layer)
{
    if (layer != nullptr) delete layer;
}

#endif