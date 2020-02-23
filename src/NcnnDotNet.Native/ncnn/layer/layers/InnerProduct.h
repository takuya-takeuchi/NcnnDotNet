#ifndef _CPP_LAYER_LAYERS_INNERPRODUCT_H_
#define _CPP_LAYER_LAYERS_INNERPRODUCT_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/innerproduct.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_InnerProduct_new(ncnn::InnerProduct** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::InnerProduct();

    return error;
}

DLLEXPORT void layer_layers_InnerProduct_delete(ncnn::InnerProduct* layer)
{
    if (layer != nullptr) delete layer;
}

#endif