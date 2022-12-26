#include "CustomLayer.h"

CustomLayer::CustomLayer(int32_t (*forward_function)(const ncnn::Mat&, ncnn::Mat&, const ncnn::Option&),
                         int32_t (*forward_inplace_function)(const ncnn::Mat&, const ncnn::Option&)):
    m_forward_function(forward_function),
    m_forward_inplace_function(forward_inplace_function)
{
}

CustomLayer::~CustomLayer()
{
    this->m_forward_function = nullptr;
    this->m_forward_inplace_function = nullptr;
}

int32_t CustomLayer::forward(const ncnn::Mat& bottom_blob, ncnn::Mat& top_blob, const ncnn::Option& opt) const
{
    if (this->m_forward_function)
        return this->m_forward_function(bottom_blob, top_blob, opt);
    return ncnn::Layer::forward(bottom_blob, top_blob, opt);
}

int32_t CustomLayer::forward_inplace(ncnn::Mat& bottom_top_blob, const ncnn::Option& opt) const
{
    if (this->m_forward_inplace_function)
        return this->m_forward_inplace_function(bottom_top_blob, opt);
    return ncnn::Layer::forward_inplace(bottom_top_blob, opt);
}