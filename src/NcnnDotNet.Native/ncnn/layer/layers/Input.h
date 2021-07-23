#ifndef _CPP_LAYER_LAYERS_INPUT_H_
#define _CPP_LAYER_LAYERS_INPUT_H_

#include "../../export.h"
#include <layer.h>
#include <layer/input.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Input_new(ncnn::Input** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Input();

    return error;
}

DLLEXPORT void layer_layers_Input_delete(ncnn::Input* layer)
{
    if (layer != nullptr) delete layer;
}

#endif