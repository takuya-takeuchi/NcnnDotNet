#ifndef _CPP_LAYER_LAYERS_LSTM_H_
#define _CPP_LAYER_LAYERS_LSTM_H_

#include "../../export.h"
#include <layer.h>
#include <layer/lstm.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_LSTM_new(ncnn::LSTM** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::LSTM();

    return error;
}

DLLEXPORT void layer_layers_LSTM_delete(ncnn::LSTM* layer)
{
    if (layer != nullptr) delete layer;
}

#endif