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

$PublishTargets = ( "NcnnDotNet",
                    "NcnnDotNet.GPU",
                    "NcnnDotNet.Xamarin"
                  )

$Token = $env:NcnnDotNetNugetToken
if ([string]::IsNullOrWhitespace($Token))
{
    Write-Host "nuget token is missing" -ForegroundColor Red
    exit
}

# Precheck whether all package is present
foreach ($Target in $PublishTargets)
{
    $Package = Join-Path $PSScriptRoot "${Target}.${Version}.nupkg"
    if (!(Test-Path ${Package}))
    {
        Write-Host "${Package} is missing" -ForegroundColor Red
        exit
    }
}

foreach ($Target in $PublishTargets)
{
    $Package = Join-Path $PSScriptRoot "${Target}.${Version}.nupkg"
    Write-Host "Publish ${Package}" -ForegroundColor Green
    dotnet nuget push ${Package} -k $Token -s https://api.nuget.org/v3/index.json
}