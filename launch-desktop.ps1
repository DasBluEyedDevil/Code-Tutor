# Code Tutor Desktop Launcher (PowerShell)
# This script builds and launches the Code Tutor desktop application

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Code Tutor - Desktop Application Launcher" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Check if Node.js is installed
$nodeCommand = Get-Command node -ErrorAction SilentlyContinue
if (-not $nodeCommand) {
    Write-Host "❌ Node.js is not installed!" -ForegroundColor Red
    Write-Host "Please install Node.js from https://nodejs.org/" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

$nodeVersion = node --version
Write-Host "✓ Node.js found: $nodeVersion" -ForegroundColor Green
Write-Host ""

# Check if dependencies are installed
if (-not (Test-Path "node_modules")) {
    Write-Host "📦 Installing dependencies..." -ForegroundColor Yellow
    npm install
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Failed to install dependencies" -ForegroundColor Red
        Read-Host "Press Enter to exit"
        exit 1
    }
    Write-Host ""
}

# Build the web frontend
Write-Host "🔨 Building frontend..." -ForegroundColor Yellow
Set-Location "apps\web"
npm run build
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Failed to build frontend" -ForegroundColor Red
    Set-Location "..\..\"
    Read-Host "Press Enter to exit"
    exit 1
}
Set-Location "..\..\"
Write-Host "✓ Frontend built successfully" -ForegroundColor Green
Write-Host ""

# Install desktop dependencies
Write-Host "📦 Installing desktop app dependencies..." -ForegroundColor Yellow
Set-Location "apps\desktop"
if (-not (Test-Path "node_modules")) {
    npm install
}
Set-Location "..\..\"
Write-Host ""

# Launch the desktop app
Write-Host "🚀 Launching Code Tutor..." -ForegroundColor Green
Write-Host ""
Write-Host "Note: The app will check for installed programming language runtimes" -ForegroundColor Cyan
Write-Host "      (Python, Java, Rust, .NET, Kotlin, Dart) on startup." -ForegroundColor Cyan
Write-Host "      You'll be notified if any are missing." -ForegroundColor Cyan
Write-Host ""
npm run start:desktop

Read-Host "Press Enter to exit"
