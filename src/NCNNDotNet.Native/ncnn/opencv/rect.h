#ifndef _CPP_OPENCV_RECT_H_
#define _CPP_OPENCV_RECT_H_

#include "../export.h"
#include <opencv2/core.hpp>
#include "../shared.h"

#pragma region template

#define MAKE_FUNC(__TYPE__, __TYPENAME__)\
DLLEXPORT cv::Rect_<__TYPE__>* opencv_Rect_##__TYPENAME__##_new(const __TYPE__ x,\
                                                                const __TYPE__ y,\
                                                                const __TYPE__ width,\
                                                                const __TYPE__ height)\
{\
    return new cv::Rect_<__TYPE__>(x, y, width, height);\
}\
\
DLLEXPORT void opencv_Rect_##__TYPENAME__##_delete(cv::Rect_<__TYPE__> *rect)\
{\
    delete rect;\
}\
\
DLLEXPORT __TYPE__ opencv_Rect_##__TYPENAME__##_get_x(cv::Rect_<__TYPE__> *rect)\
{\
    return rect->x;\
}\
\
DLLEXPORT __TYPE__ opencv_Rect_##__TYPENAME__##_get_y(cv::Rect_<__TYPE__> *rect)\
{\
    return rect->y;\
}\
\
DLLEXPORT __TYPE__ opencv_Rect_##__TYPENAME__##_get_width(cv::Rect_<__TYPE__> *rect)\
{\
    return rect->width;\
}\
\
DLLEXPORT __TYPE__ opencv_Rect_##__TYPENAME__##_get_height(cv::Rect_<__TYPE__> *rect)\
{\
    return rect->height;\
}\
\
DLLEXPORT __TYPE__ opencv_Rect_##__TYPENAME__##_area(cv::Rect_<__TYPE__> *rect)\
{\
    return rect->area();\
}\

#pragma endregion template

MAKE_FUNC(int32_t, int32_t)
MAKE_FUNC(float, float)

#endif