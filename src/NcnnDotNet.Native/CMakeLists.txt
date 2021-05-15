cmake_minimum_required(VERSION 3.0.0)
cmake_policy(SET CMP0053 NEW)
cmake_policy(SET CMP0054 NEW)

set(PROJ_NAME NcnnDotNetNative)
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

project(${PROJ_NAME} VERSION 0.0.0.20200106)

# definitions
option(NCNN_VULKAN "vulkan compute support" OFF)

# OS info
message("-------------------------------------------------------")
message("-- CMAKE_SYSTEM_INFO_FILE: ${CMAKE_SYSTEM_INFO_FILE}")
message("-- CMAKE_SYSTEM_NAME:      ${CMAKE_SYSTEM_NAME}")
message("-- CMAKE_SYSTEM_PROCESSOR: ${CMAKE_SYSTEM_PROCESSOR}")
message("-- CMAKE_SYSTEM:           ${CMAKE_SYSTEM}")
message("-- CMAKE_C_COMPILER:       ${CMAKE_C_COMPILER}")
message("-- CMAKE_CXX_COMPILER:     ${CMAKE_CXX_COMPILER}")
string (REGEX MATCH "\\.el[1-9]" os_version_suffix ${CMAKE_SYSTEM})
message("-- os_version_suffix:      ${os_version_suffix}")
message("-- Project: ${CMAKE_CURRENT_SOURCE_DIR}")
message("-- Binaries: ${CMAKE_CURRENT_BINARY_DIR}")
message("-------------------------------------------------------")

# Version info
set(VERSION_MAJOR 0)
set(VERSION_MINOR 0)
set(VERSION_PATCH 0)
set(VERSION_DATE 20210417)

# Only GCC requires -fPIC
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")
	set(CMAKE_C_FLAGS "-fPIC")
	set(CMAKE_CXX_FLAGS "-fPIC")
endif()

# typeinfo for DataReader_: error: undefined reference to 'typeinfo for ncnn::DataReader'
if ("${CMAKE_SYSTEM_NAME}" STREQUAL "Android")
	set(CMAKE_C_FLAGS "-fPIC")
    # use rtti to use dynamic_cast
	set(CMAKE_CXX_FLAGS "-fPIC")
endif()

# set architecture
if (NOT ${CMAKE_GENERATOR} MATCHES "Visual Studio")
    if ("${ARCH_TYPE}" STREQUAL "32")
        set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -m32")
        set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -m32")
    elseif ("${ARCH_TYPE}" STREQUAL "64")
        set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -m64")
        set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -m64")
    endif()
endif()

# Select the release build type by default
if (NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE "Release" CACHE STRING "" FORCE)
    message("-- CMAKE_BUILD_TYPE: ${CMAKE_BUILD_TYPE}")
endif()

# get root directory path
get_filename_component(NCNNDOTNET_ROOT_DIR "../.." ABSOLUTE)

# include 3rdparty libraries
set(OpenCV_STATIC ON)
find_package(OpenCV REQUIRED)

# link OpenMP (https://github.com/Tencent/ncnn/issues/659)
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")
    find_package(OpenMP REQUIRED)
    if(OPENMP_FOUND)
        set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${OpenMP_C_FLAGS}")
        set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${OpenMP_CXX_FLAGS}")
        set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} ${OpenMP_EXE_LINKER_FLAGS}")
    else()
        message(FATAL_ERROR "Failed to find OpenMP")
    endif()
endif()

# https://github.com/Tencent/ncnn/wiki/FAQ-ncnn-throw-error#undefined-reference-to-__kmpc_xyz_xyz
if ("${CMAKE_SYSTEM_NAME}" STREQUAL "Android")
    FIND_PACKAGE( OpenMP REQUIRED)

    if(OPENMP_FOUND)
        set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${OpenMP_C_FLAGS}")
        set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${OpenMP_CXX_FLAGS}")
        set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} ${OpenMP_EXE_LINKER_FLAGS}")
    else()
        message(FATAL_ERROR "Failed to find OpenMP")
    endif()
endif()

if (MSVC OR MSYS OR MINGW)
    include_directories("$ENV{ncnn_DIR}/include"
                        "$ENV{ncnn_SRC_DIR}/src"
                        "${OpenCV_INCLUDE_DIRS}"
                        "$ENV{VULKAN_SDK}/include")
else()
    include_directories("$ENV{ncnn_DIR}/include"
                        "$ENV{ncnn_SRC_DIR}/src"
                        "${OpenCV_INCLUDE_DIRS}"
                        "$ENV{VULKAN_SDK}/include")
endif()

# Set /bigobj for MSVC
if (${CMAKE_GENERATOR} MATCHES "Visual Studio")
	# C4819: The file contains a character that cannot be represented in the current code page
	# C4503: identifier' : decorated name length exceeded, name was truncated
	set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /wd4819 /wd4503 /bigobj")

	# C4146: unary minus operator applied to unsigned type, result still unsigned
	if ("${CMAKE_SYSTEM_NAME}" STREQUAL "WindowsStore")
		set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /wd4146 /wd4996 /ZW /EHsc")
	endif()
endif()

# set parallel build
if (${CMAKE_GENERATOR} MATCHES "Visual Studio")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /MP")
endif()

# create config file
configure_file(
    ${PROJECT_SOURCE_DIR}/ncnn/config.h.in
    ${PROJECT_SOURCE_DIR}/ncnn/config.h
    @ONLY)

FILE(GLOB_RECURSE HEADERS ${PROJECT_SOURCE_DIR}/ncnn/*.h)
FILE(GLOB_RECURSE SOURCES ${PROJECT_SOURCE_DIR}/ncnn/*.cpp)

if (NCNN_VULKAN)
    configure_file(
        ${CMAKE_CURRENT_SOURCE_DIR}/version-vulkan.rc.in
        ${CMAKE_CURRENT_BINARY_DIR}/version.rc
        @ONLY)
else()
    configure_file(
        ${CMAKE_CURRENT_SOURCE_DIR}/version.rc.in
        ${CMAKE_CURRENT_BINARY_DIR}/version.rc
        @ONLY)
endif()

# STATIC_LIBRARIES
if (MSVC OR MSYS OR MINGW)
    set(STATIC_LIBRARIES
        $ENV{ncnn_DIR}/lib/ncnn.lib
        opencv_highgui
        opencv_imgcodecs
        opencv_imgproc
    )
elseif(APPLE)
    set(STATIC_LIBRARIES
        $ENV{ncnn_DIR}/lib/libncnn.a
        opencv_highgui
        opencv_imgcodecs
        opencv_imgproc
    )
elseif("${CMAKE_SYSTEM_NAME}" STREQUAL "Android")
    set(STATIC_LIBRARIES
        $ENV{ncnn_DIR}/lib/libncnn.a
        ${OpenCV_INSTALL_DIR}/sdk/native/staticlibs/${ANDROID_ABI}/libopencv_world.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/libcpufeatures.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/libIlmImf.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/liblibjasper.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/liblibjpeg.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/liblibpng.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/liblibtiff.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/liblibwebp.a
        ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/libtegra_hal.a
        z
        log
        android
    )
    if (${CMAKE_SYSTEM_PROCESSOR} MATCHES "x86_64|i686")
        list(REMOVE_ITEM STATIC_LIBRARIES
            ${OpenCV_INSTALL_DIR}/sdk/native/3rdparty/libs/${ANDROID_ABI}/libtegra_hal.a
        )
    endif()
elseif(UNIX AND NOT APPLE)
    # centos7 uses glibc-2.17-292.el7.x86_64 but build dev uses latest GCC
    # Therefore, link libmvec as static
    # https://sourceware.org/glibc/wiki/libmvec
    # But it is not linked. why?
    set(STATIC_LIBRARIES
        $ENV{ncnn_DIR}/lib/libncnn.a
        opencv_highgui
        opencv_imgcodecs
        opencv_imgproc
    )
else()
    message(FATAL_ERROR "Failed to link library")
endif()

if (NCNN_VULKAN)
    if (MSVC OR MSYS OR MINGW)
        list(APPEND STATIC_LIBRARIES
            $ENV{VULKAN_SDK}/Lib/vulkan-1.lib
            $ENV{ncnn_DIR}/lib/glslang.lib
            $ENV{ncnn_DIR}/lib/GenericCodeGen.lib
            $ENV{ncnn_DIR}/lib/MachineIndependent.lib
            $ENV{ncnn_DIR}/lib/OGLCompiler.lib
            $ENV{ncnn_DIR}/lib/OSDependent.lib
            $ENV{ncnn_DIR}/lib/SPIRV.lib
        )
    elseif(APPLE)
        list(APPEND STATIC_LIBRARIES
            $ENV{VULKAN_SDK}/lib/libvulkan.dylib
            $ENV{ncnn_DIR}/lib/libglslang.a
            $ENV{ncnn_DIR}/lib/libGenericCodeGen.a
            $ENV{ncnn_DIR}/lib/libMachineIndependent.a
            $ENV{ncnn_DIR}/lib/libOGLCompiler.a
            $ENV{ncnn_DIR}/lib/libOSDependent.a
            $ENV{ncnn_DIR}/lib/libSPIRV.a
        )   
    elseif("${CMAKE_SYSTEM_NAME}" STREQUAL "Android")
        list(APPEND STATIC_LIBRARIES
            vulkan
            $ENV{ncnn_DIR}/lib/libglslang.a
            $ENV{ncnn_DIR}/lib/libGenericCodeGen.a
            $ENV{ncnn_DIR}/lib/libMachineIndependent.a
            $ENV{ncnn_DIR}/lib/libOGLCompiler.a
            $ENV{ncnn_DIR}/lib/libOSDependent.a
            $ENV{ncnn_DIR}/lib/libSPIRV.a
        )
    elseif(UNIX AND NOT APPLE)
        list(APPEND STATIC_LIBRARIES
            $ENV{VULKAN_SDK}/lib/libvulkan.so
            $ENV{ncnn_DIR}/lib/libglslang.a
            $ENV{ncnn_DIR}/lib/libGenericCodeGen.a
            $ENV{ncnn_DIR}/lib/libMachineIndependent.a
            $ENV{ncnn_DIR}/lib/libOGLCompiler.a
            $ENV{ncnn_DIR}/lib/libOSDependent.a
            $ENV{ncnn_DIR}/lib/libSPIRV.a
        )
    endif()
endif()

add_library(${PROJ_NAME} SHARED ${HEADERS}
                                ${SOURCES}
                                ${CMAKE_CURRENT_BINARY_DIR}/version.rc)

target_link_libraries(${PROJ_NAME} ${STATIC_LIBRARIES})

set(CompilerFlags
    CMAKE_CXX_FLAGS
    CMAKE_CXX_FLAGS_DEBUG
    CMAKE_CXX_FLAGS_RELEASE
    CMAKE_C_FLAGS
    CMAKE_C_FLAGS_DEBUG
    CMAKE_C_FLAGS_RELEASE
)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
