---
type: "THEORY"
title: "Complex Queries: Joins and Aggregates"
---

### JOIN Queries

```sql
-- Category.sq
CREATE TABLE Category (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL
);

-- Note.sq (with category reference)
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    category_id INTEGER,
    FOREIGN KEY (category_id) REFERENCES Category(id)
);

-- Join query to get notes with category names
getNotesWithCategory:
SELECT 
    Note.id,
    Note.title,
    Category.name AS category_name
FROM Note
LEFT JOIN Category ON Note.category_id = Category.id;
```

**Generated code:**
```kotlin
// SQLDelight generates a custom data class for the query result:
data class GetNotesWithCategory(
    val id: Long,
    val title: String,
    val category_name: String?
)

val notesWithCategories = noteQueries.getNotesWithCategory().executeAsList()
```

### Aggregate Queries

```sql
-- Count notes per category
countNotesByCategory:
SELECT 
    Category.name,
    COUNT(Note.id) AS note_count
FROM Category
LEFT JOIN Note ON Note.category_id = Category.id
GROUP BY Category.id;

-- Get total note count
totalNoteCount:
SELECT COUNT(*) FROM Note;
```