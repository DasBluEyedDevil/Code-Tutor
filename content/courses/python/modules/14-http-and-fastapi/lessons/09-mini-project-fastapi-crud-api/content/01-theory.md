---
type: "THEORY"
title: "Building a Complete API"
---

**Project: Task Management API**

We'll build the same API as the Flask project, but with FastAPI:

**Features:**
- Create, read, update, delete tasks
- Filter by status (pending/completed)
- Pagination
- Input validation
- Auto-generated docs

**Structure:**
```
app/
├── main.py          # FastAPI app
├── models.py        # Pydantic models
├── database.py      # DB connection
└── routers/
    └── tasks.py     # Task endpoints
```

**Comparison with Flask:**
| Flask | FastAPI |
|-------|----------|
| `@app.route('/tasks', methods=['GET'])` | `@app.get('/tasks')` |
| `request.get_json()` | `def create(task: TaskCreate)` |
| Manual validation | Automatic via Pydantic |
| No built-in docs | Swagger at /docs |