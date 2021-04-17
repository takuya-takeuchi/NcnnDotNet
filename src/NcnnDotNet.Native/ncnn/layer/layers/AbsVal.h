#ifndef _CPP_LAYER_LAYERS_ABSVAL_H_
#define _CPP_LAYER_LAYERS_ABSVAL_H_

#include "../../export.h"
#include <ncnn/layer.h>
#include <layer/absval.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_AbsVal_new(ncnn::AbsVal** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::AbsVal();

    return error;
}

DLLEXPORT void layer_layers_AbsVal_delete(ncnn::AbsVal* layer)
{
    if (layer != nullptr) delete layer;
}

#endif