#ifndef _CPP_LAYER_CUSTOMLAYER_H_
#define _CPP_LAYER_CUSTOMLAYER_H_

#include "../export.h"
#include <layer.h>
#include "../shared.h"

class CustomLayer : ncnn::Layer
{
public:
    CustomLayer(int32_t (*forward_function)(const ncnn::Mat&, ncnn::Mat&, const ncnn::Option&) = nullptr,
                int32_t (*forward_inplace_function)(const ncnn::Mat&, const ncnn::Option&) = nullptr);
    virtual ~CustomLayer();

public:
    virtual int32_t forward(const ncnn::Mat& bottom_blob, ncnn::Mat& top_blob, const ncnn::Option& opt) const;
    virtual int32_t forward_inplace(ncnn::Mat& bottom_top_blob, const ncnn::Option& opt) const;

private:
    int32_t (*m_forward_function)(const ncnn::Mat&, ncnn::Mat&, const ncnn::Option&);
    int32_t (*m_forward_inplace_function)(const ncnn::Mat&, const ncnn::Option&);
};

DLLEXPORT int32_t layer_CustomLayer_new(CustomLayer** returnValue,
                                        int32_t (*forward_function)(const ncnn::Mat&, ncnn::Mat&, const ncnn::Option&),
                                        int32_t (*forward_inplace_function)(const ncnn::Mat&, const ncnn::Option&))
{
    int32_t error = ERR_OK;

    *returnValue = new CustomLayer(forward_function, forward_inplace_function);

    return error;
}

DLLEXPORT void layer_CustomLayer_delete(CustomLayer* layer)
{
    if (layer != nullptr) delete layer;
}

#endif