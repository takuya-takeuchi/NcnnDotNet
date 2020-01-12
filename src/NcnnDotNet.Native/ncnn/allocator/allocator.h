#ifndef _CPP_ALLOCATOR_ALLOCATOR_H_
#define _CPP_ALLOCATOR_ALLOCATOR_H_

#include "../export.h"
#include <ncnn/allocator.h>
#include "../shared.h"

#pragma region template

#define MAKE_ALLOCATOR(__TYPE__, __TYPENAME__)\
DLLEXPORT int32_t allocator_##__TYPENAME__##_new(ncnn::__TYPE__** returnValue)\
{\
    int32_t error = ERR_OK;\
\
    *returnValue = new ncnn::__TYPE__();\
\
    return error;\
}\
\
DLLEXPORT void allocator_##__TYPENAME__##_delete(ncnn::__TYPE__* allocator)\
{\
    if (allocator != nullptr) delete allocator;\
}\
\
DLLEXPORT void allocator_##__TYPENAME__##_clear(ncnn::__TYPE__* allocator)\
{\
    allocator->clear();\
}\
\
DLLEXPORT void allocator_##__TYPENAME__##_set_size_compare_ratio(ncnn::__TYPE__* allocator, const float scr)\
{\
    allocator->set_size_compare_ratio(scr);\
}

#define MAKE_VKALLOCATOR(__TYPE__, __TYPENAME__)\
DLLEXPORT int32_t allocator_##__TYPENAME__##_new(const ncnn::VulkanDevice* vkdev, ncnn::__TYPE__** returnValue)\
{\
    int32_t error = ERR_OK;\
\
    *returnValue = new ncnn::__TYPE__(vkdev);\
\
    return error;\
}\
\
DLLEXPORT void allocator_##__TYPENAME__##_delete(ncnn::__TYPE__* allocator)\
{\
    if (allocator != nullptr) delete allocator;\
}\
\
DLLEXPORT void allocator_##__TYPENAME__##_clear(ncnn::__TYPE__* allocator)\
{\
    allocator->clear();\
}

#pragma endregion template

MAKE_ALLOCATOR(UnlockedPoolAllocator, UnlockedPoolAllocator)
MAKE_ALLOCATOR(PoolAllocator, PoolAllocator)

#if NCNN_VULKAN

MAKE_VKALLOCATOR(VkBlobBufferAllocator, VkBlobBufferAllocator)
MAKE_VKALLOCATOR(VkWeightBufferAllocator, VkWeightBufferAllocator)
MAKE_VKALLOCATOR(VkStagingBufferAllocator, VkStagingBufferAllocator)
MAKE_VKALLOCATOR(VkWeightStagingBufferAllocator, VkWeightStagingBufferAllocator)

#endif

#endif