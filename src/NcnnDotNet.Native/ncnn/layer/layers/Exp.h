#ifndef _CPP_LAYER_LAYERS_EXP_H_
#define _CPP_LAYER_LAYERS_EXP_H_

#include "../../export.h"
#include <layer.h>
#include <layer/exp.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Exp_new(ncnn::Exp** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Exp();

    return error;
}

DLLEXPORT void layer_layers_Exp_delete(ncnn::Exp* layer)
{
    if (layer != nullptr) delete layer;
}

#endif