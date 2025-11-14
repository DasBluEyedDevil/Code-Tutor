#!/usr/bin/env pwsh
# Validate all imported courses

$projectRoot = Split-Path $PSScriptRoot -Parent
$contentDir = Join-Path (Join-Path $projectRoot "apps") "api\content"

Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Course Import Validation Report" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host ""

$courses = Get-ChildItem (Join-Path $contentDir "*.json")

if ($courses.Count -eq 0) {
    Write-Host "[ERROR] No course files found!" -ForegroundColor Red
    exit 1
}

Write-Host "Found $($courses.Count) course file(s):" -ForegroundColor Cyan
Write-Host ""

$totalModules = 0
$totalLessons = 0
$totalSize = 0

foreach ($file in $courses) {
    $content = Get-Content $file.FullName -Raw | ConvertFrom-Json
    $sizeKB = [math]::Round($file.Length / 1KB, 2)
    $totalSize += $file.Length

    $moduleCount = $content.modules.Count
    $lessonCount = ($content.modules | ForEach-Object { $_.lessons.Count } | Measure-Object -Sum).Sum

    $totalModules += $moduleCount
    $totalLessons += $lessonCount

    Write-Host "  $($file.Name)" -ForegroundColor White
    Write-Host "    Language:       $($content.courseMetadata.language)" -ForegroundColor Gray
    Write-Host "    Display Name:   $($content.courseMetadata.displayName)" -ForegroundColor Gray
    Write-Host "    Modules:        $moduleCount" -ForegroundColor Gray
    Write-Host "    Lessons:        $lessonCount" -ForegroundColor Gray
    Write-Host "    Est. Hours:     $($content.courseMetadata.estimatedHours)" -ForegroundColor Gray
    Write-Host "    Difficulty:     $($content.courseMetadata.difficulty)" -ForegroundColor Gray
    Write-Host "    File Size:      $sizeKB KB" -ForegroundColor Gray
    Write-Host ""
}

Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Summary Statistics" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host "Total Courses:      $($courses.Count)" -ForegroundColor White
Write-Host "Total Modules:      $totalModules" -ForegroundColor White
Write-Host "Total Lessons:      $totalLessons" -ForegroundColor White
Write-Host "Total Size:         $([math]::Round($totalSize / 1KB, 2)) KB ($([math]::Round($totalSize / 1MB, 2)) MB)" -ForegroundColor White
Write-Host ""

# Check for required languages
$requiredLanguages = @("python", "java", "javascript", "kotlin", "rust", "csharp", "flutter")
$importedLanguages = $courses | ForEach-Object {
    $content = Get-Content $_.FullName -Raw | ConvertFrom-Json
    $content.courseMetadata.language
}

$missing = $requiredLanguages | Where-Object { $_ -notin $importedLanguages }

if ($missing.Count -eq 0) {
    Write-Host "[SUCCESS] All 7 required courses have been imported!" -ForegroundColor Green
} else {
    Write-Host "[WARNING] Missing courses: $($missing -join ', ')" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Import Status: Complete!" -ForegroundColor Green

