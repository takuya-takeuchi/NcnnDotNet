#ifndef _CPP_LAYER_LAYERS_POOLING3D_H_
#define _CPP_LAYER_LAYERS_POOLING3D_H_

#include "../../export.h"
#include <layer.h>
#include <layer/pooling3d.h>
#include "../../shared.h"

DLLEXPORT int layer_layers_Pooling3D_new(ncnn::Pooling3D** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Pooling3D();

    return error;
}

DLLEXPORT void layer_layers_Pooling3D_delete(ncnn::Pooling3D* layer)
{
    if (layer != nullptr) delete layer;
}

#endif