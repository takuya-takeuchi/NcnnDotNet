Param()

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$NugetPath = Join-Path $NcnnDotNetRoot "nuget" | `
             Join-Path -ChildPath "BuildUtils.ps1"
import-module $NugetPath -function *

$OperatingSystem="ios"

# Store current directory
$Current = Get-Location
$NcnnDotNetRoot = (Split-Path (Get-Location) -Parent)
$NcnnDotNetSourceRoot = Join-Path $NcnnDotNetRoot src

$BuildSourceHash = [Config]::GetBinaryLibraryIOSHash()

$VulkanSDKDir = ${env:VULKAN_SDK}
if ([string]::IsNullOrEmpty($VulkanSDKDir))
{
   Write-Host "Environmental Value 'VULKAN_SDK' is not defined." -ForegroundColor Yellow
}

if ($VulkanSDKDir -And !(Test-Path $VulkanSDKDir))
{
   Write-Host "Environmental Value 'VULKAN_SDK' does not exist." -ForegroundColor Yellow
}

$DeveloperDir = ${env:DEVELOPER_DIR}
if ([string]::IsNullOrEmpty($DeveloperDir))
{
   Write-Host "Environmental Value 'DEVELOPER_DIR' is not defined." -ForegroundColor Yellow
}

if ($DeveloperDir -And !(Test-Path $DeveloperDir))
{
   Write-Host "Environmental Value 'DEVELOPER_DIR' does not exist." -ForegroundColor Yellow
}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "vulkan"; Device = "ios";           Architecture = 64; Option = "arm64"  }
# $BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "vulkan"; Device = "ios";           Architecture = 64; Option = "arm64e" }
# $BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "cpu";    Device = "ios";           Architecture = 32; Option = "armv7s" }
# $BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "cpu";    Device = "ios";           Architecture = 32; Option = "armv7"  }
$BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "cpu";    Device = "ios-simulator"; Architecture = 64; Option = "x86_64" }
# $BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "cpu";    Device = "ios-simulator"; Architecture = 32; Option = "i386"   }

foreach($BuildTarget in $BuildTargets)
{
   $platform = $BuildTarget.Platform
   $target = $BuildTarget.Target
   $architecture = $BuildTarget.Architecture
   $device = $BuildTarget.Device
   $option = $BuildTarget.Option

   $Config = [Config]::new($NcnnDotNetRoot, "Release", $target, $architecture, $platform, $option)
   $libraryDir = Join-Path "artifacts" $Config.GetArtifactDirectoryName()
   $build = $Config.GetBuildDirectoryName($OperatingSystem)

   foreach ($key in $BuildSourceHash.keys)
   {
      $srcDir = Join-Path $NcnnDotNetSourceRoot $key

      # Move to build target directory
      Set-Location -Path $srcDir

      $arc = $Config.GetArchitectureName()
      Write-Host "Build $key [$arc] for $target" -ForegroundColor Green
      Build -Config $Config

      if ($lastexitcode -ne 0)
      {
         Set-Location -Path $Current
         exit -1
      }
   }
  
   # Copy output binary
   foreach ($key in $BuildSourceHash.keys)
   {
      $srcDir = Join-Path $NcnnDotNetSourceRoot $key
      $dll = $BuildSourceHash[$key]
      $dstDir = Join-Path $Current $libraryDir

      CopyToArtifact -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $option
   }
}

# create fat binary
Set-Location $ScriptPath
foreach ($key in $BuildSourceHash.keys)
{
   libtool -static `
           "artifacts/ios/runtimes/arm64/native/${key}" `
         #   "artifacts/ios/runtimes/arm64e/native/${key}" `
         #   "artifacts/ios/runtimes/armv7/native/${key}" `
         #   "artifacts/ios/runtimes/armv7s/native/${key}" `
         #   "artifacts/ios/runtimes/i386/native/${key}" `
           "artifacts/ios/runtimes/x86_64/native/${key}" `
           -o "artifacts/ios/runtimes/fat/native/${key}"
}

# Move to Root directory 
Set-Location -Path $Current