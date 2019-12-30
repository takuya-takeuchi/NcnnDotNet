#ifndef _CPP_OPENCV_MAT_H_
#define _CPP_OPENCV_MAT_H_

#include "../export.h"
#include <ncnn/opencv.h>
#include "../shared.h"

DLLEXPORT void opencv_Mat_delete(cv::Mat* mat)
{
    if (mat != nullptr) delete mat;
}

DLLEXPORT bool opencv_Mat_empty(cv::Mat* mat)
{
    return mat->empty();
}

DLLEXPORT unsigned char* opencv_Mat_get_data(cv::Mat* mat)
{
    return mat->data;
}

DLLEXPORT int32_t opencv_Mat_get_cols(cv::Mat* mat)
{
    return mat->cols;
}

DLLEXPORT int32_t opencv_Mat_get_rows(cv::Mat* mat)
{
    return mat->rows;
}

#endif