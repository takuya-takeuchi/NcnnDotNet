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
del bin\Debug\%APPDIR%\VkLayer_device_simulation.dll
del bin\Debug\%APPDIR%\VkLayer_gfxreconstruct.dll
del bin\Debug\%APPDIR%\VkLayer_khronos_synchronization2.dll
del bin\Debug\%APPDIR%\VkLayer_khronos_validation.dll
del bin\Debug\%APPDIR%\VkLayer_monitor.dll
del bin\Debug\%APPDIR%\VkLayer_screenshot.dll
rem del bin\Debug\%APPDIR%\VkLayer_api_dump.dll
rem del bin\Debug\%APPDIR%\VkLayer_assistant_layer.dll
rem del bin\Debug\%APPDIR%\VkLayer_core_validation.dll
rem del bin\Debug\%APPDIR%\VkLayer_device_simulation.dll
rem del bin\Debug\%APPDIR%\VkLayer_khronos_validation.dll
rem del bin\Debug\%APPDIR%\VkLayer_monitor.dll
rem del bin\Debug\%APPDIR%\VkLayer_object_lifetimes.dll
rem del bin\Debug\%APPDIR%\VkLayer_screenshot.dll
rem del bin\Debug\%APPDIR%\VkLayer_stateless_validation.dll
rem del bin\Debug\%APPDIR%\VkLayer_thread_safety.dll
rem del bin\Debug\%APPDIR%\VkLayer_unique_objects.dll
rem del bin\Debug\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\Debug\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\Debug\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\Debug\%APPDIR%\VkLayer_gfxreconstruct.dll "%VULKAN_SDK%\Bin\VkLayer_gfxreconstruct.dll"
mklink bin\Debug\%APPDIR%\VkLayer_khronos_synchronization2.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_synchronization2.dll"
mklink bin\Debug\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\Debug\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\Debug\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
rem mklink bin\Debug\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"


del bin\Release\%APPDIR%\VkLayer_api_dump.dll
del bin\Release\%APPDIR%\VkLayer_device_simulation.dll
del bin\Release\%APPDIR%\VkLayer_gfxreconstruct.dll
del bin\Release\%APPDIR%\VkLayer_khronos_synchronization2.dll
del bin\Release\%APPDIR%\VkLayer_khronos_validation.dll
del bin\Release\%APPDIR%\VkLayer_monitor.dll
del bin\Release\%APPDIR%\VkLayer_screenshot.dll
rem del bin\Release\%APPDIR%\VkLayer_api_dump.dll
rem del bin\Release\%APPDIR%\VkLayer_assistant_layer.dll
rem del bin\Release\%APPDIR%\VkLayer_core_validation.dll
rem del bin\Release\%APPDIR%\VkLayer_device_simulation.dll
rem del bin\Release\%APPDIR%\VkLayer_khronos_validation.dll
rem del bin\Release\%APPDIR%\VkLayer_monitor.dll
rem del bin\Release\%APPDIR%\VkLayer_object_lifetimes.dll
rem del bin\Release\%APPDIR%\VkLayer_screenshot.dll
rem del bin\Release\%APPDIR%\VkLayer_stateless_validation.dll
rem del bin\Release\%APPDIR%\VkLayer_thread_safety.dll
rem del bin\Release\%APPDIR%\VkLayer_unique_objects.dll
rem del bin\Release\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\Release\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\Release\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\Release\%APPDIR%\VkLayer_gfxreconstruct.dll "%VULKAN_SDK%\Bin\VkLayer_gfxreconstruct.dll"
mklink bin\Release\%APPDIR%\VkLayer_khronos_synchronization2.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_synchronization2.dll"
mklink bin\Release\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\Release\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\Release\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
rem mklink bin\Release\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"

mkdir bin\%ARCH%\Debug\%APPDIR%
del bin\%ARCH%\Debug\%APPDIR%\%DLL%
del bin\%ARCH%\Debug\%APPDIR%\%PDB%
mklink bin\%ARCH%\Debug\%APPDIR%\%DLL% %NATIVEROOT%_d\%DLL%
mklink bin\%ARCH%\Debug\%APPDIR%\%PDB% %NATIVEROOT%_d\%PDB%
mkdir bin\%ARCH%\Release\%APPDIR%
del bin\%ARCH%\Release\%APPDIR%\%DLL%
mklink bin\%ARCH%\Release\%APPDIR%\%DLL% %NATIVEROOT%\%DLL%

del bin\%ARCH%\Debug\%APPDIR%\VkLayer_api_dump.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_device_simulation.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_gfxreconstruct.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_synchronization2.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_validation.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_monitor.dll
del bin\%ARCH%\Debug\%APPDIR%\VkLayer_screenshot.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_api_dump.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_assistant_layer.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_core_validation.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_device_simulation.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_validation.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_monitor.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_object_lifetimes.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_screenshot.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_stateless_validation.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_thread_safety.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_unique_objects.dll
rem del bin\%ARCH%\Debug\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_gfxreconstruct.dll "%VULKAN_SDK%\Bin\VkLayer_gfxreconstruct.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_synchronization2.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_synchronization2.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
rem mklink bin\%ARCH%\Debug\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"


del bin\%ARCH%\Release\%APPDIR%\VkLayer_api_dump.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_device_simulation.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_gfxreconstruct.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_synchronization2.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_validation.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_monitor.dll
del bin\%ARCH%\Release\%APPDIR%\VkLayer_screenshot.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_api_dump.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_assistant_layer.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_core_validation.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_device_simulation.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_validation.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_monitor.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_object_lifetimes.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_screenshot.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_stateless_validation.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_thread_safety.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_unique_objects.dll
rem del bin\%ARCH%\Release\%APPDIR%\VkLayer_vktrace_layer.dll
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_gfxreconstruct.dll "%VULKAN_SDK%\Bin\VkLayer_gfxreconstruct.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_synchronization2.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_synchronization2.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_api_dump.dll "%VULKAN_SDK%\Bin\VkLayer_api_dump.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_assistant_layer.dll "%VULKAN_SDK%\Bin\VkLayer_assistant_layer.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_core_validation.dll "%VULKAN_SDK%\Bin\VkLayer_core_validation.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_device_simulation.dll "%VULKAN_SDK%\Bin\VkLayer_device_simulation.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_khronos_validation.dll "%VULKAN_SDK%\Bin\VkLayer_khronos_validation.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_monitor.dll "%VULKAN_SDK%\Bin\VkLayer_monitor.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_object_lifetimes.dll "%VULKAN_SDK%\Bin\VkLayer_object_lifetimes.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_screenshot.dll "%VULKAN_SDK%\Bin\VkLayer_screenshot.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_stateless_validation.dll "%VULKAN_SDK%\Bin\VkLayer_stateless_validation.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_thread_safety.dll "%VULKAN_SDK%\Bin\VkLayer_thread_safety.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_unique_objects.dll "%VULKAN_SDK%\Bin\VkLayer_unique_objects.dll"
rem mklink bin\%ARCH%\Release\%APPDIR%\VkLayer_vktrace_layer.dll "%VULKAN_SDK%\Bin\VkLayer_vktrace_layer.dll"