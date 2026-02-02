---
type: "EXAMPLE"
title: "Understanding Each Section"
---

**Let's break down the workflow step by step:**

```yaml
# ============================================
# WORKFLOW NAME AND TRIGGERS
# ============================================
name: CI  # Displayed in GitHub Actions UI

on:
  push:
    branches: [main]  # Run on pushes to main
  pull_request:
    branches: [main]  # Run on PRs targeting main

# ============================================
# SERVICE CONTAINERS
# ============================================
# PostgreSQL runs alongside your tests
services:
  postgres:
    image: postgres:16              # Docker image to use
    env:
      POSTGRES_USER: test           # Database credentials
      POSTGRES_PASSWORD: test
      POSTGRES_DB: test
    options: >-                     # Health check settings
      --health-cmd pg_isready       # Command to check if ready
      --health-interval 10s         # Check every 10 seconds
      --health-timeout 5s           # Timeout for each check
      --health-retries 5            # Retry 5 times before failing
    ports:
      - 5432:5432                   # Map container port to host

# ============================================
# UV AND PYTHON SETUP
# ============================================
- name: Install uv
  uses: astral-sh/setup-uv@v4      # Official uv action
  with:
    version: "latest"              # Always use latest uv

- name: Set up Python
  run: uv python install 3.13      # Install Python via uv

- name: Install dependencies
  run: uv sync --all-extras --dev  # Install all deps from pyproject.toml

# ============================================
# CODE QUALITY CHECKS
# ============================================
- name: Lint with ruff
  run: uv run ruff check .         # Check for code issues

- name: Format check with ruff
  run: uv run ruff format --check . # Verify formatting

- name: Type check with mypy
  run: uv run mypy src/            # Static type analysis

# ============================================
# TESTING WITH DATABASE
# ============================================
- name: Run tests
  run: uv run pytest --cov=src tests/
  env:
    # Connect to the service container
    DATABASE_URL: postgresql+asyncpg://test:test@localhost:5432/test

# ============================================
# CONDITIONAL DEPLOYMENT
# ============================================
deploy:
  needs: test                      # Wait for test job to pass
  if: github.ref == 'refs/heads/main'  # Only on main branch
  # ... deployment steps
```
