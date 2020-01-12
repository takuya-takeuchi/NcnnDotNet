#ifndef _CPP_CPU_CPU_H_
#define _CPP_CPU_CPU_H_

#include "../export.h"
#include <ncnn/cpu.h>
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

#endif