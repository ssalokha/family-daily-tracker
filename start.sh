#!/bin/bash

# Family Daily Tracker - Quick Start Script
echo "🚀 Starting Family Daily Tracker Application..."

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Error: Docker is not running. Please start Docker Desktop first."
    exit 1
fi

# Check if docker-compose is available
if ! command -v docker-compose &> /dev/null; then
    echo "❌ Error: docker-compose is not installed."
    exit 1
fi

echo "📦 Building and starting containers..."
docker-compose up --build -d

echo "⏳ Waiting for services to be ready..."
sleep 10

# Check if services are running
if docker-compose ps | grep -q "Up"; then
    echo "✅ Services are up and running!"
    echo ""
    echo "📍 Application URLs:"
    echo "   Frontend: http://localhost:3000"
    echo "   Backend API: http://localhost:5000"
    echo "   API Documentation: http://localhost:5000/swagger"
    echo "   Database: localhost:5432"
    echo ""
    echo "👤 Default Admin User:"
    echo "   Username: Sergey"
    echo "   Password: 210686"
    echo ""
    echo "💡 To view logs: docker-compose logs -f"
    echo "🛑 To stop: docker-compose down"
else
    echo "❌ Error: Services failed to start properly"
    echo "Run 'docker-compose logs' to see the error details"
    exit 1
fi
