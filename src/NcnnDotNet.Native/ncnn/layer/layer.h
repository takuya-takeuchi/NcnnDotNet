#ifndef _CPP_LAYER_LAYER_H_
#define _CPP_LAYER_LAYER_H_

#include "../export.h"
#include <layer.h>
#include "../shared.h"

DLLEXPORT int32_t layer_Layer_new(ncnn::Layer** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Layer();

    return error;
}

DLLEXPORT void layer_Layer_delete(ncnn::Layer* layer)
{
    if (layer != nullptr) delete layer;
}

DLLEXPORT int32_t layer_Layer_load_param(ncnn::Layer* layer, ncnn::ParamDict* pd, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    const auto& p = *pd;
    *returnValue = layer->load_param(p);

    return error;
}

DLLEXPORT int32_t layer_Layer_load_model(ncnn::Layer* layer, ncnn::ModelBin* mb, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    const auto& p = *mb;
    *returnValue = layer->load_model(p);

    return error;
}

DLLEXPORT int32_t layer_Layer_create_pipeline(ncnn::Layer* layer, ncnn::Option* opt, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    const auto& p = *opt;
    *returnValue = layer->create_pipeline(p);

    return error;
}

DLLEXPORT int32_t layer_Layer_destroy_pipeline(ncnn::Layer* layer, ncnn::Option* opt, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    const auto& p = *opt;
    *returnValue = layer->destroy_pipeline(p);

    return error;
}

DLLEXPORT int32_t layer_Layer_forward2(ncnn::Layer* layer,
                                       ncnn::Mat* bottom_blob,
                                       ncnn::Mat* top_blob,
                                       ncnn::Option* opt,
                                       int32_t* returnValue)
{
    int32_t error = ERR_OK;

    const auto& b = *bottom_blob;
    auto& t = *top_blob;
    const auto& o = *opt;
    *returnValue = layer->forward(b, t, o);

    return error;
}

#ifdef NCNN_VULKAN

DLLEXPORT int32_t layer_Layer_forward2_vkmat(ncnn::Layer* layer,
                                             ncnn::VkMat* bottom_blob,
                                             ncnn::VkMat* top_blob,
                                             ncnn::VkCompute* cmd,
                                             ncnn::Option* opt,
                                             int32_t* returnValue)
{
    int32_t error = ERR_OK;

    const auto& b = *bottom_blob;
    auto& t = *top_blob;
    auto& c = *cmd;
    const auto& o = *opt;
    *returnValue = layer->forward(b, t, c, o);

    return error;
}

#endif

DLLEXPORT int32_t layer_Layer_forward_inplace(ncnn::Layer* layer, ncnn::Mat* mat, ncnn::Option* opt, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    auto& m = *mat;
    const auto& o = *opt;
    *returnValue = layer->forward_inplace(m, o);

    return error;
}

DLLEXPORT void layer_Layer_get_one_blob_only(ncnn::Layer* layer, bool* returnValue)
{
    *returnValue = layer->one_blob_only;
}

DLLEXPORT void layer_Layer_set_one_blob_only(ncnn::Layer* layer, bool value)
{
    layer->one_blob_only = value;
}

DLLEXPORT void layer_Layer_get_support_packing(ncnn::Layer* layer, bool* returnValue)
{
    *returnValue = layer->support_packing;
}

DLLEXPORT void layer_Layer_set_support_packing(ncnn::Layer* layer, bool value)
{
    layer->support_packing = value;
}

#ifdef NCNN_VULKAN

DLLEXPORT void layer_Layer_get_vkdev(ncnn::Layer* layer, const ncnn::VulkanDevice** returnValue)
{
    *returnValue = layer->vkdev;
}

DLLEXPORT void layer_Layer_set_vkdev(ncnn::Layer* layer, ncnn::VulkanDevice* value)
{
    layer->vkdev = value;
}

DLLEXPORT int32_t layer_Layer_upload_model(ncnn::Layer* layer,
                                           ncnn::VkTransfer* cmd,
                                           ncnn::Option* opt,
                                           int32_t* returnValue)
{
    int32_t error = ERR_OK;

    auto& c = *cmd;
    const auto& o = *opt;
    *returnValue = layer->upload_model(c, o);

    return error;
}

#endif

DLLEXPORT int32_t layer_layer_to_index(const char* type, const int32_t type_len, int32_t* returnValue)
{
    int32_t error = ERR_OK;

    std::string t(type, type_len);
    *returnValue = ncnn::layer_to_index(t.c_str());

    return error;
}

DLLEXPORT int32_t layer_create_layer(const char* type, const int32_t type_len, ncnn::Layer** returnValue)
{
    int32_t error = ERR_OK;

    std::string t(type, type_len);
    *returnValue = ncnn::create_layer(t.c_str());

    return error;
}

DLLEXPORT int32_t layer_create_layer2(const int32_t index, ncnn::Layer** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = ncnn::create_layer(index);

    return error;
}

#pragma region layer template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT int layer_##__TYPENAME__##_new(ncnn::__TYPE__** returnValue)\
{\
    int32_t error = ERR_OK;\
    *returnValue = new ncnn::__TYPE__();\
    return error;\
}\
\
DLLEXPORT void layer_##__TYPENAME__##_delete(ncnn::__TYPE__* layer)\
{\
    if (layer != nullptr) delete layer;\
}\

#pragma endregion layer template

#endif