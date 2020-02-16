#ifndef _CPP_LAYER_LAYER_H_
#define _CPP_LAYER_LAYER_H_

#include "../export.h"
#include <ncnn/layer.h>
#include "../shared.h"

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

#include <absval.h>
#include <argmax.h>
#include <batchnorm.h>
#include <bias.h>
#include <binaryop.h>
#include <bnll.h>
#include <cast.h>
#include <clip.h>
#include <concat.h>
#include <convolution.h>
#include <convolutiondepthwise.h>
#include <crop.h>
#include <deconvolution.h>
#include <deconvolutiondepthwise.h>
#include <dequantize.h>
#include <detectionoutput.h>
#include <dropout.h>
#include <eltwise.h>
#include <elu.h>
#include <embed.h>
#include <exp.h>
#include <expanddims.h>
#include <flatten.h>
#include <hardsigmoid.h>
#include <hardswish.h>
#include <innerproduct.h>
#include <input.h>
#include <instancenorm.h>
#include <interp.h>
#include <log.h>
#include <lrn.h>
#include <lstm.h>
#include <memorydata.h>
#include <mvn.h>
#include <noop.h>
#include <normalize.h>
#include <packing.h>
#include <padding.h>
#include <permute.h>
#include <pooling.h>
#include <power.h>
#include <prelu.h>
#include <priorbox.h>
#include <proposal.h>
#include <psroipooling.h>
#include <quantize.h>
#include <reduction.h>
#include <reorg.h>
#include <requantize.h>
#include <reshape.h>
#include <rnn.h>
#include <roialign.h>
#include <roipooling.h>
#include <scale.h>
#include <selu.h>
#include <shufflechannel.h>
#include <sigmoid.h>
#include <slice.h>
#include <softmax.h>
#include <split.h>
#include <spp.h>
#include <squeeze.h>
#include <tanh.h>
#include <threshold.h>
#include <tile.h>
#include <unaryop.h>
#include <yolodetectionoutput.h>
#include <yolov3detectionoutput.h>

// ToDo: move to layers directory
// some layer is not included because set OFF in https://github.com/Tencent/ncnn/blob/master/src/CMakeLists.txt
MAKE_FUNC(AbsVal, AbsVal)
//MAKE_FUNC(ArgMax, ArgMax)
MAKE_FUNC(BatchNorm, BatchNorm)
MAKE_FUNC(Bias, Bias)
MAKE_FUNC(BinaryOp, BinaryOp)
MAKE_FUNC(BNLL, BNLL)
MAKE_FUNC(Cast, Cast)
MAKE_FUNC(Clip, Clip)
MAKE_FUNC(Concat, Concat)
MAKE_FUNC(Convolution, Convolution)
MAKE_FUNC(ConvolutionDepthWise, ConvolutionDepthWise)
MAKE_FUNC(Crop, Crop)
MAKE_FUNC(Deconvolution, Deconvolution)
MAKE_FUNC(DeconvolutionDepthWise, DeconvolutionDepthWise)
MAKE_FUNC(Dequantize, Dequantize)
MAKE_FUNC(DetectionOutput, DetectionOutput)
MAKE_FUNC(Dropout, Dropout)
MAKE_FUNC(Eltwise, Eltwise)
MAKE_FUNC(ELU, ELU)
MAKE_FUNC(Embed, Embed)
MAKE_FUNC(Exp, Exp)
MAKE_FUNC(ExpandDims, ExpandDims)
MAKE_FUNC(Flatten, Flatten)
MAKE_FUNC(HardSigmoid, HardSigmoid)
MAKE_FUNC(HardSwish, HardSwish)
MAKE_FUNC(InnerProduct, InnerProduct)
MAKE_FUNC(Input, Input)
MAKE_FUNC(InstanceNorm, InstanceNorm)
MAKE_FUNC(Interp, Interp)
MAKE_FUNC(Log, Log)
MAKE_FUNC(LRN, LRN)
//MAKE_FUNC(LSTM, LSTM)
MAKE_FUNC(MemoryData, MemoryData)
MAKE_FUNC(MVN, MVN)
MAKE_FUNC(Noop, Noop)
MAKE_FUNC(Normalize, Normalize)
MAKE_FUNC(Packing, Packing)
MAKE_FUNC(Padding, Padding)
MAKE_FUNC(Permute, Permute)
MAKE_FUNC(Pooling, Pooling)
MAKE_FUNC(Power, Power)
MAKE_FUNC(PReLU, PReLU)
MAKE_FUNC(PriorBox, PriorBox)
MAKE_FUNC(Proposal, Proposal)
MAKE_FUNC(PSROIPooling, PSROIPooling)
MAKE_FUNC(Quantize, Quantize)
MAKE_FUNC(Reduction, Reduction)
MAKE_FUNC(Reorg, Reorg)
MAKE_FUNC(Requantize, Requantize)
MAKE_FUNC(Reshape, Reshape)
//MAKE_FUNC(RNN, RNN)
//MAKE_FUNC(ROIAlign, ROIAlign)
MAKE_FUNC(ROIPooling, ROIPooling)
MAKE_FUNC(Scale, Scale)
MAKE_FUNC(SELU, SELU)
MAKE_FUNC(ShuffleChannel, ShuffleChannel)
MAKE_FUNC(Sigmoid, Sigmoid)
MAKE_FUNC(Slice, Slice)
MAKE_FUNC(Softmax, Softmax)
MAKE_FUNC(Split, Split)
//MAKE_FUNC(SPP, SPP)
MAKE_FUNC(Squeeze, Squeeze)
MAKE_FUNC(TanH, TanH)
MAKE_FUNC(Threshold, Threshold)
//MAKE_FUNC(Tile, Tile)
MAKE_FUNC(UnaryOp, UnaryOp)
MAKE_FUNC(YoloDetectionOutput, YoloDetectionOutput)
MAKE_FUNC(Yolov3DetectionOutput, Yolov3DetectionOutput)

#endif