#ifndef _CPP_LAYER_LAYERS_GEMM_H_
#define _CPP_LAYER_LAYERS_GEMM_H_

#include "../../export.h"
#include <layer.h>
#include <layer/gemm.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Gemm_new(ncnn::Gemm** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Gemm();

    return error;
}

DLLEXPORT void layer_layers_Gemm_delete(ncnn::Gemm* layer)
{
    if (layer != nullptr) delete layer;
}

#endif