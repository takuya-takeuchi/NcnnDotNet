#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Version
)

Set-StrictMode -Version Latest

$RidOperatingSystem="linux"
$OperatingSystem="ubuntu"
$OperatingSystemVersion="16"

# Store current directory
$Current = Get-Location
$NcnnDotNetRoot = (Split-Path (Get-Location) -Parent)
$DockerDir = Join-Path $Current docker

Set-Location -Path $DockerDir

$DockerFileDir = Join-Path $DockerDir test  | `
                 Join-Path -ChildPath $OperatingSystem | `
                 Join-Path -ChildPath $OperatingSystemVersion

# https://github.com/dotnet/coreclr/issues/9265
# linux-x86 does not support
$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";     Architecture = 64; CUDA = 0;   Package = "NcnnDotNet";         PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Target = "vulkan";  Architecture = 64; CUDA = 0;   Package = "NcnnDotNet.GPU";     PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }

foreach($BuildTarget in $BuildTargets)
{
   $target = $BuildTarget.Target
   $package = $BuildTarget.Package
   $platformTarget = $BuildTarget.PlatformTarget
   $rid = $BuildTarget.RID
   $postfix = $BuildTarget.Postfix

   $dockername = "ncnndotnet/test/$OperatingSystem/$OperatingSystemVersion/$target" + $postfix
   $imagename  = "ncnndotnet/runtime/$OperatingSystem/$OperatingSystemVersion/$target" + $postfix

   Write-Host "Start docker build -t $dockername $DockerFileDir --build-arg IMAGE_NAME=""$imagename""" -ForegroundColor Green
   docker build --force-rm=true -t $dockername $DockerFileDir --build-arg IMAGE_NAME="$imagename"

   if ($lastexitcode -ne 0)
   {
      Write-Host "Test Fail for $package" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }

   if ($BuildTarget.CUDA -ne 0)
   {
      Write-Host "Start docker run --runtime=nvidia --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $OperatingSystem $OperatingSystemVersion" -ForegroundColor Green
      docker run --runtime=nvidia --rm `
                  -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                  -e "LOCAL_UID=$(id -u $env:USER)" `
                  -e "LOCAL_GID=$(id -g $env:USER)" `
                  -t "$dockername" $Version $package $platformTarget $rid
   }
   else
   {
      Write-Host "Start docker run --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $OperatingSystem $OperatingSystemVersion" -ForegroundColor Green
      docker run --rm `
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