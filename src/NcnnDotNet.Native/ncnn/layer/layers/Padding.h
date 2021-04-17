#ifndef _CPP_LAYER_LAYERS_PADDING_H_
#define _CPP_LAYER_LAYERS_PADDING_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/padding.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Padding_new(ncnn::Padding** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Padding();

    return error;
}

DLLEXPORT void layer_layers_Padding_delete(ncnn::Padding* layer)
{
    if (layer != nullptr) delete layer;
}

#endif