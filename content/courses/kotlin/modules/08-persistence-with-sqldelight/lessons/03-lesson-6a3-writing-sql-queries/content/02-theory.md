---
type: "THEORY"
title: "Basic CRUD Operations"
---

### CREATE - Inserting Data

```sql
-- Note.sq

-- Schema
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    created_at INTEGER NOT NULL,
    updated_at INTEGER NOT NULL
);

-- Insert query (named 'insertNote')
insertNote:
INSERT INTO Note(title, content, created_at, updated_at)
VALUES (?, ?, ?, ?);

-- Insert with named parameters
insertNoteNamed:
INSERT INTO Note(title, content, created_at, updated_at)
VALUES (:title, :content, :createdAt, :updatedAt);
```

### READ - Selecting Data

```sql
-- Get all notes
getAllNotes:
SELECT * FROM Note ORDER BY updated_at DESC;

-- Get single note by ID
getNoteById:
SELECT * FROM Note WHERE id = ?;

-- Get notes by title search
searchNotes:
SELECT * FROM Note 
WHERE title LIKE '%' || :query || '%'
ORDER BY updated_at DESC;
```

### UPDATE - Modifying Data

```sql
-- Update entire note
updateNote:
UPDATE Note 
SET title = ?, content = ?, updated_at = ?
WHERE id = ?;

-- Update just the content
updateContent:
UPDATE Note SET content = ?, updated_at = ? WHERE id = ?;
```

### DELETE - Removing Data

```sql
-- Delete by ID
deleteNote:
DELETE FROM Note WHERE id = ?;

-- Delete all notes
deleteAllNotes:
DELETE FROM Note;
```