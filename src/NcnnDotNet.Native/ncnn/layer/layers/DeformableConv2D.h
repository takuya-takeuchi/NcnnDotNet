#ifndef _CPP_LAYER_LAYERS_DEFORMABLECONV2D_H_
#define _CPP_LAYER_LAYERS_DEFORMABLECONV2D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deformableconv2d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_DeformableConv2D_new(ncnn::DeformableConv2D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::DeformableConv2D();

    return error;
}

DLLEXPORT void layer_layers_DeformableConv2D_delete(ncnn::DeformableConv2D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif