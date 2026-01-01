-- Task.sq
CREATE TABLE Task (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    description TEXT,
    is_completed INTEGER NOT NULL DEFAULT 0,
    priority TEXT NOT NULL DEFAULT 'MEDIUM',
    due_date INTEGER,
    created_at INTEGER NOT NULL
);

getAllTasks:
SELECT * FROM Task 
ORDER BY due_date ASC NULLS LAST, 
    CASE priority 
        WHEN 'HIGH' THEN 1 
        WHEN 'MEDIUM' THEN 2 
        ELSE 3 
    END;

getTaskById:
SELECT * FROM Task WHERE id = ?;

getIncompleteTasks:
SELECT * FROM Task WHERE is_completed = 0 ORDER BY due_date ASC;

getTasksByPriority:
SELECT * FROM Task WHERE priority = ? ORDER BY due_date ASC;

getOverdueTasks:
SELECT * FROM Task 
WHERE due_date < :currentTime 
  AND is_completed = 0
ORDER BY due_date ASC;

insertTask:
INSERT INTO Task(title, description, is_completed, priority, due_date, created_at)
VALUES (?, ?, 0, ?, ?, ?);

updateTask:
UPDATE Task 
SET title = ?, description = ?, priority = ?, due_date = ?
WHERE id = ?;

toggleComplete:
UPDATE Task SET is_completed = NOT is_completed WHERE id = ?;

deleteTask:
DELETE FROM Task WHERE id = ?;

deleteCompletedTasks:
DELETE FROM Task WHERE is_completed = 1;