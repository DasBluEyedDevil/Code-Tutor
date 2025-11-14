#!/bin/bash

# Code Tutor Desktop Launcher
# This script builds and launches the Code Tutor desktop application

echo "================================================"
echo "  Code Tutor - Desktop Application Launcher"
echo "================================================"
echo ""

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
    echo "❌ Node.js is not installed!"
    echo "Please install Node.js from https://nodejs.org/"
    exit 1
fi

echo "✓ Node.js found: $(node --version)"
echo ""

# Check if dependencies are installed
if [ ! -d "node_modules" ]; then
    echo "📦 Installing dependencies..."
    npm install
    if [ $? -ne 0 ]; then
        echo "❌ Failed to install dependencies"
        exit 1
    fi
    echo ""
fi

# Build the web frontend
echo "🔨 Building frontend..."
cd apps/web
npm run build
if [ $? -ne 0 ]; then
    echo "❌ Failed to build frontend"
    exit 1
fi
cd ../..
echo "✓ Frontend built successfully"
echo ""

# Install desktop dependencies
echo "📦 Installing desktop app dependencies..."
cd apps/desktop
if [ ! -d "node_modules" ]; then
    npm install
fi
cd ../..
echo ""

# Launch the desktop app
echo "🚀 Launching Code Tutor..."
echo ""
npm run start:desktop
