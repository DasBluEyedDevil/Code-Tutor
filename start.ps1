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

# Check Docker (required)
Write-Host ""
Write-Host "[INFO] Checking Docker (required for code execution)..." -ForegroundColor Cyan
docker version 2>&1 | Out-Null
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Docker is not running!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Docker Desktop is required for Code-Tutor to work properly." -ForegroundColor Yellow
    Write-Host "Please:" -ForegroundColor Yellow
    Write-Host "  1. Install Docker Desktop from: https://www.docker.com/products/docker-desktop" -ForegroundColor White
    Write-Host "  2. Start Docker Desktop" -ForegroundColor White
    Write-Host "  3. Wait for Docker to fully start (whale icon in system tray)" -ForegroundColor White
    Write-Host "  4. Run this script again" -ForegroundColor White
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "[SUCCESS] Docker is running" -ForegroundColor Green

# Start executors
Write-Host "[INFO] Starting code execution containers..." -ForegroundColor Cyan
docker-compose up -d
if ($LASTEXITCODE -eq 0) {
    Write-Host "[SUCCESS] Executors started successfully" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Failed to start executors" -ForegroundColor Red
    Write-Host "Try running: docker-compose up -d" -ForegroundColor Yellow
    Read-Host "Press Enter to continue anyway"
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