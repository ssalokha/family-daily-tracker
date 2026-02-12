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

echo "📦 Starting containers..."
docker-compose up -d

if [ $? -ne 0 ]; then
    echo "❌ Error: Failed to start containers. Run 'docker-compose logs' to see details."
    exit 1
fi

echo "⏳ Waiting for services to be ready..."
sleep 15

# Check if services are running
if docker-compose ps | grep -q "Up"; then
    echo ""
    echo "==================================================="
    echo "   ✅ Family Daily Tracker - Ready!"
    echo "==================================================="
    echo ""
    echo "📍 Application URLs:"
    echo "   Frontend: http://localhost:3000"
    echo "   Backend API: http://localhost:5000"
    echo "   Database: localhost:5432"
    echo ""
    echo "👤 Default Users (all passwords: 111111):"
    echo "   Admin: Sergey"
    echo "   Users: Natallia, Dasha, Alex, Home"
    echo ""
    echo "💡 Useful commands:"
    echo "   View logs:        docker-compose logs -f"
    echo "   Stop services:    docker-compose down"
    echo "   Rebuild (if needed): docker-compose up --build -d"
    echo ""
    echo "==================================================="
    echo ""
else
    echo "❌ Error: Services failed to start properly"
    echo "Run 'docker-compose logs' to see the error details"
    exit 1
fi
