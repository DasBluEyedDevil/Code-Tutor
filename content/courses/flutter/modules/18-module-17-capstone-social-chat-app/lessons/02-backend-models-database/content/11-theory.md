---
type: "THEORY"
title: "Database Migrations"
---


**Creating the Initial Schema**

Serverpod generates database migrations automatically from your protocol files. Here's the workflow:

**Migration Workflow**

```
1. Define/modify protocol YAML files
           ↓
2. Run 'serverpod generate'
           ↓
3. Review generated migration
           ↓
4. Apply migration to database
           ↓
5. Test with sample data
```

**Generating Migrations**

```bash
# From the server directory
cd server

# Generate code and migrations
serverpod generate

# This creates:
# - lib/src/generated/protocol.dart (Dart classes)
# - migrations/[timestamp]/definition.sql (Schema)
# - migrations/[timestamp]/migration.sql (Changes)
```

**Migration Structure**

Serverpod creates a migrations folder with:

```
migrations/
├── 20240101120000000/
│   ├── definition.sql      # Full schema at this point
│   ├── definition.json     # Schema metadata
│   └── migration.sql       # Changes from previous
└── 20240115150000000/
    ├── definition.sql
    ├── definition.json
    └── migration.sql
```

**Applying Migrations**

Migrations are applied when the server starts:

```bash
# Start with migration (development)
dart run bin/main.dart --apply-migrations

# Or use the maintenance command
serverpod run maintenance --apply-migrations
```

**Migration Best Practices**

1. **Review before applying**: Always check generated SQL
2. **Backup production**: Before migrating production databases
3. **Test rollback**: Ensure you can recover from failed migrations
4. **Version control**: Commit migrations with your code
5. **Seed data**: Create separate scripts for test data

