#!/usr/bin/env pwsh
# Simple startup script for Code-Tutor platform

Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Code Tutor - Starting Platform" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host ""

$projectRoot = $PSScriptRoot

# Check Node.js
Write-Host "[INFO] Checking Node.js..." -ForegroundColor Cyan
$nodeVersion = node --version 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Node.js is not installed!" -ForegroundColor Red
    Write-Host "Please install Node.js 18+ from: https://nodejs.org/" -ForegroundColor Yellow
    exit 1
}
Write-Host "[SUCCESS] Node.js $nodeVersion found" -ForegroundColor Green

# Check if dependencies are installed
Write-Host ""
Write-Host "[INFO] Checking dependencies..." -ForegroundColor Cyan
if (-not (Test-Path "node_modules")) {
    Write-Host "[INFO] Installing dependencies (this may take a minute)..." -ForegroundColor Yellow
    npm install
}

if (-not (Test-Path "apps/web/node_modules")) {
    Write-Host "[INFO] Installing web dependencies..." -ForegroundColor Yellow
    Push-Location apps/web
    npm install
    Pop-Location
}

if (-not (Test-Path "apps/api/node_modules")) {
    Write-Host "[INFO] Installing API dependencies..." -ForegroundColor Yellow
    Push-Location apps/api
    npm install
    Pop-Location
}

Write-Host "[SUCCESS] Dependencies ready" -ForegroundColor Green

# Check Docker (optional)
Write-Host ""
Write-Host "[INFO] Checking Docker (optional for code execution)..." -ForegroundColor Cyan
docker version 2>&1 | Out-Null
if ($LASTEXITCODE -eq 0) {
    Write-Host "[SUCCESS] Docker is running" -ForegroundColor Green
    $useDocker = Read-Host "Start code executors? (y/N)"
    if ($useDocker -eq "y" -or $useDocker -eq "Y") {
        Write-Host "[INFO] Starting executors..." -ForegroundColor Cyan
        docker-compose up -d
        Write-Host "[SUCCESS] Executors started" -ForegroundColor Green
    }
} else {
    Write-Host "[WARNING] Docker not running - code execution will not work" -ForegroundColor Yellow
    Write-Host "          Platform will still work for viewing courses" -ForegroundColor Gray
}

# Start the platform
Write-Host ""
Write-Host "================================================" -ForegroundColor Blue
Write-Host "  Starting Services" -ForegroundColor Blue
Write-Host "================================================" -ForegroundColor Blue
Write-Host ""
Write-Host "Starting API server and web app..." -ForegroundColor Cyan
Write-Host ""
Write-Host "Once started, open your browser to:" -ForegroundColor Green
Write-Host "  http://localhost:3000" -ForegroundColor White
Write-Host ""
Write-Host "Press Ctrl+C to stop all services" -ForegroundColor Yellow
Write-Host ""

# Start both services
npm run dev
ry