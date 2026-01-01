---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **CI runs automatically** on every push and pull request
- **Workflows live in** `.github/workflows/` as YAML files
- **Service containers** provide databases for testing (PostgreSQL, Redis)
- **`uv sync`** installs all dependencies from pyproject.toml
- **Ruff** handles both linting and formatting in one fast tool
- **Mypy** catches type errors before runtime
- **Secrets** are encrypted and never exposed in logs
- **Caching** speeds up workflows dramatically (60s to 5s)
- **Matrix builds** test against multiple Python versions in parallel
- **`needs:`** creates job dependencies (deploy after test passes)
- **`if:`** conditions control when jobs or steps run