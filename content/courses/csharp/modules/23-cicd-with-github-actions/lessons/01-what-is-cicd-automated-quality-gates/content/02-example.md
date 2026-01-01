---
type: "EXAMPLE"
title: "ShopFlow CI Pipeline"
---

This GitHub Actions workflow defines our automated pipeline for ShopFlow. Let's examine each section to understand how the pieces fit together.

```yaml
# ===== .github/workflows/ci.yml =====
# This file lives in your repository and GitHub automatically detects it

name: ShopFlow CI  # Display name in GitHub Actions UI

# TRIGGERS: When should this pipeline run?
on:
  push:
    branches: [main]        # Run when code is pushed to main branch
  pull_request:
    branches: [main]        # Run when PRs target main branch

# JOBS: What work should be performed?
jobs:
  build-and-test:           # Job name (can have multiple jobs)
    runs-on: ubuntu-latest  # Virtual machine environment

    # SERVICES: External dependencies needed for tests
    services:
      postgres:             # Service name (accessed as 'postgres' hostname)
        image: postgres:16  # Docker image to use
        env:                # Environment variables for the container
          POSTGRES_USER: shopflow
          POSTGRES_PASSWORD: shopflow_test
          POSTGRES_DB: shopflow_test
        ports:
          - 5432:5432       # Map container port to runner port
        options: >-         # Health check to ensure DB is ready
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

    # STEPS: Sequential actions within the job
    steps:
      # Step 1: Get the code
      - uses: actions/checkout@v4
        # This action clones your repository into the runner

      # Step 2: Install .NET SDK
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'  # Use latest .NET 9 patch version

      # Step 3: Download NuGet packages
      - name: Restore dependencies
        run: dotnet restore
        # Downloads all packages defined in .csproj files

      # Step 4: Compile the application
      - name: Build
        run: dotnet build --no-restore --configuration Release
        # --no-restore: Skip restore (already done)
        # --configuration Release: Build optimized version

      # Step 5: Run fast unit tests first
      - name: Run unit tests
        run: dotnet test tests/ShopFlow.Tests.Unit --no-build --configuration Release
        # Unit tests are fast, isolated, no external dependencies

      # Step 6: Run integration tests (need database)
      - name: Run integration tests
        run: dotnet test tests/ShopFlow.Tests.Integration --no-build --configuration Release
        env:
          # Tell our app how to connect to the PostgreSQL service
          ConnectionStrings__DefaultConnection: "Host=localhost;Database=shopflow_test;Username=shopflow;Password=shopflow_test"
        # Integration tests verify real database operations work
```
