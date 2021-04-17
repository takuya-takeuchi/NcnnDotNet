#ifndef _CPP_LAYER_LAYERS_REORG_H_
#define _CPP_LAYER_LAYERS_REORG_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/reorg.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Reorg_new(ncnn::Reorg** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Reorg();

    return error;
}

DLLEXPORT void layer_layers_Reorg_delete(ncnn::Reorg* layer)
{
    if (layer != nullptr) delete layer;
}

#endif