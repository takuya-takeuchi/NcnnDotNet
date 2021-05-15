#ifndef _CPP_LAYER_LAYERS_LOG_H_
#define _CPP_LAYER_LAYERS_LOG_H_

#include "../../export.h"
#include <layer.h>
#include <layer/log.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Log_new(ncnn::Log** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Log();

    return error;
}

DLLEXPORT void layer_layers_Log_delete(ncnn::Log* layer)
{
    if (layer != nullptr) delete layer;
}

#endif