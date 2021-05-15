#ifndef _CPP_LAYER_LAYERS_RNN_H_
#define _CPP_LAYER_LAYERS_RNN_H_

#include "../../export.h"
#include <layer.h>
#include <layer/rnn.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_RNN_new(ncnn::RNN** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::RNN();

    return error;
}

DLLEXPORT void layer_layers_RNN_delete(ncnn::RNN* layer)
{
    if (layer != nullptr) delete layer;
}

#endif