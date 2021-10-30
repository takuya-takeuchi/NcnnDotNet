#***************************************
#Arguments
#%1: Build Configuration (Release/Debug)
#%2: Build Directory (cpu/vulkan)
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
   $Directory
)

# configuration
$targetDir = "netcoreapp2.1"
$examples =
@(
   "FasterRCNN",
   "MobileNetSSD",
   "MobileNetV2SSDLite",
   "MobileNetV3SSDLite",
   "PeleeNetSSDSeg",
   "RetinaFace",
   "RFCN",
   "ShuffleNetV2",
   "SimplePose",
   "SqueezeNet",
   "SqueezeNetSSD",
   "YoloV2",
   "YoloV3"
)

# build path
$ExamplesRoot = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ExamplesRoot -Parent
$SrcPath = Join-Path $NcnnDotNetRoot src
$NcnnDotNetNativeRoot = Join-Path $SrcPath NcnnDotNet.Native
if ($Configuration -eq "Debug")
{
    $NcnnDotNetNativeBuildDir = Join-Path $NcnnDotNetNativeRoot "build_win_desktop_${Directory}_x64_d"
}
else
{
    $NcnnDotNetNativeBuildDir = Join-Path $NcnnDotNetNativeRoot "build_win_desktop_${Directory}_x64"
}

# check path
if (!(Test-Path $NcnnDotNetNativeBuildDir))
{
   Write-Host "[Error] '$NcnnDotNetNativeBuildDir' does not exist" -ForegroundColor Red
   exit
}

# create symlink
$NcnnDotNetNativeLibraries =
@(
   "NcnnDotNetNative.dll"
)

# create symlink
function create-symlink($Targets, $SrcDir, $DstDir)
{
   foreach ($Target in $Targets)
   {
      $src = Join-Path $SrcDir $Target
      $dst = Join-Path $DstDir $Target

      if (!(Test-Path $src))
      {
         Write-Host "[Error] '$src' does not exist" -ForegroundColor Red
         exit
      }

      if (Test-Path $dst)
      {
         Remove-Item $dst
      }

      New-Item -Path "${DstDir}" -Value "${src}" -Name "${Target}" -ItemType SymbolicLink | Out-Null
   }
}

foreach ($example in $examples)
{
   $AppDir = Join-Path $ExamplesRoot $example | `
             Join-Path -child bin | `
             Join-Path -child $Configuration | `
             Join-Path -child $targetDir
   New-Item $AppDir -Force -ItemType Directory | Out-Null

   create-symlink $NcnnDotNetNativeLibraries $NcnnDotNetNativeBuildDir $AppDir
}