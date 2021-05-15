#ifndef _CPP_LAYER_LAYERS_DECONVOLUTIONDEPTHWIDE_H_
#define _CPP_LAYER_LAYERS_DECONVOLUTIONDEPTHWIDE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deconvolutiondepthwise.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_DeconvolutionDepthWise_new(ncnn::DeconvolutionDepthWise** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::DeconvolutionDepthWise();

    return error;
}

DLLEXPORT void layer_layers_DeconvolutionDepthWise_delete(ncnn::DeconvolutionDepthWise* layer)
{
    if (layer != nullptr) delete layer;
}

#endif