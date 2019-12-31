#ifndef _CPP_OPENCV_OPENCV_H_
#define _CPP_OPENCV_OPENCV_H_

#include "../export.h"
#include <opencv2/highgui.hpp>
#include <opencv2/imgcodecs.hpp>
#include "../shared.h"

DLLEXPORT int32_t opencv_imread(const char* filename, const int32_t filename_len, int32_t flags, cv::Mat** returnValue)
{
    int32_t error = ERR_OK;

    std::string path(filename, filename_len);
    const auto ret = cv::imread(path, flags);
    *returnValue = new cv::Mat(ret);

    return error;
}

#endif