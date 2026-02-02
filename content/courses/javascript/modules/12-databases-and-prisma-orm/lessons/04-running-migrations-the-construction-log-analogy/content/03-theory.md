---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Prisma Migration Commands Guide:

1. **Creating Migrations** (Development):
   ```bash
   # Standard workflow
   npx prisma migrate dev --name add_user_table
   
   # What this does:
   # 1. Compares schema.prisma to current database
   # 2. Generates SQL migration file
   # 3. Applies migration to database
   # 4. Runs prisma generate (updates Prisma Client)
   ```

2. **Migration File Structure**:
   ```
   prisma/
   ├── schema.prisma
   └── migrations/
       ├── 20250114120000_init/
       │   └── migration.sql
       ├── 20250114130000_add_posts/
       │   └── migration.sql
       └── migration_lock.toml
   ```

3. **Example Migration File**:
   ```sql
   -- CreateTable
   CREATE TABLE "User" (
       "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
       "email" TEXT NOT NULL,
       "name" TEXT NOT NULL
   );
   
   -- CreateIndex
   CREATE UNIQUE INDEX "User_email_key" ON "User"("email");
   ```

4. **Common Migration Workflows**:

   **First Migration**:
   ```bash
   # 1. Write schema.prisma
   # 2. Create initial migration
   npx prisma migrate dev --name init
   ```

   **Add New Table**:
   ```bash
   # 1. Add model to schema.prisma
   # 2. Create migration
   npx prisma migrate dev --name add_posts
   ```

   **Modify Existing Table**:
   ```bash
   # 1. Update model in schema.prisma
   # 2. Create migration
   npx prisma migrate dev --name add_user_bio
   ```

5. **Development vs Production**:

   **Development** (migrate dev):
   - Creates migration files
   - Applies to local database
   - Updates Prisma Client
   - Can reset database easily
   
   ```bash
   npx prisma migrate dev
   ```

   **Production** (migrate deploy):
   - Only applies existing migrations
   - Never creates new migrations
   - Safe for production
   - No schema changes
   
   ```bash
   npx prisma migrate deploy
   ```

6. **Reset Database** (Development Only!):
   ```bash
   npx prisma migrate reset
   
   # This:
   # 1. Drops entire database
   # 2. Creates new database
   # 3. Applies all migrations
   # 4. Runs seed script (if exists)
   ```

7. **Prototyping Without Migrations**:
   ```bash
   npx prisma db push
   
   # Use when:
   # - Early prototyping
   # - Don't want migration files yet
   # - Quick schema tests
   
   # DON'T use in production!
   ```

8. **Check Migration Status**:
   ```bash
   npx prisma migrate status
   
   # Shows:
   # - Applied migrations
   # - Pending migrations
   # - Database drift (manual changes)
   ```

9. **Naming Conventions**:
   ```bash
   # Good names (descriptive):
   npx prisma migrate dev --name init
   npx prisma migrate dev --name add_user_profile
   npx prisma migrate dev --name make_email_unique
   npx prisma migrate dev --name add_post_comments
   
   # Bad names (not descriptive):
   npx prisma migrate dev --name update
   npx prisma migrate dev --name fix
   npx prisma migrate dev --name changes
   ```

10. **Full Deployment Workflow**:
    ```bash
    # Development:
    git checkout -b feature/add-comments
    # Update schema.prisma
    npx prisma migrate dev --name add_comments
    git add prisma/
    git commit -m "Add comments table"
    git push
    
    # Production (CI/CD or manual):
    git pull
    npx prisma migrate deploy  # Apply pending migrations
    npm run build
    npm run start
    ```