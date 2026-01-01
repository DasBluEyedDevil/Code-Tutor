---
type: "EXAMPLE"
title: "Anatomy of a GitHub Actions Workflow"
---

This comprehensive workflow demonstrates all major GitHub Actions concepts. We will build, test, and prepare ShopFlow for deployment, using services, caching, and job dependencies.

```yaml
# ===== .github/workflows/ci.yml =====
# Complete CI workflow for ShopFlow e-commerce application

name: ShopFlow CI Pipeline

# TRIGGERS: Define when this workflow executes
on:
  # Run on pushes to main and develop branches
  push:
    branches: [main, develop]
    paths-ignore:
      - '**.md'           # Skip for documentation changes
      - 'docs/**'         # Skip for docs folder
  
  # Run on PRs targeting main
  pull_request:
    branches: [main]
    types: [opened, synchronize, reopened]
  
  # Allow manual triggers from GitHub UI
  workflow_dispatch:
    inputs:
      run_integration_tests:
        description: 'Run integration tests'
        required: false
        default: true
        type: boolean
  
  # Scheduled nightly build
  schedule:
    - cron: '0 2 * * *'   # Run at 2 AM UTC daily

# Environment variables available to all jobs
env:
  DOTNET_VERSION: '9.0.x'
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: true
  DOTNET_CLI_TELEMETRY_OPTOUT: true

# JOBS: Define the work to perform
jobs:
  # ========== BUILD JOB ==========
  build:
    name: Build Application
    runs-on: ubuntu-latest
    
    outputs:
      version: ${{ steps.version.outputs.version }}
    
    steps:
      - name: Checkout code
        uses: actions/checkout@v4
        with:
          fetch-depth: 0    # Full history for versioning
      
      - name: Setup .NET SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}
      
      # Cache NuGet packages for faster builds
      - name: Cache NuGet packages
        uses: actions/cache@v4
        with:
          path: ~/.nuget/packages
          key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
          restore-keys: |
            ${{ runner.os }}-nuget-
      
      - name: Restore dependencies
        run: dotnet restore ShopFlow.sln
      
      - name: Build solution
        run: dotnet build ShopFlow.sln --no-restore --configuration Release
      
      # Generate version from git tags
      - name: Generate version
        id: version
        run: |
          VERSION=$(git describe --tags --always --dirty)
          echo "version=$VERSION" >> $GITHUB_OUTPUT
          echo "Generated version: $VERSION"
      
      # Upload build artifacts for other jobs
      - name: Upload build artifacts
        uses: actions/upload-artifact@v4
        with:
          name: build-output
          path: |
            src/**/bin/Release/net9.0/
            !src/**/bin/Release/net9.0/**/*.pdb
          retention-days: 7

  # ========== UNIT TESTS JOB ==========
  unit-tests:
    name: Unit Tests
    needs: build           # Wait for build to complete
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}
      
      - name: Cache NuGet packages
        uses: actions/cache@v4
        with:
          path: ~/.nuget/packages
          key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
          restore-keys: |
            ${{ runner.os }}-nuget-
      
      - name: Restore and build
        run: dotnet build tests/ShopFlow.Tests.Unit --configuration Release
      
      - name: Run unit tests
        run: |
          dotnet test tests/ShopFlow.Tests.Unit \
            --no-build \
            --configuration Release \
            --logger "trx;LogFileName=test-results.trx" \
            --collect:"XPlat Code Coverage"
      
      # Upload test results for PR annotations
      - name: Upload test results
        uses: actions/upload-artifact@v4
        if: always()        # Upload even if tests fail
        with:
          name: unit-test-results
          path: '**/test-results.trx'

  # ========== INTEGRATION TESTS JOB ==========
  integration-tests:
    name: Integration Tests
    needs: build
    runs-on: ubuntu-latest
    if: github.event.inputs.run_integration_tests != 'false'
    
    # Services provide external dependencies
    services:
      postgres:
        image: postgres:16
        env:
          POSTGRES_USER: shopflow
          POSTGRES_PASSWORD: shopflow_test
          POSTGRES_DB: shopflow_test
        ports:
          - 5432:5432
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
      
      redis:
        image: redis:7-alpine
        ports:
          - 6379:6379
        options: >-
          --health-cmd "redis-cli ping"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}
      
      - name: Cache NuGet packages
        uses: actions/cache@v4
        with:
          path: ~/.nuget/packages
          key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
          restore-keys: |
            ${{ runner.os }}-nuget-
      
      - name: Restore and build
        run: dotnet build tests/ShopFlow.Tests.Integration --configuration Release
      
      - name: Run integration tests
        run: |
          dotnet test tests/ShopFlow.Tests.Integration \
            --no-build \
            --configuration Release \
            --logger "trx;LogFileName=integration-results.trx"
        env:
          ConnectionStrings__DefaultConnection: "Host=localhost;Database=shopflow_test;Username=shopflow;Password=shopflow_test"
          ConnectionStrings__Redis: "localhost:6379"
      
      - name: Upload test results
        uses: actions/upload-artifact@v4
        if: always()
        with:
          name: integration-test-results
          path: '**/integration-results.trx'

  # ========== CODE QUALITY JOB ==========
  code-quality:
    name: Code Quality Analysis
    needs: build
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}
      
      - name: Restore dependencies
        run: dotnet restore ShopFlow.sln
      
      # Run code formatting check
      - name: Check code formatting
        run: dotnet format ShopFlow.sln --verify-no-changes --verbosity diagnostic
      
      # Run security vulnerability scan
      - name: Check for vulnerable packages
        run: dotnet list package --vulnerable --include-transitive 2>&1 | tee vuln-report.txt
      
      - name: Fail if vulnerabilities found
        run: |
          if grep -q "has the following vulnerable packages" vuln-report.txt; then
            echo "::error::Vulnerable packages detected!"
            exit 1
          fi

  # ========== SUMMARY JOB ==========
  ci-complete:
    name: CI Complete
    needs: [unit-tests, integration-tests, code-quality]
    runs-on: ubuntu-latest
    if: always()           # Run even if previous jobs failed
    
    steps:
      - name: Check job results
        run: |
          if [[ "${{ needs.unit-tests.result }}" == "failure" ]] || \
             [[ "${{ needs.integration-tests.result }}" == "failure" ]] || \
             [[ "${{ needs.code-quality.result }}" == "failure" ]]; then
            echo "::error::One or more CI jobs failed"
            exit 1
          fi
          echo "All CI checks passed successfully!"
```
