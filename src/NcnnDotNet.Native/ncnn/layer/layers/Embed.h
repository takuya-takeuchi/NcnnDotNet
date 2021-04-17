#ifndef _CPP_LAYER_LAYERS_EMBED_H_
#define _CPP_LAYER_LAYERS_EMBED_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/embed.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Embed_new(ncnn::Embed** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Embed();

    return error;
}

DLLEXPORT void layer_layers_Embed_delete(ncnn::Embed* layer)
{
    if (layer != nullptr) delete layer;
}

#endif