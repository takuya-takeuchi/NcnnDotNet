#ifndef _CPP_LAYER_LAYERS_MEMORYDATA_H_
#define _CPP_LAYER_LAYERS_MEMORYDATA_H_

#include "../../export.h"
#include <layer.h>
#include <layer/memorydata.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_MemoryData_new(ncnn::MemoryData** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::MemoryData();

    return error;
}

DLLEXPORT void layer_layers_MemoryData_delete(ncnn::MemoryData* layer)
{
    if (layer != nullptr) delete layer;
}

#endif