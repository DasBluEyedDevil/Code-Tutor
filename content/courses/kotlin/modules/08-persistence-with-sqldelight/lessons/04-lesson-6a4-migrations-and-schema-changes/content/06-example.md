---
type: "EXAMPLE"
title: "Complete Migration Example"
---

A real-world migration workflow:

```kotlin
// ===== Starting Schema (Note.sq v1) =====
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL
);

// ===== After adding features, schema is now (v3): =====
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    is_favorite INTEGER NOT NULL DEFAULT 0,  -- Added in v2
    category_id INTEGER,                      -- Added in v3
    FOREIGN KEY (category_id) REFERENCES Category(id)
);

CREATE TABLE Category (  -- Added in v3
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL
);

// ===== 1.sqm (v1 → v2) =====
-- Add favorite support
ALTER TABLE Note ADD COLUMN is_favorite INTEGER NOT NULL DEFAULT 0;

// ===== 2.sqm (v2 → v3) =====
-- Add categories
CREATE TABLE Category (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL
);

ALTER TABLE Note ADD COLUMN category_id INTEGER REFERENCES Category(id);

// ===== Driver setup with migrations =====
fun createDatabase(driver: SqlDriver): AppDatabase {
    // Migrations run automatically based on version
    AppDatabase.Schema.migrate(
        driver = driver,
        oldVersion = driver.currentVersion(),  // e.g., 1
        newVersion = AppDatabase.Schema.version  // e.g., 3
    )
    return AppDatabase(driver)
}
```
