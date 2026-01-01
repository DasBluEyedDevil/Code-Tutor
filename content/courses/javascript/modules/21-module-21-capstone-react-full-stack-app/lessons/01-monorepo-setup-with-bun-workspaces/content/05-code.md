---
type: "CODE"
title: "Setup API Package"
---

Configure the backend API package:

```json
// packages/api/package.json
{
  "name": "@app/api",
  "version": "1.0.0",
  "type": "module",
  "scripts": {
    "dev": "bun run --watch src/index.ts",
    "start": "bun run src/index.ts",
    "db:generate": "bunx prisma generate",
    "db:push": "bunx prisma db push",
    "db:migrate": "bunx prisma migrate dev",
    "db:studio": "bunx prisma studio",
    "typecheck": "tsc --noEmit"
  },
  "dependencies": {
    "@app/shared": "*",
    "hono": "^4.0.0",
    "@hono/zod-validator": "^0.2.0",
    "zod": "^3.22.0",
    "@prisma/client": "^5.0.0"
  },
  "devDependencies": {
    "prisma": "^5.0.0",
    "typescript": "^5.0.0",
    "@types/bun": "latest"
  }
}
```
