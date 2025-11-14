@echo off
REM Quick start script for Code-Tutor
REM This starts both the API and Web servers

echo ================================================
echo   Code Tutor - Quick Start
echo ================================================
echo.

echo Starting API server...
start "Code-Tutor API" cmd /k "cd apps\api && npm run dev"

timeout /t 3 /nobreak

echo Starting Web app...
start "Code-Tutor Web" cmd /k "cd apps\web && npm run dev"

timeout /t 5 /nobreak

echo.
echo Opening browser in 5 seconds...
timeout /t 5 /nobreak

start http://localhost:3000

echo.
echo ================================================
echo   Code Tutor is Running!
echo ================================================
echo.
echo Web App: http://localhost:3000
echo API:     http://localhost:3001
echo.
echo Close both terminal windows to stop the servers
echo.
pause

