#ifndef _CPP_LAYER_LAYERS_CONVOLUTION_H_
#define _CPP_LAYER_LAYERS_CONVOLUTION_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/convolution.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Convolution_new(ncnn::Convolution** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Convolution();

    return error;
}

DLLEXPORT void layer_layers_Convolution_delete(ncnn::Convolution* layer)
{
    if (layer != nullptr) delete layer;
}

#endif