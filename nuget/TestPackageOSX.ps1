#***************************************
#Arguments
#%1: Version of Release (0.0.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$False,
      Position = 1
      )][string]
      $Version
)

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $NcnnDotNetRoot "nuget" | `
              Join-Path -ChildPath "TestPackage.ps1"
import-module $ScriptPath -function *

Set-StrictMode -Version Latest

$OperatingSystem="osx"

# Store current directory
$Current = $PSScriptRoot

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Package = "NcnnDotNet";     PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Package = "NcnnDotNet.GPU"; PlatformTarget="x64"; RID = "$OperatingSystem-x64"; }

foreach($BuildTarget in $BuildTargets)
{
   $package = $BuildTarget.Package
   $platformTarget = $BuildTarget.PlatformTarget
   $runtimeIdentifier = $BuildTarget.RID
   $versionStr = Get-Version $Version $Current
   
   $command = ".\\TestPackage.ps1 -Package ${package} -Version $versionStr -PlatformTarget ${platformTarget} -RuntimeIdentifier ${runtimeIdentifier}"
   Invoke-Expression $command

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}