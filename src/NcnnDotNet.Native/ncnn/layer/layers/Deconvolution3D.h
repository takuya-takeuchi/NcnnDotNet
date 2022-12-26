#ifndef _CPP_LAYER_LAYERS_DECONVOLUTION3D_H_
#define _CPP_LAYER_LAYERS_DECONVOLUTION3D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deconvolution3d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Deconvolution3D_new(ncnn::Deconvolution3D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Deconvolution3D();

    return error;
}

DLLEXPORT void layer_layers_Deconvolution3D_delete(ncnn::Deconvolution3D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif