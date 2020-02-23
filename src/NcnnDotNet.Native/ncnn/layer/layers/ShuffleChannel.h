#ifndef _CPP_LAYER_LAYERS_SHUFFLECHANNEL_H_
#define _CPP_LAYER_LAYERS_SHUFFLECHANNEL_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/shufflechannel.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_ShuffleChannel_new(ncnn::ShuffleChannel** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ShuffleChannel();

    return error;
}

DLLEXPORT void layer_layers_ShuffleChannel_delete(ncnn::ShuffleChannel* layer)
{
    if (layer != nullptr) delete layer;
}

#endif