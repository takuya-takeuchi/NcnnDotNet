Param()

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $NcnnDotNetRoot "nuget" | `
              Join-Path -ChildPath "BuildUtils.ps1"
import-module $ScriptPath -function *

$OperatingSystem="win"

# Store current directory
$Current = Get-Location

$BuildSourceHash = [Config]::GetBinaryLibraryWindowsHash()

$BuildTargets = @()
$BuildTargets += [BuildTarget]::new("uwp", "cpu", 64, "${OperatingSystem}10-x64",   "" )
$BuildTargets += [BuildTarget]::new("uwp", "cpu", 32, "${OperatingSystem}10-x86",   "" )
$BuildTargets += [BuildTarget]::new("uwp", "arm", 64, "${OperatingSystem}10-arm64", "" )
$BuildTargets += [BuildTarget]::new("uwp", "arm", 32, "${OperatingSystem}10-arm",   "" )

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