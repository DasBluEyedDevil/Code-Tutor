-- Categories table
CREATE TABLE Category (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    color TEXT NOT NULL DEFAULT '#808080'
);

-- Tasks table with foreign key to Category
CREATE TABLE Task (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    description TEXT,
    is_completed INTEGER NOT NULL DEFAULT 0,
    due_date INTEGER,
    category_id INTEGER,
    created_at INTEGER NOT NULL,
    FOREIGN KEY (category_id) REFERENCES Category(id) ON DELETE SET NULL
);

-- Named queries
getAllTasks:
SELECT * FROM Task ORDER BY due_date ASC;

getTasksByCategory:
SELECT * FROM Task WHERE category_id = ?;

getAllCategories:
SELECT * FROM Category;

insertTask:
INSERT INTO Task(title, description, is_completed, due_date, category_id, created_at)
VALUES (?, ?, 0, ?, ?, ?);

insertCategory:
INSERT INTO Category(name, color) VALUES (?, ?);

toggleTaskCompletion:
UPDATE Task SET is_completed = NOT is_completed WHERE id = ?;