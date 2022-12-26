#ifndef _CPP_LAYER_LAYERS_CONVOLUTIONDEPTHWISE1D_H_
#define _CPP_LAYER_LAYERS_CONVOLUTIONDEPTHWISE1D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/convolutiondepthwise1d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ConvolutionDepthWise1D_new(ncnn::ConvolutionDepthWise1D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ConvolutionDepthWise1D();

    return error;
}

DLLEXPORT void layer_layers_ConvolutionDepthWise1D_delete(ncnn::ConvolutionDepthWise1D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif