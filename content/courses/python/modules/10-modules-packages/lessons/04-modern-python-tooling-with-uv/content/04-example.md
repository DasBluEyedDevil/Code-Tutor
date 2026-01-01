---
type: "EXAMPLE"
title: "Code Example: Python Version Management"
---

**uv can install and manage Python versions too!** No need for pyenv or separate Python installers. uv downloads and manages Python versions automatically.

**Supported versions:** Python 3.8 through 3.13 (and 3.14 beta)

```python
print("="*60)
print("PYTHON VERSION MANAGEMENT WITH uv")
print("="*60)

print("""
# ============================================
# INSTALLING PYTHON VERSIONS
# ============================================

# Install the latest Python 3.13
uv python install 3.13

# Install a specific version
uv python install 3.13.1

# Install multiple versions
uv python install 3.12 3.13

# Install the latest available Python
uv python install

# ============================================
# LISTING PYTHON VERSIONS
# ============================================

# See all installed Python versions
uv python list

# Output example:
# cpython-3.13.1-macos-aarch64-none    ~/.local/share/uv/python/cpython-3.13.1
# cpython-3.12.8-macos-aarch64-none    ~/.local/share/uv/python/cpython-3.12.8

# ============================================
# PINNING PROJECT PYTHON VERSION
# ============================================

# Pin Python version for a project
uv python pin 3.13

# This creates/updates .python-version file
# Contents: 3.13

# Now 'uv run' always uses Python 3.13 for this project!

# ============================================
# CREATING PROJECT WITH SPECIFIC PYTHON
# ============================================

# Create project requiring Python 3.13+
uv init my-project --python 3.13

# Or specify in pyproject.toml:
# requires-python = ">=3.13"
""")

print("\n" + "="*60)
print("EXAMPLE: COMPLETE PROJECT SETUP")
print("="*60)

print("""
# Full workflow for a new FastAPI project:

# 1. Ensure Python 3.13 is available
uv python install 3.13

# 2. Create the project
uv init fastapi-demo --python 3.13
cd fastapi-demo

# 3. Add dependencies
uv add fastapi uvicorn pydantic

# 4. Add dev dependencies
uv add --dev pytest httpx ruff

# 5. Run the app
uv run uvicorn main:app --reload

# That's it! No manual venv creation, no activation,
# no pip install. Just uv and you're running!
""")
```
