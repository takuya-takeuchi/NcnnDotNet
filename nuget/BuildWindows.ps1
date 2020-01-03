Param()

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$NugetPath = Join-Path $NcnnDotNetRoot "nuget" | `
             Join-Path -ChildPath "BuildUtils.ps1"
import-module $NugetPath -function *

$OperatingSystem="win"

# Store current directory
$Current = Get-Location
$NcnnDotNetRoot = (Split-Path (Get-Location) -Parent)
$NcnnDotNetSourceRoot = Join-Path $NcnnDotNetRoot src

$BuildSourceHash = [Config]::GetBinaryLibraryWindowsHash()

$VulkanSDKDir = $env:VULKAN_SDK
if ([string]::IsNullOrEmpty($VulkanSDKDir))
{
   Write-Host "Environmental Value 'VULKAN_SDK' is not defined." -ForegroundColor Yellow
}

if ($VulkanSDKDir -And !(Test-Path $VulkanSDKDir))
{
   Write-Host "Environmental Value 'VULKAN_SDK' does not exist." -ForegroundColor Yellow
}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";    Architecture = 64; RID = "$OperatingSystem-x64";  }
$BuildTargets += New-Object PSObject -Property @{Target = "vulkan"; Architecture = 64; RID = "$OperatingSystem-x64";  }

foreach ($BuildTarget in $BuildTargets)
{
   $target = $BuildTarget.Target
   $architecture = $BuildTarget.Architecture
   $rid = $BuildTarget.RID
   $option = ""

   $Config = [Config]::new($NcnnDotNetRoot, "Release", $target, $architecture, "desktop", $option)
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

      CopyToArtifact -configuration "Release" -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
   }
}

# Move to Root directory 
Set-Location -Path $Current