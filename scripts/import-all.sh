#!/bin/bash

# Bulk Content Import Script
# This script helps you import all 7 courses at once

set -e  # Exit on error

echo "================================================"
echo "  Code Tutor - Bulk Content Import"
echo "================================================"
echo ""

# Configuration - UPDATE THESE PATHS
PYTHON_SOURCE=""
JAVA_SOURCE=""
KOTLIN_SOURCE=""
RUST_SOURCE=""
CSHARP_SOURCE=""
FLUTTER_SOURCE=""
JAVASCRIPT_SOURCE=""

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Function to import a course
import_course() {
    local lang=$1
    local source=$2

    if [ -z "$source" ]; then
        echo -e "${YELLOW}⚠ Skipping $lang (source not configured)${NC}"
        return
    fi

    if [ ! -d "$source" ]; then
        echo -e "${RED}❌ Error: Source directory not found for $lang: $source${NC}"
        return 1
    fi

    echo -e "${BLUE}📦 Importing $lang from $source...${NC}"

    if npx ts-node scripts/import-cli.ts \
        --source "$source" \
        --language "$lang" \
        --format markdown \
        --validate; then
        echo -e "${GREEN}✅ Successfully imported $lang${NC}"
        echo ""
    else
        echo -e "${RED}❌ Failed to import $lang${NC}"
        echo ""
        return 1
    fi
}

# Check if any sources are configured
if [ -z "$PYTHON_SOURCE" ] && [ -z "$JAVA_SOURCE" ] && [ -z "$KOTLIN_SOURCE" ] && \
   [ -z "$RUST_SOURCE" ] && [ -z "$CSHARP_SOURCE" ] && [ -z "$FLUTTER_SOURCE" ] && \
   [ -z "$JAVASCRIPT_SOURCE" ]; then
    echo -e "${YELLOW}⚠ No source paths configured!${NC}"
    echo ""
    echo "Please edit this script and set the source paths at the top:"
    echo "  PYTHON_SOURCE=\"/path/to/python-course\""
    echo "  JAVA_SOURCE=\"/path/to/java-course\""
    echo "  etc..."
    echo ""
    echo "Or run individual imports:"
    echo "  npm run import -- --source /path/to/course --language python"
    echo ""
    exit 1
fi

# Track statistics
total=0
success=0
failed=0

# Import each course
for course in "python:$PYTHON_SOURCE" \
              "java:$JAVA_SOURCE" \
              "kotlin:$KOTLIN_SOURCE" \
              "rust:$RUST_SOURCE" \
              "csharp:$CSHARP_SOURCE" \
              "flutter:$FLUTTER_SOURCE" \
              "javascript:$JAVASCRIPT_SOURCE"; do

    IFS=':' read -r lang source <<< "$course"

    if [ -n "$source" ]; then
        total=$((total + 1))
        if import_course "$lang" "$source"; then
            success=$((success + 1))
        else
            failed=$((failed + 1))
        fi
    fi
done

# Summary
echo "================================================"
echo "  Import Summary"
echo "================================================"
echo -e "Total courses processed: $total"
echo -e "${GREEN}Successfully imported: $success${NC}"
if [ $failed -gt 0 ]; then
    echo -e "${RED}Failed: $failed${NC}"
fi
echo ""

if [ $failed -eq 0 ] && [ $success -gt 0 ]; then
    echo -e "${GREEN}✨ All imports completed successfully!${NC}"
    echo ""
    echo "Next steps:"
    echo "  1. Review generated files in apps/api/content/"
    echo "  2. Start the development server: npm run dev"
    echo "  3. Test each course in the application"
    echo ""
    exit 0
else
    echo -e "${YELLOW}⚠ Some imports had issues. Please review the errors above.${NC}"
    exit 1
fi
