#ifndef _CPP_CSTD_STRING_H_
#define _CPP_CSTD_STRING_H_

#include "../export.h"
#include <string.h>
#include "../shared.h"

DLLEXPORT void* cstd_memcpy(void* buf1, const void* buf2, size_t n)
{
    return memcpy(buf1, buf2, n);
}

DLLEXPORT void cstd_memset(void* buf, uint8_t ch, size_t n)
{
    memset(buf, ch, n);
}

#endif