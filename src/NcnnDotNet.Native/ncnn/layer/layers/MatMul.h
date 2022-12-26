#ifndef _CPP_LAYER_LAYERS_MATMUL_H_
#define _CPP_LAYER_LAYERS_MATMUL_H_

#include "../../export.h"
#include <layer.h>
#include <layer/matmul.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_MatMul_new(ncnn::MatMul** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::MatMul();

    return error;
}

DLLEXPORT void layer_layers_MatMul_delete(ncnn::MatMul* layer)
{
    if (layer != nullptr) delete layer;
}

#endif