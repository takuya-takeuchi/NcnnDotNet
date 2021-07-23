#ifndef _CPP_CPU_CPU_H_
#define _CPP_CPU_CPU_H_

#include "../export.h"
#include <cpu.h>
#include "../shared.h"

DLLEXPORT int32_t cpu_get_cpu_count()
{
    return ncnn::get_cpu_count();
}

DLLEXPORT int32_t cpu_get_cpu_powersave()
{
    return ncnn::get_cpu_powersave();
}

DLLEXPORT int32_t cpu_set_cpu_powersave(const int powersave)
{
    return ncnn::set_cpu_powersave(powersave);
}

DLLEXPORT int32_t cpu_get_omp_num_threads()
{
    return ncnn::get_omp_num_threads();
}

DLLEXPORT void cpu_set_omp_num_threads(const int num_threads)
{
    ncnn::set_omp_num_threads(num_threads);
}

DLLEXPORT int32_t cpu_get_omp_dynamic()
{
    return ncnn::get_omp_dynamic();
}

DLLEXPORT void cpu_set_omp_dynamic(const int dynamic)
{
    ncnn::set_omp_dynamic(dynamic);
}

#endif