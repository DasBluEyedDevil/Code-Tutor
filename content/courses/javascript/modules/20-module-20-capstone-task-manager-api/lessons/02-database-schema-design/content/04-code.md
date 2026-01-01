---
type: "CODE"
title: "Run the Migration"
---

Apply the schema to create your database:

```bash
# Generate Prisma Client (creates TypeScript types)
bunx prisma generate

# Create the database and apply schema
bunx prisma db push

# Or, for production-ready migrations:
bunx prisma migrate dev --name init

# Open Prisma Studio to view your database
bunx prisma studio
```
