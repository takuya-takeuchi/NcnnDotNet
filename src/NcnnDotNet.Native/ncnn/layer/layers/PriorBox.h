#ifndef _CPP_LAYER_LAYERS_PRIORBOX_H_
#define _CPP_LAYER_LAYERS_PRIORBOX_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/priorbox.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_PriorBox_new(ncnn::PriorBox** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::PriorBox();

    return error;
}

DLLEXPORT void layer_layers_PriorBox_delete(ncnn::PriorBox* layer)
{
    if (layer != nullptr) delete layer;
}

#endif