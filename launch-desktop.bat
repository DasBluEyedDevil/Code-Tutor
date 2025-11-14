@echo off
:: Code Tutor Desktop Launcher for Windows
:: This script builds and launches the Code Tutor desktop application

echo ================================================
echo   Code Tutor - Desktop Application Launcher
echo ================================================
echo.

:: Check if Node.js is installed
where node >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Node.js is not installed!
    echo Please install Node.js from https://nodejs.org/
    pause
    exit /b 1
)

for /f "tokens=*" %%i in ('node --version') do set NODE_VERSION=%%i
echo [OK] Node.js found: %NODE_VERSION%
echo.

:: Check if dependencies are installed
if not exist "node_modules\" (
    echo [INFO] Installing dependencies...
    call npm install
    if %ERRORLEVEL% NEQ 0 (
        echo [ERROR] Failed to install dependencies
        pause
        exit /b 1
    )
    echo.
)

:: Build the web frontend
echo [INFO] Building frontend...
cd apps\web
call npm run build
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Failed to build frontend
    cd ..\..
    pause
    exit /b 1
)
cd ..\..
echo [OK] Frontend built successfully
echo.

:: Install desktop dependencies
echo [INFO] Installing desktop app dependencies...
cd apps\desktop
if not exist "node_modules\" (
    call npm install
)
cd ..\..
echo.

:: Launch the desktop app
echo [INFO] Launching Code Tutor...
echo.
echo Note: The app will check for installed programming language runtimes
echo       (Python, Java, Rust, .NET, Kotlin, Dart) on startup.
echo       You'll be notified if any are missing.
echo.
call npm run start:desktop

pause
