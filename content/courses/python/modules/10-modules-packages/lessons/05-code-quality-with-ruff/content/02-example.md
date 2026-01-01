---
type: "EXAMPLE"
title: "Installing and Running Ruff"
---

**Ruff can be installed with uv or pip.** Since we're using uv for our projects, we add it as a dev dependency.

**Note:** Ruff integrates with VS Code, PyCharm, and other editors for real-time feedback as you type.

```python
# Ruff installation and basic usage demonstration
# Run these commands in your terminal!

print("="*60)
print("INSTALLING RUFF")
print("="*60)

print("""
# ============================================
# INSTALLATION OPTIONS
# ============================================

# Option 1: Add to project with uv (recommended)
uv add --dev ruff

# Option 2: Install globally with uv
uv tool install ruff

# Option 3: Install with pip
pip install ruff

# Verify installation
ruff --version
# Output: ruff 0.8.x
""")

print("\n" + "="*60)
print("BASIC COMMANDS")
print("="*60)

print("""
# ============================================
# LINTING (finding problems)
# ============================================

# Check all Python files in current directory
ruff check .

# Check a specific file
ruff check app.py

# Check with auto-fix (safe fixes only)
ruff check --fix .

# Check with ALL auto-fixes (including unsafe)
ruff check --fix --unsafe-fixes .

# ============================================
# FORMATTING (making code pretty)
# ============================================

# Format all Python files
ruff format .

# Format a specific file
ruff format app.py

# Check formatting without changing files
ruff format --check .

# Show what would change (diff)
ruff format --diff .

# ============================================
# COMBINING BOTH
# ============================================

# Typical workflow: format then lint
ruff format . && ruff check --fix .

# Or in one command with uv
uv run ruff format . && uv run ruff check --fix .
""")
```
