#ifndef _CPP_LAYER_LAYERS_PACKING_H_
#define _CPP_LAYER_LAYERS_PACKING_H_

#include "../../export.h"
#include <layer.h>
#include <layer/packing.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Packing_new(ncnn::Packing** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Packing();

    return error;
}

DLLEXPORT void layer_layers_Packing_delete(ncnn::Packing* layer)
{
    if (layer != nullptr) delete layer;
}

#endif