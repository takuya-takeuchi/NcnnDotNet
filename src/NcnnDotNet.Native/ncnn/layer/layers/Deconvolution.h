#ifndef _CPP_LAYER_LAYERS_DECONVOLUTION_H_
#define _CPP_LAYER_LAYERS_DECONVOLUTION_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deconvolution.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Deconvolution_new(ncnn::Deconvolution** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Deconvolution();

    return error;
}

DLLEXPORT void layer_layers_Deconvolution_delete(ncnn::Deconvolution* layer)
{
    if (layer != nullptr) delete layer;
}

#endif