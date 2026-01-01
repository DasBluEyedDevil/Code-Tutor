# Create the following structure:
# full-stack-app/
# ├── packages/
# │   ├── api/
# │   │   └── package.json
# │   ├── web/
# │   │   └── package.json
# │   └── shared/
# │       └── package.json
# ├── package.json (root with workspaces)
# └── tsconfig.json

# 1. Initialize each package with correct package.json
# 2. Create root package.json with workspaces array
# 3. Run: bun install
# 4. Verify: bun workspaces list