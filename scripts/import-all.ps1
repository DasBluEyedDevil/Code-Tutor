#!/usr/bin/env pwsh
# PowerShell script to import all course content from GitHub repositories

# Color output functions
function Write-Success { param($Message) Write-Host "[SUCCESS] $Message" -ForegroundColor Green }
function Write-Error-Custom { param($Message) Write-Host "[ERROR] $Message" -ForegroundColor Red }
function Write-Info { param($Message) Write-Host "[INFO] $Message" -ForegroundColor Cyan }
function Write-Warning-Custom { param($Message) Write-Host "[WARNING] $Message" -ForegroundColor Yellow }

Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Code Tutor - Bulk Content Import" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host ""

# Configuration - GitHub repositories
$repos = @{
    python = @{
        url = "https://github.com/DasBluEyedDevil/Python-Training-Course"
        language = "python"
    }
    java = @{
        url = "https://github.com/DasBluEyedDevil/Java-Training-Course"
        language = "java"
    }
    kotlin = @{
        url = "https://github.com/DasBluEyedDevil/Kotlin-Training-Course"
        language = "kotlin"
    }
    rust = @{
        url = "https://github.com/DasBluEyedDevil/Rust-Training-Course"
        language = "rust"
    }
    csharp = @{
        url = "https://github.com/DasBluEyedDevil/CSharp-Training-Course"
        language = "csharp"
    }
    flutter = @{
        url = "https://github.com/DasBluEyedDevil/Flutter-Training-Course"
        language = "flutter"
    }
    javascript = @{
        url = "https://github.com/DasBluEyedDevil/JavaScript-TypeScript-Training-Course"
        language = "javascript"
    }
}

# Create temp directory for cloning repos
$tempDir = Join-Path (Join-Path (Split-Path $PSScriptRoot -Parent) "temp") "course-repos"
if (-not (Test-Path $tempDir)) {
    New-Item -ItemType Directory -Path $tempDir -Force | Out-Null
}

# Statistics
$total = 0
$success = 0
$failed = 0
$skipped = 0

# Function to clone or update repository
function Get-Repository {
    param(
        [string]$Url,
        [string]$Name
    )

    $repoPath = Join-Path $tempDir $Name

    if (Test-Path $repoPath) {
        Write-Info "Updating existing repository: $Name"
        Push-Location $repoPath
        try {
            git pull origin main 2>&1 | Out-Null
            if ($LASTEXITCODE -ne 0) {
                git pull origin master 2>&1 | Out-Null
            }
            Write-Success "Updated $Name"
        }
        catch {
            Write-Warning-Custom "Failed to update $Name, using existing version"
        }
        finally {
            Pop-Location
        }
    }
    else {
        Write-Info "Cloning repository: $Name"
        try {
            git clone $Url $repoPath 2>&1 | Out-Null
            if ($LASTEXITCODE -eq 0) {
                Write-Success "Cloned $Name"
            }
            else {
                Write-Error-Custom "Failed to clone $Name"
                return $null
            }
        }
        catch {
            Write-Error-Custom "Failed to clone $Name : $_"
            return $null
        }
    }

    return $repoPath
}

# Function to import a course
function Import-Course {
    param(
        [string]$RepoPath,
        [string]$Language
    )

    if (-not $RepoPath -or -not (Test-Path $RepoPath)) {
        Write-Error-Custom "Repository path not found: $RepoPath"
        return $false
    }

    Write-Info "Importing $Language from $RepoPath"

    # Get the project root (parent of scripts directory)
    $projectRoot = Split-Path $PSScriptRoot -Parent

    try {
        # Run the import CLI
        Push-Location $projectRoot

        $importArgs = @(
            "scripts\import-cli.ts"
            "--source"
            $RepoPath
            "--language"
            $Language
            "--format"
            "markdown"
            "--validate"
        )

        npx ts-node @importArgs

        if ($LASTEXITCODE -eq 0) {
            Write-Success "Successfully imported $Language"
            return $true
        }
        else {
            Write-Error-Custom "Failed to import $Language (exit code: $LASTEXITCODE)"
            return $false
        }
    }
    catch {
        Write-Error-Custom "Error importing $Language : $_"
        return $false
    }
    finally {
        Pop-Location
    }
}

# Main import loop
Write-Host ""
Write-Host "Starting course imports..." -ForegroundColor Blue
Write-Host ""

foreach ($key in $repos.Keys) {
    $repo = $repos[$key]
    $total++

    Write-Host "----------------------------------------" -ForegroundColor Gray
    Write-Host "Processing: $($repo.language)" -ForegroundColor Magenta
    Write-Host "Repository: $($repo.url)" -ForegroundColor Gray
    Write-Host ""

    # Clone/update repository
    $repoPath = Get-Repository -Url $repo.url -Name $key

    if (-not $repoPath) {
        Write-Error-Custom "Skipping $($repo.language) - repository not available"
        $skipped++
        Write-Host ""
        continue
    }

    # Import the course
    if (Import-Course -RepoPath $repoPath -Language $repo.language) {
        $success++
    }
    else {
        $failed++
    }

    Write-Host ""
}

# Print summary
Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Import Summary" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host "Total courses:    $total" -ForegroundColor White
Write-Host "Successful:       $success" -ForegroundColor Green
Write-Host "Failed:           $failed" -ForegroundColor Red
Write-Host "Skipped:          $skipped" -ForegroundColor Yellow
Write-Host ""

if ($success -eq $total) {
    Write-Success "All courses imported successfully!"
}
elseif ($success -gt 0) {
    Write-Warning-Custom "Some courses imported with errors. Check the output above."
}
else {
    Write-Error-Custom "All imports failed. Please check the errors above."
    exit 1
}

# Cleanup option
Write-Host ""
$cleanup = Read-Host "Do you want to remove the cloned repositories? (y/N)"
if ($cleanup -eq "y" -or $cleanup -eq "Y") {
    Write-Info "Cleaning up temporary repositories..."
    Remove-Item -Recurse -Force $tempDir
    Write-Success "Cleanup complete"
}
else {
    Write-Info "Repositories kept in: $tempDir"
}

Write-Host ""
Write-Host "Done!" -ForegroundColor Green

