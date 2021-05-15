#ifndef _CPP_LAYER_LAYERS_CONVOLUTIONDEPTHWIDE_H_
#define _CPP_LAYER_LAYERS_CONVOLUTIONDEPTHWIDE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/convolutiondepthwise.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ConvolutionDepthWise_new(ncnn::ConvolutionDepthWise** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ConvolutionDepthWise();

    return error;
}

DLLEXPORT void layer_layers_ConvolutionDepthWise_delete(ncnn::ConvolutionDepthWise* layer)
{
    if (layer != nullptr) delete layer;
}

#endif