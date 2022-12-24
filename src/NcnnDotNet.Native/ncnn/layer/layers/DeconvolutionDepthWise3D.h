#ifndef _CPP_LAYER_LAYERS_DECONVOLUTIONDEPTHWISE3D_H_
#define _CPP_LAYER_LAYERS_DECONVOLUTIONDEPTHWISE3D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deconvolutiondepthwise3d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_DeconvolutionDepthWise3D_new(ncnn::DeconvolutionDepthWise3D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::DeconvolutionDepthWise3D();

    return error;
}

DLLEXPORT void layer_layers_DeconvolutionDepthWise3D_delete(ncnn::DeconvolutionDepthWise3D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif