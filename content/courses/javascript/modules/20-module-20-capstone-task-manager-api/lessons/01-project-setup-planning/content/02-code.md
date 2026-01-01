---
type: "CODE"
title: "Initialize the Project"
---

Let's set up our project with Bun and TypeScript in strict mode:

```bash
# Create project directory
mkdir task-manager-api && cd task-manager-api

# Initialize Bun project
bun init -y

# Install dependencies
bun add hono @hono/zod-validator zod prisma @prisma/client
bun add -d @types/bun typescript

# Initialize Prisma with SQLite
bunx prisma init --datasource-provider sqlite
```
