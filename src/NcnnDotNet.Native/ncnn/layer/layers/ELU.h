#ifndef _CPP_LAYER_LAYERS_ELU_H_
#define _CPP_LAYER_LAYERS_ELU_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/elu.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ELU_new(ncnn::ELU** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ELU();

    return error;
}

DLLEXPORT void layer_layers_ELU_delete(ncnn::ELU* layer)
{
    if (layer != nullptr) delete layer;
}

#endif