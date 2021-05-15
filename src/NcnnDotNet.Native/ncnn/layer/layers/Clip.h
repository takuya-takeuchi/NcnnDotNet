#ifndef _CPP_LAYER_LAYERS_CLIP_H_
#define _CPP_LAYER_LAYERS_CLIP_H_

#include "../../export.h"
#include <layer.h>
#include <layer/clip.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Clip_new(ncnn::Clip** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Clip();

    return error;
}

DLLEXPORT void layer_layers_Clip_delete(ncnn::Clip* layer)
{
    if (layer != nullptr) delete layer;
}

#endif