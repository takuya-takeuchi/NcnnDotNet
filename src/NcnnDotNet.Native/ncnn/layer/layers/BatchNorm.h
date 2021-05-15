#ifndef _CPP_LAYER_LAYERS_BATCHNORM_H_
#define _CPP_LAYER_LAYERS_BATCHNORM_H_

#include "../../export.h"
#include <layer.h>
#include <layer/batchnorm.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_BatchNorm_new(ncnn::BatchNorm** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::BatchNorm();

    return error;
}

DLLEXPORT void layer_layers_BatchNorm_delete(ncnn::BatchNorm* layer)
{
    if (layer != nullptr) delete layer;
}

#endif