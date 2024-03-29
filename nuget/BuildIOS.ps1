Param()

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $NcnnDotNetRoot "nuget" | `
              Join-Path -ChildPath "BuildUtils.ps1"
import-module $ScriptPath -function *

$OperatingSystem="ios"

# Store current directory
$Current = Get-Location

$BuildSourceHash = [Config]::GetBinaryLibraryIOSHash()

$VulkanSDKDir = $env:VULKAN_SDK
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
$BuildTargets += [BuildTarget]::new("ios", "vulkan", 64, "arm64",  "" )
$BuildTargets += [BuildTarget]::new("ios", "vulkan", 64, "arm64e", "" )
$BuildTargets += [BuildTarget]::new("ios", "cpu",    32, "armv7s", "" )
$BuildTargets += [BuildTarget]::new("ios", "cpu",    32, "armv7",  "" )
$BuildTargets += [BuildTarget]::new("ios", "vulkan", 64, "x86_64", "" )
$BuildTargets += [BuildTarget]::new("ios", "cpu",    32, "i386",   "" )

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