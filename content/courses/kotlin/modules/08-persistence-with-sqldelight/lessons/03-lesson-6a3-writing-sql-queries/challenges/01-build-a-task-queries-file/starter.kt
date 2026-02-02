-- Task.sq
-- Create a complete SQLDelight file for tasks

-- Schema:
-- - id: auto-incrementing primary key
-- - title: required text
-- - description: optional text  
-- - is_completed: boolean (stored as integer)
-- - priority: text ("LOW", "MEDIUM", "HIGH")
-- - due_date: optional integer (epoch millis)
-- - created_at: integer

-- TODO: Write CREATE TABLE

-- TODO: Write these named queries:
-- 1. getAllTasks (ordered by due_date, then priority)
-- 2. getTaskById
-- 3. getIncompleteTasks
-- 4. getTasksByPriority (takes priority param)
-- 5. getOverdueTasks (due_date < current time, not completed)
-- 6. insertTask
-- 7. updateTask
-- 8. toggleComplete
-- 9. deleteTask
-- 10. deleteCompletedTasks