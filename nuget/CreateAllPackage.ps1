$targets = @(
   "CPU",
   "GPU",
   "Xamarin"
)

$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent

$source = Join-Path $NcnnDotNetRoot src | `
          Join-Path -ChildPath NcnnDotNet
dotnet restore ${source}
# build for iOS
dotnet build -c Release -p:CustomDefinition=LIB_STATIC ${source}
$output = Join-Path $source bin | `
          Join-Path -ChildPath Release
$dest = Join-Path $source bin | `
        Join-Path -ChildPath Release_Static
if (Test-path($dest))
{
   Remove-Item -Path "${dest}" -Recurse -Force > $null
}
Move-Item "${output}" "${dest}"
# build for general
dotnet build -c Release ${source}

foreach ($target in $targets)
{
   pwsh CreatePackage.ps1 $target
}