#ifndef _CPP_OPENCV_MAT_H_
#define _CPP_OPENCV_MAT_H_

#include "../export.h"
#include <opencv2/highgui.hpp>
#include <opencv2/imgcodecs.hpp>
#include "../shared.h"

DLLEXPORT void opencv_Mat_delete(cv::Mat* mat)
{
    if (mat != nullptr) delete mat;
}

DLLEXPORT cv::Mat* opencv_Mat_clone(cv::Mat* mat)
{
    const auto& ret = mat->clone();
    return new cv::Mat(ret);
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

DLLEXPORT int32_t opencv_Mat_total(cv::Mat* mat)
{
    return mat->total();
}

DLLEXPORT int32_t opencv_Mat_channels(cv::Mat* mat)
{
    return mat->channels();
}

#endif