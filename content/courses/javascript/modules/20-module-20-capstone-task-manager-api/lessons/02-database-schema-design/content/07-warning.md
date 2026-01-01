---
type: "WARNING"
title: "Database Best Practices"
---

**Never do these:**
1. Store plain text passwords - Always hash with bcrypt/argon2
2. Skip migrations in production - Use `prisma migrate deploy`
3. Forget indexes on frequently queried fields
4. Delete production data without backup

**Always do these:**
1. Use transactions for multiple related operations
2. Validate data before it hits the database (Zod schemas)
3. Use parameterized queries (Prisma does this automatically)
4. Test your schema with seed data before deployment

**Development vs Production:**
```bash
# Development (schema changes, quick iteration)
bunx prisma db push

# Production (tracked migrations, safe rollbacks)
bunx prisma migrate deploy
```