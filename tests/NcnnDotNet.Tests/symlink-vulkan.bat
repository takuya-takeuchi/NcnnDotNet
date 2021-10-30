@set APPDIR=netcoreapp3.1
@set DLL=NcnnDotNetNative.dll
@set PDB=NcnnDotNetNative.pdb
@set ARCH=x64

pushd ..\..
@set ROOT=%cd%
popd

@set NATIVEROOT="%ROOT%\src\NcnnDotNet.Native\build_win_desktop_vulkan_x64"

mkdir bin\Debug\%APPDIR%
del bin\Debug\%APPDIR%\%DLL%
del bin\Debug\%APPDIR%\%PDB%
mklink bin\Debug\%APPDIR%\%DLL% %NATIVEROOT%_d\%DLL%
mklink bin\Debug\%APPDIR%\%PDB% %NATIVEROOT%_d\%PDB%
mkdir bin\Release\%APPDIR%
del bin\Release\%APPDIR%\%DLL%
mklink bin\Release\%APPDIR%\%DLL% %NATIVEROOT%\%DLL%

del bin\Debug\%APPDIR%\VkLayer_api_dump.dll
del bin\Debug\%APPDIR%\VkLayer_assistant_layer.dll
del bin\Debug\%APPDIR%\VkLayer_core_validation.dll
del bin\Debug\%APPDIR%\VkLayer_device_simulation.dll
del bin\Debug\%APPDIR%\VkLayer_khronos_validation.dll
del bin\Debug\%APPDIR%\VkLayer_monitor.dll
del bin\Debug\%APPDIR%\VkLayer_object_lifetimes.dll
del bin\Debug\%APPDIR%\VkLayer_screenshot.dll
del bin\Debug\%APPDIR%\VkLayer_stateless_validation.dll
del bin\Debug\%APPDIR%\VkLayer_thread_safety.dll
del bin\Debug\%APPDIR%\VkLayer_unique_objects.dll
del bin\Debug\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\Debug\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\Debug\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
mklink bin\Debug\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
mklink bin\Debug\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\Debug\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\Debug\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\Debug\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
mklink bin\Debug\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
mklink bin\Debug\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
mklink bin\Debug\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
mklink bin\Debug\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
mklink bin\Debug\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"

del bin\Release\%APPDIR%\VkLayer_api_dump.dll
del bin\Release\%APPDIR%\VkLayer_assistant_layer.dll
del bin\Release\%APPDIR%\VkLayer_core_validation.dll
del bin\Release\%APPDIR%\VkLayer_device_simulation.dll
del bin\Release\%APPDIR%\VkLayer_khronos_validation.dll
del bin\Release\%APPDIR%\VkLayer_monitor.dll
del bin\Release\%APPDIR%\VkLayer_object_lifetimes.dll
del bin\Release\%APPDIR%\VkLayer_screenshot.dll
del bin\Release\%APPDIR%\VkLayer_stateless_validation.dll
del bin\Release\%APPDIR%\VkLayer_thread_safety.dll
del bin\Release\%APPDIR%\VkLayer_unique_objects.dll
del bin\Release\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\Release\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\Release\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
mklink bin\Release\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
mklink bin\Release\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\Release\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\Release\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\Release\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
mklink bin\Release\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
mklink bin\Release\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
mklink bin\Release\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
mklink bin\Release\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
mklink bin\Release\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"

mkdir bin\%ARCH%\Debug\%APPDIR%
del bin\%ARCH%\Debug\%APPDIR%\%DLL%
del bin\%ARCH%\Debug\%APPDIR%\%PDB%
mklink bin\%ARCH%\Debug\%APPDIR%\%DLL% %NATIVEROOT%_d\%DLL%
mklink bin\%ARCH%\Debug\%APPDIR%\%PDB% %NATIVEROOT%_d\%PDB%
mkdir bin\%ARCH%\Release\%APPDIR%
del bin\%ARCH%\Release\%APPDIR%\%DLL%
mklink bin\%ARCH%\Release\%APPDIR%\%DLL% %NATIVEROOT%\%DLL%

del bin\%ARCH%\Debug\%APPDIR%\VkLayer_api_dump.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_assistant_layer.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_core_validation.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_device_simulation.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_validation.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_monitor.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_object_lifetimes.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_screenshot.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_stateless_validation.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_thread_safety.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_unique_objects.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"

del bin\%ARCH%\Release\%APPDIR%\VkLayer_api_dump.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_assistant_layer.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_core_validation.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_device_simulation.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_validation.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_monitor.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_object_lifetimes.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_screenshot.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_stateless_validation.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_thread_safety.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_unique_objects.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"