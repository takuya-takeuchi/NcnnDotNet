echo off

@set CONFIG=%1
@set CURRENT=%cd%
@cd ..
@set ROOT=%cd%
@cd "%CURRENT%"

@set BUILDDIR=build-vs2017
@set OpenCV_DIR=%cd%\%BUILDDIR%\opencv
@set ncnn_DIR=%cd%\%BUILDDIR%\ncnn\install

@call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars64.bat"

@mkdir %BUILDDIR%
@cd %BUILDDIR%
@cmake -G "Visual Studio 15 2017" -A x64 ^
                                  -T host=x64 ^
                                  -D BUILD_SHARED_LIBS=ON ^
                                  -D USE_NCNN_VULKAN=ON ^
                                  "%CURRENT%"
@cmake --build . --config %CONFIG%