---
type: "EXAMPLE"
title: "Complete CI Workflow for Python with uv"
---

**A production-ready CI pipeline for our Finance Tracker:**

This workflow:
1. Triggers on push to main and all PRs
2. Sets up PostgreSQL as a service container
3. Installs Python and dependencies with uv
4. Runs linting, formatting, and type checks
5. Executes tests with coverage
6. Has a deploy job that only runs on main

```yaml
# .github/workflows/ci.yml
name: CI

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest

    services:
      postgres:
        image: postgres:16
        env:
          POSTGRES_USER: test
          POSTGRES_PASSWORD: test
          POSTGRES_DB: test
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
        ports:
          - 5432:5432

    steps:
      - uses: actions/checkout@v4

      - name: Install uv
        uses: astral-sh/setup-uv@v4
        with:
          version: "latest"

      - name: Set up Python
        run: uv python install 3.13

      - name: Install dependencies
        run: uv sync --all-extras --dev

      - name: Lint with ruff
        run: uv run ruff check .

      - name: Format check with ruff
        run: uv run ruff format --check .

      - name: Type check with mypy
        run: uv run mypy src/

      - name: Run tests
        run: uv run pytest --cov=src tests/
        env:
          DATABASE_URL: postgresql+asyncpg://test:test@localhost:5432/test

  deploy:
    needs: test
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Deploy to production
        run: echo "Deploy step goes here"
        # In production, use:
        # - uses: superfly/flyctl-actions/setup-flyctl@master
        # - run: flyctl deploy --remote-only
```
