---
type: "THEORY"
title: "Database Migrations"
---

Migrations are files that describe how to change your database schema over time. When you modify your protocol YAML files, Serverpod can generate migration files that update the database to match your new definitions.

**Why Migrations Matter:**

1. **Version Control for Database**: Track all schema changes in git
2. **Team Coordination**: Everyone applies the same changes
3. **Reversible Changes**: Roll back if something goes wrong
4. **Production Safety**: Test changes before applying to production

**Migration Workflow:**

1. Modify your protocol YAML files
2. Run `serverpod create-migration` to generate migration files
3. Review the generated SQL to understand what will change
4. Run the server with `--apply-migrations` to apply changes
5. Commit the migration files to git

**Migration Files Location:**

```
my_app_server/
  migrations/
    20240101120000/
      definition.yaml
      migration.sql
    20240115143000/
      definition.yaml
      migration.sql
```

Each migration folder is timestamped. The `definition.yaml` describes the target state, and `migration.sql` contains the SQL to get there.

