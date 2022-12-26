#ifndef _CPP_LAYER_LAYERS_GLU_H_
#define _CPP_LAYER_LAYERS_GLU_H_

#include "../../export.h"
#include <layer.h>
#include <layer/glu.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_GLU_new(ncnn::GLU** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::GLU();

    return error;
}

DLLEXPORT void layer_layers_GLU_delete(ncnn::GLU* layer)
{
    if (layer != nullptr) delete layer;
}

#endif