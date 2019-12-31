#ifndef _CPP_OPENCV_SIZE_H_
#define _CPP_OPENCV_SIZE_H_

#include "../export.h"
#include <opencv2/core.hpp>
#include "../shared.h"

#pragma region template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT cv::Point_<__TYPE__>* opencv_Point_##__TYPENAME__##_new(const __TYPE__ x,\
                                                                  const __TYPE__ y)\
{\
    return new cv::Point_<__TYPE__>(x, y);\
}\
\
DLLEXPORT void opencv_Point_##__TYPENAME__##_delete(cv::Point_<__TYPE__> *size)\
{\
    delete size;\
}\
\
DLLEXPORT __TYPE__ opencv_Point_##__TYPENAME__##_get_x(cv::Point_<__TYPE__> *size)\
{\
    return size->x;\
}\
\
DLLEXPORT __TYPE__ opencv_Point_##__TYPENAME__##_get_y(cv::Point_<__TYPE__> *size)\
{\
    return size->y;\
}\

#pragma endregion template

MAKE_FUNC(int32_t, int32_t)
MAKE_FUNC(int64_t, int64_t)
MAKE_FUNC(float, float)
MAKE_FUNC(double, double)

#endif