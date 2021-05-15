#ifndef _CPP_SHARED_H_
#define _CPP_SHARED_H_

#include <platform.h>
#include <cstdint>
#include <string>
#include <vector>

#pragma region error-codegeneral

#pragma region general

#define ERR_OK                                                            0x00000000

// General
#define ERR_GENERAL_ERROR                                                 0x76000000
#define ERR_GENERAL_FILE_IO                         -(ERR_GENERAL_ERROR | 0x00000001)
#define ERR_GENERAL_IMAGE_LOAD                      -(ERR_GENERAL_ERROR | 0x00000002)
#define ERR_GENERAL_SERIALIZATION                   -(ERR_GENERAL_ERROR | 0x00000003)
#define ERR_GENERAL_INVALID_PARAMETER               -(ERR_GENERAL_ERROR | 0x00000004)
#define ERR_GENERAL_NOT_SUPPORT                     -(ERR_GENERAL_ERROR | 0x00000005)

#pragma endregion general

#pragma endregion error-codegeneral


#pragma endregion template

enum struct allocator_type : int
{
    UnlockedPoolAllocator = 0,
    PoolAllocator
};

enum struct vkallocator_type : int
{
    VkBlobAllocator = 0,
    VkWeightAllocator,
    VkStagingAllocator,
    VkWeightStagingAllocator
};

#endif