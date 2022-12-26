#ifndef _CPP_LAYER_LAYERS_CONVOLUTION1D_H_
#define _CPP_LAYER_LAYERS_CONVOLUTION1D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/convolution1d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Convolution1D_new(ncnn::Convolution1D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Convolution1D();

    return error;
}

DLLEXPORT void layer_layers_Convolution1D_delete(ncnn::Convolution1D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif