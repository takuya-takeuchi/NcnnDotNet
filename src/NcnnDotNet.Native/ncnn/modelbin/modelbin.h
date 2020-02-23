#ifndef _CPP_MODELBIN_MODELBIN_H_
#define _CPP_MODELBIN_MODELBIN_H_

#include "../export.h"
#include <ncnn/modelbin.h>
#include "../shared.h"

DLLEXPORT int modelbin_ModelBinFromMatArray_new(std::vector<ncnn::Mat>* weights, ncnn::ModelBinFromMatArray ** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::ModelBinFromMatArray(weights->data());

    return error;
}

DLLEXPORT void modelbin_ModelBinFromMatArray_delete(ncnn::ModelBinFromMatArray* array)
{
    if (array != nullptr) delete array;
}

#endif