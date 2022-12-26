#ifndef _CPP_LAYER_LAYERS_UNFOLD_H_
#define _CPP_LAYER_LAYERS_UNFOLD_H_

#include "../../export.h"
#include <layer.h>
#include <layer/unfold.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Unfold_new(ncnn::Unfold** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Unfold();

    return error;
}

DLLEXPORT void layer_layers_Unfold_delete(ncnn::Unfold* layer)
{
    if (layer != nullptr) delete layer;
}

#endif