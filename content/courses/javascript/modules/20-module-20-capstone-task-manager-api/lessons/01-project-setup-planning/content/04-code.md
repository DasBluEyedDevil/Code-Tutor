---
type: "CODE"
title: "Project Structure"
---

Create a clean project structure that separates concerns:

```bash
# Create directory structure
mkdir -p src/{routes,middleware,lib,schemas}

# Your project should look like this:
# task-manager-api/
# ├── prisma/
# │   └── schema.prisma
# ├── src/
# │   ├── routes/
# │   │   ├── auth.ts      # Registration & login
# │   │   ├── tasks.ts     # Task CRUD
# │   │   └── categories.ts # Category CRUD
# │   ├── middleware/
# │   │   └── auth.ts      # JWT verification
# │   ├── lib/
# │   │   └── db.ts        # Prisma client
# │   ├── schemas/
# │   │   └── validation.ts # Zod schemas
# │   └── index.ts         # Entry point
# ├── package.json
# └── tsconfig.json
```
