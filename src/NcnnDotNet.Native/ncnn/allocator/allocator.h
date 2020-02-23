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

DLLEXPORT const ncnn::VulkanDevice* allocator_VkAllocator_get_vkdev(ncnn::VkAllocator* allocator)
{
    return allocator->vkdev;
}

DLLEXPORT uint32_t allocator_VkAllocator_get_memory_type_index(ncnn::VkAllocator* allocator)
{
    return allocator->memory_type_index;
}

DLLEXPORT bool allocator_VkAllocator_get_mappable(ncnn::VkAllocator* allocator)
{
    return allocator->mappable;
}

DLLEXPORT bool allocator_VkAllocator_get_coherent(ncnn::VkAllocator* allocator)
{
    return allocator->coherent;
}

#endif

DLLEXPORT bool allocator_Allocator_dynamic_cast(ncnn::Allocator* allocator, allocator_type* type)
{
    auto unlockedPoolAllocator = dynamic_cast<ncnn::UnlockedPoolAllocator*>(allocator);
    if (unlockedPoolAllocator != nullptr)
    {
        *type = allocator_type::UnlockedPoolAllocator;
        return true;
    }

    auto poolAllocator = dynamic_cast<ncnn::PoolAllocator*>(allocator);
    if (poolAllocator != nullptr)
    {
        *type = allocator_type::PoolAllocator;
        return true;
    }

    return false;
}

#if NCNN_VULKAN

DLLEXPORT bool allocator_VkAllocator_dynamic_cast(ncnn::VkAllocator* allocator, vkallocator_type* type)
{
    auto vkBlobBufferAllocator = dynamic_cast<ncnn::VkBlobBufferAllocator*>(allocator);
    if (vkBlobBufferAllocator != nullptr)
    {
        *type = vkallocator_type::VkBlobBufferAllocator;
        return true;
    }

    auto vkWeightBufferAllocator = dynamic_cast<ncnn::VkWeightBufferAllocator*>(allocator);
    if (vkWeightBufferAllocator != nullptr)
    {
        *type = vkallocator_type::VkWeightBufferAllocator;
        return true;
    }
    auto vkStagingBufferAllocator = dynamic_cast<ncnn::VkStagingBufferAllocator*>(allocator);
    if (vkStagingBufferAllocator != nullptr)
    {
        *type = vkallocator_type::VkStagingBufferAllocator;
        return true;
    }

    auto vkWeightStagingBufferAllocator = dynamic_cast<ncnn::VkWeightStagingBufferAllocator*>(allocator);
    if (vkWeightStagingBufferAllocator != nullptr)
    {
        *type = vkallocator_type::VkWeightStagingBufferAllocator;
        return true;
    }

    return false;
}

#endif

#endif