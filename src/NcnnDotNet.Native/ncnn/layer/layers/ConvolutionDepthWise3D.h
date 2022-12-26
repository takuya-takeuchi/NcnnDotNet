#ifndef _CPP_LAYER_LAYERS_CONVOLUTIONDEPTHWISE3D_H_
#define _CPP_LAYER_LAYERS_CONVOLUTIONDEPTHWISE3D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/convolutiondepthwise3d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ConvolutionDepthWise3D_new(ncnn::ConvolutionDepthWise3D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ConvolutionDepthWise3D();

    return error;
}

DLLEXPORT void layer_layers_ConvolutionDepthWise3D_delete(ncnn::ConvolutionDepthWise3D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif