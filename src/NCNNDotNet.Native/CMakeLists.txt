cmake_minimum_required(VERSION 3.0.0)
cmake_policy(SET CMP0053 NEW)
cmake_policy(SET CMP0054 NEW)

set(PROJ_NAME NCNNDotNetNative)

project(${PROJ_NAME} VERSION 2019.12.23)

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
set(VERSION_MAJOR 2019)
set(VERSION_MINOR 12)
set(VERSION_PATCH 23)
set(VERSION_DATE 0)

# Only GCC requires -fPIC
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")
	set(CMAKE_C_FLAGS "-fPIC")
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
if (MSVC OR MSYS OR MINGW)
    include_directories("${NCNNDOTNET_ROOT_DIR}/src/ncnn/build-vs2017_Release/install/include")
else()
    find_package( GFlags REQUIRED )
    find_package( OpenCV REQUIRED )
    include_directories("${NCNNDOTNET_ROOT_DIR}/openpose/include")
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

configure_file(
    ${CMAKE_CURRENT_SOURCE_DIR}/ncnn/config.h.in
    ${CMAKE_CURRENT_SOURCE_DIR}/ncnn/config.h
    @ONLY)

configure_file(
    ${CMAKE_CURRENT_SOURCE_DIR}/version.rc.in
    ${CMAKE_CURRENT_BINARY_DIR}/version.rc
    @ONLY)

if (MSVC OR MSYS OR MINGW)
    set(STATIC_LIBRARIES
        optimized ${NCNNDOTNET_ROOT_DIR}/src/ncnn/build-vs2017_Release/install/lib/ncnn.lib
        debug ${NCNNDOTNET_ROOT_DIR}/src/ncnn/build-vs2017_Debug/install/lib/ncnn.lib
    )
elseif(APPLE)
    set(STATIC_LIBRARIES
        opencv_imgproc
        opencv_imgcodecs
        opencv_highgui
        optimized gflags
        optimized ${NCNNDOTNET_ROOT_DIR}/openpose/build_osx/src/openpose/libopenpose.dylib
        debug gflagsd
        debug ${NCNNDOTNET_ROOT_DIR}/openpose/build_osx/src/openpose/libopenposed.dylib
    )
elseif(UNIX AND NOT APPLE)
    set(STATIC_LIBRARIES
        opencv_imgproc
        optimized gflags
        optimized ${NCNNDOTNET_ROOT_DIR}/openpose_linux/build/src/openpose/libopenpose.so
        debug gflagsd
        debug ${NCNNDOTNET_ROOT_DIR}/openpose_linux/build/src/openpose/libopenposed.so
    )
else()
    message(FATAL_ERROR "Failed to link library")
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
