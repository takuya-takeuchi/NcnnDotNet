#ifndef _CPP_LAYER_LAYERS_BNLL_H_
#define _CPP_LAYER_LAYERS_BNLL_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/bnll.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_BNLL_new(ncnn::BNLL** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::BNLL();

    return error;
}

DLLEXPORT void layer_layers_BNLL_delete(ncnn::BNLL* layer)
{
    if (layer != nullptr) delete layer;
}

#endif