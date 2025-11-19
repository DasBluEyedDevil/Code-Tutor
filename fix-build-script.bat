@echo off
REM Quick fix script to download the corrected build-installer.ps1

echo Downloading fixed build-installer.ps1 from GitHub...
echo.

REM Backup the old file
if exist build-installer.ps1 (
    copy build-installer.ps1 build-installer.ps1.backup >nul
    echo Backed up old file to build-installer.ps1.backup
)

REM Download the fixed version using curl (available on Windows 10+)
curl -L -o build-installer.ps1 https://raw.githubusercontent.com/DasBluEyedDevil/Code-Tutor/claude/refactor-desktop-hybrid-015YHyuhkBinxu7nT7vDKwQQ/build-installer.ps1

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ✓ Successfully downloaded fixed build-installer.ps1
    echo.
    echo You can now run: build-installer.bat
) else (
    echo.
    echo ✗ Download failed. Restoring backup...
    if exist build-installer.ps1.backup (
        copy build-installer.ps1.backup build-installer.ps1 >nul
    )
    echo.
    echo Please try manually:
    echo 1. Delete build-installer.ps1
    echo 2. Run: git checkout origin/claude/refactor-desktop-hybrid-015YHyuhkBinxu7nT7vDKwQQ -- build-installer.ps1
)

pause
