#ifndef _CPP_LAYER_CUSTOMLAYER_H_
#define _CPP_LAYER_CUSTOMLAYER_H_

#include "../export.h"
#include <layer.h>
#include "../shared.h"

class CustomLayer : ncnn::Layer
{
public:
    CustomLayer();
    virtual ~CustomLayer();
};

DLLEXPORT int32_t layer_CustomLayer_new(CustomLayer** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new CustomLayer();

    return error;
}

DLLEXPORT void layer_CustomLayer_delete(CustomLayer* layer)
{
    if (layer != nullptr) delete layer;
}

#endif