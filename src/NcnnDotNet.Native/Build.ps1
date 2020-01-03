#***************************************
#Arguments
#%1: Build Configuration (Release/Debug)
#%2: Target (cpu/vulkan/arm)
#%3: Architecture (32/64)
#%4: Platform (desktop,android/ios/uwp)
#%5: Optional Argument
#   Reserved
#***************************************
Param
(
   [Parameter(
   Mandatory=$True,
   Position = 1
   )][string]
   $Configuration,

   [Parameter(
   Mandatory=$True,
   Position = 2
   )][string]
   $Target,

   [Parameter(
   Mandatory=$True,
   Position = 3
   )][int]
   $Architecture,

   [Parameter(
   Mandatory=$False,
   Position = 4
   )][string]
   $Platform,

   [Parameter(
   Mandatory=$False,
   Position = 5
   )][string]
   $Option
)

# import class and function
$ScriptPath = $PSScriptRoot
Write-Host "Build"(Split-Path $ScriptPath -Leaf) -ForegroundColor Green

$SrcPath = Split-Path $ScriptPath -Parent
$NcnnDotNetRoot = Split-Path $SrcPath -Parent
$NugetPath = Join-Path $NcnnDotNetRoot "nuget" | `
             Join-Path -ChildPath "BuildUtils.ps1"
import-module $NugetPath -function *

$Config = [Config]::new($NcnnDotNetRoot, $Configuration, $Target, $Architecture, $Platform, $Option)
Build -Config $Config