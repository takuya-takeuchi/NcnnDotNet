echo off

@set CONFIG=%1
IF "%CONFIG%"=="Debug" (
    set PROTOBUFLIB=libprotobufd.lib
) else (
    set PROTOBUFLIB=libprotobuf.lib
)
@set CURRENT=%cd%
@cd ..
@set ROOT=%cd%
@cd "%CURRENT%"
@set PROTOBUF=D:\Works\Lib\Google-protobuf\3.4.0
@set OPENCV=D:\Works\Lib\OpenCV\3.4.1\sources
@set BUILDDIR=build-vs2017

@call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars64.bat"

@cd "%CURRENT%"
@mkdir %BUILDDIR%\protobuf
@cd %BUILDDIR%\protobuf
@cmake -G "NMake Makefiles" -D CMAKE_BUILD_TYPE=%CONFIG% ^
                            -D BUILD_SHARED_LIBS=OFF ^
                            -D CMAKE_INSTALL_PREFIX="%cd%/install" ^
                            -D protobuf_BUILD_TESTS=OFF ^
                            -D protobuf_MSVC_STATIC_RUNTIME=OFF ^
                            "%PROTOBUF%\cmake"
@nmake
@nmake install

@cd "%CURRENT%"
@mkdir %BUILDDIR%\opencv
@cd %BUILDDIR%\opencv
@cmake -G "NMake Makefiles" -D CMAKE_BUILD_TYPE=Release ^
                            -D BUILD_SHARED_LIBS=OFF ^
                            -D BUILD_WITH_STATIC_CRT=OFF ^
                            -D CMAKE_INSTALL_PREFIX="%cd%/install" ^
                            -D BUILD_SHARED_LIBS=OFF ^
                            -D BUILD_opencv_world=ON ^
                            -D BUILD_opencv_java=OFF ^
                            -D BUILD_opencv_python=OFF ^
                            -D BUILD_opencv_python2=OFF ^
                            -D BUILD_opencv_python3=OFF ^
                            -D BUILD_PERF_TESTS=OFF ^
                            -D BUILD_TESTS=OFF ^
                            -D BUILD_DOCS=OFF ^
                            -D WITH_CUDA=OFF ^
                            -D BUILD_PROTOBUF=OFF ^
                            -D WITH_PROTOBUF=OFF ^
                            -D WITH_IPP=OFF ^
                            -D WITH_FFMPEG=OFF ^
                            "%OPENCV%"
@nmake
@nmake install

@cd "%CURRENT%"
@mkdir %BUILDDIR%\ncnn
@cd %BUILDDIR%\ncnn
@cmake -G "NMake Makefiles" -D CMAKE_BUILD_TYPE=%CONFIG% ^
                            -D BUILD_SHARED_LIBS=OFF ^
                            -D CMAKE_INSTALL_PREFIX="%cd%/install" ^
                            -D Protobuf_INCLUDE_DIR="%CURRENT%\%BUILDDIR%\protobuf/install/include" ^
                            -D Protobuf_LIBRARIES="%CURRENT%\%BUILDDIR%\protobuf/install/lib/%PROTOBUFLIB%" ^
                            -D Protobuf_PROTOC_EXECUTABLE="%CURRENT%\%BUILDDIR%\protobuf/install/bin/protoc.exe" ^
                            -D NCNN_VULKAN=ON ^
                            -D NCNN_OPENCV=OFF ^
                            "%ROOT%\ncnn"
@nmake
@nmake install