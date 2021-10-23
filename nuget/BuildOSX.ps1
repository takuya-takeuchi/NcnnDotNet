Param()

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $NcnnDotNetRoot "nuget" | `
              Join-Path -ChildPath "BuildUtils.ps1"
import-module $ScriptPath -function *

$OperatingSystem="osx"

# Store current directory
$Current = Get-Location

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

# https://docs.microsoft.com/ja-jp/dotnet/core/rid-catalog#macos-rids
# osx-x86 does not support
$BuildTargets = @()
$BuildTargets += [BuildTarget]::new("desktop", "cpu",    64, "$OperatingSystem-x64", "" )
$BuildTargets += [BuildTarget]::new("desktop", "vulkan", 64, "$OperatingSystem-x64", "" )

foreach ($BuildTarget in $BuildTargets)
{
   $BuildTarget.OperatingSystem = ${OperatingSystem}
   
   $ret = [Config]::Build($NcnnDotNetRoot, $False, $BuildSourceHash, $BuildTarget)
   if ($ret -eq $False)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory 
Set-Location -Path $Current