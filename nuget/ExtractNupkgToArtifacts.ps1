#***************************************
#Arguments
#%1: Version of Release (1.2.3.0)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Version
)

$PublishTargets = @{ "NcnnDotNet"="cpu";
                     "NcnnDotNet.GPU"="vulkan";
                  }

$Token = $env:FaceRecognitionDotNetNugetToken
if ([string]::IsNullOrWhitespace($Token))
{
    Write-Host "nuget token is missing" -ForegroundColor Red
    exit
}

# Precheck whether all package is present
foreach ($key in $PublishTargets.keys)
{
    $value = $PublishTargets[$key]
    
    $Package = Join-Path $PSScriptRoot "${key}.${Version}.nupkg"
    if (!(Test-Path ${Package}))
    {
        Write-Host "${Package} is missing" -ForegroundColor Red
        exit
    }

    Expand-Archive -Path "${Package}" -DestinationPath tmp
    $runtime = Join-Path tmp runtimes
    $artifacts = Join-Path artifacts ${value} | `
                 Join-Path -ChildPath runtimes
    Copy-Item "${runtime}/*" "${artifacts}" -Recurse -Force
    Remove-Item tmp -Recurse -Force
}