---
type: "KEY_POINT"
title: "Migrations Workflow"
---

**Migrations track model changes:**

```bash
# After changing models.py:

# 1. Create migration (generates SQL)
python manage.py makemigrations
# Output: Migrations for 'transactions':
#   transactions/migrations/0001_initial.py
#     - Create model Category
#     - Create model Account
#     - Create model Transaction

# 2. Apply migration (runs SQL)
python manage.py migrate
# Output: Applying transactions.0001_initial... OK

# 3. View SQL without running
python manage.py sqlmigrate transactions 0001
```

**Migration Best Practices:**
- Always commit migrations to version control
- Review generated migrations before applying
- Never edit applied migrations
- Use `--fake` only when you know what you're doing
- Test migrations on a copy of production data