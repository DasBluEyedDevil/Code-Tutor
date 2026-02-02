---
type: "EXAMPLE"
title: "Run the Seed"
---

Execute the seed script to populate your database:

```bash
# Run the seed script
bun run prisma/seed.ts

# Expected output:
# Seeding database...
# Created user: demo@example.com
# Created categories: Work Personal
# Created tasks: 4
# Seeding complete!

# Verify with Prisma Studio
bunx prisma studio
```
