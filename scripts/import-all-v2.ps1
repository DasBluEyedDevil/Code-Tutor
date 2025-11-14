#!/usr/bin/env pwsh
# PowerShell script to import all course content from GitHub repositories

param(
    [switch]$SkipClone = $false
)

# Configuration - GitHub repositories
$repos = @(
    @{ name = "python"; url = "https://github.com/DasBluEyedDevil/Python-Training-Course"; language = "python" },
    @{ name = "java"; url = "https://github.com/DasBluEyedDevil/Java-Training-Course"; language = "java" },
    @{ name = "kotlin"; url = "https://github.com/DasBluEyedDevil/Kotlin-Training-Course"; language = "kotlin" },
    @{ name = "rust"; url = "https://github.com/DasBluEyedDevil/Rust-Training-Course"; language = "rust" },
    @{ name = "csharp"; url = "https://github.com/DasBluEyedDevil/CSharp-Training-Course"; language = "csharp" },
    @{ name = "flutter"; url = "https://github.com/DasBluEyedDevil/Flutter-Training-Course"; language = "flutter" },
    @{ name = "javascript"; url = "https://github.com/DasBluEyedDevil/JavaScript-TypeScript-Training-Course"; language = "javascript" }
)

Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Code Tutor - Bulk Content Import" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host ""

# Get project root
$projectRoot = Split-Path $PSScriptRoot -Parent
$tempBase = Join-Path $projectRoot "temp"
$tempRepos = Join-Path $tempBase "course-repos"

# Create temp directory
if (-not (Test-Path $tempRepos)) {
    New-Item -ItemType Directory -Path $tempRepos -Force | Out-Null
    Write-Host "[INFO] Created temp directory: $tempRepos" -ForegroundColor Cyan
}

# Statistics
$total = $repos.Count
$success = 0
$failed = 0

Write-Host "Starting course imports..." -ForegroundColor Cyan
Write-Host ""

foreach ($repo in $repos) {
    Write-Host "----------------------------------------" -ForegroundColor Gray
    Write-Host "Processing: $($repo.language)" -ForegroundColor Magenta
    Write-Host "Repository: $($repo.url)" -ForegroundColor Gray

    $repoPath = Join-Path $tempRepos $repo.name

    # Clone or update repository
    if (-not $SkipClone) {
        if (Test-Path $repoPath) {
            Write-Host "[INFO] Updating existing repository..." -ForegroundColor Cyan
            Push-Location $repoPath
            git pull 2>&1 | Out-Null
            Pop-Location
        }
        else {
            Write-Host "[INFO] Cloning repository..." -ForegroundColor Cyan
            git clone $repo.url $repoPath 2>&1 | Out-Null
            if ($LASTEXITCODE -ne 0) {
                Write-Host "[ERROR] Failed to clone repository" -ForegroundColor Red
                $failed++
                Write-Host ""
                continue
            }
        }
        Write-Host "[SUCCESS] Repository ready" -ForegroundColor Green
    }

    # Import the course
    if (Test-Path $repoPath) {
        Write-Host "[INFO] Importing course content..." -ForegroundColor Cyan
        Push-Location $projectRoot

        $output = npx ts-node scripts/import-cli.ts --source $repoPath --language $repo.language --format markdown --validate 2>&1

        if ($LASTEXITCODE -eq 0) {
            Write-Host "[SUCCESS] Successfully imported $($repo.language)" -ForegroundColor Green
            $success++
        }
        else {
            Write-Host "[ERROR] Failed to import $($repo.language)" -ForegroundColor Red
            Write-Host $output -ForegroundColor DarkGray
            $failed++
        }

        Pop-Location
    }
    else {
        Write-Host "[ERROR] Repository path not found: $repoPath" -ForegroundColor Red
        $failed++
    }

    Write-Host ""
}

# Print summary
Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Import Summary" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host "Total courses:    $total"
Write-Host "Successful:       " -NoNewline; Write-Host $success -ForegroundColor Green
Write-Host "Failed:           " -NoNewline; Write-Host $failed -ForegroundColor Red
Write-Host ""

if ($success -eq $total) {
    Write-Host "[SUCCESS] All courses imported successfully!" -ForegroundColor Green
    $exitCode = 0
}
elseif ($success -gt 0) {
    Write-Host "[WARNING] Some courses imported with errors." -ForegroundColor Yellow
    $exitCode = 0
}
else {
    Write-Host "[ERROR] All imports failed." -ForegroundColor Red
    $exitCode = 1
}

# Cleanup option
if (-not $SkipClone) {
    Write-Host ""
    $cleanup = Read-Host "Remove cloned repositories? (y/N)"
    if ($cleanup -eq "y" -or $cleanup -eq "Y") {
        Write-Host "[INFO] Cleaning up..." -ForegroundColor Cyan
        Remove-Item -Recurse -Force $tempRepos -ErrorAction SilentlyContinue
        Write-Host "[SUCCESS] Cleanup complete" -ForegroundColor Green
    }
    else {
        Write-Host "[INFO] Repositories kept in: $tempRepos" -ForegroundColor Cyan
    }
}

Write-Host ""
Write-Host "Done!" -ForegroundColor Green
exit $exitCode

