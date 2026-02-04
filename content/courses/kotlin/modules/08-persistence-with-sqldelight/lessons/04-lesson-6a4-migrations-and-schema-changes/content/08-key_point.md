---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Schema migrations are SQL scripts that evolve your database structure** between versions. Place migration files in `src/commonMain/sqldelight/migrations/` numbered sequentially (1.sqm, 2.sqm, etc.).

**Migrations must be backward-compatible with existing data**—use ALTER TABLE ADD COLUMN with DEFAULT values, not DROP/CREATE patterns that lose data. Test migrations on production-like data before releasing.

**SQLDelight validates that migrations match your current schema**, ensuring migration scripts and schema definitions stay in sync. This compile-time check prevents deployment of inconsistent database states.
