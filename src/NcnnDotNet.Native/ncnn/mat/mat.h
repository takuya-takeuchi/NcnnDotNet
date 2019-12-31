#ifndef _CPP_MAT_MAT_H_
#define _CPP_MAT_MAT_H_

#include "../export.h"
#include <ncnn/mat.h>
#include "../shared.h"

DLLEXPORT int mat_Mat_new(ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat();

    return error;
}

DLLEXPORT int mat_Mat_new2(const int w, const size_t elemsize, ncnn::Allocator* allocator, ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat(w, elemsize, allocator);

    return error;
}

DLLEXPORT void mat_Mat_delete(ncnn::Mat* mat)
{
    if (mat != nullptr) delete mat;
}

DLLEXPORT bool mat_Mat_empty(ncnn::Mat* mat)
{
    return mat->empty();
}

DLLEXPORT float* mat_Mat_row(ncnn::Mat* mat, const int32_t y)
{
    return mat->row(y);
}

DLLEXPORT ncnn::Mat* mat_Mat_channel(ncnn::Mat* mat, const int32_t c)
{
    const auto& ret = mat->channel(c);
    return new ncnn::Mat(ret);
}

DLLEXPORT int32_t mat_Mat_get_w(ncnn::Mat* mat)
{
    return mat->w;
}

DLLEXPORT int32_t mat_Mat_get_h(ncnn::Mat* mat)
{
    return mat->h;
}

DLLEXPORT int32_t mat_Mat_get_c(ncnn::Mat* mat)
{
    return mat->c;
}

DLLEXPORT size_t mat_Mat_get_elemsize(ncnn::Mat* mat)
{
    return mat->elemsize;
}

DLLEXPORT int32_t mat_Mat_get_elempack(ncnn::Mat* mat)
{
    return mat->elempack;
}

DLLEXPORT int32_t mat_Mat_get_dims(ncnn::Mat* mat)
{
    return mat->dims;
}

DLLEXPORT int32_t mat_Mat_get_operator_indexer(ncnn::Mat* mat, const int32_t index, float* returnValue)
{
    int32_t error = ERR_OK;

    const auto& m = *mat;
    *returnValue = m[index];

    return error;
}

DLLEXPORT int32_t mat_Mat_set_operator_indexer(ncnn::Mat* mat, const int32_t index, const float value)
{
    int32_t error = ERR_OK;

    mat->operator[](index) = value;

    return error;
}

DLLEXPORT int32_t mat_Mat_substract_mean_normalize(ncnn::Mat* mat,
                                                   float* mean_vals,
                                                   float* norm_vals)
{
    int32_t error = ERR_OK;

    mat->substract_mean_normalize(mean_vals, norm_vals);

    return error;
}

DLLEXPORT int32_t mat_Mat_from_pixels_resize(const unsigned char* pixels,
                                             const int32_t type,
                                             const int32_t w,
                                             const int32_t h,
                                             const int32_t target_width,
                                             const int32_t target_height,
                                             ncnn::Allocator* allocator,
                                             ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = ncnn::Mat::from_pixels_resize(pixels,
                                                    type,
                                                    w,
                                                    h,
                                                    target_width,
                                                    target_height,
                                                    allocator);

    *returnValue = new ncnn::Mat(ret);

    return error;
}

#endif