---
type: "EXAMPLE"
title: "Setup Shared Package"
---

Create the shared package for types and utilities:

```json
// packages/shared/package.json
{
  "name": "@app/shared",
  "version": "1.0.0",
  "type": "module",
  "exports": {
    ".": {
      "types": "./src/index.ts",
      "default": "./src/index.ts"
    }
  },
  "files": [
    "src"
  ],
  "scripts": {
    "typecheck": "tsc --noEmit"
  },
  "devDependencies": {
    "typescript": "^5.0.0",
    "zod": "^3.22.0"
  },
  "peerDependencies": {
    "zod": "^3.22.0"
  }
}
```
