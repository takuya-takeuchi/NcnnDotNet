#ifndef _CPP_LAYER_LAYERS_PSROIPOOLING_H_
#define _CPP_LAYER_LAYERS_PSROIPOOLING_H_

#include "../../export.h"
#include <layer.h>
#include <layer/psroipooling.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_PSROIPooling_new(ncnn::PSROIPooling** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::PSROIPooling();

    return error;
}

DLLEXPORT void layer_layers_PSROIPooling_delete(ncnn::PSROIPooling* layer)
{
    if (layer != nullptr) delete layer;
}

#endif