---
type: "EXAMPLE"
title: "Initialize the Monorepo"
---

Create the root project structure with workspaces:

```bash
# Create monorepo root
mkdir full-stack-app && cd full-stack-app

# Initialize with Bun
bun init -y

# Create package directories
mkdir -p packages/api packages/web packages/shared

# Initialize each package
cd packages/api && bun init -y && cd ../..
cd packages/web && bun init -y && cd ../..
cd packages/shared && bun init -y && cd ../..

# Your structure should now be:
# full-stack-app/
# ├── packages/
# │   ├── api/package.json
# │   ├── web/package.json
# │   └── shared/package.json
# ├── package.json
# └── bun.lockb
```
