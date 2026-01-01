-- Given this initial schema (v1):
CREATE TABLE Note (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    created_at INTEGER NOT NULL
);

-- Final schema (v3) should have:
-- Note table with: id, title, content, created_at, updated_at, is_pinned, folder_id
-- Folder table with: id, name, color

-- TODO: Write 1.sqm (v1 → v2)
-- Add: updated_at (INTEGER, copy created_at value), is_pinned (INTEGER DEFAULT 0)


-- TODO: Write 2.sqm (v2 → v3)
-- Add: Folder table, folder_id column in Note