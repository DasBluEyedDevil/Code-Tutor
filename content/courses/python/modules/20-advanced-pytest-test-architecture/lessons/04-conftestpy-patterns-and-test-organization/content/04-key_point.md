---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **`conftest.py`** automatically provides fixtures to tests in its directory
- **Hierarchy matters** - fixtures cascade from parent to child directories
- **`autouse=True`** runs a fixture for every test automatically
- **`pytest_configure`** sets up pytest at startup
- **`pytest_collection_modifyitems`** can skip or modify tests dynamically
- Keep conftest.py organized: config, fixtures, hooks