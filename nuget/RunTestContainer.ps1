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
   $cuda = 0
   $dockername = "ncnndotnet/test/$Distribution/$DistributionVersion/$Target" + $postfix
}
else
{
   $cudaVersion = ($Option / 10).ToString("0.0")
   $cuda = 1
   $dockername = "ncnndotnet/test/$Distribution/$DistributionVersion/$Target/$cudaVersion"
}

Write-Host "Run $dockername" -ForegroundColor Green

if ($cuda -ne 0)
{
   $dockerAPIVersion = docker version --format '{{.Server.APIVersion}}'
   Write-Host "Docker API Version: $dockerAPIVersion" -ForegroundColor Yellow
   if ($dockerAPIVersion -ge 1.40)
   {
      docker run --gpus all --rm `
                 --entrypoint="/bin/bash" `
                 -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                 -e "LOCAL_UID=$(id -u $env:USER)" `
                 -e "LOCAL_GID=$(id -g $env:USER)" `
                 -w "/opt/data/NcnnDotNet" `
                 -it "$dockername"
   }
   else
   {
      docker run --runtime=nvidia --rm `
                 --entrypoint="/bin/bash" `
                 -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                 -e "LOCAL_UID=$(id -u $env:USER)" `
                 -e "LOCAL_GID=$(id -g $env:USER)" `
                 -w "/opt/data/NcnnDotNet" `
                 -it "$dockername"
   }
}
else
{
   docker run --rm `
              --entrypoint="/bin/bash" `
              -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
              -e "LOCAL_UID=$(id -u $env:USER)" `
              -e "LOCAL_GID=$(id -g $env:USER)" `
              -w "/opt/data/NcnnDotNet" `
              -it "$dockername"
}