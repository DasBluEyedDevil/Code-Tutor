---
type: "THEORY"
title: "SQLDelight Migration Files"
---

Migrations are `.sqm` files named with version numbers:

```
src/commonMain/sqldelight/
├── com/example/app/
│   ├── Note.sq          # Current schema (v2)
│   ├── 1.sqm             # Migration: v1 → v2
│   └── 2.sqm             # Migration: v2 → v3 (future)
```

### Migration File Format

**1.sqm** (first migration, from v1 to v2):
```sql
-- Add color column to Note table
ALTER TABLE Note ADD COLUMN color TEXT;
```

**2.sqm** (second migration, from v2 to v3):
```sql
-- Add tags table
CREATE TABLE Tag (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

-- Add junction table for note-tag relationship
CREATE TABLE NoteTag (
    note_id INTEGER NOT NULL,
    tag_id INTEGER NOT NULL,
    PRIMARY KEY (note_id, tag_id)
);
```

### Version Tracking

SQLDelight tracks versions automatically:
1. Fresh install: Creates tables from `.sq` schema
2. Upgrade: Runs migration files in order (1.sqm, 2.sqm, ...)
3. Downgrade: Not supported (plan carefully!)