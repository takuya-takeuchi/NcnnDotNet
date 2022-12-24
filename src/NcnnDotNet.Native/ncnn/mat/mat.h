#ifndef _CPP_MAT_MAT_H_
#define _CPP_MAT_MAT_H_

#include "../export.h"
#include <mat.h>
#include "../shared.h"

DLLEXPORT int mat_Mat_new(ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat();

    return error;
}

DLLEXPORT int mat_Mat_new2(const int32_t w, const size_t elemsize, ncnn::Allocator* allocator, ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat(w, elemsize, allocator);

    return error;
}

DLLEXPORT int mat_Mat_new3(const int32_t w,
                           const int32_t h,
                           const size_t elemsize,
                           ncnn::Allocator* allocator,
                           ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat(w, h, elemsize, allocator);

    return error;
}

DLLEXPORT int mat_Mat_new4(const int32_t w,
                           const int32_t h,
                           const int32_t c,
                           const size_t elemsize,
                           ncnn::Allocator* allocator,
                           ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat(w, h, c, elemsize, allocator);

    return error;
}

DLLEXPORT int32_t mat_Mat_new5(const int32_t w,
                               void* data,
                               const size_t elemsize,
                               ncnn::Allocator* allocator,
                               ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat(w, data, elemsize, allocator);

    return error;
}

DLLEXPORT int32_t mat_Mat_new6(const int32_t w,
                               const int32_t h,
                               void* data,
                               const size_t elemsize,
                               ncnn::Allocator* allocator,
                               ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat(w, h, data, elemsize, allocator);

    return error;
}

DLLEXPORT int32_t mat_Mat_new7(const int32_t w,
                               const int32_t h,
                               const int32_t c,
                               void* data,
                               const size_t elemsize,
                               ncnn::Allocator* allocator,
                               ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    *returnValue = new ncnn::Mat(w, h, c, data, elemsize, allocator);

    return error;
}

DLLEXPORT void mat_Mat_delete(ncnn::Mat* mat)
{
    if (mat != nullptr) delete mat;
}

#pragma region fill

DLLEXPORT int32_t mat_Mat_fill_float(ncnn::Mat* mat, const float v)
{
    int32_t error = ERR_OK;

    mat->fill(v);

    return error;
}

DLLEXPORT int32_t mat_Mat_fill_int(ncnn::Mat* mat, const int32_t v)
{
    int32_t error = ERR_OK;

    mat->fill(v);

    return error;
}

#pragma endregion fill

#pragma region reshape

DLLEXPORT int32_t mat_Mat_reshape(ncnn::Mat* mat, int32_t w, ncnn::Allocator* allocator, ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = mat->reshape(w, allocator);
    *returnValue = new ncnn::Mat(ret);

    return error;
}

DLLEXPORT int32_t mat_Mat_reshape2(ncnn::Mat* mat,
                                   int32_t w,
                                   int32_t h,
                                   ncnn::Allocator* allocator,
                                   ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = mat->reshape(w, h, allocator);
    *returnValue = new ncnn::Mat(ret);

    return error;
}

DLLEXPORT int32_t mat_Mat_reshape3(ncnn::Mat* mat,
                                   int32_t w,
                                   int32_t h,
                                   int32_t c,
                                   ncnn::Allocator* allocator,
                                   ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = mat->reshape(w, h, c, allocator);
    *returnValue = new ncnn::Mat(ret);

    return error;
}

#pragma endregion reshape

#pragma region create

DLLEXPORT int32_t mat_Mat_create(ncnn::Mat* mat,
                                 const int32_t w,
                                 const size_t celemsize,
                                 ncnn::Allocator* allocator)
{
    int32_t error = ERR_OK;

    mat->create(w, celemsize, allocator);

    return error;
}

DLLEXPORT int32_t mat_Mat_create2(ncnn::Mat* mat,
                                  const int32_t w,
                                  const int32_t h,
                                  const size_t celemsize,
                                  ncnn::Allocator* allocator)
{
    int32_t error = ERR_OK;

    mat->create(w, h, celemsize, allocator);

    return error;
}

DLLEXPORT int32_t mat_Mat_create6(ncnn::Mat* mat,
                                  const int32_t w,
                                  const int32_t h,
                                  const int32_t c,
                                  const size_t celemsize,
                                  const int32_t elempack,
                                  ncnn::Allocator* allocator)
{
    int32_t error = ERR_OK;

    mat->create(w, h, c, celemsize, elempack, allocator);

    return error;
}

#pragma endregion create

#pragma region create_like

DLLEXPORT int32_t mat_Mat_create_like_mat(ncnn::Mat* mat,
                                          ncnn::Mat* m,
                                          ncnn::Allocator* allocator)
{
    int32_t error = ERR_OK;

    const auto& in_m = *m;
    mat->create_like(in_m, allocator);

    return error;
}

#ifdef NCNN_VULKAN

DLLEXPORT int32_t mat_Mat_create_like_vkmat(ncnn::Mat* mat,
                                            ncnn::VkMat* m,
                                            ncnn::Allocator* allocator)
{
    int32_t error = ERR_OK;

    const auto& in_m = *m;
    mat->create_like(in_m, allocator);

    return error;
}

#endif

#pragma endregion create_like

DLLEXPORT bool mat_Mat_empty(ncnn::Mat* mat)
{
    return mat->empty();
}

DLLEXPORT size_t mat_Mat_total(ncnn::Mat* mat)
{
    return mat->total();
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

DLLEXPORT ncnn::Mat* mat_Mat_channel_range(ncnn::Mat* mat, const int32_t c, const int32_t channels)
{
    const auto& ret = mat->channel_range(c, channels);
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

DLLEXPORT void* mat_Mat_get_data(ncnn::Mat* mat)
{
    return mat->data;
}

DLLEXPORT ncnn::Allocator* mat_Mat_get_allocator(ncnn::Mat* mat)
{
    return mat->allocator;
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

DLLEXPORT int32_t mat_Mat_get_operator_indexer2(ncnn::Mat* mat, const size_t index, float* returnValue)
{
    int32_t error = ERR_OK;

    const auto& m = *mat;
    *returnValue = m[index];

    return error;
}

DLLEXPORT int32_t mat_Mat_set_operator_indexer2(ncnn::Mat* mat, const size_t index, const float value)
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

DLLEXPORT int32_t mat_Mat_from_pixels(const unsigned char* pixels,
                                      const int32_t type,
                                      const int32_t w,
                                      const int32_t h,
                                      ncnn::Allocator* allocator,
                                      ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = ncnn::Mat::from_pixels(pixels,
                                             type,
                                             w,
                                             h,
                                             allocator);

    *returnValue = new ncnn::Mat(ret);

    return error;
}

DLLEXPORT int32_t mat_Mat_from_pixels2(const unsigned char* pixels,
                                       const int32_t type,
                                       const int32_t w,
                                       const int32_t h,
                                       const int32_t stride,
                                       ncnn::Allocator* allocator,
                                       ncnn::Mat** returnValue)
{
    int32_t error = ERR_OK;

    const auto& ret = ncnn::Mat::from_pixels(pixels,
                                             type,
                                             w,
                                             h,
                                             stride,
                                             allocator);

    *returnValue = new ncnn::Mat(ret);

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

DLLEXPORT int32_t mat_Mat_from_pixels_resize2(const unsigned char* pixels,
                                              const int32_t type,
                                              const int32_t w,
                                              const int32_t h,
                                              const int32_t stride,
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
                                                    stride,
                                                    target_width,
                                                    target_height,
                                                    allocator);

    *returnValue = new ncnn::Mat(ret);

    return error;
}

DLLEXPORT int32_t mat_copy_make_border(ncnn::Mat* src,
                                       ncnn::Mat* dst,
                                       const int32_t top,
                                       const int32_t bottom,
                                       const int32_t left,
                                       const int32_t right,
                                       const int32_t type,
                                       const float v, 
                                       ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& s = *src;
    auto& d = *dst;
    const auto& o = *opt;
    ncnn::copy_make_border(s, d, top, bottom, left, right, type, v, o);

    return error;
}

DLLEXPORT int32_t mat_resize_bicubic(ncnn::Mat* src,
                                     ncnn::Mat* dst,
                                     const int32_t w,
                                     const int32_t h,
                                     ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& s = *src;
    auto& d = *dst;
    const auto& o = *opt;
    ncnn::resize_bicubic(s, d, w, h, o);

    return error;
}

DLLEXPORT int32_t mat_resize_bilinear(ncnn::Mat* src,
                                      ncnn::Mat* dst,
                                      const int32_t w,
                                      const int32_t h,
                                      ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& s = *src;
    auto& d = *dst;
    const auto& o = *opt;
    ncnn::resize_bilinear(s, d, w, h, o);

    return error;
}

DLLEXPORT int32_t mat_convert_packing(ncnn::Mat* src,
                                      ncnn::Mat* dst,
                                      const int32_t elempack,
                                      ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& s = *src;
    auto& d = *dst;
    const auto& o = *opt;
    ncnn::convert_packing(s, d, elempack, o);

    return error;
}

DLLEXPORT int32_t mat_cast_float16_to_float32(ncnn::Mat* src,
                                              ncnn::Mat* dst,
                                              ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& s = *src;
    auto& d = *dst;
    const auto& o = *opt;
    ncnn::cast_float16_to_float32(s, d, o);

    return error;
}

DLLEXPORT int32_t mat_cast_float32_to_float16(ncnn::Mat* src,
                                              ncnn::Mat* dst,
                                              ncnn::Option* opt)
{
    int32_t error = ERR_OK;

    const auto& s = *src;
    auto& d = *dst;
    const auto& o = *opt;
    ncnn::cast_float32_to_float16(s, d, o);

    return error;
}

#endif