#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$False,
      Position = 1
      )][string]
      $Version
)

Set-StrictMode -Version Latest

$RidOperatingSystem="ubuntu"
$OperatingSystem="ubuntu"
$OperatingSystemVersion="16"

# Store current directory
$Current = $PSScriptRoot
$NcnnDotNetRoot = (Split-Path (Get-Location) -Parent)
$DockerDir = Join-Path $NcnnDotNetRoot docker

$DockerFileDir = Join-Path $DockerDir test  | `
                 Join-Path -ChildPath $OperatingSystem | `
                 Join-Path -ChildPath $OperatingSystemVersion

# https://github.com/dotnet/coreclr/issues/9265
# linux-x86 does not support
$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";     Architecture = 64; CUDA = 0;   Package = "NcnnDotNet";         PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Target = "vulkan";  Architecture = 64; CUDA = 0;   Package = "NcnnDotNet.GPU";     PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }

if ([string]::IsNullOrEmpty($Version))
{
   $packages = Get-ChildItem *.* -include *.nupkg | Sort-Object -Property Name -Descending
   foreach ($file in $packages)
   {
      $file = Split-Path $file -leaf
      $file = $file -replace "OpenJpegDotNet\.",""
      $file = $file -replace "\.nupkg",""
      $Version = $file
      break
   }
}

Set-Location -Path $DockerDir

foreach($BuildTarget in $BuildTargets)
{
   $target = $BuildTarget.Target
   $cudaVersion = $BuildTarget.CUDA
   $package = $BuildTarget.Package
   $platformTarget = $BuildTarget.PlatformTarget
   $rid = $BuildTarget.RID
   $postfix = $BuildTarget.Postfix

   if ($target -ne "cuda")
   {
      $dockername = "ncnndotnet/test/$OperatingSystem/$OperatingSystemVersion/$Target" + $postfix
      $imagename  = "ncnndotnet/runtime/$OperatingSystem/$OperatingSystemVersion/$Target" + $postfix
   }
   else
   {
      $cudaVersion = ($cudaVersion / 10).ToString("0.0")
      $dockername = "ncnndotnet/test/$OperatingSystem/$OperatingSystemVersion/$Target/$cudaVersion"
      $imagename  = "ncnndotnet/runtime/$OperatingSystem/$OperatingSystemVersion/$Target/$cudaVersion"
   }

   Write-Host "Start docker build -t $dockername $DockerFileDir --build-arg IMAGE_NAME=""$imagename""" -ForegroundColor Green
   docker build --network host --force-rm=true -t $dockername $DockerFileDir --build-arg IMAGE_NAME="$imagename"

   if ($lastexitcode -ne 0)
   {
      Write-Host "Test Fail for $package" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }

   if ($BuildTarget.CUDA -ne 0)
   {
      $dockerAPIVersion = docker version --format '{{.Server.APIVersion}}'
      Write-Host "Docker API Version: $dockerAPIVersion" -ForegroundColor Yellow
      if ($dockerAPIVersion -ge 1.40)
      {
         Write-Host "Start docker run --network host --gpus all --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $platformTarget $rid" -ForegroundColor Green
         docker run --network host `
                    --gpus all --rm `
                    -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                    -e "LOCAL_UID=$(id -u $env:USER)" `
                    -e "LOCAL_GID=$(id -g $env:USER)" `
                    -t "$dockername" $Version $package $platformTarget $rid
      }
      else
      {
         Write-Host "Start docker run --network host --runtime=nvidia --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $platformTarget $rid" -ForegroundColor Green
         docker run --network host `
                    --runtime=nvidia --rm `
                    -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                    -e "LOCAL_UID=$(id -u $env:USER)" `
                    -e "LOCAL_GID=$(id -g $env:USER)" `
                    -t "$dockername" $Version $package $platformTarget $rid
      }
   }
   else
   {
      Write-Host "Start docker run --network host --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $platformTarget $rid" -ForegroundColor Green
      docker run --network host `
                 --rm `
                 -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                 -e "LOCAL_UID=$(id -u $env:USER)" `
                 -e "LOCAL_GID=$(id -g $env:USER)" `
                 -t "$dockername" $Version $package $platformTarget $rid
   }

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory
Set-Location -Path $Current