#ifndef _CPP_LAYER_LAYERS_CONVOLUTION3D_H_
#define _CPP_LAYER_LAYERS_CONVOLUTION3D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/convolution3d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Convolution3D_new(ncnn::Convolution3D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Convolution3D();

    return error;
}

DLLEXPORT void layer_layers_Convolution3D_delete(ncnn::Convolution3D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif