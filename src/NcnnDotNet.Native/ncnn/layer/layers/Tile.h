#ifndef _CPP_LAYER_LAYERS_TILE_H_
#define _CPP_LAYER_LAYERS_TILE_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/tile.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Tile_new(ncnn::Tile** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Tile();

    return error;
}

DLLEXPORT void layer_layers_Tile_delete(ncnn::Tile* layer)
{
    if (layer != nullptr) delete layer;
}

#endif