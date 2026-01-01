---
type: "WARNING"
title: "Migration Gotchas"
---

### SQLite Limitations

SQLite has limited ALTER TABLE support:

**Supported:**
- `ADD COLUMN`
- `RENAME TABLE` (limited)
- `RENAME COLUMN` (SQLite 3.25+)

**NOT Supported:**
- `DROP COLUMN` (before SQLite 3.35)
- `ALTER COLUMN` type
- Change constraints

**Workaround for unsupported operations:**
```sql
-- Create new table with correct schema
-- Copy data
-- Drop old table
-- Rename new table
```

### Testing Migrations

Always test migrations:
1. Create a database with old schema
2. Insert test data
3. Run migrations
4. Verify data is preserved
5. Verify new schema works

### Never Delete Migration Files

Users might be upgrading from v1 → v5.
They need ALL intermediate migrations:
- 1.sqm (v1 → v2)
- 2.sqm (v2 → v3)
- 3.sqm (v3 → v4)
- 4.sqm (v4 → v5)