#ifndef _CPP_LAYER_LAYERS_NOOP_H_
#define _CPP_LAYER_LAYERS_NOOP_H_

#include "../../export.h"
#include <layer.h>
#include <layer/noop.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Noop_new(ncnn::Noop** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Noop();

    return error;
}

DLLEXPORT void layer_layers_Noop_delete(ncnn::Noop* layer)
{
    if (layer != nullptr) delete layer;
}

#endif