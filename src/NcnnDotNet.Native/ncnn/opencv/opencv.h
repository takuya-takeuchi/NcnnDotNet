#ifndef _CPP_OPENCV_OPENCV_H_
#define _CPP_OPENCV_OPENCV_H_

#include "../export.h"
#include <opencv2/core.hpp>
#include <opencv2/imgcodecs.hpp>
#include <opencv2/imgproc.hpp>
#ifndef NO_GUI_SUPPORT
#include <opencv2/highgui.hpp>
#endif
#include "../shared.h"

#pragma region template

#define MAKE_DRAWING(__TYPE__, __TYPENAME__)\
DLLEXPORT int32_t opencv_line_##__TYPENAME__(cv::Mat* mat,\
                                             cv::Point_<__TYPE__>* p1,\
                                             cv::Point_<__TYPE__>* p2,\
                                             cv::Scalar* scalar,\
                                             const int32_t thickness,\
                                             const int lineType,\
                                             const int shift)\
{\
    int32_t error = ERR_OK;\
\
    auto& m = *mat;\
    auto& p1_ = *p1;\
    auto& p2_ = *p2;\
    auto& s = *scalar;\
    cv::line(m, p1_, p2_, s, thickness, lineType, shift);\
\
    return error;\
}\
\
DLLEXPORT int32_t opencv_rectangle_##__TYPENAME__(cv::Mat* mat,\
                                                  cv::Rect_<__TYPE__>* rect,\
                                                  cv::Scalar* scalar,\
                                                  const int32_t thickness,\
                                                  const int lineType,\
                                                  const int shift)\
{\
    int32_t error = ERR_OK;\
\
    auto& m = *mat;\
    auto& r = *rect;\
    auto& s = *scalar;\
    cv::rectangle(m, r, s, thickness, lineType, shift);\
\
    return error;\
}\
\
DLLEXPORT int32_t opencv_rectangle2_##__TYPENAME__(cv::Mat* mat,\
                                                   cv::Point_<__TYPE__>* pt1,\
                                                   cv::Point_<__TYPE__>* pt2,\
                                                   cv::Scalar* scalar,\
                                                   const int32_t thickness,\
                                                   const int lineType,\
                                                   const int shift)\
{\
    int32_t error = ERR_OK;\
\
    auto& m = *mat;\
    auto& p1 = *pt1;\
    auto& p2 = *pt2;\
    auto& s = *scalar;\
    cv::rectangle(m, p1, p2, s, thickness, lineType, shift);\
\
    return error;\
}\
\
DLLEXPORT int32_t opencv_circle_##__TYPENAME__(cv::Mat* mat,\
                                               cv::Point_<__TYPE__>* center,\
                                               const int32_t radius,\
                                               cv::Scalar* scalar,\
                                               const int32_t thickness,\
                                               const int lineType,\
                                               const int shift)\
{\
    int32_t error = ERR_OK;\
\
    auto& m = *mat;\
    auto& c = *center;\
    auto& s = *scalar;\
    cv::circle(m, c, radius, s, thickness, lineType, shift);\
\
    return error;\
}\

#define MAKE_PUTTEXT(__TYPE__, __TYPENAME__)\
DLLEXPORT int32_t opencv_putText_##__TYPENAME__(cv::Mat* mat,\
                                                const char* text,\
                                                const int32_t text_len,\
                                                cv::Point_<__TYPE__>* point,\
                                                const int32_t font,\
                                                const double fontScale,\
                                                cv::Scalar* scalar,\
                                                const int32_t thickness,\
                                                const int lineType,\
                                                const bool bottomLeftOrigin)\
{\
    int32_t error = ERR_OK;\
\
    auto& m = *mat;\
    auto& p = *point;\
    auto& s = *scalar;\
    std::string t(text, text_len);\
    cv::putText(m, t.c_str(), p, font, fontScale, s, thickness, lineType, bottomLeftOrigin);\
\
    return error;\
}\

#pragma endregion template

DLLEXPORT int32_t opencv_getTextSize(const char* text,
                                     const int32_t text_len,
                                     const int32_t font,
                                     const double fontScale,
                                     const int32_t thickness,
                                     int32_t* baseLine,
                                     cv::Size** returnValue)
{
    int32_t error = ERR_OK;

    std::string t(text, text_len);
    const auto ret = cv::getTextSize(t.c_str(), font, fontScale, thickness, baseLine);
    *returnValue = new cv::Size(ret);

    return error;
}

DLLEXPORT int32_t opencv_imread(const char* filename, const int32_t filename_len, int32_t flags, cv::Mat** returnValue)
{
    int32_t error = ERR_OK;

    std::string path(filename, filename_len);
    const auto ret = cv::imread(path, flags);
    *returnValue = new cv::Mat(ret);

    return error;
}

DLLEXPORT void opencv_imshow(const char *winname, const int32_t winname_len, cv::Mat *mat)
{
#ifndef NO_GUI_SUPPORT
    std::string win(winname, winname_len);
    const auto& m = *mat;
    cv::imshow(win, m);
#endif
}

DLLEXPORT int32_t opencv_imwrite(const char* filename, const int32_t filename_len, cv::Mat* mat)
{
    int32_t error = ERR_OK;

    std::string path(filename, filename_len);
    const auto& m = *mat;
    const auto ret = cv::imwrite(path, m);

    return error;
}

DLLEXPORT int32_t opencv_waitKey(const int32_t delay, int32_t* returnValue)
{
    int32_t error = ERR_OK;

#ifndef NO_GUI_SUPPORT
    *returnValue = cv::waitKey(delay);
#endif

    return error;
}

MAKE_DRAWING(int32_t, int32_t)
MAKE_DRAWING(float, float)

MAKE_PUTTEXT(int32_t, int32_t)
MAKE_PUTTEXT(int64_t, int64_t)
MAKE_PUTTEXT(float, float)
MAKE_PUTTEXT(double, double)

#endif