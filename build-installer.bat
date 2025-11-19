@echo off
REM Code Tutor - Windows Installer Build Script (Batch Wrapper)
REM This calls the PowerShell script with elevated permissions

echo ========================================
echo   Code Tutor - Windows Installer Build
echo ========================================
echo.

REM Check if PowerShell is available
where powershell >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: PowerShell not found!
    echo Please install PowerShell or use the build-installer.ps1 script directly.
    pause
    exit /b 1
)

REM Run the PowerShell script
powershell -ExecutionPolicy Bypass -File "%~dp0build-installer.ps1" %*

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Build completed successfully!
) else (
    echo.
    echo Build failed with error code %ERRORLEVEL%
)

pause