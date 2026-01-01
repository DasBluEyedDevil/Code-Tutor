---
type: "CODE"
title: "Install Dependencies"
---

Install all workspace dependencies at once:

```bash
# From root directory
bun install

# This installs all dependencies for all packages
# and creates the workspace structure.

# Verify workspace setup:
bun workspaces list

# Expected output:
# @app/api
# @app/web
# @app/shared
```
