#ifndef _CPP_LAYER_LAYERS_INSTANCENORM_H_
#define _CPP_LAYER_LAYERS_INSTANCENORM_H_

#include "../../export.h"
#include <layer.h>
#include <layer/instancenorm.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_InstanceNorm_new(ncnn::InstanceNorm** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::InstanceNorm();

    return error;
}

DLLEXPORT void layer_layers_InstanceNorm_delete(ncnn::InstanceNorm* layer)
{
    if (layer != nullptr) delete layer;
}

#endif