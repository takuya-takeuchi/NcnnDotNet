#ifndef _CPP_LAYER_LAYERS_GROUPNORM_H_
#define _CPP_LAYER_LAYERS_GROUPNORM_H_

#include "../../export.h"
#include <layer.h>
#include <layer/groupnorm.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_GroupNorm_new(ncnn::GroupNorm** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::GroupNorm();

    return error;
}

DLLEXPORT void layer_layers_GroupNorm_delete(ncnn::GroupNorm* layer)
{
    if (layer != nullptr) delete layer;
}

#endif