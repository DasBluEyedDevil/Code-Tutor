---
type: "EXAMPLE"
title: "Update package.json with Test Scripts"
---

Add test scripts to make running tests easy:

```json
{
  "scripts": {
    "dev": "bun run --watch src/index.ts",
    "start": "bun run src/index.ts",
    "test": "bun test",
    "test:watch": "bun test --watch",
    "test:coverage": "bun test --coverage",
    "db:generate": "bunx prisma generate",
    "db:push": "bunx prisma db push",
    "db:migrate": "bunx prisma migrate dev",
    "db:seed": "bun run prisma/seed.ts",
    "db:studio": "bunx prisma studio",
    "typecheck": "tsc --noEmit"
  }
}

```
