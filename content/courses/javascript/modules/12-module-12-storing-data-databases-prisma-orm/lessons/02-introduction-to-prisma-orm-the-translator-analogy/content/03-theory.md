---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding Prisma ORM:

1. **What is Prisma?**
   - Modern ORM (Object-Relational Mapping)
   - Translates TypeScript ↔ SQL
   - Type-safe database client
   - Works with PostgreSQL, MySQL, SQLite, MongoDB, etc.

2. **Prisma Setup** (typical workflow):
   ```bash
   # Install Prisma
   npm install prisma --save-dev
   npm install @prisma/client
   
   # Initialize Prisma
   npx prisma init
   
   # This creates:
   # - prisma/schema.prisma (database schema)
   # - .env (database connection string)
   ```

3. **Prisma Schema** (schema.prisma):
   ```prisma
   model User {
     id        Int      @id @default(autoincrement())
     email     String   @unique
     name      String
     posts     Post[]
     createdAt DateTime @default(now())
   }
   
   model Post {
     id        Int      @id @default(autoincrement())
     title     String
     content   String?
     published Boolean  @default(false)
     userId    Int
     user      User     @relation(fields: [userId], references: [id])
   }
   ```

4. **Prisma Client Usage**:
   ```typescript
   import { PrismaClient } from '@prisma/client';
   const prisma = new PrismaClient();
   
   // All your database operations...
   ```

5. **CRUD Operations**:
   - **Create**: `prisma.user.create({ data: {...} })`
   - **Read**: `prisma.user.findUnique({ where: {...} })`
   - **Update**: `prisma.user.update({ where: {...}, data: {...} })`
   - **Delete**: `prisma.user.delete({ where: {...} })`

6. **Relationships**:
   ```typescript
   // Get user with all their posts
   const userWithPosts = await prisma.user.findUnique({
     where: { id: 1 },
     include: { posts: true }
   });
   ```

7. **Migrations** (database schema changes):
   ```bash
   # Create migration
   npx prisma migrate dev --name add_users_table
   
   # Apply migrations to production
   npx prisma migrate deploy
   ```

8. **Prisma Studio** (database GUI):
   ```bash
   npx prisma studio
   # Opens visual database editor in browser
   ```