param(
    [string]$Name,
    [ValidateSet("Query","Command")]
    [string]$Type
)

if (-not $Name) {
    $Name = Read-Host "Enter name (e.g. GetTicket)"
}

if (-not $Type) {
    $Type = Read-Host "Select type (Query or Command)"
    if ($Type -notin @("Query","Command")) {
        Write-Error "Type must be Query or Command"
        exit 1
    }
}

# Determine project root and namespace based on current folder and csproj
$currentDir = Get-Location
$projectRoot = $currentDir.Path
while (-not (Test-Path (Join-Path $projectRoot "*.csproj"))) {
    $parent = Split-Path $projectRoot -Parent
    if ($parent -eq $projectRoot) { break }
    $projectRoot = $parent
}

$relativePath = $currentDir.Path.Substring($projectRoot.Length).TrimStart('\','/')
$namespaceParts = $relativePath -split '[\\/]' | Where-Object { $_ -ne '' }
$baseNamespace = (Get-ChildItem -Path $projectRoot -Filter *.csproj | Select-Object -First 1 | ForEach-Object {
    [System.IO.Path]::GetFileNameWithoutExtension($_.Name)
})
$fullNamespace = $baseNamespace
if ($namespaceParts.Count -gt 0) {
    $fullNamespace += '.' + ($namespaceParts -join '.')
}

# Create folder for the feature
$folderPath = Join-Path $currentDir $Name
if (-not (Test-Path $folderPath)) {
    New-Item -ItemType Directory -Path $folderPath | Out-Null
}

# Handler file content
$handlerPath = Join-Path $folderPath "${Name}Handler.cs"
$handlerContent = @"
using MediatR;

namespace $fullNamespace.$Name;

public class ${Name}Handler : IRequestHandler<${Name}${Type}, ${Name}Response>
{
	public async Task<${Name}Response> Handle(${Name}${Type} request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
"@
Set-Content -Path $handlerPath -Value $handlerContent -Encoding utf8

# Query/Command file content
$queryCommandPath = Join-Path $folderPath "${Name}${Type}.cs"
$queryCommandContent = @"
using MediatR;

namespace $fullNamespace.$Name;

public record ${Name}${Type}() : IRequest<${Name}Response>;
"@
Set-Content -Path $queryCommandPath -Value $queryCommandContent -Encoding utf8

# Response file content
$responsePath = Join-Path $folderPath "${Name}Response.cs"
$responseContent = @"
namespace $fullNamespace.$Name;

public record ${Name}Response();
"@
Set-Content -Path $responsePath -Value $responseContent -Encoding utf8

Write-Host "Created files in $folderPath"

# Add files to csproj
$csprojPath = Get-ChildItem -Path $projectRoot -Filter *.csproj | Select-Object -First 1 | ForEach-Object { $_.FullName }

if (-not $csprojPath) {
    Write-Warning "No csproj file found - skipping project file update."
    exit 0
}

$relativeHandler = (Resolve-Path $handlerPath).Path.Substring($projectRoot.Length + 1)
$relativeQueryCommand = (Resolve-Path $queryCommandPath).Path.Substring($projectRoot.Length + 1)
$relativeResponse = (Resolve-Path $responsePath).Path.Substring($projectRoot.Length + 1)

[xml]$csprojXml = Get-Content $csprojPath

function FileInProject($path) {
    $csprojXml.Project.ItemGroup.Compile | Where-Object { $_.Include -eq $path }
}

$changed = $false

if (-not (FileInProject $relativeHandler)) {
    $ig = $csprojXml.CreateElement("ItemGroup", $csprojXml.Project.NamespaceURI)
    $compile = $csprojXml.CreateElement("Compile", $csprojXml.Project.NamespaceURI)
    $compile.SetAttribute("Include", $relativeHandler)
    $ig.AppendChild($compile) | Out-Null
    $csprojXml.Project.AppendChild($ig) | Out-Null
    $changed = $true
}

if (-not (FileInProject $relativeQueryCommand)) {
    $ig = $csprojXml.CreateElement("ItemGroup", $csprojXml.Project.NamespaceURI)
    $compile = $csprojXml.CreateElement("Compile", $csprojXml.Project.NamespaceURI)
    $compile.SetAttribute("Include", $relativeQueryCommand)
    $ig.AppendChild($compile) | Out-Null
    $csprojXml.Project.AppendChild($ig) | Out-Null
    $changed = $true
}

if (-not (FileInProject $relativeResponse)) {
    $ig = $csprojXml.CreateElement("ItemGroup", $csprojXml.Project.NamespaceURI)
    $compile = $csprojXml.CreateElement("Compile", $csprojXml.Project.NamespaceURI)
    $compile.SetAttribute("Include", $relativeResponse)
    $ig.AppendChild($compile) | Out-Null
    $csprojXml.Project.AppendChild($ig) | Out-Null
    $changed = $true
}

if ($changed) {
    $csprojXml.Save($csprojPath)
    Write-Host "Updated project file: $csprojPath"
} else {
    Write-Host "Files already included in project."
}
