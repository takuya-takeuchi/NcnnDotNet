#ifndef _CPP_LAYER_LAYERS_DECONVOLUTIONDEPTHWISE1D_H_
#define _CPP_LAYER_LAYERS_DECONVOLUTIONDEPTHWISE1D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deconvolutiondepthwise1d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_DeconvolutionDepthWise1D_new(ncnn::DeconvolutionDepthWise1D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::DeconvolutionDepthWise1D();

    return error;
}

DLLEXPORT void layer_layers_DeconvolutionDepthWise1D_delete(ncnn::DeconvolutionDepthWise1D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif