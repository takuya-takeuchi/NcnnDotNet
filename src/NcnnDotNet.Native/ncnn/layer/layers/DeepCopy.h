#ifndef _CPP_LAYER_LAYERS_DEEPCOPY_H_
#define _CPP_LAYER_LAYERS_DEEPCOPY_H_

#include "../../export.h"
#include <layer.h>
#include <layer/deepcopy.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_DeepCopy_new(ncnn::DeepCopy** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::DeepCopy();

    return error;
}

DLLEXPORT void layer_layers_DeepCopy_delete(ncnn::DeepCopy* layer)
{
    if (layer != nullptr) delete layer;
}

#endif