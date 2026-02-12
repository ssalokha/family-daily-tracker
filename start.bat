@echo off
REM Family Daily Tracker - Quick Start Script for Windows

echo Starting Family Daily Tracker Application...

REM Check if Docker is running
docker info >nul 2>&1
if %errorlevel% neq 0 (
    echo Error: Docker is not running. Please start Docker Desktop first.
    pause
    exit /b 1
)

echo Starting containers...
docker-compose up -d

if %errorlevel% neq 0 (
    echo Error: Failed to start containers. Run 'docker-compose logs' to see details.
    pause
    exit /b 1
)

echo Waiting for services to be ready...
timeout /t 15 /nobreak >nul

echo.
echo ===================================================
echo   Family Daily Tracker - Ready!
echo ===================================================
echo.
echo Application URLs:
echo    Frontend: http://localhost:3000
echo    Backend API: http://localhost:5000
echo    Database: localhost:5432
echo.
echo Default Users (all passwords: 111111):
echo    Admin: Sergey
echo    Users: Natallia, Dasha, Alex, Home
echo.
echo Useful commands:
echo    View logs:        docker-compose logs -f
echo    Stop services:    docker-compose down
echo    Rebuild (if needed): docker-compose up --build -d
echo.
echo ===================================================
echo.

pause
