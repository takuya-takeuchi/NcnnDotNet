#ifndef _CPP_LAYER_LAYERS_PERMUTE_H_
#define _CPP_LAYER_LAYERS_PERMUTE_H_

#include "../../export.h"
#include <layer.h>
#include <layer/permute.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Permute_new(ncnn::Permute** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Permute();

    return error;
}

DLLEXPORT void layer_layers_Permute_delete(ncnn::Permute* layer)
{
    if (layer != nullptr) delete layer;
}

#endif