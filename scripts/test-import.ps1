#!/usr/bin/env pwsh
# Simple test script to import a single course

param(
    [Parameter(Mandatory=$true)]
    [string]$Language
)

$projectRoot = Split-Path $PSScriptRoot -Parent
$repoPath = Join-Path (Join-Path (Join-Path $projectRoot "temp") "course-repos") $Language

Write-Host "Testing import for: $Language" -ForegroundColor Cyan
Write-Host "Repository path: $repoPath" -ForegroundColor Gray

if (-not (Test-Path $repoPath)) {
    Write-Host "[ERROR] Repository not found at: $repoPath" -ForegroundColor Red
    exit 1
}

Write-Host "[INFO] Running import..." -ForegroundColor Cyan
Push-Location $projectRoot

npx ts-node scripts/import-cli.ts --source $repoPath --language $Language --format markdown --validate

$exitCode = $LASTEXITCODE
Pop-Location

if ($exitCode -eq 0) {
    Write-Host "[SUCCESS] Import completed successfully!" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Import failed with exit code: $exitCode" -ForegroundColor Red
}

exit $exitCode

