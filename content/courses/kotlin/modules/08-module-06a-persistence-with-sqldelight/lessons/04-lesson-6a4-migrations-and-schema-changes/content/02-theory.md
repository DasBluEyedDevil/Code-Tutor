---
type: "THEORY"
title: "Why Migrations Matter"
---

### The Problem

Your app is live with users. You need to add a new column:

```sql
-- Version 1 (current)
CREATE TABLE Note (
    id INTEGER PRIMARY KEY,
    title TEXT NOT NULL
);

-- Version 2 (new)
CREATE TABLE Note (
    id INTEGER PRIMARY KEY,
    title TEXT NOT NULL,
    color TEXT  -- NEW!
);
```

**Without migrations:**
- ❌ App crashes on startup
- ❌ All user data is lost
- ❌ Users leave 1-star reviews

**With migrations:**
- ✅ Database updates smoothly
- ✅ All existing data preserved
- ✅ Users don't notice anything