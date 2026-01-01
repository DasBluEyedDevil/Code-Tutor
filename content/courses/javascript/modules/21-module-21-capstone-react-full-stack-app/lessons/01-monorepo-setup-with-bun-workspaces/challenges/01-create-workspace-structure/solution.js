# Root package.json should contain:
{
  "name": "full-stack-app",
  "private": true,
  "workspaces": [
    "packages/api",
    "packages/web",
    "packages/shared"
  ],
  "scripts": {
    "dev:api": "bun run --filter=api dev",
    "dev:web": "bun run --filter=web dev",
    "typecheck": "bun run --filter=./packages/* typecheck"
  }
}

# Each package.json should have:
# - Unique "name" field (e.g., "@app/api")
# - "type": "module"
# - Relevant scripts and dependencies
# - For shared: "exports" field

# Run from root:
bun install
bun workspaces list

# Should output:
# @app/api
# @app/web
# @app/shared