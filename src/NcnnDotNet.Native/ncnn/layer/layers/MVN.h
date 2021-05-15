#ifndef _CPP_LAYER_LAYERS_MVN_H_
#define _CPP_LAYER_LAYERS_MVN_H_

#include "../../export.h"
#include <layer.h>
#include <layer/mvn.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_MVN_new(ncnn::MVN** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::MVN();

    return error;
}

DLLEXPORT void layer_layers_MVN_delete(ncnn::MVN* layer)
{
    if (layer != nullptr) delete layer;
}

#endif