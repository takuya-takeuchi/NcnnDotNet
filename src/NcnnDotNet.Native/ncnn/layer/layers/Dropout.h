#ifndef _CPP_LAYER_LAYERS_DROPOUT_H_
#define _CPP_LAYER_LAYERS_DROPOUT_H_

#include "../../export.h"
#include <layer.h>
#include <layer/dropout.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Dropout_new(ncnn::Dropout** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Dropout();

    return error;
}

DLLEXPORT void layer_layers_Dropout_delete(ncnn::Dropout* layer)
{
    if (layer != nullptr) delete layer;
}

#endif