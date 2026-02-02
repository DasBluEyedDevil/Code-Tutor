---
type: "WARNING"
title: "SQL Gotchas"
---

### Boolean as INTEGER
SQLite doesn't have a boolean type:
```sql
is_completed INTEGER NOT NULL DEFAULT 0  -- 0 = false, 1 = true

-- Query booleans
WHERE is_completed = 1  -- true
WHERE is_completed = 0  -- false
```

### Date/Time as INTEGER
Store dates as epoch milliseconds:
```sql
created_at INTEGER NOT NULL  -- epoch millis

-- Sort by date
ORDER BY created_at DESC
```

### NULL Handling
```sql
-- Nullable column
description TEXT  -- can be NULL

-- Kotlin code:
val description: String? = note.description

-- Query with NULL
WHERE category_id IS NULL  -- not = NULL!
```

### LIKE is Case-Sensitive
```sql
-- Case-insensitive search
WHERE LOWER(title) LIKE '%' || LOWER(:query) || '%'
```