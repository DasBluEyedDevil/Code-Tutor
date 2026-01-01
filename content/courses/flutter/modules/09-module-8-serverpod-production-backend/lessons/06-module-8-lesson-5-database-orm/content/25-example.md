---
type: "EXAMPLE"
title: "Creating and Running Migrations"
---

Here is the step-by-step process for database migrations:



```bash
# Step 1: Make changes to your protocol files
# For example, add a new field to user.yaml:
#   phoneNumber: String?

# Step 2: Generate the migration
cd my_app_server
serverpod create-migration

# Output:
# Created migration: migrations/20240115143000
# Review the migration before applying.

# Step 3: Review the generated SQL
# migrations/20240115143000/migration.sql

# Example migration.sql content:
# BEGIN;
# ALTER TABLE users ADD COLUMN phone_number TEXT;
# COMMIT;

# Step 4: Apply the migration (development)
dart run bin/main.dart --apply-migrations

# Output:
# Applying migration: 20240115143000
# Migration applied successfully.
# Serverpod is running.

# Step 5: Commit migration files to git
git add migrations/
git commit -m "Add phone number field to users"

# For production deployment:
# Migrations are applied automatically when the server starts
# with --apply-migrations flag, or you can apply them manually:
dart run bin/main.dart --apply-migrations --mode production
```
