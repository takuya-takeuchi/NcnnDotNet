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

Set-StrictMode -Version Latest

$RidOperatingSystem="centos"
$OperatingSystem="centos"
$OperatingSystemVersion="7"

# Store current directory
$Current = Get-Location
$NcnnDotNetRoot = (Split-Path (Get-Location) -Parent)
$DockerDir = Join-Path $NcnnDotNetRoot docker

$DockerFileDir = Join-Path $DockerDir test  | `
                 Join-Path -ChildPath $OperatingSystem | `
                 Join-Path -ChildPath $OperatingSystemVersion

# https://github.com/dotnet/coreclr/issues/9265
# linux-x86 does not support
$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";    Architecture = 64; CUDA = 0;   Package = "NcnnDotNet";         PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Target = "vulkan"; Architecture = 64; CUDA = 0;   Package = "NcnnDotNet.GPU";     PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }

foreach($BuildTarget in $BuildTargets)
{
   $target = $BuildTarget.Target
   $cudaVersion = $BuildTarget.CUDA
   $package = $BuildTarget.Package
   $platformTarget = $BuildTarget.PlatformTarget
   $rid = $BuildTarget.RID
   $postfix = $BuildTarget.Postfix
   $versionStr = $Version

   if ([string]::IsNullOrEmpty($versionStr))
   {
      $packages = Get-ChildItem "${Current}/*" -include *.nupkg | `
                  Where-Object -FilterScript {$_.Name -match "${package}\.([0-9\.]+).nupkg"} | `
                  Sort-Object -Property Name -Descending
      foreach ($file in $packages)
      {
         Write-Host $file -ForegroundColor Blue
      }

      foreach ($file in $packages)
      {
         $file = Split-Path $file -leaf
         $file = $file -replace "${package}\.",""
         $file = $file -replace "\.nupkg",""
         $versionStr = $file
         break
      }

      if ([string]::IsNullOrEmpty($versionStr))
      {
         Write-Host "Version is not specified" -ForegroundColor Red
         exit -1
      }
   }

   if ($target -ne "cuda")
   {
      $dockername = "ncnndotnet/test/$OperatingSystem/$OperatingSystemVersion/$Target" + $postfix
      $imagename  = "dlibdotnet/runtime/$OperatingSystem/$OperatingSystemVersion/$Target" + $postfix
   }
   else
   {
      $cudaVersion = ($cudaVersion / 10).ToString("0.0")
      $dockername = "ncnndotnet/test/$OperatingSystem/$OperatingSystemVersion/$Target/$cudaVersion"
      $imagename  = "dlibdotnet/runtime/$OperatingSystem/$OperatingSystemVersion/$Target/$cudaVersion"
   }

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
      Write-Host "Start docker run--gpus all --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $versionStr $package $OperatingSystem $OperatingSystemVersion" -ForegroundColor Green
      docker run --gpus all --rm `
                 -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                 -e "LOCAL_UID=$(id -u $env:USER)" `
                 -e "LOCAL_GID=$(id -g $env:USER)" `
                 -t "$dockername" $versionStr $package $OperatingSystem $OperatingSystemVersion
   }
   else
   {   
      Write-Host "Start docker run --rm -v ""$($NcnnDotNetRoot):/opt/data/NcnnDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $versionStr $package $OperatingSystem $OperatingSystemVersion" -ForegroundColor Green
      docker run --rm `
                  -v "$($NcnnDotNetRoot):/opt/data/NcnnDotNet" `
                  -e "LOCAL_UID=$(id -u $env:USER)" `
                  -e "LOCAL_GID=$(id -g $env:USER)" `
                  -t "$dockername" $versionStr $package $OperatingSystem $OperatingSystemVersion
   }

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory
Set-Location -Path $Current