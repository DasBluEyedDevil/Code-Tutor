@echo off
REM Code Tutor Desktop - Complete Build and Package Script (Windows)
REM This script builds the entire application and creates distributable installers

echo ======================================
echo Code Tutor Desktop Build Process
echo ======================================
echo.

REM Check if we're in the correct directory
if not exist "package.json" (
    echo Error: Must run this script from the Code-Tutor root directory
    exit /b 1
)

REM Step 1: Clean previous builds
echo [1/5] Cleaning previous builds...
if exist "apps\web\dist" rmdir /s /q "apps\web\dist"
if exist "apps\desktop\dist" rmdir /s /q "apps\desktop\dist"
if exist "apps\desktop\dist-electron" rmdir /s /q "apps\desktop\dist-electron"
echo   Done
echo.

REM Step 2: Build Web Frontend
echo [2/5] Building web frontend...
cd apps\web
call npm run build
if errorlevel 1 (
    echo Error building web frontend
    exit /b 1
)
cd ..\..
echo   Done
echo.

REM Step 3: Build Desktop Backend
echo [3/5] Building desktop backend...
cd apps\desktop
call npm run build:electron
if errorlevel 1 (
    echo Error building desktop backend
    exit /b 1
)
cd ..\..
echo   Done
echo.

REM Step 4: Verify content directory
echo [4/5] Verifying content directory...
if not exist "content\courses" (
    echo Warning: content\courses directory not found
    echo The app will work but courses may not load
) else (
    echo Content directory verified
)
echo.

REM Step 5: Build installers
echo [5/5] Building Windows installers...
echo This may take several minutes...
cd apps\desktop
call npm run dist -- --win
if errorlevel 1 (
    echo Error building installers
    exit /b 1
)
cd ..\..

REM Display results
echo.
echo ======================================
echo Build Complete!
echo ======================================
echo.
echo Installers are located in:
echo   apps\desktop\dist-electron\
echo.
echo Available installers:
cd apps\desktop\dist-electron
dir /b *.exe 2>nul
cd ..\..\..
echo.
echo To install:
echo   - Run the .exe installer
echo   - Or use the portable version (no installation required)
echo.
echo ======================================
