# GitHub Actions CI Workflow Solution
# This file outputs the CI workflow YAML that students should create

CI_WORKFLOW = """name: CI

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

      - name: Format check
        run: uv run ruff format --check .

      - name: Run tests
        run: uv run pytest --cov=src tests/
        env:
          DATABASE_URL: postgresql+asyncpg://test:test@localhost:5432/test
"""

print("=== GitHub Actions CI Workflow ===")
print()
print("Save this content to '.github/workflows/ci.yml':")
print()
print(CI_WORKFLOW)
print()
print("=== Key Features ===")
print("1. Triggers on push/PR to main branch")
print("2. PostgreSQL 16 service container with health checks")
print("3. Uses uv for fast dependency management")
print("4. Ruff for linting and formatting checks")
print("5. pytest with coverage reporting")
print()
print("=== Pipeline Steps ===")
print("1. Checkout code")
print("2. Install uv (fast Python package manager)")
print("3. Set up Python 3.13")
print("4. Install dependencies with uv sync")
print("5. Run linting (ruff check)")
print("6. Run format check (ruff format --check)")
print("7. Run tests with coverage")
