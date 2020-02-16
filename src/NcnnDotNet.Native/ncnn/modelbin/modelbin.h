#ifndef _CPP_MODELBIN_MODELBIN_H_
#define _CPP_MODELBIN_MODELBIN_H_

#include "../export.h"
#include <ncnn/modelbin.h>
#include "../shared.h"

DLLEXPORT int modelbin_ModelBinFromMatArray_new(ncnn::Mat** weights, const int32_t weights_len, ncnn::ModelBinFromMatArray ** returnValue)
{
    int32_t error = ERR_OK;

    std::vector<ncnn::Mat> tmp(weights_len);
    for (int32_t i = 0; i < weights_len; i++)
    {
        auto& m = *(weights[i]);
        tmp.push_back(m);
    }

    *returnValue = new ncnn::ModelBinFromMatArray(tmp.data());

    return error;
}

DLLEXPORT void modelbin_ModelBinFromMatArray_delete(ncnn::ModelBinFromMatArray* array)
{
    if (array != nullptr) delete array;
}

#endif