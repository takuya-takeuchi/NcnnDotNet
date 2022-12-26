#ifndef _CPP_LAYER_LAYERS_STATISTICSPOOLING_H_
#define _CPP_LAYER_LAYERS_STATISTICSPOOLING_H_

#include "../../export.h"
#include <layer.h>
#include <layer/statisticspooling.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_StatisticsPooling_new(ncnn::StatisticsPooling** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::StatisticsPooling();

    return error;
}

DLLEXPORT void layer_layers_StatisticsPooling_delete(ncnn::StatisticsPooling* layer)
{
    if (layer != nullptr) delete layer;
}

#endif