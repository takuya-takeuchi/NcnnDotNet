#ifndef _CPP_LAYER_LAYERS_EINSUM_H_
#define _CPP_LAYER_LAYERS_EINSUM_H_

#include "../../export.h"
#include <layer.h>
#include <layer/einsum.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Einsum_new(ncnn::Einsum** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Einsum();

    return error;
}

DLLEXPORT void layer_layers_Einsum_delete(ncnn::Einsum* layer)
{
    if (layer != nullptr) delete layer;
}

#endif