@set APPDIR=netcoreapp3.1
@set DLL=NcnnDotNetNative.dll
@set PDB=NcnnDotNetNative.pdb
@set ARCH=x64

pushd ..\..
@set ROOT=%cd%
popd

@set NATIVEROOT="%ROOT%\src\NcnnDotNet.Native\build_win_desktop_cpu_x64"

mkdir bin\Debug\%APPDIR%
del bin\Debug\%APPDIR%\%DLL%
del bin\Debug\%APPDIR%\%PDB%
mklink bin\Debug\%APPDIR%\%DLL% %NATIVEROOT%_d\%DLL%
mklink bin\Debug\%APPDIR%\%PDB% %NATIVEROOT%_d\%PDB%
mkdir bin\Release\%APPDIR%
del bin\Release\%APPDIR%\%DLL%
mklink bin\Release\%APPDIR%\%DLL% %NATIVEROOT%\%DLL%

mkdir bin\%ARCH%\Debug\%APPDIR%
del bin\%ARCH%\Debug\%APPDIR%\%DLL%
del bin\%ARCH%\Debug\%APPDIR%\%PDB%
mklink bin\%ARCH%\Debug\%APPDIR%\%DLL% %NATIVEROOT%_d\%DLL%
mklink bin\%ARCH%\Debug\%APPDIR%\%PDB% %NATIVEROOT%_d\%PDB%
mkdir bin\%ARCH%\Release\%APPDIR%
del bin\%ARCH%\Release\%APPDIR%\%DLL%
mklink bin\%ARCH%\Release\%APPDIR%\%DLL% %NATIVEROOT%\%DLL%