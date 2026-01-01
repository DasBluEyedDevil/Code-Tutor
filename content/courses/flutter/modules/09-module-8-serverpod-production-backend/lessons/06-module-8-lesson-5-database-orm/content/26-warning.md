---
type: "WARNING"
title: "Migration Best Practices"
---

**Never Edit Migration Files After They Are Committed**

Once a migration file is committed to git and possibly applied to other databases (staging, production, teammates' machines), never edit it. Create a new migration instead.

**Why:**
- Other databases have already applied the original migration
- Editing creates inconsistencies between environments
- The migration system tracks which migrations have been applied by their timestamp

**Safe Changes (Add-Only):**
- Adding new tables
- Adding new columns (nullable or with defaults)
- Adding new indexes

**Risky Changes (Require Care):**
- Renaming columns (data stays, but code breaks)
- Changing column types (data might not convert)
- Removing columns (data is lost)

**Destructive Changes (Require Data Migration):**
- Removing tables
- Removing columns with important data
- Changing relationships

**Repair Migrations:**

If something goes wrong, use:

```bash
serverpod create-repair-migration
```

This generates a migration that repairs the current state by comparing the actual database schema to the expected schema.

