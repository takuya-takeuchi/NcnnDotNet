Param()

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$NugetPath = Join-Path $NcnnDotNetRoot "nuget" | `
             Join-Path -ChildPath "BuildUtils.ps1"
import-module $NugetPath -function *

$OperatingSystem="linux"
$Distribution="ubuntu"
$DistributionVersion="16"
$AndroidVersion="28.0.3-r20-jdk8"
$AndroidNativeApiLevel="24"

# Store current directory
$Current = Get-Location
$NcnnDotNetRoot = (Split-Path (Get-Location) -Parent)
$NcnnDotNetSourceRoot = Join-Path $NcnnDotNetRoot src
$DockerDir = Join-Path $NcnnDotNetRoot docker

Set-Location -Path $DockerDir

$DockerFileDir = Join-Path $DockerDir build  | `
                 Join-Path -ChildPath $Distribution | `
                 Join-Path -ChildPath $DistributionVersion

$BuildSourceHash = [Config]::GetBinaryLibraryLinuxHash()

# https://docs.microsoft.com/ja-jp/xamarin/cross-platform/cpp/
# arm64-v8a
# armeabi-v7a
# x86
# x86_64
$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{ Platform = "android"; Target = "arm"; Architecture = 64; RID = "arm64-v8a"   }
$BuildTargets += New-Object PSObject -Property @{ Platform = "android"; Target = "arm"; Architecture = 32; RID = "armeabi-v7a" }
$BuildTargets += New-Object PSObject -Property @{ Platform = "android"; Target = "arm"; Architecture = 32; RID = "x86"         }
$BuildTargets += New-Object PSObject -Property @{ Platform = "android"; Target = "arm"; Architecture = 64; RID = "x86_64"      }

$dockername = "ncnndotnet/build/$Distribution/$DistributionVersion/android/$AndroidVersion"
$imagename  = "ncnndotnet/devel/$Distribution/$DistributionVersion/android/$AndroidVersion"

Write-Host "Start 'docker build -t $dockername $DockerFileDir --build-arg IMAGE_NAME=""$imagename""'" -ForegroundColor Green
docker build --force-rm=true -t $dockername $DockerFileDir --build-arg IMAGE_NAME="$imagename"

if ($lastexitcode -ne 0)
{
   Set-Location -Path $Current
   exit -1
}

foreach($BuildTarget in $BuildTargets)
{
   $platform = $BuildTarget.Platform
   $target = $BuildTarget.Target
   $architecture = $BuildTarget.Architecture
   $rid = $BuildTarget.RID

   $setting =
   @{
      'ANDROID_ABI' = $rid;
      'ANDROID_NATIVE_API_LEVEL' = $AndroidNativeApiLevel
   }

   # Build binary
   foreach ($key in $BuildSourceHash.keys)
   {
      $option = [Config]::Base64Encode((ConvertTo-Json -Compress $setting))
      $Config = [Config]::new($NcnnDotNetRoot, "Release", $target, $architecture, $platform, $option)
      $libraryDir = Join-Path "artifacts" $Config.GetArtifactDirectoryName()
      Write-Host "Start 'docker run --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t $dockername'" -ForegroundColor Green
      docker run --rm `
                  -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                  -e "LOCAL_UID=$(id -u $env:USER)" `
                  -e "LOCAL_GID=$(id -g $env:USER)" `
                  -e "CIBuildDir=/opt/data/builds" `
                  -t "$dockername" $key $target $architecture $platform $option
   
      if ($lastexitcode -ne 0)
      {
         Set-Location -Path $Current
         exit -1
      }
   }

   # Copy output binary
   foreach ($key in $BuildSourceHash.keys)
   {
      $build = $Config.GetBuildDirectoryName($OperatingSystem)
      $srcDir = Join-Path $NcnnDotNetSourceRoot $key
      $dll = $BuildSourceHash[$key]
      $dstDir = Join-Path $Current $libraryDir

      CopyToArtifact -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
   }
}

# Move to Root directory 
Set-Location -Path $Current