# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** HTTP & Web APIs
- **Lesson:** Database Integration with SQLite (ID: 14_03)
- **Difficulty:** advanced
- **Estimated Time:** 40 minutes

## Current Lesson Content

{
    "id":  "14_03",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: Persistent Data Storage",
                                "content":  "**Databases = Permanent data storage**\n\n**Without database:**\n```python\nusers = []  # Lost when server restarts!\n```\n\n**With database:**\n```python\n# Data persists between restarts\nusers = db.get_all_users()\n```\n\n**Why SQLite?**\n- ✅ Built into Python (no installation)\n- ✅ File-based (easy to use)\n- ✅ Perfect for learning\n- ✅ Great for small/medium apps\n- ✅ No server needed\n\n**SQL Basics:**\n\n**CREATE** - Make tables\n```sql\nCREATE TABLE users (\n    id INTEGER PRIMARY KEY,\n    name TEXT,\n    email TEXT UNIQUE\n)\n```\n\n**INSERT** - Add data\n```sql\nINSERT INTO users (name, email) VALUES (\u0027Alice\u0027, \u0027alice@example.com\u0027)\n```\n\n**SELECT** - Read data\n```sql\nSELECT * FROM users WHERE id = 1\n```\n\n**UPDATE** - Modify data\n```sql\nUPDATE users SET name = \u0027Bob\u0027 WHERE id = 1\n```\n\n**DELETE** - Remove data\n```sql\nDELETE FROM users WHERE id = 1\n```"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: SQLite Basics",
                                "content":  "**SQLite key concepts:**\n\n**1. Connection:**\n```python\nconn = sqlite3.connect(\u0027database.db\u0027)\n```\n\n**2. Cursor:**\n```python\ncursor = conn.cursor()\ncursor.execute(\"SQL query\")\n```\n\n**3. Parameterized queries (prevent SQL injection):**\n```python\n# GOOD - uses placeholders\ncursor.execute(\"SELECT * FROM users WHERE id = ?\", (user_id,))\n\n# BAD - vulnerable to SQL injection\ncursor.execute(f\"SELECT * FROM users WHERE id = {user_id}\")\n```\n\n**4. Fetching results:**\n```python\ncursor.fetchone()   # Single row\ncursor.fetchall()   # All rows\ncursor.fetchmany(5) # 5 rows\n```\n\n**5. Commit changes:**\n```python\nconn.commit()  # Save INSERT/UPDATE/DELETE\n```\n\n**6. Row factory:**\n```python\nconn.row_factory = sqlite3.Row\n# Access by name: user[\u0027name\u0027]\n```",
                                "code":  "import sqlite3\nimport os\n\nprint(\"=== Creating Database ===\")\n\n# Remove old database if exists\nif os.path.exists(\u0027example.db\u0027):\n    os.remove(\u0027example.db\u0027)\n\n# Connect to database (creates if doesn\u0027t exist)\nconn = sqlite3.connect(\u0027example.db\u0027)\ncursor = conn.cursor()\n\nprint(\"Database created: example.db\")\n\nprint(\"\\n=== Creating Table ===\")\n\ncursor.execute(\u0027\u0027\u0027\n    CREATE TABLE users (\n        id INTEGER PRIMARY KEY AUTOINCREMENT,\n        name TEXT NOT NULL,\n        email TEXT UNIQUE NOT NULL,\n        age INTEGER,\n        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP\n    )\n\u0027\u0027\u0027)\n\nprint(\"Table \u0027users\u0027 created\")\n\nprint(\"\\n=== INSERT - Adding Data ===\")\n\n# Single insert\ncursor.execute(\n    \"INSERT INTO users (name, email, age) VALUES (?, ?, ?)\",\n    (\u0027Alice\u0027, \u0027alice@example.com\u0027, 25)\n)\nprint(f\"Inserted user, ID: {cursor.lastrowid}\")\n\n# Multiple inserts\nusers_data = [\n    (\u0027Bob\u0027, \u0027bob@example.com\u0027, 30),\n    (\u0027Charlie\u0027, \u0027charlie@example.com\u0027, 35),\n    (\u0027Diana\u0027, \u0027diana@example.com\u0027, 28)\n]\n\ncursor.executemany(\n    \"INSERT INTO users (name, email, age) VALUES (?, ?, ?)\",\n    users_data\n)\nprint(f\"Inserted {cursor.rowcount} users\")\n\n# Commit changes\nconn.commit()\nprint(\"Changes committed\")\n\nprint(\"\\n=== SELECT - Reading Data ===\")\n\n# Get all users\ncursor.execute(\"SELECT * FROM users\")\nusers = cursor.fetchall()\n\nprint(f\"All users ({len(users)}):\")\nfor user in users:\n    print(f\"  ID: {user[0]}, Name: {user[1]}, Email: {user[2]}, Age: {user[3]}\")\n\n# Get specific user\ncursor.execute(\"SELECT * FROM users WHERE id = ?\", (1,))\nuser = cursor.fetchone()\nprint(f\"\\nUser #1: {user}\")\n\n# Get with WHERE clause\ncursor.execute(\"SELECT name, email FROM users WHERE age \u003e ?\", (28,))\nolder_users = cursor.fetchall()\nprint(f\"\\nUsers older than 28:\")\nfor name, email in older_users:\n    print(f\"  {name} ({email})\")\n\nprint(\"\\n=== UPDATE - Modifying Data ===\")\n\ncursor.execute(\n    \"UPDATE users SET age = ? WHERE name = ?\",\n    (26, \u0027Alice\u0027)\n)\nconn.commit()\nprint(f\"Updated {cursor.rowcount} row(s)\")\n\n# Verify update\ncursor.execute(\"SELECT name, age FROM users WHERE name = \u0027Alice\u0027\")\nname, age = cursor.fetchone()\nprint(f\"Alice\u0027s new age: {age}\")\n\nprint(\"\\n=== DELETE - Removing Data ===\")\n\ncursor.execute(\"DELETE FROM users WHERE id = ?\", (4,))\nconn.commit()\nprint(f\"Deleted {cursor.rowcount} row(s)\")\n\n# Count remaining\ncursor.execute(\"SELECT COUNT(*) FROM users\")\ncount = cursor.fetchone()[0]\nprint(f\"Remaining users: {count}\")\n\nprint(\"\\n=== Using Row Factory ===\")\n\n# Make results dict-like\nconn.row_factory = sqlite3.Row\ncursor = conn.cursor()\n\ncursor.execute(\"SELECT * FROM users LIMIT 1\")\nuser = cursor.fetchone()\n\nprint(\"Accessing by column name:\")\nprint(f\"  Name: {user[\u0027name\u0027]}\")\nprint(f\"  Email: {user[\u0027email\u0027]}\")\nprint(f\"  Age: {user[\u0027age\u0027]}\")\n\n# Close connection\nconn.close()\nprint(\"\\nConnection closed\")\n\nprint(\"\\n=== Context Manager Pattern ===\")\n\ndef get_all_users():\n    \"\"\"Get all users using context manager\"\"\"\n    with sqlite3.connect(\u0027example.db\u0027) as conn:\n        conn.row_factory = sqlite3.Row\n        cursor = conn.cursor()\n        cursor.execute(\"SELECT * FROM users\")\n        return cursor.fetchall()\n\nusers = get_all_users()\nprint(f\"Retrieved {len(users)} users using context manager\")\n\n# Cleanup\nos.remove(\u0027example.db\u0027)",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Syntax Breakdown",
                                "content":  "**Basic SQLite workflow:**\n\n```python\nimport sqlite3\n\n# 1. Connect\nconn = sqlite3.connect(\u0027database.db\u0027)\ncursor = conn.cursor()\n\n# 2. Execute SQL\ncursor.execute(\"CREATE TABLE ...\")\ncursor.execute(\"INSERT INTO ...\")\ncursor.execute(\"SELECT ...\")\n\n# 3. Fetch results (SELECT only)\nrows = cursor.fetchall()\n\n# 4. Commit changes (INSERT/UPDATE/DELETE)\nconn.commit()\n\n# 5. Close\nconn.close()\n```\n\n**With context manager (recommended):**\n\n```python\nwith sqlite3.connect(\u0027database.db\u0027) as conn:\n    cursor = conn.cursor()\n    cursor.execute(\"SELECT ...\")\n    results = cursor.fetchall()\n# Auto-commits and closes\n```\n\n**Common SQL patterns:**\n\n```sql\n-- Create table\nCREATE TABLE table_name (\n    id INTEGER PRIMARY KEY AUTOINCREMENT,\n    column1 TEXT NOT NULL,\n    column2 INTEGER DEFAULT 0\n)\n\n-- Insert\nINSERT INTO table_name (col1, col2) VALUES (?, ?)\n\n-- Select\nSELECT * FROM table_name WHERE condition\nSELECT col1, col2 FROM table_name ORDER BY col1\nSELECT * FROM table_name LIMIT 10\n\n-- Update\nUPDATE table_name SET col1 = ? WHERE id = ?\n\n-- Delete\nDELETE FROM table_name WHERE condition\n```"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: FastAPI + SQLite Integration",
                                "content":  "**FastAPI + SQLite integration patterns:**\n\n**1. Dependency injection for database:**\n```python\ndef get_db():\n    conn = sqlite3.connect(\u0027database.db\u0027)\n    conn.row_factory = sqlite3.Row\n    try:\n        yield conn\n    finally:\n        conn.close()\n\n@app.get(\u0027/items/\u0027)\ndef get_items(db = Depends(get_db)):\n    cursor = db.execute(\u0027SELECT * FROM items\u0027)\n    return cursor.fetchall()\n```\n\n**2. Row factory for dict-like access:**\n```python\nconn.row_factory = sqlite3.Row\n# Access columns by name: row[\u0027name\u0027]\n```\n\n**3. Parameterized queries (prevent SQL injection):**\n```python\n# Always use placeholders (?)\ndb.execute(\u0027SELECT * FROM users WHERE id = ?\u0027, (user_id,))\n```\n\n**4. Pydantic models for validation:**\n```python\nclass TaskCreate(BaseModel):\n    title: str\n    description: str = \u0027\u0027\n\n@app.post(\u0027/tasks/\u0027)\ndef create_task(task: TaskCreate, db = Depends(get_db)):\n    # task is already validated!\n```\n\n**5. FastAPI\u0027s Depends() system:**\n- Clean dependency injection\n- Automatic cleanup with yield\n- Reusable across endpoints",
                                "code":  "from fastapi import FastAPI, Depends, HTTPException\nfrom pydantic import BaseModel\nimport sqlite3\nimport os\nfrom typing import Optional\nfrom contextlib import contextmanager\n\napp = FastAPI()\nDATABASE = \u0027api.db\u0027\n\nprint(\"=== Database Helper Functions ===\")\n\n# Pydantic models for request/response validation\nclass TaskCreate(BaseModel):\n    title: str\n    description: str = \u0027\u0027\n    completed: bool = False\n\nclass TaskUpdate(BaseModel):\n    title: Optional[str] = None\n    description: Optional[str] = None\n    completed: Optional[bool] = None\n\nclass TaskResponse(BaseModel):\n    id: int\n    title: str\n    description: str\n    completed: bool\n    created_at: str\n\ndef get_db():\n    \"\"\"Database dependency - yields connection, auto-closes after request\"\"\"\n    conn = sqlite3.connect(DATABASE)\n    conn.row_factory = sqlite3.Row\n    try:\n        yield conn\n    finally:\n        conn.close()\n\ndef init_db():\n    \"\"\"Initialize database with schema\"\"\"\n    with sqlite3.connect(DATABASE) as conn:\n        conn.execute(\u0027\u0027\u0027\n            CREATE TABLE IF NOT EXISTS tasks (\n                id INTEGER PRIMARY KEY AUTOINCREMENT,\n                title TEXT NOT NULL,\n                description TEXT DEFAULT \u0027\u0027,\n                completed BOOLEAN DEFAULT 0,\n                created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP\n            )\n        \u0027\u0027\u0027)\n        conn.commit()\n    print(\"Database initialized\")\n\nprint(\"\\n=== API Endpoints with Database ===\")\n\n@app.get(\u0027/api/tasks\u0027)\ndef get_tasks(db = Depends(get_db)):\n    \"\"\"Get all tasks\"\"\"\n    cursor = db.execute(\u0027\u0027\u0027\n        SELECT id, title, description, completed, created_at \n        FROM tasks \n        ORDER BY created_at DESC\n    \u0027\u0027\u0027)\n    \n    tasks = [\n        {\n            \u0027id\u0027: row[\u0027id\u0027],\n            \u0027title\u0027: row[\u0027title\u0027],\n            \u0027description\u0027: row[\u0027description\u0027],\n            \u0027completed\u0027: bool(row[\u0027completed\u0027]),\n            \u0027created_at\u0027: row[\u0027created_at\u0027]\n        }\n        for row in cursor.fetchall()\n    ]\n    \n    return {\u0027tasks\u0027: tasks, \u0027count\u0027: len(tasks)}\n\n@app.get(\u0027/api/tasks/{task_id}\u0027)\ndef get_task(task_id: int, db = Depends(get_db)):\n    \"\"\"Get specific task\"\"\"\n    cursor = db.execute(\n        \u0027SELECT * FROM tasks WHERE id = ?\u0027,\n        (task_id,)\n    )\n    row = cursor.fetchone()\n    \n    if not row:\n        raise HTTPException(status_code=404, detail=\u0027Task not found\u0027)\n    \n    return {\n        \u0027id\u0027: row[\u0027id\u0027],\n        \u0027title\u0027: row[\u0027title\u0027],\n        \u0027description\u0027: row[\u0027description\u0027],\n        \u0027completed\u0027: bool(row[\u0027completed\u0027]),\n        \u0027created_at\u0027: row[\u0027created_at\u0027]\n    }\n\n@app.post(\u0027/api/tasks\u0027, status_code=201)\ndef create_task(task: TaskCreate, db = Depends(get_db)):\n    \"\"\"Create new task - Pydantic validates input automatically\"\"\"\n    cursor = db.execute(\n        \u0027INSERT INTO tasks (title, description, completed) VALUES (?, ?, ?)\u0027,\n        (task.title, task.description, task.completed)\n    )\n    db.commit()\n    \n    # Get the created task\n    cursor = db.execute(\u0027SELECT * FROM tasks WHERE id = ?\u0027, (cursor.lastrowid,))\n    row = cursor.fetchone()\n    \n    return {\n        \u0027id\u0027: row[\u0027id\u0027],\n        \u0027title\u0027: row[\u0027title\u0027],\n        \u0027description\u0027: row[\u0027description\u0027],\n        \u0027completed\u0027: bool(row[\u0027completed\u0027]),\n        \u0027created_at\u0027: row[\u0027created_at\u0027]\n    }\n\n@app.put(\u0027/api/tasks/{task_id}\u0027)\ndef update_task(task_id: int, task: TaskUpdate, db = Depends(get_db)):\n    \"\"\"Update task\"\"\"\n    # Check if task exists\n    cursor = db.execute(\u0027SELECT id FROM tasks WHERE id = ?\u0027, (task_id,))\n    if not cursor.fetchone():\n        raise HTTPException(status_code=404, detail=\u0027Task not found\u0027)\n    \n    # Build update query dynamically based on provided fields\n    updates = []\n    values = []\n    if task.title is not None:\n        updates.append(\u0027title = ?\u0027)\n        values.append(task.title)\n    if task.description is not None:\n        updates.append(\u0027description = ?\u0027)\n        values.append(task.description)\n    if task.completed is not None:\n        updates.append(\u0027completed = ?\u0027)\n        values.append(task.completed)\n    \n    if updates:\n        values.append(task_id)\n        db.execute(f\u0027UPDATE tasks SET {\", \".join(updates)} WHERE id = ?\u0027, values)\n        db.commit()\n    \n    # Return updated task\n    cursor = db.execute(\u0027SELECT * FROM tasks WHERE id = ?\u0027, (task_id,))\n    row = cursor.fetchone()\n    \n    return {\n        \u0027id\u0027: row[\u0027id\u0027],\n        \u0027title\u0027: row[\u0027title\u0027],\n        \u0027description\u0027: row[\u0027description\u0027],\n        \u0027completed\u0027: bool(row[\u0027completed\u0027]),\n        \u0027created_at\u0027: row[\u0027created_at\u0027]\n    }\n\n@app.delete(\u0027/api/tasks/{task_id}\u0027)\ndef delete_task(task_id: int, db = Depends(get_db)):\n    \"\"\"Delete task\"\"\"\n    cursor = db.execute(\u0027DELETE FROM tasks WHERE id = ?\u0027, (task_id,))\n    db.commit()\n    \n    if cursor.rowcount == 0:\n        raise HTTPException(status_code=404, detail=\u0027Task not found\u0027)\n    \n    return {\u0027message\u0027: \u0027Task deleted\u0027, \u0027id\u0027: task_id}\n\nprint(\"\\n=== Database Class Wrapper ===\")\n\nclass TaskDatabase:\n    \"\"\"Database wrapper class - reusable across your app\"\"\"\n    \n    def __init__(self, db_path: str):\n        self.db_path = db_path\n    \n    @contextmanager\n    def get_connection(self):\n        conn = sqlite3.connect(self.db_path)\n        conn.row_factory = sqlite3.Row\n        try:\n            yield conn\n        finally:\n            conn.close()\n    \n    def get_all(self):\n        with self.get_connection() as conn:\n            cursor = conn.execute(\u0027SELECT * FROM tasks ORDER BY created_at DESC\u0027)\n            return [dict(row) for row in cursor.fetchall()]\n    \n    def get_by_id(self, task_id: int):\n        with self.get_connection() as conn:\n            cursor = conn.execute(\u0027SELECT * FROM tasks WHERE id = ?\u0027, (task_id,))\n            row = cursor.fetchone()\n            return dict(row) if row else None\n    \n    def create(self, title: str, description: str = \u0027\u0027, completed: bool = False):\n        with self.get_connection() as conn:\n            cursor = conn.execute(\n                \u0027INSERT INTO tasks (title, description, completed) VALUES (?, ?, ?)\u0027,\n                (title, description, completed)\n            )\n            conn.commit()\n            return cursor.lastrowid\n    \n    def update(self, task_id: int, **kwargs):\n        with self.get_connection() as conn:\n            for field, value in kwargs.items():\n                if value is not None:\n                    conn.execute(f\u0027UPDATE tasks SET {field} = ? WHERE id = ?\u0027, (value, task_id))\n            conn.commit()\n    \n    def delete(self, task_id: int) -\u003e bool:\n        with self.get_connection() as conn:\n            cursor = conn.execute(\u0027DELETE FROM tasks WHERE id = ?\u0027, (task_id,))\n            conn.commit()\n            return cursor.rowcount \u003e 0\n\nif __name__ == \u0027__main__\u0027:\n    print(\"\\n=== FastAPI + SQLite API ===\")\n    print(\"Initializing database...\")\n    \n    # Remove old database\n    if os.path.exists(DATABASE):\n        os.remove(DATABASE)\n    \n    init_db()\n    \n    print(\"\\nAPI endpoints:\")\n    print(\"  GET    /api/tasks         - List all tasks\")\n    print(\"  GET    /api/tasks/{id}    - Get task\")\n    print(\"  POST   /api/tasks         - Create task\")\n    print(\"  PUT    /api/tasks/{id}    - Update task\")\n    print(\"  DELETE /api/tasks/{id}    - Delete task\")\n    print(\"  GET    /docs              - Interactive API docs\")\n    print(\"\\nRun with: uvicorn main:app --reload\")\n    \n    # Cleanup\n    if os.path.exists(DATABASE):\n        os.remove(DATABASE)",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **SQLite is file-based** - No server needed, perfect for small apps\n- **Use placeholders (?)** - Prevent SQL injection, never use f-strings\n- **Context managers for connections** - with sqlite3.connect() as conn:\n- **Commit changes** - conn.commit() after INSERT/UPDATE/DELETE\n- **Row factory for dict-like access** - conn.row_factory = sqlite3.Row\n- **Validate before database ops** - Check required fields first\n- **fetchone() vs fetchall()** - one() for single row, all() for multiple\n- **AUTOINCREMENT for IDs** - Let database handle ID generation"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "13_03-challenge-4",
                           "title":  "Interactive Exercise",
                           "description":  "Create a database-backed FastAPI app for a simple notes app:\n- Table: notes (id, title, content, created_at)\n- Endpoints: GET all, GET by id, POST create, DELETE\n- Use Depends() for database dependency injection\n- Use Pydantic models for request validation\n- Return proper status codes with HTTPException",
                           "instructions":  "Create a database-backed FastAPI app for a simple notes app:\n- Table: notes (id, title, content, created_at)\n- Endpoints: GET all, GET by id, POST create, DELETE\n- Use Depends() for database dependency injection\n- Use Pydantic models for request validation\n- Return proper status codes with HTTPException",
                           "starterCode":  "from fastapi import FastAPI, Depends, HTTPException\nfrom pydantic import BaseModel\nimport sqlite3\n\napp = FastAPI()\nDATABASE = \u0027notes.db\u0027\n\n# TODO: Create Pydantic model for note creation\n\n# TODO: Create database dependency with get_db()\n\n# TODO: Initialize database with notes table\n\n# TODO: Implement GET /api/notes\n\n# TODO: Implement GET /api/notes/{note_id}\n\n# TODO: Implement POST /api/notes\n\n# TODO: Implement DELETE /api/notes/{note_id}\n\n# Run with: uvicorn main:app --reload",
                           "solution":  "from fastapi import FastAPI, Depends, HTTPException\nfrom pydantic import BaseModel\nimport sqlite3\nfrom datetime import datetime\n\n# FastAPI Notes API with SQLite\n# This solution demonstrates database-backed REST API with FastAPI\n\napp = FastAPI()\nDATABASE = \u0027notes.db\u0027\n\n# Pydantic model for request validation\nclass NoteCreate(BaseModel):\n    title: str\n    content: str\n\ndef get_db():\n    \"\"\"Database dependency - yields connection, auto-closes.\"\"\"\n    conn = sqlite3.connect(DATABASE)\n    conn.row_factory = sqlite3.Row\n    try:\n        yield conn\n    finally:\n        conn.close()\n\ndef init_db():\n    \"\"\"Initialize the database with notes table.\"\"\"\n    with sqlite3.connect(DATABASE) as conn:\n        conn.execute(\u0027\u0027\u0027\n            CREATE TABLE IF NOT EXISTS notes (\n                id INTEGER PRIMARY KEY AUTOINCREMENT,\n                title TEXT NOT NULL,\n                content TEXT NOT NULL,\n                created_at TEXT DEFAULT CURRENT_TIMESTAMP\n            )\n        \u0027\u0027\u0027)\n        conn.commit()\n\ndef row_to_dict(row):\n    \"\"\"Convert sqlite3.Row to dictionary.\"\"\"\n    return dict(row) if row else None\n\n@app.get(\u0027/api/notes\u0027)\ndef get_all_notes(db = Depends(get_db)):\n    \"\"\"Get all notes.\"\"\"\n    cursor = db.execute(\u0027SELECT * FROM notes ORDER BY created_at DESC\u0027)\n    notes = [row_to_dict(row) for row in cursor.fetchall()]\n    return {\u0027notes\u0027: notes, \u0027count\u0027: len(notes)}\n\n@app.get(\u0027/api/notes/{note_id}\u0027)\ndef get_note(note_id: int, db = Depends(get_db)):\n    \"\"\"Get a specific note.\"\"\"\n    cursor = db.execute(\u0027SELECT * FROM notes WHERE id = ?\u0027, (note_id,))\n    note = row_to_dict(cursor.fetchone())\n    if not note:\n        raise HTTPException(status_code=404, detail=\u0027Note not found\u0027)\n    return note\n\n@app.post(\u0027/api/notes\u0027, status_code=201)\ndef create_note(note: NoteCreate, db = Depends(get_db)):\n    \"\"\"Create a new note - Pydantic validates automatically.\"\"\"\n    cursor = db.execute(\n        \u0027INSERT INTO notes (title, content) VALUES (?, ?)\u0027,\n        (note.title, note.content)\n    )\n    db.commit()\n    return {\u0027id\u0027: cursor.lastrowid, \u0027message\u0027: \u0027Note created\u0027}\n\n@app.delete(\u0027/api/notes/{note_id}\u0027)\ndef delete_note(note_id: int, db = Depends(get_db)):\n    \"\"\"Delete a note.\"\"\"\n    cursor = db.execute(\u0027DELETE FROM notes WHERE id = ?\u0027, (note_id,))\n    db.commit()\n    if cursor.rowcount == 0:\n        raise HTTPException(status_code=404, detail=\u0027Note not found\u0027)\n    return {\u0027message\u0027: f\u0027Note {note_id} deleted\u0027}\n\nif __name__ == \u0027__main__\u0027:\n    init_db()\n    print(\u0027Notes API - Run with: uvicorn main:app --reload\u0027)\n    print(\u0027API docs at: http://localhost:8000/docs\u0027)",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use \u0027def get_db()\u0027 with yield for the dependency. Use Depends(get_db) in route parameters. Raise HTTPException for errors."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting to use Depends() for the database",
                                                      "consequence":  "Database connection not managed properly",
                                                      "correction":  "Use db = Depends(get_db) in route parameters"
                                                  },
                                                  {
                                                      "mistake":  "Using return for errors instead of HTTPException",
                                                      "consequence":  "Wrong status code returned",
                                                      "correction":  "Use raise HTTPException(status_code=404, detail=\u0027message\u0027)"
                                                  },
                                                  {
                                                      "mistake":  "Forgetting to commit after INSERT/DELETE",
                                                      "consequence":  "Changes not saved to database",
                                                      "correction":  "Always call db.commit() after modifications"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Database Integration with SQLite",
    "estimatedMinutes":  40
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python Database Integration with SQLite 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "14_03",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

