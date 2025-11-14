@echo off
REM Quick start script for Code-Tutor
REM This starts Docker executors, API, and Web servers

echo ================================================
echo   Code Tutor - Quick Start
echo ================================================
echo.

REM Check Docker
echo Checking Docker...
docker version >nul 2>&1
if errorlevel 1 (
    echo [ERROR] Docker is not running!
    echo.
    echo Docker Desktop is required for Code-Tutor.
    echo Please:
    echo   1. Install Docker Desktop from: https://www.docker.com/products/docker-desktop
    echo   2. Start Docker Desktop
    echo   3. Wait for it to fully start
    echo   4. Run START.bat again
    echo.
    pause
    exit /b 1
)
echo [SUCCESS] Docker is running
echo.

echo Starting Docker executors...
docker-compose up -d
if errorlevel 1 (
    echo [WARNING] Failed to start executors
    echo You may need to run: docker-compose build
    pause
) else (
    echo [SUCCESS] Executors started
)
echo.

echo Starting API server...
start "Code-Tutor API" cmd /k "cd apps\api && npm run dev"

timeout /t 3 /nobreak >nul

echo Starting Web app...
start "Code-Tutor Web" cmd /k "cd apps\web && npm run dev"

timeout /t 5 /nobreak >nul

echo.
echo Opening browser in 5 seconds...
timeout /t 5 /nobreak >nul

start http://localhost:3000

echo.
echo ================================================
echo   Code Tutor is Running!
echo ================================================
echo.
echo Web App:    http://localhost:3000
echo API:        http://localhost:3001
echo Executors:  Running in Docker
echo.
echo To stop: Close terminal windows and run 'docker-compose down'
echo.
pause

