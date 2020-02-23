#ifndef _CPP_LAYER_LAYERS_UNARYOP_H_
#define _CPP_LAYER_LAYERS_UNARYOP_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/unaryop.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_UnaryOp_new(ncnn::UnaryOp** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::UnaryOp();

    return error;
}

DLLEXPORT void layer_layers_UnaryOp_delete(ncnn::UnaryOp* layer)
{
    if (layer != nullptr) delete layer;
}

#endif