# Code Tutor - Windows Installer Build Script
# This script creates a self-contained Windows installer for Code Tutor

param(
    [string]$Configuration = "Release",
    [string]$Version = "1.0.0",
    [switch]$SkipBuild = $false
)

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Code Tutor - Windows Installer Build" -ForegroundColor Cyan
Write-Host "  Version: $Version" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Paths
$RootDir = $PSScriptRoot
$ProjectDir = Join-Path $RootDir "native-app"
$ProjectFile = Join-Path $ProjectDir "CodeTutor.Native.csproj"
$ContentDir = Join-Path $RootDir "content"
$DocsDir = Join-Path $RootDir "docs"
$PublishDir = Join-Path $RootDir "publish"
$OutputDir = Join-Path $RootDir "dist"

# Check prerequisites
Write-Host "[1/6] Checking prerequisites..." -ForegroundColor Yellow

if (-not (Test-Path $ProjectFile)) {
    Write-Error "Project file not found: $ProjectFile"
    exit 1
}

if (-not (Test-Path $ContentDir)) {
    Write-Error "Content directory not found: $ContentDir"
    exit 1
}

# Check for dotnet
try {
    $dotnetVersion = dotnet --version
    Write-Host "  [OK] .NET SDK found: $dotnetVersion" -ForegroundColor Green
}
catch {
    Write-Error ".NET SDK not found. Please install .NET 8.0 SDK from https://dotnet.microsoft.com/download"
    exit 1
}

# Check for Inno Setup
$InnoSetupPath = "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe"
$HasInnoSetup = Test-Path $InnoSetupPath

if (-not $HasInnoSetup) {
    Write-Host "  [WARN] Inno Setup not found (installer creation will be skipped)" -ForegroundColor Yellow
    Write-Host "    Download from: https://jrsoftware.org/isdl.php" -ForegroundColor Gray
}
else {
    Write-Host "  [OK] Inno Setup found" -ForegroundColor Green
}

Write-Host ""

# Clean publish directory
Write-Host "[2/6] Cleaning publish directory..." -ForegroundColor Yellow
if (Test-Path $PublishDir) {
    Remove-Item $PublishDir -Recurse -Force
}
New-Item -ItemType Directory -Path $PublishDir | Out-Null
Write-Host "  [OK] Cleaned: $PublishDir" -ForegroundColor Green
Write-Host ""

# Build and publish self-contained app
if (-not $SkipBuild) {
    Write-Host "[3/6] Building self-contained Windows executable..." -ForegroundColor Yellow
    Write-Host "  This may take a few minutes..." -ForegroundColor Gray

    $publishArgs = @(
        "publish"
        $ProjectFile
        "-c", $Configuration
        "-r", "win-x64"
        "--self-contained", "true"
        "-p:PublishSingleFile=true"
        "-p:PublishTrimmed=false"
        "-p:IncludeNativeLibrariesForSelfExtract=true"
        "-o", $PublishDir
    )

    & dotnet $publishArgs

    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed with exit code $LASTEXITCODE"
        exit 1
    }

    Write-Host "  [OK] Build complete" -ForegroundColor Green
}
else {
    Write-Host "[3/6] Skipping build (using existing publish directory)..." -ForegroundColor Yellow
}
Write-Host ""

# Copy content directory
Write-Host "[4/6] Copying content files..." -ForegroundColor Yellow
$PublishContentDir = Join-Path $PublishDir "Content"
Copy-Item -Path $ContentDir -Destination $PublishContentDir -Recurse -Force
Write-Host "  [OK] Copied content to publish directory" -ForegroundColor Green
Write-Host ""

# Copy documentation
Write-Host "[5/6] Copying documentation..." -ForegroundColor Yellow
Copy-Item -Path (Join-Path $RootDir "README.md") -Destination $PublishDir -Force
if (Test-Path $DocsDir) {
    Copy-Item -Path $DocsDir -Destination (Join-Path $PublishDir "docs") -Recurse -Force
}
Write-Host "  [OK] Copied documentation" -ForegroundColor Green
Write-Host ""

# Get published exe size
$ExePath = Join-Path $PublishDir "CodeTutor.Native.exe"
if (Test-Path $ExePath) {
    $ExeSize = (Get-Item $ExePath).Length / 1MB
    Write-Host "  [INFO] Executable size: $([math]::Round($ExeSize, 2)) MB" -ForegroundColor Cyan
}

# Create installer with Inno Setup
Write-Host "[6/6] Creating installer..." -ForegroundColor Yellow

if (-not $HasInnoSetup) {
    Write-Host "  [WARN] Skipping installer creation (Inno Setup not installed)" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "To create an installer:" -ForegroundColor Cyan
    Write-Host "  1. Install Inno Setup: https://jrsoftware.org/isdl.php" -ForegroundColor Gray
    Write-Host "  2. Run this script again" -ForegroundColor Gray
    Write-Host ""
    Write-Host "Published files are in: $PublishDir" -ForegroundColor Cyan
}
else {
    # Create output directory
    if (-not (Test-Path $OutputDir)) {
        New-Item -ItemType Directory -Path $OutputDir | Out-Null
    }

    # Load Inno Setup script template and replace placeholders
    $TemplateFile = Join-Path $RootDir "installer-template.iss"
    if (-not (Test-Path $TemplateFile)) {
        Write-Error "Installer template not found: $TemplateFile"
        exit 1
    }

    $InnoScript = Get-Content $TemplateFile -Raw
    $InnoScript = $InnoScript -replace '\{\{VERSION\}\}', $Version
    $InnoScript = $InnoScript -replace '\{\{ROOTDIR\}\}', $RootDir
    $InnoScript = $InnoScript -replace '\{\{OUTPUTDIR\}\}', $OutputDir
    $InnoScript = $InnoScript -replace '\{\{PUBLISHDIR\}\}', $PublishDir

    # Save Inno Setup script
    $InnoScriptPath = Join-Path $RootDir "installer.iss"
    $InnoScript | Out-File -FilePath $InnoScriptPath -Encoding UTF8 -Force

    # Run Inno Setup
    Write-Host "  Compiling installer..." -ForegroundColor Gray
    & $InnoSetupPath $InnoScriptPath

    if ($LASTEXITCODE -eq 0) {
        Write-Host "  [OK] Installer created successfully!" -ForegroundColor Green

        # Find the created installer
        $InstallerFile = Get-ChildItem $OutputDir -Filter "CodeTutor-Setup-*.exe" | Select-Object -First 1
        if ($InstallerFile) {
            $InstallerSize = $InstallerFile.Length / 1MB
            Write-Host ""
            Write-Host "========================================" -ForegroundColor Green
            Write-Host "  [SUCCESS] BUILD COMPLETE!" -ForegroundColor Green
            Write-Host "========================================" -ForegroundColor Green
            Write-Host ""
            Write-Host "Installer: $($InstallerFile.FullName)" -ForegroundColor Cyan
            Write-Host "Size: $([math]::Round($InstallerSize, 2)) MB" -ForegroundColor Cyan
            Write-Host ""
            Write-Host "To install:" -ForegroundColor Yellow
            Write-Host "  1. Run the installer" -ForegroundColor Gray
            Write-Host "  2. Follow the installation wizard" -ForegroundColor Gray
            Write-Host "  3. Launch Code Tutor from the desktop or start menu" -ForegroundColor Gray
        }
    }
    else {
        Write-Error "Installer compilation failed with exit code $LASTEXITCODE"
        exit 1
    }
}

Write-Host ""
Write-Host "Done!" -ForegroundColor Green
