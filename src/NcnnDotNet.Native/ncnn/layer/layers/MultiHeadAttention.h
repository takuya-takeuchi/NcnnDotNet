#ifndef _CPP_LAYER_LAYERS_MULTIHEADATTENTION_H_
#define _CPP_LAYER_LAYERS_MULTIHEADATTENTION_H_

#include "../../export.h"
#include <layer.h>
#include <layer/multiheadattention.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_MultiHeadAttention_new(ncnn::MultiHeadAttention** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::MultiHeadAttention();

    return error;
}

DLLEXPORT void layer_layers_MultiHeadAttention_delete(ncnn::MultiHeadAttention* layer)
{
    if (layer != nullptr) delete layer;
}

#endif