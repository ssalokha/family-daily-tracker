@echo off
REM Family Daily Tracker - Quick Start Script for Windows

echo Starting Family Daily Tracker Application...

REM Check if Docker is running
docker info >nul 2>&1
if %errorlevel% neq 0 (
    echo Error: Docker is not running. Please start Docker Desktop first.
    exit /b 1
)

echo Building and starting containers...
docker-compose up --build -d

echo Waiting for services to be ready...
timeout /t 10 /nobreak >nul

echo Services are up and running!
echo.
echo Application URLs:
echo    Frontend: http://localhost:3000
echo    Backend API: http://localhost:5000
echo    API Documentation: http://localhost:5000/swagger
echo    Database: localhost:5432
echo.
echo Default Admin User:
echo    Username: Sergey
echo    Password: 210686
echo.
echo To view logs: docker-compose logs -f
echo To stop: docker-compose down

pause
