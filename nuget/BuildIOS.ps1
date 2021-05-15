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

$BuildSourceHash = [Config]::GetBinaryLibraryOSXHash()

$VulkanSDKDir = $env:VULKAN_SDK
if ([string]::IsNullOrEmpty($VulkanSDKDir))
{
   Write-Host "Environmental Value 'VULKAN_SDK' is not defined." -ForegroundColor Yellow
}

if ($VulkanSDKDir -And !(Test-Path $VulkanSDKDir))
{
   Write-Host "Environmental Value 'VULKAN_SDK' does not exist." -ForegroundColor Yellow
}

# OS - to build for iOS (armv7, armv7s, arm64) DEPRECATED in favour of OS64
# OS64 - to build for iOS (arm64 only)
# OS64COMBINED - to build for iOS & iOS Simulator (FAT lib) (arm64, x86_64)
# SIMULATOR - to build for iOS simulator 32 bit (i386) DEPRECATED
# SIMULATOR64 - to build for iOS simulator 64 bit (x86_64)
# SIMULATORARM64 - to build for iOS simulator 64 bit (arm64)
# TVOS - to build for tvOS (arm64)
# TVOSCOMBINED - to build for tvOS & tvOS Simulator (arm64, x86_64)
# SIMULATOR_TVOS - to build for tvOS Simulator (x86_64)
# WATCHOS - to build for watchOS (armv7k, arm64_32)
# WATCHOSCOMBINED - to build for watchOS & Simulator (armv7k, arm64_32, i386)
# SIMULATOR_WATCHOS - to build for watchOS Simulator (i386)
# MAC - to build for macOS (x86_64)
# MAC_ARM64 - to build for macOS on Apple Silicon (arm64)
# MAC_CATALYST - to build iOS for Mac (Catalyst, x86_64)
# MAC_CATALYST_ARM64 - to build iOS for Mac on Apple Silicon (Catalyst, arm64)
$BuildTargets = @()
# $BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "cpu";     Architecture = 64; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{ Platform = "ios"; Target = "cpu";  Architecture = 64; Option = "os64" }

foreach($BuildTarget in $BuildTargets)
{
   $platform = $BuildTarget.Platform
   $target = $BuildTarget.Target
   $architecture = $BuildTarget.Architecture
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

      CopyToArtifact -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $target
   }
}

# Move to Root directory 
Set-Location -Path $Current