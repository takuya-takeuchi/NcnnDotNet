#ifndef _CPP_LAYER_LAYERS_BINARYOP_H_
#define _CPP_LAYER_LAYERS_BINARYOP_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/binaryop.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_BinaryOp_new(ncnn::BinaryOp** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::BinaryOp();

    return error;
}

DLLEXPORT void layer_layers_BinaryOp_delete(ncnn::BinaryOp* layer)
{
    if (layer != nullptr) delete layer;
}

#endif