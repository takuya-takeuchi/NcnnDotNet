#***************************************
#Arguments
#%1: Target (cpu/cuda/mkl/arm)
#%2: Architecture (32/64)
#%3: Platform (desktop,android/ios/uwp)
#%4: Optional Argument
#   if Target is cuda, CUDA version if Target is cuda [90/91/92/100/101/102/110/111]
#   if Target is mkl and Windows, IntelMKL directory path
#***************************************
Param
(
   [Parameter(
   Mandatory=$True,
   Position = 1
   )][string]
   $Target,

   [Parameter(
   Mandatory=$True,
   Position = 2
   )][int]
   $Architecture,

   [Parameter(
   Mandatory=$True,
   Position = 3
   )][string]
   $Platform,

   [Parameter(
   Mandatory=$True,
   Position = 4
   )][string]
   $Distribution,

   [Parameter(
   Mandatory=$True,
   Position = 5
   )][string]
   $DistributionVersion,

   [Parameter(
   Mandatory=$False,
   Position = 6
   )][string]
   $Option
)

$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent
$BuildUtilsPath = Join-Path $NcnnDotNetRoot "nuget" | `
                  Join-Path -ChildPath "BuildUtils.ps1"
import-module $BuildUtilsPath -function *
$Config = [Config]::new($NcnnDotNetRoot, "Release", $Target, $Architecture, $Platform, $Option)

if ($Target -ne "cuda")
{
   $postfix = $Option
   $dockername = "ncnndotnet/build/$Distribution/$DistributionVersion/$Target" + $postfix
}
else
{
   $cudaVersion = ($Option / 10).ToString("0.0")
   $dockername = "ncnndotnet/build/$Distribution/$DistributionVersion/$Target/$cudaVersion"
}

if ($target -eq "arm")
{
   Write-Host "Start 'docker run --rm --privileged multiarch/qemu-user-static --reset -p yes'" -ForegroundColor Green
   docker run --rm --privileged multiarch/qemu-user-static --reset -p yes
}

Write-Host "Run $dockername" -ForegroundColor Green

if ($Config.HasStoreDriectory())
{
   $storeDirecotry = $Config.GetRootStoreDriectory()
   docker run --rm `
              --privileged `
              --entrypoint="/bin/bash" `
              -v "$($storeDirecotry):/opt/data/builds" `
              -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
              -e "LOCAL_UID=$(id -u $env:USER)" `
              -e "LOCAL_GID=$(id -g $env:USER)" `
              -e "CIBuildDir=/opt/data/builds" `
              -w "/opt/data/NcnnDotNet" `
              -it "$dockername"
}
else
{
   Write-Host "CIBuildDir is not set" -ForegroundColor Yellow
   docker run --rm `
              --privileged `
              --entrypoint="/bin/bash" `
              -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
              -e "LOCAL_UID=$(id -u $env:USER)" `
              -e "LOCAL_GID=$(id -g $env:USER)" `
              -w "/opt/data/NcnnDotNet" `
              -it "$dockername"
}