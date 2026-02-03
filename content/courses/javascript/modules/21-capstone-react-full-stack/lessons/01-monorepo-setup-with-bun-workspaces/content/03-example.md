---
type: "EXAMPLE"
title: "Configure Root package.json with Workspaces"
---

Set up the root package.json to manage all packages:

```json
{
  "name": "full-stack-app",
  "version": "1.0.0",
  "private": true,
  "type": "module",
  "scripts": {
    "dev": "bun run --filter=./packages/* dev",
    "dev:api": "bun run --filter=api dev",
    "dev:web": "bun run --filter=web dev",
    "build": "bun run --filter=./packages/* build",
    "test": "bun run --filter=./packages/* test",
    "typecheck": "bun run --filter=./packages/* typecheck",
    "db:migrate": "bun run --filter=api db:migrate",
    "db:studio": "bun run --filter=api db:studio"
  },
  "workspaces": [
    "packages/api",
    "packages/web",
    "packages/shared"
  ],
  "devDependencies": {
    "typescript": "^5.0.0",
    "@types/bun": "latest"
  }
}
```
