$targets = @(
   "CPU",
   "GPU"
)

$ScriptPath = $PSScriptRoot
$NcnnDotNetRoot = Split-Path $ScriptPath -Parent

$source = Join-Path $NcnnDotNetRoot src | `
          Join-Path -ChildPath NcnnDotNet
dotnet restore ${source}
dotnet build -c Release ${source}

foreach ($target in $targets)
{
   pwsh CreatePackage.ps1 $target
}