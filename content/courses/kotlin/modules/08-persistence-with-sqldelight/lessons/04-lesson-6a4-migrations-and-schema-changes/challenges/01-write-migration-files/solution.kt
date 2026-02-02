-- 1.sqm (v1 → v2)
ALTER TABLE Note ADD COLUMN updated_at INTEGER;
ALTER TABLE Note ADD COLUMN is_pinned INTEGER NOT NULL DEFAULT 0;

-- Set updated_at to created_at for existing rows
UPDATE Note SET updated_at = created_at WHERE updated_at IS NULL;

-- 2.sqm (v2 → v3)
CREATE TABLE Folder (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    color TEXT NOT NULL DEFAULT '#808080'
);

ALTER TABLE Note ADD COLUMN folder_id INTEGER REFERENCES Folder(id);

CREATE INDEX idx_note_folder ON Note(folder_id);