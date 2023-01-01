#ifndef _CPP_MAT_PIXEL_MAT_PIXEL_H_
#define _CPP_MAT_PIXEL_MAT_PIXEL_H_

#include "../export.h"
#include <mat.h>
#include "../shared.h"

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

DLLEXPORT int32_t mat_Mat_to_pixels(ncnn::Mat* mat,
                                    unsigned char* pixels,
                                    const int32_t type)
{
    int32_t error = ERR_OK;

    mat->to_pixels(pixels, type);

    return error;
}

DLLEXPORT int32_t mat_Mat_to_pixels2(ncnn::Mat* mat,
                                     unsigned char* pixels,
                                     const int32_t type,
                                     const int32_t stride)
{
    int32_t error = ERR_OK;

    mat->to_pixels(pixels, type, stride);

    return error;
}

DLLEXPORT int32_t mat_Mat_to_pixels_resize(ncnn::Mat* mat,
                                           unsigned char* pixels,
                                           const int32_t type,
                                           const int32_t target_width,
                                           const int32_t target_height)
{
    int32_t error = ERR_OK;

    mat->to_pixels_resize(pixels, type, target_width, target_height);

    return error;
}

DLLEXPORT int32_t mat_Mat_to_pixels_resize2(ncnn::Mat* mat,
                                            unsigned char* pixels,
                                            const int32_t type,
                                            const int32_t target_width,
                                            const int32_t target_height,
                                            const int32_t target_stride)
{
    int32_t error = ERR_OK;

    mat->to_pixels_resize(pixels, type, target_width, target_height, target_stride);

    return error;
}

#endif