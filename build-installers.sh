#!/bin/bash

# Code Tutor Desktop - Complete Build and Package Script
# This script builds the entire application and creates distributable installers

set -e  # Exit on error

echo "======================================"
echo "Code Tutor Desktop Build Process"
echo "======================================"
echo ""

# Colors for output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Check if we're in the correct directory
if [ ! -f "package.json" ]; then
    echo "Error: Must run this script from the Code-Tutor root directory"
    exit 1
fi

# Step 1: Clean previous builds
echo -e "${BLUE}[1/5] Cleaning previous builds...${NC}"
rm -rf apps/web/dist
rm -rf apps/desktop/dist
rm -rf apps/desktop/dist-electron
echo -e "${GREEN}✓ Cleaned${NC}"
echo ""

# Step 2: Build Web Frontend
echo -e "${BLUE}[2/5] Building web frontend...${NC}"
cd apps/web
npm run build:prod
cd ../..
echo -e "${GREEN}✓ Web frontend built successfully${NC}"
echo ""

# Step 3: Build Desktop Backend
echo -e "${BLUE}[3/5] Building desktop backend...${NC}"
cd apps/desktop
npm run build:electron
cd ../..
echo -e "${GREEN}✓ Desktop backend built successfully${NC}"
echo ""

# Step 4: Verify content directory
echo -e "${BLUE}[4/5] Verifying content directory...${NC}"
if [ ! -d "content/courses" ]; then
    echo -e "${YELLOW}Warning: content/courses directory not found${NC}"
    echo "The app will work but courses may not load"
else
    COURSE_COUNT=$(ls -1 content/courses/*.json 2>/dev/null | wc -l || echo 0)
    echo "Found $COURSE_COUNT course files"
    echo -e "${GREEN}✓ Content verified${NC}"
fi
echo ""

# Step 5: Build installers
echo -e "${BLUE}[5/5] Building installers for all platforms...${NC}"
echo "This may take several minutes..."
cd apps/desktop

# Determine which platform we're on
PLATFORM=$(uname -s)
echo "Detected platform: $PLATFORM"
echo ""

if [ "$PLATFORM" = "Linux" ]; then
    echo "Building for Linux..."
    npm run dist -- --linux
    echo -e "${GREEN}✓ Linux packages created${NC}"
elif [ "$PLATFORM" = "Darwin" ]; then
    echo "Building for macOS..."
    npm run dist -- --mac
    echo -e "${GREEN}✓ macOS packages created${NC}"
else
    echo "Building for Windows..."
    npm run dist -- --win
    echo -e "${GREEN}✓ Windows packages created${NC}"
fi

cd ../..

# Display results
echo ""
echo "======================================"
echo -e "${GREEN}Build Complete!${NC}"
echo "======================================"
echo ""
echo "Installers are located in:"
echo "  apps/desktop/dist-electron/"
echo ""
echo "Available installers:"
cd apps/desktop/dist-electron
ls -lh *.{exe,dmg,AppImage,deb,zip} 2>/dev/null || echo "  (none found - check dist-electron directory)"
cd ../../..

echo ""
echo "To install:"
echo "  - Windows: Run the .exe installer or use the portable version"
echo "  - macOS: Open the .dmg and drag to Applications"
echo "  - Linux: Run the .AppImage or install the .deb package"
echo ""
echo "======================================"
