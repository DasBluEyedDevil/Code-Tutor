---
type: "EXAMPLE"
title: "Migrations"
---

**Database Migrations with Django**

Migrations track changes to your models and apply them to the database.

**Migration Workflow:**

1. **Make changes to models.py**
2. **Generate migration files:**
   ```bash
   python manage.py makemigrations
   ```
   Creates a file like `0001_initial.py` in `migrations/` folder

3. **Apply migrations to database:**
   ```bash
   python manage.py migrate
   ```
   Executes the SQL to create/alter tables

**Useful Commands:**

- **See pending migrations:**
  ```bash
  python manage.py showmigrations
  ```

- **See generated SQL:**
  ```bash
  python manage.py sqlmigrate tracker 0001
  ```
  Shows the actual SQL that will run

- **Rollback migrations:**
  ```bash
  python manage.py migrate tracker 0001
  ```
  Reverts to a specific migration

**Best Practices:**
- Commit migration files to version control
- Never edit existing migrations
- Run `makemigrations` after every model change
- Test migrations on a copy of production data

```python
# Django Migration Commands

print("=== Django Migration Commands ===")

print("\n1. Create migration files:")
print("   python manage.py makemigrations")
print("   # Detects changes in models.py and creates migration files")

print("\n2. Apply migrations to database:")
print("   python manage.py migrate")
print("   # Runs all pending migrations")

print("\n3. Show generated SQL:")
print("   python manage.py sqlmigrate tracker 0001")
print("   # Displays the SQL that will be executed")

print("\n=== Example SQL Output ===")
example_sql = '''
BEGIN;
--
-- Create model Category
--
CREATE TABLE "tracker_category" (
    "id" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" varchar(50) NOT NULL,
    "description" text NOT NULL
);
--
-- Create model Transaction
--
CREATE TABLE "tracker_transaction" (
    "id" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
    "amount" decimal NOT NULL,
    "transaction_type" varchar(10) NOT NULL,
    "description" text NOT NULL,
    "created_at" datetime NOT NULL,
    "category_id" integer NULL REFERENCES "tracker_category" ("id"),
    "user_id" integer NOT NULL REFERENCES "auth_user" ("id")
);
COMMIT;
'''
print(example_sql)

print("\n=== Migration Best Practices ===")
print("  - Always commit migration files to git")
print("  - Never manually edit existing migrations")
print("  - Run makemigrations after each model change")
print("  - Test migrations locally before deploying")
```
