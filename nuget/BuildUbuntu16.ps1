Param()

# import class and function
$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$ScriptPath = Join-Path $NcnnDotNetRoot "nuget" | `
              Join-Path -ChildPath "BuildUtils.ps1"
import-module $ScriptPath -function *

$OperatingSystem="linux"
$Distribution="ubuntu"
$DistributionVersion="16"

# Store current directory
$Current = Get-Location

$BuildSourceHash = [Config]::GetBinaryLibraryLinuxHash()

# https://github.com/dotnet/coreclr/issues/9265
# linux-x86 does not support
$BuildTargets = @()
$BuildTargets += [BuildTarget]::new("desktop", "cpu",    64, "$OperatingSystem-x64",   "/x64" )
$BuildTargets += [BuildTarget]::new("desktop", "vulkan", 64, "$OperatingSystem-x64",   "/x64" )
$BuildTargets += [BuildTarget]::new("desktop", "arm",    64, "$OperatingSystem-arm64", "/arm64" )

foreach($BuildTarget in $BuildTargets)
{
   $BuildTarget.OperatingSystem     = ${OperatingSystem}
   $BuildTarget.Distribution        = ${Distribution}
   $BuildTarget.DistributionVersion = ${DistributionVersion}
   
   $ret = [Config]::Build($NcnnDotNetRoot, $True, $BuildSourceHash, $BuildTarget)
   if ($ret -eq $False)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory 
Set-Location -Path $Current