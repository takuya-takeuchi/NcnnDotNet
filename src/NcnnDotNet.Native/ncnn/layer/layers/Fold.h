#ifndef _CPP_LAYER_LAYERS_FOLD_H_
#define _CPP_LAYER_LAYERS_FOLD_H_

#include "../../export.h"
#include <layer.h>
#include <layer/fold.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Fold_new(ncnn::Fold** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Fold();

    return error;
}

DLLEXPORT void layer_layers_Fold_delete(ncnn::Fold* layer)
{
    if (layer != nullptr) delete layer;
}

#endif