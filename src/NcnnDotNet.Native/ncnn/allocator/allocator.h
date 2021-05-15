#ifndef _CPP_ALLOCATOR_ALLOCATOR_H_
#define _CPP_ALLOCATOR_ALLOCATOR_H_

#include "../export.h"
#include <allocator.h>
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

MAKE_VKALLOCATOR(VkBlobAllocator, VkBlobAllocator)
MAKE_VKALLOCATOR(VkWeightAllocator, VkWeightAllocator)
MAKE_VKALLOCATOR(VkStagingAllocator, VkStagingAllocator)
MAKE_VKALLOCATOR(VkWeightStagingAllocator, VkWeightStagingAllocator)

DLLEXPORT const ncnn::VulkanDevice* allocator_VkAllocator_get_vkdev(ncnn::VkAllocator* allocator)
{
    return allocator->vkdev;
}

DLLEXPORT uint32_t allocator_VkAllocator_get_buffer_memory_type_index(ncnn::VkAllocator* allocator)
{
    return allocator->buffer_memory_type_index;
}

DLLEXPORT uint32_t allocator_VkAllocator_get_image_memory_type_index(ncnn::VkAllocator* allocator)
{
    return allocator->image_memory_type_index;
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
    auto VkBlobAllocator = dynamic_cast<ncnn::VkBlobAllocator*>(allocator);
    if (VkBlobAllocator != nullptr)
    {
        *type = vkallocator_type::VkBlobAllocator;
        return true;
    }

    auto VkWeightAllocator = dynamic_cast<ncnn::VkWeightAllocator*>(allocator);
    if (VkWeightAllocator != nullptr)
    {
        *type = vkallocator_type::VkWeightAllocator;
        return true;
    }
    auto VkStagingAllocator = dynamic_cast<ncnn::VkStagingAllocator*>(allocator);
    if (VkStagingAllocator != nullptr)
    {
        *type = vkallocator_type::VkStagingAllocator;
        return true;
    }

    auto VkWeightStagingAllocator = dynamic_cast<ncnn::VkWeightStagingAllocator*>(allocator);
    if (VkWeightStagingAllocator != nullptr)
    {
        *type = vkallocator_type::VkWeightStagingAllocator;
        return true;
    }

    return false;
}

#endif

#endif