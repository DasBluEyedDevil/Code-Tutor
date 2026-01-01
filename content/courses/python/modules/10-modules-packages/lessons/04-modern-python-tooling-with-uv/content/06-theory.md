---
type: "THEORY"
title: "Understanding pyproject.toml"
---

**pyproject.toml is the modern standard for Python project configuration.** It replaces setup.py, setup.cfg, requirements.txt, and various tool-specific config files.

**Sections in pyproject.toml:**

**[project]** - Core project metadata
- `name` - Package name (lowercase, hyphens)
- `version` - Semantic version ("1.0.0")
- `description` - One-line summary
- `requires-python` - Minimum Python version
- `dependencies` - Runtime dependencies

**[project.optional-dependencies]** - Extra dependencies
- `dev` - Development tools (pytest, ruff)
- `docs` - Documentation (sphinx, mkdocs)

**[tool.*]** - Tool-specific configuration
- `[tool.ruff]` - Ruff linter settings
- `[tool.pytest.ini_options]` - Pytest settings
- `[tool.mypy]` - Type checker settings

**Version specifiers:**
- `>=3.0.0` - At least version 3.0.0
- `>=3.0.0,<4.0.0` - Compatible range
- `~=3.0.0` - Same as >=3.0.0,<3.1.0
- `==3.0.0` - Exact version (avoid when possible)