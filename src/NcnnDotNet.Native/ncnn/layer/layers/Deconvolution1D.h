#ifndef _CPP_LAYER_LAYERS_DECONVOLUTION1D_H_
#define _CPP_LAYER_LAYERS_DECONVOLUTION1D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deconvolution1d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Deconvolution1D_new(ncnn::Deconvolution1D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Deconvolution1D();

    return error;
}

DLLEXPORT void layer_layers_Deconvolution1D_delete(ncnn::Deconvolution1D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif