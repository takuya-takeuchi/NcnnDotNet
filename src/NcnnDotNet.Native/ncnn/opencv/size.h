#ifndef _CPP_OPENCV_SIZE_H_
#define _CPP_OPENCV_SIZE_H_

#include "../export.h"
#include <opencv2/core.hpp>
#include "../shared.h"

#pragma region template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT cv::Size_<__TYPE__>* opencv_Size_##__TYPENAME__##_new(const __TYPE__ width,\
                                                                const __TYPE__ height)\
{\
    return new cv::Size_<__TYPE__>(width, height);\
}\
\
DLLEXPORT void opencv_Size_##__TYPENAME__##_delete(cv::Size_<__TYPE__> *size)\
{\
    delete size;\
}\
\
DLLEXPORT __TYPE__ opencv_Size_##__TYPENAME__##_get_width(cv::Size_<__TYPE__> *size)\
{\
    return size->width;\
}\
\
DLLEXPORT __TYPE__ opencv_Size_##__TYPENAME__##_get_height(cv::Size_<__TYPE__> *size)\
{\
    return size->height;\
}\

#pragma endregion template

MAKE_FUNC(int32_t, int32_t)
MAKE_FUNC(int64_t, int64_t)
MAKE_FUNC(float, float)
MAKE_FUNC(double, double)

#endif