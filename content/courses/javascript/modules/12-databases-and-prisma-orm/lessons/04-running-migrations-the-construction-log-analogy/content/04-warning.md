---
type: "WARNING"
title: "Common Pitfalls"
---

Common migration mistakes:

1. **Forgetting to run migrations**:
   ```bash
   # Changed schema.prisma but database not updated!
   
   # Must run:
   npx prisma migrate dev
   ```

2. **Using db push in production**:
   ```bash
   # NEVER do this in production:
   npx prisma db push  # No migration history!
   
   # Use migrate deploy instead:
   npx prisma migrate deploy
   ```

3. **Editing migration files manually**:
   - Don't edit generated migrations unless absolutely necessary
   - Prisma tracks checksums - manual edits can cause errors
   - Better: create new migration to fix issues

4. **Not committing migration files**:
   ```bash
   # WRONG: Ignoring migrations in .gitignore
   prisma/migrations/  # Don't do this!
   
   # CORRECT: Commit migrations to git
   git add prisma/migrations/
   git commit -m "Add user table migration"
   ```

5. **Running migrate dev in production**:
   ```bash
   # Development:
   npx prisma migrate dev  # Creates migrations
   
   # Production:
   npx prisma migrate deploy  # Only applies existing migrations
   ```

6. **Schema drift (manual database changes)**:
   ```bash
   # Problem: Someone manually altered database
   # Solution: Check drift
   npx prisma migrate diff
   
   # Then either:
   # - Revert manual changes
   # - Create migration to match changes
   ```

7. **Migration conflicts in teams**:
   ```bash
   # Two developers create migrations at same time
   # Git merge conflict in migrations folder
   
   # Resolution:
   # 1. Pull latest changes
   # 2. Reset local database
   npx prisma migrate reset
   # 3. Migrations will replay in correct order
   ```

8. **Forgetting to generate Prisma Client**:
   ```bash
   # After migration, types might be outdated
   npx prisma generate
   
   # Or use migrate dev which does it automatically
   npx prisma migrate dev
   ```

9. **Not testing migrations**:
   ```bash
   # Best practice:
   # 1. Create migration in development
   npx prisma migrate dev --name add_feature
   
   # 2. Test thoroughly
   npm run test
   
   # 3. Deploy to staging
   npx prisma migrate deploy
   
   # 4. Test staging
   
   # 5. Deploy to production
   npx prisma migrate deploy
   ```

10. **Breaking changes without data migration**:
    ```prisma
    // Dangerous: Deleting field loses data!
    model User {
      id    Int    @id
      email String
      // name String  ← Deleted! Data lost!
    }
    
    // Better: Make optional first, then remove later
    model User {
      id    Int     @id
      email String
      name  String? ← Made optional, can migrate data
    }
    ```