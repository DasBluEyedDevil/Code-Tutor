---
type: "EXAMPLE"
title: "Caching Dependencies for Speed"
---

**Speed up workflows by caching dependencies:**

uv has built-in caching support with the setup action:

```yaml
# ============================================
# UV CACHING (RECOMMENDED)
# ============================================
# The astral-sh/setup-uv action handles caching automatically!
steps:
  - uses: actions/checkout@v4
  
  - name: Install uv
    uses: astral-sh/setup-uv@v4
    with:
      version: "latest"
      enable-cache: true  # Enable uv cache (default: true)

  - name: Install dependencies
    run: uv sync --all-extras --dev
    # Second run is much faster due to cache!

# ============================================
# MANUAL CACHING (for other tools)
# ============================================
steps:
  - uses: actions/checkout@v4
  
  - name: Cache pip packages
    uses: actions/cache@v4
    with:
      path: ~/.cache/pip
      key: ${{ runner.os }}-pip-${{ hashFiles('**/requirements*.txt') }}
      restore-keys: |
        ${{ runner.os }}-pip-

  - name: Cache mypy
    uses: actions/cache@v4
    with:
      path: .mypy_cache
      key: ${{ runner.os }}-mypy-${{ hashFiles('src/**/*.py') }}

# ============================================
# CACHE BEHAVIOR
# ============================================
# Cache key: Unique identifier for the cache
# - Changes when dependencies change (hashFiles)
# - Different per OS
# - Restored from closest matching key

# restore-keys: Fallback keys if exact match not found
# - Uses prefix matching
# - Allows partial cache restoration

# ============================================
# CHECKING CACHE EFFECTIVENESS
# ============================================
# In workflow logs, look for:
# - "Cache hit" = Cache was restored
# - "Cache miss" = No cache found, will save new one
# - "Cache restored from key" = Partial match used

# Typical speedup:
# - First run: 60-90 seconds (install all deps)
# - Cached run: 5-10 seconds (deps already installed)
```
