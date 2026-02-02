---
type: "EXAMPLE"
title: "Matrix Builds for Multiple Python Versions"
---

**Test against multiple Python versions simultaneously:**

```yaml
# ============================================
# MATRIX STRATEGY
# ============================================
name: CI

on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest
    strategy:
      fail-fast: false  # Don't cancel other jobs if one fails
      matrix:
        python-version: ["3.11", "3.12", "3.13"]

    steps:
      - uses: actions/checkout@v4

      - name: Install uv
        uses: astral-sh/setup-uv@v4
        with:
          version: "latest"

      - name: Set up Python ${{ matrix.python-version }}
        run: uv python install ${{ matrix.python-version }}

      - name: Install dependencies
        run: uv sync --all-extras --dev

      - name: Run tests
        run: uv run pytest

# ============================================
# MULTI-DIMENSIONAL MATRIX
# ============================================
jobs:
  test:
    runs-on: ${{ matrix.os }}
    strategy:
      matrix:
        os: [ubuntu-latest, windows-latest, macos-latest]
        python-version: ["3.11", "3.12", "3.13"]
        exclude:
          - os: windows-latest
            python-version: "3.11"  # Skip Python 3.11 on Windows
        include:
          - os: ubuntu-latest
            python-version: "3.13"
            experimental: true  # Add custom variable

    steps:
      - uses: actions/checkout@v4
      
      - name: Install uv
        uses: astral-sh/setup-uv@v4
      
      - name: Set up Python
        run: uv python install ${{ matrix.python-version }}
      
      - name: Run tests
        run: uv run pytest
        continue-on-error: ${{ matrix.experimental || false }}

# This creates 9 jobs (3 OS x 3 Python versions)
# minus 1 exclusion = 8 parallel test runs!
```
