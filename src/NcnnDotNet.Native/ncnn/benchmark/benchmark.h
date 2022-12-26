#ifndef _CPP_BENCHMARK_BENCHMARK_H_
#define _CPP_BENCHMARK_BENCHMARK_H_

#include "../export.h"
#include <benchmark.h>
#include <datareader.h>
#include "../shared.h"

DLLEXPORT double benchmark_get_current_time()
{
    return ncnn::get_current_time();
}

#pragma region DataReaderFromEmpty

// from ncnn\benchmark\benchncnn.cpp
class DataReaderFromEmpty : public ncnn::DataReader
{
public:
    virtual int scan(const char* format, void* p) const
    {
        return 0;
    }
    virtual size_t read(void* buf, size_t size) const
    {
        memset(buf, 0, size);
        return size;
    }
};

DLLEXPORT DataReaderFromEmpty* datareader_DataReaderFromEmpty_new()
{
    return new DataReaderFromEmpty();
}

DLLEXPORT void datareader_DataReaderFromEmpty_delete(DataReaderFromEmpty* reader)
{
    if (reader != nullptr) delete reader;
}

#pragma endregion DataReaderFromEmpty

#endif