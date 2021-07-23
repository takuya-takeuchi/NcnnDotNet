class BuildTarget
{
   [string] $Platform
   [string] $Target
   [int]    $Architecture
   [string] $Postfix
   [string] $RID

   BuildTarget( [string]$Platform,
                [string]$Target,
                [int]   $Architecture,
                [string]$RID,
                [string]$Postfix = ""
              )
   {
      $this.Platform = $Platform
      $this.Target = $Target
      $this.Architecture = $Architecture
      $this.Postfix = $Postfix
      $this.RID = $RID
   }

   [string] $OperatingSystem
   [string] $Distribution
   [string] $DistributionVersion

   [string] $CudaVersion

   [string] $AndroidVersion
   [string] $AndroidNativeApiLevel
}

class Config
{

   $ConfigurationArray =
   @(
      "Debug",
      "Release"
   )

   $TargetArray =
   @(
      "cpu",
      "vulkan",
      "arm"
   )

   $PlatformArray =
   @(
      "desktop",
      "android",
      "ios",
      "uwp"
   )

   $ArchitectureArray =
   @(
      32,
      64
   )

   $VisualStudio = "Visual Studio 15 2017"

   static $BuildLibraryWindowsHash =
   @{
      "NcnnDotNet.Native"     = "NcnnDotNetNative.dll";
   }

   static $BuildLibraryLinuxHash =
   @{
      "NcnnDotNet.Native"     = "libNcnnDotNetNative.so";
   }

   static $BuildLibraryOSXHash =
   @{
      "NcnnDotNet.Native"     = "libNcnnDotNetNative.dylib";
   }

   static $BuildLibraryIOSHash =
   @{
      "NcnnDotNet.Native"     = "libNcnnDotNetNative_merged.a";
   }

   [string]   $_Root
   [string]   $_Configuration
   [int]      $_Architecture
   [string]   $_Target
   [string]   $_Platform
   [string]   $_VulkanSDKDirectory
   [int]      $_CudaVersion
   [string]   $_AndroidABI
   [string]   $_AndroidNativeAPILevel
   [string]   $_OSXArchitectures

   #***************************************
   # Arguments
   #  %1: Root directory of NcnnDotNet
   #  %2: Build Configuration (Release/Debug)
   #  %3: Target (cpu/vulkan/arm)
   #  %4: Architecture (32/64)
   #  %5: Platform (desktop/android/ios/uwp)
   #  %6: Optional Argument
   #    Reserved
   #***************************************
   Config(  [string]$Root,
            [string]$Configuration,
            [string]$Target,
            [int]   $Architecture,
            [string]$Platform,
            [string]$Option
         )
   {
      if ($this.ConfigurationArray.Contains($Configuration) -eq $False)
      {
         $candidate = $this.ConfigurationArray -join "/"
         Write-Host "Error: Specify build configuration [${candidate}]" -ForegroundColor Red
         exit -1
      }

      if ($this.TargetArray.Contains($Target) -eq $False)
      {
         $candidate = $this.TargetArray -join "/"
         Write-Host "Error: Specify Target [${candidate}]" -ForegroundColor Red
         exit -1
      }

      if ($this.ArchitectureArray.Contains($Architecture) -eq $False)
      {
         $candidate = $this.ArchitectureArray -join "/"
         Write-Host "Error: Specify Architecture [${candidate}]" -ForegroundColor Red
         exit -1
      }

      if ($this.PlatformArray.Contains($Platform) -eq $False)
      {
         $candidate = $this.PlatformArray -join "/"
         Write-Host "Error: Specify Platform [${candidate}]" -ForegroundColor Red
         exit -1
      }

      switch ($Target)
      {
         "vulkan"
         {
            $this._VulkanSDKDirectory = $env:VULKAN_SDK
         }
      }

      $this._Root = $Root
      $this._Configuration = $Configuration
      $this._Architecture = $Architecture
      $this._Target = $Target
      $this._Platform = $Platform

      switch ($Platform)
      {
         "android"
         {
            $decoded = [Config]::Base64Decode($Option)
            $setting = ConvertFrom-Json $decoded
            $this._AndroidABI            = $setting.ANDROID_ABI
            $this._AndroidNativeAPILevel = $setting.ANDROID_NATIVE_API_LEVEL
         }
         "ios"
         {
            $this._OSXArchitectures = $Option
         }
      }
   }

   static [string] Base64Encode([string]$text)
   {
      $byte = ([System.Text.Encoding]::Default).GetBytes($text)
      return [Convert]::ToBase64String($byte)
   }

   static [string] Base64Decode([string]$base64)
   {
      $byte = [System.Convert]::FromBase64String($base64)
      return [System.Text.Encoding]::Default.GetString($byte)
   }

   static [hashtable] GetBinaryLibraryWindowsHash()
   {
      return [Config]::BuildLibraryWindowsHash
   }

   static [hashtable] GetBinaryLibraryOSXHash()
   {
      return [Config]::BuildLibraryOSXHash
   }

   static [hashtable] GetBinaryLibraryLinuxHash()
   {
      return [Config]::BuildLibraryLinuxHash
   }

   static [hashtable] GetBinaryLibraryIOSHash()
   {
      return [Config]::BuildLibraryIOSHash
   }

   [string] GetRootDir()
   {
      return $this._Root
   }

   [string] GetNcnnRootDir()
   {
      return   Join-Path $this.GetRootDir() src |
               Join-Path -ChildPath ncnn
   }

   [string] GetToolchainDir()
   {
      return   Join-Path $this.GetRootDir() src |
               Join-Path -ChildPath toolchains
   }

   [string] GetOpenCVRootDir()
   {
      return   Join-Path $this.GetRootDir() src |
               Join-Path -ChildPath opencv
   }

   [string] GetProtobufRootDir()
   {
      return   Join-Path $this.GetRootDir() src |
               Join-Path -ChildPath protobuf
   }

   [string] GetNugetDir()
   {
      return   Join-Path $this.GetRootDir() nuget
   }

   [int] GetArchitecture()
   {
      return $this._Architecture
   }

   [string] GetConfigurationName()
   {
      return $this._Configuration
   }

   [string] GetAndroidABI()
   {
      return $this._AndroidABI
   }

   [string] GetAndroidNativeAPILevel()
   {
      return $this._AndroidNativeAPILevel
   }

   [string] GetArtifactDirectoryName()
   {
      $target = $this._Target
      $platform = $this._Platform
      $name = ""

      switch ($platform)
      {
         "desktop"
         {
            $name = $target
         }
         "android"
         {
            $name = $platform
         }
         "ios"
         {
            $name = $platform
         }
         "uwp"
         {
            $name = Join-Path $platform $target
         }
      }

      return $name
   }

   [string] GetOSName()
   {
      $os = ""

      if ($global:IsWindows)
      {
         $os = "win"
      }
      elseif ($global:IsMacOS)
      {
         if (![string]::IsNullOrEmpty($this._OSXArchitectures))
         {
            $os = "ios"
         }
         else
         {
            $os = "osx"
         }
      }
      elseif ($global:IsLinux)
      {
         $os = "linux"
      }
      else
      {
         Write-Host "Error: This plaform is not support" -ForegroundColor Red
         exit -1
      }

      return $os
   }

   [string] GetVulkanSDKDirectory()
   {
      return [string]$this._VulkanSDKDirectory
   }

   [string] GetArchitectureName()
   {
      $arch = ""
      $target = $this._Target
      $architecture = $this._Architecture

      if ($target -eq "arm")
      {
         if ($architecture -eq 32)
         {
            $arch = "arm"
         }
         elseif ($architecture -eq 64)
         {
            $arch = "arm64"
         }
      }
      else
      {
         if ($architecture -eq 32)
         {
            $arch = "x86"
         }
         elseif ($architecture -eq 64)
         {
            $arch = "x64"
         }
      }

      return $arch
   }

   [string] GetTarget()
   {
      return $this._Target
   }

   [string] GetPlatform()
   {
      return $this._Platform
   }

   [string] GetRootStoreDriectory()
   {
      return $env:CIBuildDir
   }

   [string] GetStoreDriectory([string]$CMakefileDir)
   {
      $DirectoryName = Split-Path $CMakefileDir -leaf
      $buildDir = $this.GetRootStoreDriectory()
      if (!(Test-Path($buildDir)))
      {
         return $CMakefileDir
      }

      return Join-Path $buildDir "DlibDotNet" | `
             Join-Path -ChildPath $DirectoryName
   }

   [bool] HasStoreDriectory()
   {
      $buildDir = $this.GetRootStoreDriectory()
      return Test-Path($buildDir)
   }

   [string] GetBuildDirectoryName([string]$os="")
   {
      if (![string]::IsNullOrEmpty($os))
      {
         $osname = $os
      }
      elseif (![string]::IsNullOrEmpty($env:TARGETRID))
      {
         $osname = $env:TARGETRID
      }
      else
      {
         $osname = $this.GetOSName()
      }

      $target = $this._Target
      $platform = $this._Platform
      $architecture = $this.GetArchitectureName()

      switch ($platform)
      {
         "android"
         {
            $architecture = $this._AndroidABI
         }
         "ios"
         {
            $architecture = $this._OSXArchitectures
         }
      }

      if ($this._Configuration -eq "Debug")
      {
         return "build_${osname}_${platform}_${target}_${architecture}_d"
      }
      else
      {
         return "build_${osname}_${platform}_${target}_${architecture}"
      }
   }

   [string] GetVisualStudio()
   {
      return $this.VisualStudio
   }

   [string] GetVisualStudioArchitecture()
   {
      $architecture = $this._Architecture
      $target = $this._Target

      if ($target -eq "arm")
      {
         if ($architecture -eq 32)
         {
            return "ARM"
         }
         elseif ($architecture -eq 64)
         {
            return "ARM64"
         }
      }
      else
      {
         if ($architecture -eq 32)
         {
            return "Win32"
         }
         elseif ($architecture -eq 64)
         {
            return "x64"
         }
      }

      Write-Host "${architecture} and ${target} do not support" -ForegroundColor Red
      exit -1
   }

   [string] GetAVXINSTRUCTIONS()
   {
      return "ON"
   }

   [string] GetSSE4INSTRUCTIONS()
   {
      return "ON"
   }

   [string] GetSSE2INSTRUCTIONS()
   {
      return "OFF"
   }

   [string] GetToolchainFile()
   {
      $architecture = $this._Architecture
      $target = $this._Target
      $toolchainDir = $this.GetToolchainDir()
      $toolchain = Join-Path $toolchainDir "empty.cmake"

      if ($global:IsLinux)
      {
         if ($target -eq "arm")
         {
            if ($architecture -eq 64)
            {
               $toolchain = Join-Path $toolchainDir "aarch64-linux-gnu.toolchain.cmake"
            }
         }
      }
      else
      {
         $Platform = $this._Platform
         switch ($Platform)
         {
            "ios"
            {
               $osxArchitectures = $this.GetOSXArchitectures()
               $toolchain = Join-Path $toolchainDir "${osxArchitectures}-ios.cmake"
            }
         }
      }

      return $toolchain
   }

   [string] GetDeveloperDir()
   {
      return $env:DEVELOPER_DIR
   }

   [string] GetOSXArchitectures()
   {
      return $this._OSXArchitectures
   }

   [string] GetIOSSDK([string]$osxArchitectures, [string]$developerDir)
   {
      switch ($osxArchitectures)
      {
         "arm64e"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "arm64"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "arm"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "armv7"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "armv7s"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "i386"
         {
            return "${developerDir}/Platforms/iPhoneSimulator.platform/Developer/SDKs/iPhoneSimulator.sdk"
         }
         "x86_64"
         {
            return "${developerDir}/Platforms/iPhoneSimulator.platform/Developer/SDKs/iPhoneSimulator.sdk"
         }
      }
      return $this._OSXArchitectures
   }

   static [bool] Build([string]$root, [bool]$docker, [hashtable]$buildHashTable, [BuildTarget]$buildTarget)
   {
      $current = $PSScriptRoot

      $platform              = $buildTarget.Platform
      $target                = $buildTarget.Target
      $architecture          = $buildTarget.Architecture
      $postfix               = $buildTarget.Postfix
      $rid                   = $buildTarget.RID
      $operatingSystem       = $buildTarget.OperatingSystem
      $distribution          = $buildTarget.Distribution
      $distributionVersion   = $buildTarget.DistributionVersion
      $cudaVersion           = $buildTarget.CudaVersion
      $androidVersion        = $buildTarget.AndroidVersion
      $androidNativeApiLevel = $buildTarget.AndroidNativeApiLevel

      $option = ""

      $sourceRoot = Join-Path $root src

      if ($docker -eq $True)
      {
         $dockerDir = Join-Path $root docker

         Set-Location -Path $dockerDir

         $dockerFileDir = Join-Path $dockerDir build  | `
                          Join-Path -ChildPath $distribution | `
                          Join-Path -ChildPath $distributionVersion

         if ($platform -eq "android")
         {
            $setting =
            @{
               'ANDROID_ABI' = $rid;
               'ANDROID_NATIVE_API_LEVEL' = $androidNativeApiLevel
            }
            $option = [Config]::Base64Encode((ConvertTo-Json -Compress $setting))

            $dockername = "ncnndotnet/build/$distribution/$distributionVersion/android/$androidVersion"
            $imagename  = "ncnndotnet/devel/$distribution/$distributionVersion/android/$androidVersion"
         }
         else
         {
            if ($target -ne "cuda")
            {
               $option = ""

               $dockername = "ncnndotnet/build/$distribution/$distributionVersion/$Target" + $postfix
               $imagename  = "ncnndotnet/devel/$distribution/$distributionVersion/$Target" + $postfix
            }
            else
            {
               $option = $cudaVersion

               $cudaVersion = ($cudaVersion / 10).ToString("0.0")
               $dockername = "ncnndotnet/build/$distribution/$distributionVersion/$Target/$cudaVersion"
               $imagename  = "ncnndotnet/devel/$distribution/$distributionVersion/$Target/$cudaVersion"
            }
         }

         $config = [Config]::new($root, "Release", $target, $architecture, $platform, $option)
         $libraryDir = Join-Path "artifacts" $config.GetArtifactDirectoryName()
         $build = $config.GetBuildDirectoryName($operatingSystem)

         Write-Host "Start 'docker build -t $dockername $dockerFileDir --build-arg IMAGE_NAME=""$imagename""'" -ForegroundColor Green
         docker build --network host --force-rm=true -t $dockername $dockerFileDir --build-arg IMAGE_NAME="$imagename" | Write-Host

         if ($lastexitcode -ne 0)
         {
            Write-Host "Failed to docker build: $lastexitcode" -ForegroundColor Red
            return $False
         }

         if ($platform -eq "desktop")
         {
            if ($target -eq "arm")
            {
               Write-Host "Start 'docker run --rm --privileged multiarch/qemu-user-static --reset -p yes'" -ForegroundColor Green
               docker run --rm --privileged multiarch/qemu-user-static --reset -p yes
            }
         }

         # Build binary
         foreach ($key in $buildHashTable.keys)
         {
            Write-Host "Start 'docker run --rm -v ""$($root):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t $dockername'" -ForegroundColor Green
            docker run --rm --network host `
                        -v "$($root):/opt/data/NcnnDotNet" `
                        -e "LOCAL_UID=$(id -u $env:USER)" `
                        -e "LOCAL_GID=$(id -g $env:USER)" `
                        -t "$dockername" $key $target $architecture $platform $option | Write-Host

            if ($lastexitcode -ne 0)
            {
               Write-Host "Failed to docker run: $lastexitcode" -ForegroundColor Red
               return $False
            }
         }

         # Copy output binary
         foreach ($key in $buildHashTable.keys)
         {
            $srcDir = Join-Path $sourceRoot $key
            $dll = $buildHashTable[$key]
            $dstDir = Join-Path $current $libraryDir

            CopyToArtifact -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
         }
      }
      else
      {
         if ($platform -eq "ios")
         {
            $option = $rid
         }

         $config = [Config]::new($root, "Release", $target, $architecture, $platform, $option)
         $libraryDir = Join-Path "artifacts" $config.GetArtifactDirectoryName()
         $build = $config.GetBuildDirectoryName($OperatingSystem)

         foreach ($key in $buildHashTable.keys)
         {
            $srcDir = Join-Path $sourceRoot $key

            # Move to build target directory
            Set-Location -Path $srcDir

            $arc = $config.GetArchitectureName()
            Write-Host "Build $key [$arc] for $target" -ForegroundColor Green
            Build -Config $config

            if ($lastexitcode -ne 0)
            {
               return $False
            }
         }

         # Copy output binary
         foreach ($key in $buildHashTable.keys)
         {
            $srcDir = Join-Path $sourceRoot $key
            $dll = $buildHashTable[$key]
            $dstDir = Join-Path $current $libraryDir

            if ($global:IsWindows)
            {
               CopyToArtifact -configuration "Release" -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
            }
            else
            {
               CopyToArtifact -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
            }
         }
      }

      return $True
   }

}

function CallVisualStudioDeveloperConsole()
{
   $target = $this._Target

   if ($target -eq "arm")
   {
      if ($architecture -eq 32)
      {
         cmd.exe /c "call `"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsamd64_arm.bat`" && set > %temp%\vcvars.txt"
      }
      elseif ($architecture -eq 64)
      {
         cmd.exe /c "call `"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsamd64_arm64.bat`" && set > %temp%\vcvars.txt"
      }
   }
   else
   {
      if ($architecture -eq 32)
      {
         cmd.exe /c "call `"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsamd64_x86.bat`" && set > %temp%\vcvars.txt"
      }
      elseif ($architecture -eq 64)
      {
         cmd.exe /c "call `"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars64.bat`" && set > %temp%\vcvars.txt"
      }
   }

   Get-Content "${env:temp}\vcvars.txt" | Foreach-Object {
      if ($_ -match "^(.*?)=(.*)$") {
        Set-Content "env:\$($matches[1])" $matches[2]
      }
    }
}

class ThirdPartyBuilder
{

   [Config]   $_Config

   ThirdPartyBuilder( [Config]$Config )
   {
      $this._Config = $Config
   }

   [string] BuildProtobuf()
   {
      $ret = ""
      $current = Get-Location

      try
      {
         Write-Host "Start Build Protobuf" -ForegroundColor Green

         $Configuration = $this._Config.GetConfigurationName()

         $protobufDir = $this._Config.GetProtobufRootDir()
         $protobufDir = Join-Path $protobufDir "cmake"
         $protobufTarget = Join-Path $current protobuf
         New-Item $protobufTarget -Force -ItemType Directory
         Set-Location $protobufTarget
         $current2 = Get-Location
         $installDir = Join-Path $current2 install
         $ret = $installDir

         if ($global:IsWindows)
         {
            Write-Host "   cmake -G `"NMake Makefiles`" -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_INSTALL_PREFIX="$installDir" `
         -D protobuf_BUILD_TESTS=OFF `
         -D protobuf_MSVC_STATIC_RUNTIME=OFF `
         $protobufDir" -ForegroundColor Yellow
            cmake -G "NMake Makefiles" -D CMAKE_BUILD_TYPE=$Configuration `
                                       -D BUILD_SHARED_LIBS=OFF `
                                       -D CMAKE_INSTALL_PREFIX="$installDir" `
                                       -D protobuf_BUILD_TESTS=OFF `
                                       -D protobuf_MSVC_STATIC_RUNTIME=OFF `
                                       $protobufDir
            Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
            cmake --build . --config $Configuration --target install
         }
         else
         {
            $toolchain = $this._Config.GetToolchainFile()

            Write-Host "   cmake -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_INSTALL_PREFIX="$installDir" `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D protobuf_BUILD_TESTS=OFF `
         -D protobuf_MSVC_STATIC_RUNTIME=OFF `
         $protobufDir" -ForegroundColor Yellow
            cmake -D CMAKE_BUILD_TYPE=$Configuration `
                  -D BUILD_SHARED_LIBS=OFF `
                  -D CMAKE_INSTALL_PREFIX="$installDir" `
                  -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
                  -D protobuf_BUILD_TESTS=OFF `
                  -D protobuf_MSVC_STATIC_RUNTIME=OFF `
                  $protobufDir
            Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
            cmake --build . --config $Configuration --target install
         }
      }
      finally
      {
         Set-Location $current
         Write-Host "End Build Protobuf" -ForegroundColor Green
      }

      return $ret
   }

   [string] BuildOpenCV([bool]$skipBuild = $False)
   {
      $ret = ""
      $current = Get-Location

      try
      {
         $Platform = $this._Config.GetPlatform()
         $Configuration = $this._Config.GetConfigurationName()

         switch ($Platform)
         {
            "desktop"
            {
               Write-Host "Start Build OpenCV" -ForegroundColor Green

               $opencvDir = $this._Config.GetOpenCVRootDir()
               $opencvTarget = Join-Path $current opencv
               New-Item $opencvTarget -Force -ItemType Directory
               Set-Location $opencvTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir
               if ($skipBuild)
               {
                  Write-Host "Skip Build OpenCV" -ForegroundColor Green
                  return $ret
               }

               if ($global:IsWindows)
               {
                  Write-Host "   cmake -G `"NMake Makefiles`" -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D BUILD_WITH_STATIC_CRT=OFF `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D BUILD_opencv_world=OFF `
         -D BUILD_opencv_java=OFF `
         -D BUILD_opencv_python=OFF `
         -D BUILD_opencv_python2=OFF `
         -D BUILD_opencv_python3=OFF `
         -D BUILD_PERF_TESTS=OFF `
         -D BUILD_TESTS=OFF `
         -D BUILD_DOCS=OFF `
         -D BUILD_opencv_core=ON `
         -D BUILD_opencv_highgui=ON `
         -D BUILD_opencv_imgcodecs=ON `
         -D BUILD_opencv_imgproc=ON `
         -D BUILD_opencv_calib3d=OFF `
         -D BUILD_opencv_features2d=OFF `
         -D BUILD_opencv_flann=OFF `
         -D BUILD_opencv_java_bindings_generator=OFF `
         -D BUILD_opencv_ml=OFF `
         -D BUILD_opencv_objdetect=OFF `
         -D BUILD_opencv_photo=OFF `
         -D BUILD_opencv_python_bindings_generator=OFF `
         -D BUILD_opencv_shape=OFF `
         -D BUILD_opencv_stitching=OFF `
         -D BUILD_opencv_superres=OFF `
         -D BUILD_opencv_video=OFF `
         -D BUILD_opencv_videoio=ON `
         -D BUILD_opencv_videostab=OFF `
         -D WITH_CUDA=OFF `
         -D BUILD_PROTOBUF=OFF `
         -D WITH_PROTOBUF=OFF `
         -D WITH_IPP=OFF `
         -D WITH_FFMPEG=OFF `
         -D WITH_ITT=OFF `
         $opencvDir" -ForegroundColor Yellow
                  cmake -G "NMake Makefiles" -D CMAKE_BUILD_TYPE=$Configuration `
                                             -D BUILD_SHARED_LIBS=OFF `
                                             -D BUILD_WITH_STATIC_CRT=OFF `
                                             -D CMAKE_INSTALL_PREFIX="${installDir}" `
                                             -D BUILD_opencv_world=OFF `
                                             -D BUILD_opencv_java=OFF `
                                             -D BUILD_opencv_python=OFF `
                                             -D BUILD_opencv_python2=OFF `
                                             -D BUILD_opencv_python3=OFF `
                                             -D BUILD_PERF_TESTS=OFF `
                                             -D BUILD_TESTS=OFF `
                                             -D BUILD_DOCS=OFF `
                                             -D BUILD_opencv_core=ON `
                                             -D BUILD_opencv_highgui=ON `
                                             -D BUILD_opencv_imgcodecs=ON `
                                             -D BUILD_opencv_imgproc=ON `
                                             -D BUILD_opencv_calib3d=OFF `
                                             -D BUILD_opencv_features2d=OFF `
                                             -D BUILD_opencv_flann=OFF `
                                             -D BUILD_opencv_java_bindings_generator=OFF `
                                             -D BUILD_opencv_ml=OFF `
                                             -D BUILD_opencv_objdetect=OFF `
                                             -D BUILD_opencv_photo=OFF `
                                             -D BUILD_opencv_python_bindings_generator=OFF `
                                             -D BUILD_opencv_shape=OFF `
                                             -D BUILD_opencv_stitching=OFF `
                                             -D BUILD_opencv_superres=OFF `
                                             -D BUILD_opencv_video=OFF `
                                             -D BUILD_opencv_videoio=ON `
                                             -D BUILD_opencv_videostab=OFF `
                                             -D WITH_CUDA=OFF `
                                             -D BUILD_PROTOBUF=OFF `
                                             -D WITH_PROTOBUF=OFF `
                                             -D WITH_IPP=OFF `
                                             -D WITH_FFMPEG=OFF `
                                             -D WITH_ITT=OFF `
                                             $opencvDir
                  Write-Host "   cmake build and install" -ForegroundColor Yellow
                  cmake --build . --config $Configuration --target install
               }
               else
               {
                  $toolchain = $this._Config.GetToolchainFile()

                  Write-Host "   cmake -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D BUILD_opencv_world=OFF `
         -D BUILD_opencv_java=OFF `
         -D BUILD_opencv_python=OFF `
         -D BUILD_opencv_python2=OFF `
         -D BUILD_opencv_python3=OFF `
         -D BUILD_PERF_TESTS=OFF `
         -D BUILD_TESTS=OFF `
         -D BUILD_DOCS=OFF `
         -D BUILD_opencv_core=ON `
         -D BUILD_opencv_highgui=ON `
         -D BUILD_opencv_imgcodecs=ON `
         -D BUILD_opencv_imgproc=ON `
         -D BUILD_opencv_calib3d=OFF `
         -D BUILD_opencv_features2d=OFF `
         -D BUILD_opencv_flann=OFF `
         -D BUILD_opencv_java_bindings_generator=OFF `
         -D BUILD_opencv_ml=OFF `
         -D BUILD_opencv_objdetect=OFF `
         -D BUILD_opencv_photo=OFF `
         -D BUILD_opencv_python_bindings_generator=OFF `
         -D BUILD_opencv_shape=OFF `
         -D BUILD_opencv_stitching=OFF `
         -D BUILD_opencv_superres=OFF `
         -D BUILD_opencv_video=OFF `
         -D BUILD_opencv_videoio=ON `
         -D BUILD_opencv_videostab=OFF `
         -D BUILD_PNG=ON `
         -D BUILD_JPEG=ON `
         -D WITH_CUDA=OFF `
         -D WITH_GTK=ON `
         -D WITH_GTK_2_X=OFF `
         -D BUILD_PROTOBUF=OFF `
         -D WITH_PROTOBUF=OFF `
         -D WITH_IPP=OFF `
         -D WITH_FFMPEG=OFF `
         -D WITH_ITT=OFF `
         $opencvDir" -ForegroundColor Yellow
                  cmake -D CMAKE_BUILD_TYPE=$Configuration `
                        -D BUILD_SHARED_LIBS=OFF `
                        -D CMAKE_INSTALL_PREFIX="${installDir}" `
                        -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
                        -D BUILD_opencv_world=OFF `
                        -D BUILD_opencv_java=OFF `
                        -D BUILD_opencv_python=OFF `
                        -D BUILD_opencv_python2=OFF `
                        -D BUILD_opencv_python3=OFF `
                        -D BUILD_PERF_TESTS=OFF `
                        -D BUILD_TESTS=OFF `
                        -D BUILD_DOCS=OFF `
                        -D BUILD_opencv_core=ON `
                        -D BUILD_opencv_highgui=ON `
                        -D BUILD_opencv_imgcodecs=ON `
                        -D BUILD_opencv_imgproc=ON `
                        -D BUILD_opencv_calib3d=OFF `
                        -D BUILD_opencv_features2d=OFF `
                        -D BUILD_opencv_flann=OFF `
                        -D BUILD_opencv_java_bindings_generator=OFF `
                        -D BUILD_opencv_ml=OFF `
                        -D BUILD_opencv_objdetect=OFF `
                        -D BUILD_opencv_photo=OFF `
                        -D BUILD_opencv_python_bindings_generator=OFF `
                        -D BUILD_opencv_shape=OFF `
                        -D BUILD_opencv_stitching=OFF `
                        -D BUILD_opencv_superres=OFF `
                        -D BUILD_opencv_video=OFF `
                        -D BUILD_opencv_videoio=ON `
                        -D BUILD_opencv_videostab=OFF `
                        -D BUILD_PNG=ON `
                        -D BUILD_JPEG=ON `
                        -D WITH_CUDA=OFF `
                        -D WITH_GTK=ON `
                        -D WITH_GTK_2_X=OFF `
                        -D BUILD_PROTOBUF=OFF `
                        -D WITH_PROTOBUF=OFF `
                        -D WITH_IPP=OFF `
                        -D WITH_FFMPEG=OFF `
                        -D WITH_ITT=OFF `
                        $opencvDir
                  Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
                  cmake --build . --config $Configuration --target install
               }
            }
            "uwp"
            {
               Write-Host "Start Build OpenCV" -ForegroundColor Green

               $opencvDir = $this._Config.GetOpenCVRootDir()
               $opencvTarget = Join-Path $current opencv
               New-Item $opencvTarget -Force -ItemType Directory
               Set-Location $opencvTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir
               if ($skipBuild)
               {
                  Write-Host "Skip Build OpenCV" -ForegroundColor Green
                  return $ret
               }

               if ($global:IsWindows)
               {
                  # NOTE
                  # libtiff looks like to depends on win32 functions like __imp_MessageBoxA and __imp_GetFocus.
                  # So disable TIFF
                  Write-Host "   cmake -G `"NMake Makefiles`" `
         -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D BUILD_WITH_STATIC_CRT=OFF `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D BUILD_opencv_world=OFF `
         -D BUILD_opencv_java=OFF `
         -D BUILD_opencv_python=OFF `
         -D BUILD_opencv_python2=OFF `
         -D BUILD_opencv_python3=OFF `
         -D BUILD_PERF_TESTS=OFF `
         -D BUILD_TESTS=OFF `
         -D BUILD_DOCS=OFF `
         -D BUILD_opencv_core=ON `
         -D BUILD_opencv_highgui=OFF `
         -D BUILD_opencv_imgcodecs=ON `
         -D BUILD_opencv_imgproc=ON `
         -D BUILD_opencv_calib3d=OFF `
         -D BUILD_opencv_features2d=OFF `
         -D BUILD_opencv_flann=OFF `
         -D BUILD_opencv_java_bindings_generator=OFF `
         -D BUILD_opencv_ml=OFF `
         -D BUILD_opencv_objdetect=OFF `
         -D BUILD_opencv_photo=OFF `
         -D BUILD_opencv_python_bindings_generator=OFF `
         -D BUILD_opencv_shape=OFF `
         -D BUILD_opencv_stitching=OFF `
         -D BUILD_opencv_superres=OFF `
         -D BUILD_opencv_video=OFF `
         -D BUILD_opencv_videoio=ON `
         -D BUILD_opencv_videostab=OFF `
         -D WITH_CUDA=OFF `
         -D BUILD_PROTOBUF=OFF `
         -D WITH_PROTOBUF=OFF `
         -D WITH_IPP=OFF `
         -D WITH_FFMPEG=OFF `
         -D WITH_ITT=OFF `
         -D WITH_TIFF=OFF `
         $opencvDir" -ForegroundColor Yellow
                  cmake -G "NMake Makefiles" -D CMAKE_BUILD_TYPE=$Configuration `
                                             -D BUILD_SHARED_LIBS=OFF `
                                             -D BUILD_WITH_STATIC_CRT=OFF `
                                             -D CMAKE_INSTALL_PREFIX="${installDir}" `
                                             -D BUILD_opencv_world=OFF `
                                             -D BUILD_opencv_java=OFF `
                                             -D BUILD_opencv_python=OFF `
                                             -D BUILD_opencv_python2=OFF `
                                             -D BUILD_opencv_python3=OFF `
                                             -D BUILD_PERF_TESTS=OFF `
                                             -D BUILD_TESTS=OFF `
                                             -D BUILD_DOCS=OFF `
                                             -D BUILD_opencv_core=ON `
                                             -D BUILD_opencv_highgui=ON `
                                             -D BUILD_opencv_imgcodecs=ON `
                                             -D BUILD_opencv_imgproc=ON `
                                             -D BUILD_opencv_calib3d=OFF `
                                             -D BUILD_opencv_features2d=OFF `
                                             -D BUILD_opencv_flann=OFF `
                                             -D BUILD_opencv_java_bindings_generator=OFF `
                                             -D BUILD_opencv_ml=OFF `
                                             -D BUILD_opencv_objdetect=OFF `
                                             -D BUILD_opencv_photo=OFF `
                                             -D BUILD_opencv_python_bindings_generator=OFF `
                                             -D BUILD_opencv_shape=OFF `
                                             -D BUILD_opencv_stitching=OFF `
                                             -D BUILD_opencv_superres=OFF `
                                             -D BUILD_opencv_video=OFF `
                                             -D BUILD_opencv_videoio=ON `
                                             -D BUILD_opencv_videostab=OFF `
                                             -D WITH_CUDA=OFF `
                                             -D BUILD_PROTOBUF=OFF `
                                             -D WITH_PROTOBUF=OFF `
                                             -D WITH_IPP=OFF `
                                             -D WITH_FFMPEG=OFF `
                                             -D WITH_ITT=OFF `
                                             -D WITH_TIFF=OFF `
                                             $opencvDir
                  Write-Host "   cmake build and install" -ForegroundColor Yellow
                  cmake --build . --config $Configuration --target install
               }
            }
            "android"
            {
               Write-Host "Start Build OpenCV" -ForegroundColor Green

               $opencvDir = $this._Config.GetOpenCVRootDir()
               $opencvTarget = Join-Path $current opencv
               New-Item $opencvTarget -Force -ItemType Directory
               Set-Location $opencvTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir
               if ($skipBuild)
               {
                  Write-Host "Skip Build OpenCV" -ForegroundColor Green
                  return $ret
               }

               $level = $this._Config.GetAndroidNativeAPILevel()
               $abi = $this._Config.GetAndroidABI()

               Write-Host "   cmake -D CMAKE_BUILD_TYPE=$Configuration `
            -D BUILD_SHARED_LIBS=OFF `
            -D BUILD_WITH_STATIC_CRT=OFF `
            -D CMAKE_INSTALL_PREFIX=`"$installDir`" `
            -D BUILD_opencv_world=ON `
            -D BUILD_opencv_java=OFF `
            -D BUILD_opencv_python=OFF `
            -D BUILD_opencv_python2=OFF `
            -D BUILD_opencv_python3=OFF `
            -D BUILD_PERF_TESTS=OFF `
            -D BUILD_TESTS=OFF `
            -D BUILD_DOCS=OFF `
            -D BUILD_opencv_core=ON `
            -D BUILD_opencv_highgui=ON `
            -D BUILD_opencv_imgcodecs=ON `
            -D BUILD_opencv_imgproc=ON `
            -D BUILD_opencv_calib3d=OFF `
            -D BUILD_opencv_features2d=OFF `
            -D BUILD_opencv_flann=OFF `
            -D BUILD_opencv_java_bindings_generator=OFF `
            -D BUILD_opencv_ml=OFF `
            -D BUILD_opencv_objdetect=OFF `
            -D BUILD_opencv_photo=OFF `
            -D BUILD_opencv_python_bindings_generator=OFF `
            -D BUILD_opencv_shape=OFF `
            -D BUILD_opencv_stitching=OFF `
            -D BUILD_opencv_superres=OFF `
            -D BUILD_opencv_video=OFF `
            -D BUILD_opencv_videoio=ON `
            -D BUILD_opencv_videostab=OFF `
            -D BUILD_PNG=ON `
            -D BUILD_JPEG=ON `
            -D WITH_CUDA=OFF `
            -D WITH_GTK=OFF `
            -D WITH_GTK_2_X=OFF `
            -D BUILD_PROTOBUF=OFF `
            -D WITH_PROTOBUF=OFF `
            -D WITH_IPP=OFF `
            -D WITH_FFMPEG=OFF `
            -D WITH_ITT=OFF `
            -D ANDROID_ABI=$abi `
            -D ANDROID_ARM_NEON=ON `
            -D ANDROID_PLATFORM=android-$level `
            $opencvDir" -ForegroundColor Yellow
               cmake -D CMAKE_TOOLCHAIN_FILE=${env:ANDROID_NDK}/build/cmake/android.toolchain.cmake `
                     -D CMAKE_BUILD_TYPE=$Configuration `
                     -D BUILD_SHARED_LIBS=OFF `
                     -D BUILD_WITH_STATIC_CRT=OFF `
                     -D CMAKE_INSTALL_PREFIX="$installDir" `
                     -D BUILD_opencv_world=ON `
                     -D BUILD_opencv_java=OFF `
                     -D BUILD_opencv_python=OFF `
                     -D BUILD_opencv_python2=OFF `
                     -D BUILD_opencv_python3=OFF `
                     -D BUILD_PERF_TESTS=OFF `
                     -D BUILD_TESTS=OFF `
                     -D BUILD_DOCS=OFF `
                     -D BUILD_opencv_core=ON `
                     -D BUILD_opencv_highgui=ON `
                     -D BUILD_opencv_imgcodecs=ON `
                     -D BUILD_opencv_imgproc=ON `
                     -D BUILD_opencv_calib3d=OFF `
                     -D BUILD_opencv_features2d=OFF `
                     -D BUILD_opencv_flann=OFF `
                     -D BUILD_opencv_java_bindings_generator=OFF `
                     -D BUILD_opencv_ml=OFF `
                     -D BUILD_opencv_objdetect=OFF `
                     -D BUILD_opencv_photo=OFF `
                     -D BUILD_opencv_python_bindings_generator=OFF `
                     -D BUILD_opencv_shape=OFF `
                     -D BUILD_opencv_stitching=OFF `
                     -D BUILD_opencv_superres=OFF `
                     -D BUILD_opencv_video=OFF `
                     -D BUILD_opencv_videoio=ON `
                     -D BUILD_opencv_videostab=OFF `
                     -D BUILD_PNG=ON `
                     -D BUILD_JPEG=ON `
                     -D WITH_CUDA=OFF `
                     -D WITH_GTK=OFF `
                     -D WITH_GTK_2_X=OFF `
                     -D BUILD_PROTOBUF=OFF `
                     -D WITH_PROTOBUF=OFF `
                     -D WITH_IPP=OFF `
                     -D WITH_FFMPEG=OFF `
                     -D WITH_ITT=OFF `
                     -D ANDROID_ABI=$abi `
                     -D ANDROID_ARM_NEON=ON `
                     -D ANDROID_PLATFORM=android-$level `
                     $opencvDir
               Write-Host "   make" -ForegroundColor Yellow
               make -j4
               Write-Host "   make install" -ForegroundColor Yellow
               make install
            }
            "ios"
            {
               Write-Host "Start Build OpenCV" -ForegroundColor Green

               $opencvDir = $this._Config.GetOpenCVRootDir()
               $opencvTarget = Join-Path $current opencv
               New-Item $opencvTarget -Force -ItemType Directory
               Set-Location $opencvTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir
               if ($skipBuild)
               {
                  Write-Host "Skip Build OpenCV" -ForegroundColor Green
                  return $ret
               }

               $developerDir = $this._Config.GetDeveloperDir()
               $osxArchitectures = $this._Config.GetOSXArchitectures()
               $toolchain = $this._Config.GetToolchainFile()

               $OSX_SYSROOT = $this._Config.GetIOSSDK($osxArchitectures, $developerDir)

               $CMAKE_IOS_INSTALL_COMBINED="NO"
               $CMAKE_XCODE_ATTRIBUTE_ONLY_ACTIVE_ARCH="NO"

               # use libc++ rather than libstdc++
               Write-Host "   cmake -D CMAKE_BUILD_TYPE=$Configuration `
         -D CMAKE_CXX_FLAGS=`"-std=c++11`" `
         -D CMAKE_EXE_LINKER_FLAGS=`"-stdlib=libc++ -lc++abi`" `
         -D CMAKE_SYSTEM_NAME=iOS `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_XCODE_ATTRIBUTE_ONLY_ACTIVE_ARCH=$CMAKE_XCODE_ATTRIBUTE_ONLY_ACTIVE_ARCH `
         -D CMAKE_IOS_INSTALL_COMBINED=$CMAKE_IOS_INSTALL_COMBINED `
         -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
         -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D BUILD_opencv_world=ON `
         -D BUILD_opencv_java=OFF `
         -D BUILD_opencv_python=OFF `
         -D BUILD_opencv_python2=OFF `
         -D BUILD_opencv_python3=OFF `
         -D BUILD_PERF_TESTS=OFF `
         -D BUILD_TESTS=OFF `
         -D BUILD_DOCS=OFF `
         -D BUILD_opencv_apps=OFF `
         -D BUILD_opencv_core=ON `
         -D BUILD_opencv_highgui=ON `
         -D BUILD_opencv_imgcodecs=ON `
         -D BUILD_opencv_imgproc=ON `
         -D BUILD_opencv_calib3d=OFF `
         -D BUILD_opencv_features2d=OFF `
         -D BUILD_opencv_flann=OFF `
         -D BUILD_opencv_java_bindings_generator=OFF `
         -D BUILD_opencv_ml=OFF `
         -D BUILD_opencv_objdetect=OFF `
         -D BUILD_opencv_photo=OFF `
         -D BUILD_opencv_python_bindings_generator=OFF `
         -D BUILD_opencv_shape=OFF `
         -D BUILD_opencv_stitching=OFF `
         -D BUILD_opencv_superres=OFF `
         -D BUILD_opencv_video=OFF `
         -D BUILD_opencv_videoio=OFF `
         -D BUILD_opencv_videostab=OFF `
         -D BUILD_PNG=ON `
         -D BUILD_JPEG=ON `
         -D BUILD_ZLIB=ON `
         -D BUILD_WEBP=OFF `
         -D WITH_PNG=ON `
         -D WITH_JPEG=ON `
         -D WITH_ZLIB=ON `
         -D WITH_WEBP=OFF `
         -D WITH_CUDA=OFF `
         -D WITH_GTK=OFF `
         -D BUILD_PROTOBUF=OFF `
         -D WITH_PROTOBUF=OFF `
         -D WITH_IPP=OFF `
         -D WITH_FFMPEG=OFF `
         -D WITH_ITT=OFF `
         $opencvDir" -ForegroundColor Yellow
               cmake -D CMAKE_BUILD_TYPE=$Configuration `
                     -D CMAKE_CXX_FLAGS="-std=c++11" `
                     -D CMAKE_EXE_LINKER_FLAGS="-stdlib=libc++ -lc++abi" `
                     -D CMAKE_SYSTEM_NAME=iOS `
                     -D BUILD_SHARED_LIBS=OFF `
                     -D CMAKE_XCODE_ATTRIBUTE_ONLY_ACTIVE_ARCH=$CMAKE_XCODE_ATTRIBUTE_ONLY_ACTIVE_ARCH `
                     -D CMAKE_IOS_INSTALL_COMBINED=$CMAKE_IOS_INSTALL_COMBINED `
                     -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
                     -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
                     -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
                     -D CMAKE_INSTALL_PREFIX="${installDir}" `
                     -D BUILD_opencv_world=ON `
                     -D BUILD_opencv_java=OFF `
                     -D BUILD_opencv_python=OFF `
                     -D BUILD_opencv_python2=OFF `
                     -D BUILD_opencv_python3=OFF `
                     -D BUILD_PERF_TESTS=OFF `
                     -D BUILD_TESTS=OFF `
                     -D BUILD_DOCS=OFF `
                     -D BUILD_opencv_apps=OFF `
                     -D BUILD_opencv_core=ON `
                     -D BUILD_opencv_highgui=ON `
                     -D BUILD_opencv_imgcodecs=ON `
                     -D BUILD_opencv_imgproc=ON `
                     -D BUILD_opencv_calib3d=OFF `
                     -D BUILD_opencv_features2d=OFF `
                     -D BUILD_opencv_flann=OFF `
                     -D BUILD_opencv_java_bindings_generator=OFF `
                     -D BUILD_opencv_ml=OFF `
                     -D BUILD_opencv_objdetect=OFF `
                     -D BUILD_opencv_photo=OFF `
                     -D BUILD_opencv_python_bindings_generator=OFF `
                     -D BUILD_opencv_shape=OFF `
                     -D BUILD_opencv_stitching=OFF `
                     -D BUILD_opencv_superres=OFF `
                     -D BUILD_opencv_video=OFF `
                     -D BUILD_opencv_videoio=OFF `
                     -D BUILD_opencv_videostab=OFF `
                     -D BUILD_PNG=ON `
                     -D BUILD_JPEG=ON `
                     -D BUILD_ZLIB=ON `
                     -D BUILD_WEBP=OFF `
                     -D WITH_PNG=ON `
                     -D WITH_JPEG=ON `
                     -D WITH_ZLIB=ON `
                     -D WITH_WEBP=OFF `
                     -D WITH_CUDA=OFF `
                     -D WITH_GTK=OFF `
                     -D BUILD_PROTOBUF=OFF `
                     -D WITH_PROTOBUF=OFF `
                     -D WITH_IPP=OFF `
                     -D WITH_FFMPEG=OFF `
                     -D WITH_ITT=OFF `
                     $opencvDir
               Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
               cmake --build . --config $Configuration --target install
            }
         }


      }
      finally
      {
         Set-Location $current
         Write-Host "End Build OpenCV" -ForegroundColor Green
      }

      return $ret
   }

   [string] BuildNcnn([string]$protobufInstallDir, [string]$vulkanOnOff)
   {
      $ret = ""
      $current = Get-Location

      # enables some layers
      $WITH_LAYER_argmax="ON"
      $WITH_LAYER_spp="ON"
      $WITH_LAYER_tile="ON"

      try
      {
         $Platform = $this._Config.GetPlatform()
         $Configuration = $this._Config.GetConfigurationName()

         # Get install directory by skipping build opencv
         $installOpenCVDir = $this.BuildOpenCV($True)
         if (!(Test-Path $installOpenCVDir))
         {
            Write-Host "OpenCV could fail to build" -ForegroundColor Red
            return $ret
         }

         switch ($Platform)
         {
            "desktop"
            {
               Write-Host "Start Build ncnn" -ForegroundColor Green

               $ncnnDir = $this._Config.GetNcnnRootDir()
               $ncnnTarget = Join-Path $current ncnn
               New-Item $ncnnTarget -Force -ItemType Directory
               Set-Location $ncnnTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir

               $env:OpenCV_DIR = $installOpenCVDir

               if ($global:IsWindows)
               {
                  $includeDir = Join-Path $protobufInstallDir include

                  if ($Configuration -eq "Debug")
                  {
                     $libraryFile = Join-Path $protobufInstallDir lib | `
                                    Join-Path -ChildPath libprotobufd.lib
                  }
                  else
                  {
                     $libraryFile = Join-Path $protobufInstallDir lib | `
                                    Join-Path -ChildPath libprotobuf.lib
                  }

                  $exeDir = Join-Path $protobufInstallDir bin | `
                            Join-Path -ChildPath protoc.exe

                  Write-Host "   cmake -G `"NMake Makefiles`" `
         -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D Protobuf_INCLUDE_DIR=`"${includeDir}`" `
         -D Protobuf_LIBRARIES=`"${libraryFile}`" `
         -D Protobuf_PROTOC_EXECUTABLE=`"${exeDir}`" `
         -D NCNN_VULKAN:BOOL=$vulkanOnOff `
         -D NCNN_OPENCV:BOOL=OFF `
         -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
         -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
         -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         $ncnnDir" -ForegroundColor Yellow
                  cmake -G "NMake Makefiles" `
                        -D BUILD_SHARED_LIBS=OFF `
                        -D CMAKE_INSTALL_PREFIX="${installDir}" `
                        -D Protobuf_INCLUDE_DIR="${includeDir}" `
                        -D Protobuf_LIBRARIES="${libraryFile}" `
                        -D Protobuf_PROTOC_EXECUTABLE="${exeDir}" `
                        -D NCNN_VULKAN:BOOL=$vulkanOnOff `
                        -D NCNN_OPENCV:BOOL=OFF `
                        -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
                        -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
                        -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
                        -D OpenCV_DIR="${installOpenCVDir}" `
                        $ncnnDir
                  Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
                  cmake --build . --config $Configuration --target install
               }
               elseif ($global:IsMacOS)
               {
                  $toolchain = $this._Config.GetToolchainFile()

                  $includeDir = Join-Path $protobufInstallDir include
                  $libraryFile = Join-Path $protobufInstallDir lib | `
                                 Join-Path -ChildPath libprotobuf.a

                  $exeDir = Join-Path $protobufInstallDir bin | `
                            Join-Path -ChildPath protoc

                  # build vulkan variables
                  $Vulkan_INCLUDE_DIR = Join-Path $env:VULKAN_SDK MoltenVK | `
                                        Join-Path -Childpath include
                  $Vulkan_LIBRARY = Join-Path $env:VULKAN_SDK MoltenVK | `
                                    Join-Path -Childpath dylib | `
                                    Join-Path -Childpath macOS | `
                                    Join-Path -Childpath libMoltenVK.dylib

                  Write-Host "   cmake -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D Protobuf_INCLUDE_DIR=`"${includeDir}`" `
         -D Protobuf_LIBRARIES=`"${libraryFile}`" `
         -D Protobuf_PROTOC_EXECUTABLE=`"${exeDir}`" `
         -D NCNN_VULKAN:BOOL=$vulkanOnOff `
         -D Vulkan_INCLUDE_DIR=`"${Vulkan_INCLUDE_DIR}`" `
         -D Vulkan_LIBRARY=`"${Vulkan_LIBRARY}`" `
         -D NCNN_OPENCV:BOOL=OFF `
         -D NCNN_DISABLE_RTTI:BOOL=OFF `
         -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
         -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
         -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         $ncnnDir" -ForegroundColor Yellow
                  cmake -D CMAKE_BUILD_TYPE=$Configuration `
                        -D BUILD_SHARED_LIBS=OFF `
                        -D CMAKE_INSTALL_PREFIX="${installDir}" `
                        -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
                        -D Protobuf_INCLUDE_DIR="${includeDir}" `
                        -D Protobuf_LIBRARIES="${libraryFile}" `
                        -D Protobuf_PROTOC_EXECUTABLE="${exeDir}" `
                        -D NCNN_VULKAN:BOOL=$vulkanOnOff `
                        -D Vulkan_INCLUDE_DIR="${Vulkan_INCLUDE_DIR}" `
                        -D Vulkan_LIBRARY="${Vulkan_LIBRARY}" `
                        -D NCNN_OPENCV:BOOL=OFF `
                        -D NCNN_DISABLE_RTTI:BOOL=OFF `
                        -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
                        -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
                        -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
                        -D OpenCV_DIR="${installOpenCVDir}" `
                        $ncnnDir
                  Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
                  cmake --build . --config $Configuration --target install

                  # centos generates some libraries into lib64
                  if (Test-Path "${installDir}/lib64")
                  {
                     Copy-Item -Recurse -Force "${installDir}/lib64/*" "${installDir}/lib"
                  }
               }
               else
               {
                  $toolchain = $this._Config.GetToolchainFile()

                  $includeDir = Join-Path $protobufInstallDir include
                  $libraryFile = Join-Path $protobufInstallDir lib | `
                                 Join-Path -ChildPath libprotobuf.a
                  # centos
                  if (!(Test-Path $libraryFile))
                  {
                     $libraryFile = Join-Path $protobufInstallDir lib64 | `
                                    Join-Path -ChildPath libprotobuf.a
                  }
                  $exeDir = Join-Path $protobufInstallDir bin | `
                            Join-Path -ChildPath protoc

                  # do not expose arm82 features for gcc < 4.7
                  # by https://github.com/Tencent/ncnn/commit/ce9ae96bde967a84341911834b96530d9568f6f9
                  # But arm says
                  # https://community.arm.com/developer/tools-software/tools/b/tools-software-ides-blog/posts/making-the-most-of-the-arm-architecture-in-gcc-10
                  $NCNN_ARM82="OFF"
                  $NCNN_COMPILER_SUPPORT_ARM82_FP16="OFF"
                  $NCNN_COMPILER_SUPPORT_ARM82_FP16_DOTPROD="OFF"

                  Write-Host "   cmake -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D Protobuf_INCLUDE_DIR=`"${includeDir}`" `
         -D Protobuf_LIBRARIES=`"${libraryFile}`" `
         -D Protobuf_PROTOC_EXECUTABLE=`"${exeDir}`" `
         -D NCNN_VULKAN:BOOL=$vulkanOnOff `
         -D NCNN_OPENCV:BOOL=OFF `
         -D NCNN_DISABLE_RTTI:BOOL=OFF `
         -D NCNN_ARM82:BOOL=${NCNN_ARM82} `
         -D NCNN_COMPILER_SUPPORT_ARM82_FP16:BOOL=${NCNN_COMPILER_SUPPORT_ARM82_FP16} `
         -D NCNN_COMPILER_SUPPORT_ARM82_FP16_DOTPROD:BOOL=${NCNN_COMPILER_SUPPORT_ARM82_FP16_DOTPROD} `
         -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
         -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
         -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         $ncnnDir" -ForegroundColor Yellow
                  cmake -D CMAKE_BUILD_TYPE=$Configuration `
                        -D BUILD_SHARED_LIBS=OFF `
                        -D CMAKE_INSTALL_PREFIX="${installDir}" `
                        -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
                        -D Protobuf_INCLUDE_DIR="${includeDir}" `
                        -D Protobuf_LIBRARIES="${libraryFile}" `
                        -D Protobuf_PROTOC_EXECUTABLE="${exeDir}" `
                        -D NCNN_VULKAN:BOOL=$vulkanOnOff `
                        -D NCNN_OPENCV:BOOL=OFF `
                        -D NCNN_DISABLE_RTTI:BOOL=OFF `
                        -D NCNN_ARM82:BOOL=${NCNN_ARM82} `
                        -D NCNN_ARM82:BOOL=${NCNN_ARM82} `
                        -D NCNN_COMPILER_SUPPORT_ARM82_FP16:BOOL=${NCNN_COMPILER_SUPPORT_ARM82_FP16} `
                        -D NCNN_COMPILER_SUPPORT_ARM82_FP16_DOTPROD:BOOL=${NCNN_COMPILER_SUPPORT_ARM82_FP16_DOTPROD} `
                        -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
                        -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
                        -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
                        -D OpenCV_DIR="${installOpenCVDir}" `
                        $ncnnDir
                  Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
                  cmake --build . --config $Configuration --target install

                  # centos generates some libraries into lib64
                  if (Test-Path "${installDir}/lib64")
                  {
                     Copy-Item -Recurse -Force "${installDir}/lib64/*" "${installDir}/lib"
                  }
               }
            }
            "ios"
            {
               Write-Host "Start Build ncnn" -ForegroundColor Green

               $env:OpenCV_DIR = "$installOpenCVDir/share/OpenCV"

               $ncnnDir = $this._Config.GetNcnnRootDir()
               $ncnnTarget = Join-Path $current ncnn
               New-Item $ncnnTarget -Force -ItemType Directory
               Set-Location $ncnnTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir

               $developerDir = $this._Config.GetDeveloperDir()
               $osxArchitectures = $this._Config.GetOSXArchitectures()
               $toolchain = $this._Config.GetToolchainFile()

               $OSX_SYSROOT = $this._Config.GetIOSSDK($osxArchitectures, $developerDir)

               $vulkanOnOff = "ON"
               $targetPlatform = ""
               switch ($osxArchitectures)
               {
                  "arm64e"
                  {
                     $vulkanOnOff = "ON"
                     $targetPlatform = "ios-arm64"
                  }
                  "arm64"
                  {
                     $vulkanOnOff = "ON"
                     $targetPlatform = "ios-arm64"
                  }
                  "arm"
                  {
                     $vulkanOnOff = "OFF"
                     $targetPlatform = ""
                  }
                  "armv7"
                  {
                     $vulkanOnOff = "OFF"
                     $targetPlatform = ""
                  }
                  "armv7s"
                  {
                     $vulkanOnOff = "OFF"
                     $targetPlatform = ""
                  }
                  "i386"
                  {
                     $vulkanOnOff = "OFF"
                     $targetPlatform = ""
                  }
                  "x86_64"
                  {
                     $vulkanOnOff = "ON"
                     $targetPlatform = "ios-arm64_x86_64-simulator"
                  }
               }

               # build vulkan variables
               $Vulkan_INCLUDE_DIR = Join-Path $env:VULKAN_SDK MoltenVK | `
                                     Join-Path -Childpath include
               $Vulkan_LIBRARY = Join-Path $env:VULKAN_SDK MoltenVK | `
                                 Join-Path -Childpath MoltenVK.xcframework | `
                                 Join-Path -Childpath ${targetPlatform} | `
                                 Join-Path -Childpath libMoltenVK.a

               # use libc++ rather than libstdc++
               Write-Host "   cmake -D CMAKE_BUILD_TYPE=$Configuration `
         -D CMAKE_CXX_FLAGS=`"-std=c++11 -stdlib=libc++ -static`" `
         -D CMAKE_EXE_LINKER_FLAGS=`"-std=c++11 -stdlib=libc++ -static`" `
         -D CMAKE_SYSTEM_NAME=iOS `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
         -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D NCNN_VULKAN:BOOL=$vulkanOnOff `
         -D NCNN_BUILD_BENCHMARK:BOOL=OFF `
         -D NCNN_DISABLE_RTTI:BOOL=OFF `
         -D Vulkan_INCLUDE_DIR=`"${Vulkan_INCLUDE_DIR}`" `
         -D Vulkan_LIBRARY=`"${Vulkan_LIBRARY}`" `
         -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
         -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
         -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         $ncnnDir" -ForegroundColor Yellow
               cmake -D CMAKE_BUILD_TYPE=$Configuration `
                     -D CMAKE_CXX_FLAGS="-std=c++11 -stdlib=libc++ -static" `
                     -D CMAKE_EXE_LINKER_FLAGS="-std=c++11 -stdlib=libc++ -static" `
                     -D CMAKE_SYSTEM_NAME=iOS `
                     -D BUILD_SHARED_LIBS=OFF `
                     -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
                     -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
                     -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
                     -D NCNN_VULKAN:BOOL=$vulkanOnOff `
                     -D NCNN_BUILD_BENCHMARK:BOOL=OFF `
                     -D NCNN_DISABLE_RTTI:BOOL=OFF `
                     -D Vulkan_INCLUDE_DIR="${Vulkan_INCLUDE_DIR}" `
                     -D Vulkan_LIBRARY="${Vulkan_LIBRARY}" `
                     -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
                     -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
                     -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
                     -D OpenCV_DIR="${installOpenCVDir}" `
                     $ncnnDir

               Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
               cmake --build . --config $Configuration --target install
            }
            "uwp"
            {
               Write-Host "Start Build ncnn" -ForegroundColor Green

               $ncnnDir = $this._Config.GetNcnnRootDir()
               $ncnnTarget = Join-Path $current ncnn
               New-Item $ncnnTarget -Force -ItemType Directory
               Set-Location $ncnnTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir

               $env:OpenCV_DIR = $installOpenCVDir

               if ($global:IsWindows)
               {
                  $includeDir = Join-Path $protobufInstallDir include

                  if ($Configuration -eq "Debug")
                  {
                     $libraryFile = Join-Path $protobufInstallDir lib | `
                                    Join-Path -ChildPath libprotobufd.lib
                  }
                  else
                  {
                     $libraryFile = Join-Path $protobufInstallDir lib | `
                                    Join-Path -ChildPath libprotobuf.lib
                  }

                  $exeDir = Join-Path $protobufInstallDir bin | `
                            Join-Path -ChildPath protoc.exe

                  Write-Host "   cmake -G `"NMake Makefiles`" `
         -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=OFF `
         -D CMAKE_INSTALL_PREFIX=`"${installDir}`" `
         -D Protobuf_INCLUDE_DIR=`"${includeDir}`" `
         -D Protobuf_LIBRARIES=`"${libraryFile}`" `
         -D Protobuf_PROTOC_EXECUTABLE=`"${exeDir}`" `
         -D NCNN_VULKAN:BOOL=$vulkanOnOff `
         -D NCNN_OPENCV:BOOL=OFF `
         -D NCNN_BUILD_TOOLS=OFF `
         -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
         -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
         -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         $ncnnDir" -ForegroundColor Yellow
                  cmake -G "NMake Makefiles" `
                        -D CMAKE_BUILD_TYPE=$Configuration `
                        -D BUILD_SHARED_LIBS=OFF `
                        -D CMAKE_INSTALL_PREFIX="${installDir}" `
                        -D Protobuf_INCLUDE_DIR="${includeDir}" `
                        -D Protobuf_LIBRARIES="${libraryFile}" `
                        -D Protobuf_PROTOC_EXECUTABLE="${exeDir}" `
                        -D NCNN_VULKAN:BOOL=$vulkanOnOff `
                        -D NCNN_OPENCV:BOOL=OFF `
                        -D NCNN_BUILD_TOOLS=OFF `
                        -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
                        -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
                        -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
                        -D OpenCV_DIR="${installOpenCVDir}" `
                        $ncnnDir
                  Write-Host "   cmake --build . --config ${Configuration} --target install" -ForegroundColor Yellow
                  cmake --build . --config $Configuration --target install
               }
            }
            "android"
            {
               Write-Host "Start Build ncnn" -ForegroundColor Green

               $env:OpenCV_DIR = "$installOpenCVDir/sdk/native/jni"

               $ncnnDir = $this._Config.GetNcnnRootDir()
               $ncnnTarget = Join-Path $current ncnn
               New-Item $ncnnTarget -Force -ItemType Directory
               Set-Location $ncnnTarget
               $current2 = Get-Location
               $installDir = Join-Path $current2 install
               $ret = $installDir

               $level = $this._Config.GetAndroidNativeAPILevel()
               $abi = $this._Config.GetAndroidABI()

               Write-Host "   cmake -D CMAKE_TOOLCHAIN_FILE=${env:ANDROID_NDK}/build/cmake/android.toolchain.cmake `
            -D CMAKE_BUILD_TYPE=$Configuration `
            -D ANDROID_ABI=$abi `
            -D ANDROID_PLATFORM=android-$level `
            -D NCNN_VULKAN:BOOL=$vulkanOnOff `
            -D NCNN_DISABLE_RTTI:BOOL=OFF `
            -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
            -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
            -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
            -D OpenCV_DIR=`"${installOpenCVDir}`" `
            $ncnnDir" -ForegroundColor Yellow
               cmake -D CMAKE_TOOLCHAIN_FILE="${env:ANDROID_NDK}/build/cmake/android.toolchain.cmake" `
                     -D CMAKE_BUILD_TYPE=$Configuration `
                     -D ANDROID_ABI=$abi `
                     -D ANDROID_PLATFORM=android-$level `
                     -D NCNN_VULKAN:BOOL=$vulkanOnOff `
                     -D NCNN_DISABLE_RTTI:BOOL=OFF `
                     -D WITH_LAYER_argmax:BOOL=${WITH_LAYER_argmax} `
                     -D WITH_LAYER_spp:BOOL=${WITH_LAYER_spp} `
                     -D WITH_LAYER_tile:BOOL=${WITH_LAYER_tile} `
                     -D OpenCV_DIR="${installOpenCVDir}" `
                     $ncnnDir

               Write-Host "   make" -ForegroundColor Yellow
               make -j4
               Write-Host "   make install" -ForegroundColor Yellow
               make install
            }
         }
      }
      finally
      {
         Set-Location $current
         Write-Host "End Build ncnn" -ForegroundColor Green
      }

      return $ret
   }
}

function ConfigCPU([Config]$Config)
{
   if ($global:IsWindows)
   {
      CallVisualStudioDeveloperConsole
   }

   $Builder = [ThirdPartyBuilder]::new($Config)

   # Build Protobuf
   $installProtobufDir = $Builder.BuildProtobuf()

   # Build opencv
   $installOpenCVDir = $Builder.BuildOpenCV($False)

   # Build ncnn
   $installNcnnDir = $Builder.BuildNcnn($installProtobufDir, "OFF")

   # To inclue src/layer
   $ncnnDir = $Config.GetNcnnRootDir()

   # Build NcnnDotNet.Native
   Write-Host "Start Build NcnnDotNet.Native" -ForegroundColor Green
   if ($global:IsWindows)
   {
      $env:OpenCV_DIR = $installOpenCVDir
      Write-Host "   cmake -G `"NMake Makefiles`" `
         -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=ON `
         -D NCNN_VULKAN:BOOL=OFF `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         .." -ForegroundColor Yellow
      cmake -G "NMake Makefiles" `
            -D CMAKE_BUILD_TYPE=$Configuration `
            -D BUILD_SHARED_LIBS=ON `
            -D NCNN_VULKAN:BOOL=OFF `
            -D OpenCV_DIR="${installOpenCVDir}" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            ..
   }
   else
   {
      $env:OpenCV_DIR = $installOpenCVDir
      Write-Host "   cmake -D BUILD_SHARED_LIBS=ON `
         -D NCNN_VULKAN:BOOL=OFF `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         .." -ForegroundColor Yellow
      cmake -D BUILD_SHARED_LIBS=ON `
            -D NCNN_VULKAN:BOOL=OFF `
            -D OpenCV_DIR="${installOpenCVDir}" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            ..
   }
}

function ConfigVulkan([Config]$Config)
{
   $vulkanSDKDirectory = $Config.GetVulkanSDKDirectory()
   if (!$vulkanSDKDirectory) {
      Write-Host "Error: Specify Vulkan SDK directory" -ForegroundColor Red
      exit -1
   }

   if ((Test-Path $vulkanSDKDirectory) -eq $False) {
      Write-Host "Error: Specified VulkanSDK directory '${vulkanSDKDirectory}' does not found" -ForegroundColor Red
      exit -1
   }

   if ($global:IsWindows)
   {
      CallVisualStudioDeveloperConsole
   }

   $Builder = [ThirdPartyBuilder]::new($Config)

   # Build Protobuf
   $installProtobufDir = $Builder.BuildProtobuf()

   # Build opencv
   $installOpenCVDir = $Builder.BuildOpenCV($False)

   # Build ncnn
   $installNcnnDir = $Builder.BuildNcnn($installProtobufDir, "ON")

   # To inclue src/layer
   $ncnnDir = $Config.GetNcnnRootDir()

   # Build NcnnDotNet.Native
   Write-Host "Start Build NcnnDotNet.Native" -ForegroundColor Green
   if ($global:IsWindows)
   {
      $env:OpenCV_DIR = $installOpenCVDir
      $env:ncnn_DIR = "${installNcnnDir}/lib/cmake/ncnn"
      Write-Host "   cmake -G `"NMake Makefiles`" `
         -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=ON `
         -D NCNN_VULKAN:BOOL=ON `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         .." -ForegroundColor Yellow
      cmake -G "NMake Makefiles" `
            -D CMAKE_BUILD_TYPE=$Configuration `
            -D BUILD_SHARED_LIBS=ON `
            -D NCNN_VULKAN:BOOL=ON `
            -D OpenCV_DIR="${installOpenCVDir}" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            ..
   }
   elseif ($global:IsMacOS)
   {
      # build vulkan variables
      $Vulkan_INCLUDE_DIR = Join-Path $env:VULKAN_SDK MoltenVK | `
                              Join-Path -Childpath include
      $Vulkan_LIBRARY = Join-Path $env:VULKAN_SDK MoltenVK | `
                        Join-Path -Childpath dylib | `
                        Join-Path -Childpath macOS | `
                        Join-Path -Childpath libMoltenVK.dylib

      $env:OpenCV_DIR = $installOpenCVDir
      $env:ncnn_DIR = "${installNcnnDir}/lib/cmake/ncnn"
      Write-Host "   cmake -D BUILD_SHARED_LIBS=ON `
         -D NCNN_VULKAN:BOOL=ON `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         -D Vulkan_INCLUDE_DIR=`"${Vulkan_INCLUDE_DIR}`" `
         -D Vulkan_LIBRARY=`"${Vulkan_LIBRARY}`" `
         .." -ForegroundColor Yellow
      cmake -D BUILD_SHARED_LIBS=ON `
            -D NCNN_VULKAN:BOOL=ON `
            -D OpenCV_DIR="${installOpenCVDir}" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            -D Vulkan_INCLUDE_DIR="${Vulkan_INCLUDE_DIR}" `
            -D Vulkan_LIBRARY="${Vulkan_LIBRARY}" `
            ..
   }
   else
   {
      $env:OpenCV_DIR = $installOpenCVDir
      $env:ncnn_DIR = "${installNcnnDir}/lib/cmake/ncnn"
      Write-Host "   cmake -D BUILD_SHARED_LIBS=ON `
         -D NCNN_VULKAN:BOOL=ON `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         .." -ForegroundColor Yellow
      cmake -D BUILD_SHARED_LIBS=ON `
            -D NCNN_VULKAN:BOOL=ON `
            -D OpenCV_DIR="${installOpenCVDir}" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            ..
   }
}

function ConfigARM([Config]$Config)
{
   if ($global:IsWindows)
   {
      CallVisualStudioDeveloperConsole
   }

   $Builder = [ThirdPartyBuilder]::new($Config)

   # Build Protobuf
   $installProtobufDir = $Builder.BuildProtobuf()

   # Build opencv
   $installOpenCVDir = $Builder.BuildOpenCV($False)

   # Build ncnn
   $installNcnnDir = $Builder.BuildNcnn($installProtobufDir, "OFF")

   # To inclue src/layer
   $ncnnDir = $Config.GetNcnnRootDir()

   # Build NcnnDotNet.Native
   Write-Host "Start Build NcnnDotNet.Native" -ForegroundColor Green
   if ($IsWindows)
   {
      $env:OpenCV_DIR = $installOpenCVDir
      Write-Host "   cmake -G `"NMake Makefiles`" `
         -D CMAKE_BUILD_TYPE=$Configuration `
         -D BUILD_SHARED_LIBS=ON `
         -D NCNN_VULKAN:BOOL=OFF `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         .." -ForegroundColor Yellow
      cmake -G "NMake Makefiles" `
            -D CMAKE_BUILD_TYPE=$Configuration `
            -D BUILD_SHARED_LIBS=ON `
            -D NCNN_VULKAN:BOOL=OFF `
            -D OpenCV_DIR="${installOpenCVDir}" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            ..
   }
   else
   {
      $toolchain = $Config.GetToolchainFile()

      $env:OpenCV_DIR = $installOpenCVDir
      Write-Host "   cmake -D BUILD_SHARED_LIBS=ON `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D NCNN_VULKAN:BOOL=OFF `
         -D OpenCV_DIR=`"${installOpenCVDir}`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         .." -ForegroundColor Yellow
      cmake -D BUILD_SHARED_LIBS=ON `
            -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
            -D NCNN_VULKAN:BOOL=OFF `
            -D OpenCV_DIR="${installOpenCVDir}" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            ..
   }
}

function ConfigUWP([Config]$Config)
{
   if ($global:IsWindows)
   {
      CallVisualStudioDeveloperConsole

      $Builder = [ThirdPartyBuilder]::new($Config)

      # Build Protobuf
      $installProtobufDir = $Builder.BuildProtobuf()

      # Build opencv
      $installOpenCVDir = $Builder.BuildOpenCV($False)

      # Build ncnn
      $installNcnnDir = $Builder.BuildNcnn($installProtobufDir, "OFF")

      # To inclue src/layer
      $ncnnDir = $Config.GetNcnnRootDir()

      # Build NcnnDotNet.Native
      Write-Host "Start Build NcnnDotNet.Native" -ForegroundColor Green

      if ($Config.GetTarget() -eq "arm")
      {
         $env:OpenCV_DIR = $installOpenCVDir
         Write-Host "   cmake -G `"NMake Makefiles`" `
      -D CMAKE_BUILD_TYPE=$Configuration `
      -D CMAKE_SYSTEM_NAME=WindowsStore `
      -D CMAKE_SYSTEM_VERSION=10.0 `
      -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
      -D _WINDLL=ON `
      -D _WIN32_UNIVERSAL_APP=ON `
      -D BUILD_SHARED_LIBS=ON `
      -D NCNN_VULKAN:BOOL=OFF `
      -D OpenCV_DIR=`"${installOpenCVDir}`" `
      -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
      -D ncnn_SRC_DIR=`"${ncnnDir}`" `
      -D NO_GUI_SUPPORT:BOOL=ON `
      .." -ForegroundColor Yellow
         cmake -G "NMake Makefiles" `
               -D CMAKE_BUILD_TYPE=$Configuration `
               -D CMAKE_SYSTEM_NAME=WindowsStore `
               -D CMAKE_SYSTEM_VERSION=10.0 `
               -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
               -D _WINDLL=ON `
               -D _WIN32_UNIVERSAL_APP=ON `
               -D BUILD_SHARED_LIBS=ON `
               -D NCNN_VULKAN:BOOL=OFF `
               -D OpenCV_DIR="${installOpenCVDir}" `
               -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
               -D ncnn_SRC_DIR="${ncnnDir}" `
               -D NO_GUI_SUPPORT:BOOL=ON `
               ..
      }
      else
      {
         $env:OpenCV_DIR = $installOpenCVDir
         Write-Host "   cmake -G `"NMake Makefiles`" `
      -D CMAKE_BUILD_TYPE=$Configuration `
      -D CMAKE_SYSTEM_NAME=WindowsStore `
      -D CMAKE_SYSTEM_VERSION=10.0 `
      -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
      -D _WINDLL=ON `
      -D _WIN32_UNIVERSAL_APP=ON `
      -D BUILD_SHARED_LIBS=ON `
      -D NCNN_VULKAN:BOOL=OFF `
      -D OpenCV_DIR=`"${installOpenCVDir}`" `
      -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
      -D ncnn_SRC_DIR=`"${ncnnDir}`" `
      -D NO_GUI_SUPPORT:BOOL=ON `
      .." -ForegroundColor Yellow
         cmake -G "NMake Makefiles" `
               -D CMAKE_BUILD_TYPE=$Configuration `
               -D CMAKE_SYSTEM_NAME=WindowsStore `
               -D CMAKE_SYSTEM_VERSION=10.0 `
               -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
               -D _WINDLL=ON `
               -D _WIN32_UNIVERSAL_APP=ON `
               -D BUILD_SHARED_LIBS=ON `
               -D NCNN_VULKAN:BOOL=OFF `
               -D OpenCV_DIR="${installOpenCVDir}" `
               -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
               -D ncnn_SRC_DIR="${ncnnDir}" `
               -D NO_GUI_SUPPORT:BOOL=ON `
               ..
      }
   }
}

function ConfigANDROID([Config]$Config)
{
   if (!${env:ANDROID_NDK_HOME})
   {
      Write-Host "Error: Specify ANDROID_NDK_HOME environmental value" -ForegroundColor Red
      exit -1
   }

   if ((Test-Path "${env:ANDROID_NDK_HOME}/build/cmake/android.toolchain.cmake") -eq $False)
   {
      Write-Host "Error: Specified Android NDK toolchain '${env:ANDROID_NDK_HOME}/build/cmake/android.toolchain.cmake' does not found" -ForegroundColor Red
      exit -1
   }

   $Builder = [ThirdPartyBuilder]::new($Config)

   # Build opencv
   $installOpenCVDir = $Builder.BuildOpenCV($False)

   # Build ncnn
   $installNcnnDir = $Builder.BuildNcnn($installProtobufDir, "ON")

   # To inclue src/layer
   $ncnnDir = $Config.GetNcnnRootDir()

   # # Build NcnnDotNet.Native
   Write-Host "Start Build NcnnDotNet.Native" -ForegroundColor Green

   $level = $Config.GetAndroidNativeAPILevel()
   $abi = $Config.GetAndroidABI()

   # https://github.com/Tencent/ncnn/wiki/FAQ-ncnn-throw-error#undefined-reference-to-__kmpc_xyz_xyz
   # $env:NDK_TOOLCHAIN_VERSION = 4.9
   $env:OpenCV_DIR = "${installOpenCVDir}/sdk/native/jni"
   $env:ncnn_DIR = "${installNcnnDir}/lib/cmake/ncnn"
      Write-Host "   cmake -D CMAKE_TOOLCHAIN_FILE=${env:ANDROID_NDK}/build/cmake/android.toolchain.cmake `
   -D ANDROID_ABI=$abi `
   -D ANDROID_PLATFORM=android-$level `
   -D ANDROID_CPP_FEATURES:STRING=`"exceptions rtti`" `
   -D BUILD_SHARED_LIBS=ON `
   -D OpenCV_DIR=`"${installOpenCVDir}/sdk/native/jni`" `
   -D OpenCV_INSTALL_DIR=`"${installOpenCVDir}`" `
   -D NCNN_VULKAN:BOOL=ON `
   -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
   -D ncnn_SRC_DIR=`"${ncnnDir}`" `
   -D NO_GUI_SUPPORT:BOOL=ON `
   .." -ForegroundColor Yellow
      cmake -D CMAKE_TOOLCHAIN_FILE=${env:ANDROID_NDK}/build/cmake/android.toolchain.cmake `
            -D ANDROID_ABI=$abi `
            -D ANDROID_PLATFORM=android-$level `
            -D ANDROID_CPP_FEATURES:STRING="exceptions rtti" `
            -D BUILD_SHARED_LIBS=ON `
            -D OpenCV_DIR="${installOpenCVDir}/sdk/native/jni" `
            -D OpenCV_INSTALL_DIR="${installOpenCVDir}" `
            -D NCNN_VULKAN:BOOL=ON `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            -D NO_GUI_SUPPORT:BOOL=ON `
            ..
}

function ConfigIOS([Config]$Config)
{
   if ($IsMacOS)
   {
      $Builder = [ThirdPartyBuilder]::new($Config)

      $osxArchitectures = $Config.GetOSXArchitectures()

      $vulkanOnOff = "ON"
      $targetPlatform = ""
      switch ($osxArchitectures)
      {
         "arm64e"
         {
            $vulkanOnOff = "ON"
            $targetPlatform = "ios-arm64"
         }
         "arm64"
         {
            $vulkanOnOff = "ON"
            $targetPlatform = "ios-arm64"
         }
         "arm"
         {
            $vulkanOnOff = "OFF"
            $targetPlatform = ""
         }
         "armv7"
         {
            $vulkanOnOff = "OFF"
            $targetPlatform = ""
         }
         "armv7s"
         {
            $vulkanOnOff = "OFF"
            $targetPlatform = ""
         }
         "i386"
         {
            $vulkanOnOff = "OFF"
            $targetPlatform = ""
         }
         "x86_64"
         {
            $vulkanOnOff = "ON"
            $targetPlatform = "ios-arm64_x86_64-simulator"
         }
      }

      # Build opencv
      $installOpenCVDir = $Builder.BuildOpenCV($False)

      # Build ncnn
      $installNcnnDir = $Builder.BuildNcnn($installProtobufDir, $vulkanOnOff)

      # To inclue src/layer
      $ncnnDir = $Config.GetNcnnRootDir()

      # # Build NcnnDotNet.Native
      Write-Host "Start Build NcnnDotNet.Native" -ForegroundColor Green

      $developerDir = $Config.GetDeveloperDir()
      $osxArchitectures = $Config.GetOSXArchitectures()
      $toolchain = $Config.GetToolchainFile()

      $OSX_SYSROOT = $Config.GetIOSSDK($osxArchitectures, $developerDir)

      $env:OpenCV_DIR = "${installOpenCVDir}/share/OpenCV"
      $env:ncnn_DIR = "${installNcnnDir}/lib/cmake/ncnn"

      # use libc++ rather than libstdc++
      Write-Host "   cmake -D CMAKE_SYSTEM_NAME=iOS `
         -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
         -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D CMAKE_CXX_FLAGS=`"-std=c++11 -stdlib=libc++ -static`" `
         -D CMAKE_EXE_LINKER_FLAGS=`"-std=c++11 -stdlib=libc++ -static`" `
         -D NCNN_VULKAN:BOOL=${vulkanOnOff} `
         -D OpenCV_DIR=`"${installOpenCVDir}/share/OpenCV`" `
         -D ncnn_DIR=`"${installNcnnDir}/lib/cmake/ncnn`" `
         -D ncnn_SRC_DIR=`"${ncnnDir}`" `
         -D Vulkan_INCLUDE_DIR=`"${env:VULKAN_SDK}/MoltenVK/include`" `
         -D Vulkan_LIBRARY=`"${env:VULKAN_SDK}/MoltenVK/MoltenVK.xcframework/${targetPlatform}/libMoltenVK.a`" `
         -D NO_GUI_SUPPORT:BOOL=ON `
         .." -ForegroundColor Yellow
      cmake -D CMAKE_SYSTEM_NAME=iOS `
            -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
            -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
            -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
            -D CMAKE_CXX_FLAGS="-std=c++11 -stdlib=libc++ -static" `
            -D CMAKE_EXE_LINKER_FLAGS="-std=c++11 -stdlib=libc++ -static" `
            -D BUILD_SHARED_LIBS=OFF `
            -D NCNN_VULKAN:BOOL=${vulkanOnOff} `
            -D OpenCV_DIR="${installOpenCVDir}/share/OpenCV" `
            -D ncnn_DIR="${installNcnnDir}/lib/cmake/ncnn" `
            -D ncnn_SRC_DIR="${ncnnDir}" `
            -D Vulkan_INCLUDE_DIR="${env:VULKAN_SDK}/MoltenVK/include" `
            -D Vulkan_LIBRARY="${env:VULKAN_SDK}/MoltenVK/MoltenVK.xcframework/${targetPlatform}/libMoltenVK.a" `
            -D NO_GUI_SUPPORT:BOOL=ON `
            ..
   }
   else
   {
      Write-Host "Error: This platform can not build iOS binary" -ForegroundColor Red
      exit -1
   }
}

function Build([Config]$Config)
{
   $Current = Get-Location

   Write-Host "git submodule update --init --recursive" -ForegroundColor Yellow
   git submodule update --init --recursive

   $Output = $Config.GetBuildDirectoryName("")
   if ((Test-Path $Output) -eq $False)
   {
      New-Item $Output -ItemType Directory
   }

   Set-Location -Path $Output

   $Target = $Config.GetTarget()
   $Platform = $Config.GetPlatform()

   switch ($Platform)
   {
      "desktop"
      {
         switch ($Target)
         {
            "cpu"
            {
               ConfigCPU $Config
            }
            "vulkan"
            {
               ConfigVulkan $Config
            }
            "arm"
            {
               ConfigARM $Config
            }
         }
      }
      "android"
      {
         ConfigANDROID $Config
      }
      "ios"
      {
         ConfigIOS $Config
      }
      "uwp"
      {
         ConfigUWP $Config
      }
   }

   $cofiguration = $Config.GetConfigurationName()
   Write-Host "cmake --build . --config ${cofiguration}" -ForegroundColor Yellow
   cmake --build . --config ${cofiguration}

   $Platform = $Config.GetPlatform()

   # Post build
   switch ($Platform)
   {
      "ios"
      {
         $BuildTargets = @()
         # $BuildTargets += New-Object PSObject -Property @{ Platform = "arm64e";  Vulkan = $True; }
         $BuildTargets += New-Object PSObject -Property @{ Platform = "arm64";   Vulkan = $True;  StaticLib = "" }
         # $BuildTargets += New-Object PSObject -Property @{ Platform = "arm";     Vulkan = $False; }
         # $BuildTargets += New-Object PSObject -Property @{ Platform = "armv7";   Vulkan = $False; }
         # $BuildTargets += New-Object PSObject -Property @{ Platform = "armv7s";  Vulkan = $False; }
         # $BuildTargets += New-Object PSObject -Property @{ Platform = "i386";    Vulkan = $False; }
         $BuildTargets += New-Object PSObject -Property @{ Platform = "x86_64";  Vulkan = $True; }

         foreach($BuildTarget in $BuildTargets)
         {
            $platform = $BuildTarget.Platform
            $vulkan = $BuildTarget.Vulkan
            $osxArchitectures = $Config.GetOSXArchitectures()

            if ($osxArchitectures -eq $platform )
            {
               Write-Host "Invoke libtool for ${platform}" -ForegroundColor Yellow

               switch ($platform)
               {
                  "arm64e"
                  {
                     $Vulkan_LIBRARY = Join-Path $env:VULKAN_SDK MoltenVK | `
                                       Join-Path -Childpath MoltenVK.xcframework | `
                                       Join-Path -Childpath "ios-arm64" | `
                                       Join-Path -Childpath libMoltenVK.a
                  }
                  "arm64"
                  {
                     $Vulkan_LIBRARY = Join-Path $env:VULKAN_SDK MoltenVK | `
                                       Join-Path -Childpath MoltenVK.xcframework | `
                                       Join-Path -Childpath "ios-arm64" | `
                                       Join-Path -Childpath libMoltenVK.a
                  }
                  "arm"
                  {
                     $targetPlatform = ""
                  }
                  "armv7"
                  {
                     $targetPlatform = ""
                  }
                  "armv7s"
                  {
                     $targetPlatform = ""
                  }
                  "i386"
                  {
                     $targetPlatform = ""
                  }
                  "x86_64"
                  {
                     $Vulkan_LIBRARY = Join-Path $env:VULKAN_SDK MoltenVK | `
                                       Join-Path -Childpath MoltenVK.xcframework | `
                                       Join-Path -Childpath "ios-arm64_x86_64-simulator" | `
                                       Join-Path -Childpath libMoltenVK.a
                  }
               }

               if (Test-Path "libNcnnDotNetNative_merged.a")
               {
                  Remove-Item "libNcnnDotNetNative_merged.a"
               }

               # https://github.com/abseil/abseil-cpp/issues/604
               if ($vulkan -eq $True)
               {
                  libtool -o "libNcnnDotNetNative_merged.a" `
                             "libNcnnDotNetNative.a" `
                             "opencv/install/lib/libopencv_world.a" `
                             "opencv/install/share/OpenCV/3rdparty/lib/liblibpng.a" `
                             "opencv/install/share/OpenCV/3rdparty/lib/liblibjpeg.a" `
                             "opencv/install/share/OpenCV/3rdparty/lib/libzlib.a" `
                             "ncnn/install/lib/libMachineIndependent.a" `
                             "ncnn/install/lib/libOGLCompiler.a" `
                             "ncnn/install/lib/libncnn.a" `
                             "ncnn/install/lib/libOSDependent.a" `
                             "ncnn/install/lib/libGenericCodeGen.a" `
                             "ncnn/install/lib/libSPIRV.a" `
                             "ncnn/install/lib/libglslang.a" `
                             "${Vulkan_LIBRARY}"
               }
               else
               {
                  libtool -o "libNcnnDotNetNative_merged.a" `
                             "libNcnnDotNetNative.a" `
                             "opencv/install/lib/libopencv_world.a" `
                             "opencv/install/share/OpenCV/3rdparty/lib/liblibpng.a" `
                             "opencv/install/share/OpenCV/3rdparty/lib/liblibjpeg.a" `
                             "opencv/install/share/OpenCV/3rdparty/lib/libzlib.a" `
                             "ncnn/install/lib/libncnn.a"
               }
            }
         }
      }
   }

   # Move to Root directory
   Set-Location -Path $Current
}

function CopyToArtifact()
{
   Param([string]$srcDir, [string]$build, [string]$libraryName, [string]$dstDir, [string]$rid, [string]$configuration="")

   if ($configuration)
   {
      $binary = Join-Path ${srcDir} ${build}  | `
               Join-Path -ChildPath ${configuration} | `
               Join-Path -ChildPath ${libraryName}
   }
   else
   {
      $binary = Join-Path ${srcDir} ${build}  | `
               Join-Path -ChildPath ${libraryName}
   }

   $dstDir = Join-Path $dstDir runtimes | `
             Join-Path -ChildPath ${rid} | `
             Join-Path -ChildPath native

   $output = Join-Path $dstDir $libraryName

   if (!(Test-Path($binary)))
   {
      Write-Host "${binary} does not exist" -ForegroundColor Red
   }

   if (!(Test-Path($dstDir)))
   {
      Write-Host "${dstDir} does not exist" -ForegroundColor Red
   }

   Write-Host "Copy ${libraryName} to ${output}" -ForegroundColor Green
   Copy-Item ${binary} ${output}
}