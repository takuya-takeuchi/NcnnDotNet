#ifndef _CPP_LAYER_LAYERS_PIXELSHUFFLE_H_
#define _CPP_LAYER_LAYERS_PIXELSHUFFLE_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/pixelshuffle.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_PixelShuffle_new(ncnn::PixelShuffle** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::PixelShuffle();

    return error;
}

DLLEXPORT void layer_layers_PixelShuffle_delete(ncnn::PixelShuffle* layer)
{
    if (layer != nullptr) delete layer;
}

#endif