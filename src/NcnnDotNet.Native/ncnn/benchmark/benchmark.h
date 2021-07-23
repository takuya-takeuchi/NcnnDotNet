#ifndef _CPP_BENCHMARK_BENCHMARK_H_
#define _CPP_BENCHMARK_BENCHMARK_H_

#include "../export.h"
#include <benchmark.h>
#include "../shared.h"

DLLEXPORT double benchmark_get_current_time()
{
    return ncnn::get_current_time();
}

#endif