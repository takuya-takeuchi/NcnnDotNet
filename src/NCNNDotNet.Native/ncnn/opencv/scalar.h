#ifndef _CPP_OPENCV_SCALAR_H_
#define _CPP_OPENCV_SCALAR_H_

#include "../export.h"
#include <opencv2/core.hpp>
#include "../shared.h"

#pragma region template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT cv::Scalar_<__TYPE__>* opencv_Scalar_##__TYPENAME__##_new(__TYPE__ v0, __TYPE__ v1, __TYPE__ v2, __TYPE__ v3)\
{\
    return new cv::Scalar_<__TYPE__>(v0, v1, v2, v3);\
}\
\
DLLEXPORT void opencv_Scalar_##__TYPENAME__##_delete(cv::Scalar_<__TYPE__>* scalar)\
{\
    delete scalar;\
}\
\
DLLEXPORT __TYPE__ opencv_Scalar_##__TYPENAME__##_operator(cv::Scalar_<__TYPE__>* scalar, int32_t index)\
{\
    return scalar->operator()(index);\
}\

#pragma endregion template

MAKE_FUNC(double, double)

#endif