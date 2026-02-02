---
type: "THEORY"
title: "Common Migration Patterns"
---

### Adding a Column
```sql
-- 1.sqm
ALTER TABLE Note ADD COLUMN category_id INTEGER;
```

### Adding a Column with Default
```sql
-- 2.sqm
ALTER TABLE Note ADD COLUMN is_archived INTEGER NOT NULL DEFAULT 0;
```

### Creating a New Table
```sql
-- 3.sqm
CREATE TABLE Category (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    color TEXT NOT NULL DEFAULT '#808080'
);
```

### Adding an Index
```sql
-- 4.sqm
CREATE INDEX idx_note_category ON Note(category_id);
```

### Renaming a Table (SQLite limitation - requires copy)
```sql
-- 5.sqm
-- SQLite doesn't support RENAME TABLE well, so we:
-- 1. Create new table
-- 2. Copy data
-- 3. Drop old table
-- 4. Rename new table

CREATE TABLE NoteNew (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    body TEXT NOT NULL  -- renamed from 'content'
);

INSERT INTO NoteNew (id, title, body)
SELECT id, title, content FROM Note;

DROP TABLE Note;

ALTER TABLE NoteNew RENAME TO Note;
```