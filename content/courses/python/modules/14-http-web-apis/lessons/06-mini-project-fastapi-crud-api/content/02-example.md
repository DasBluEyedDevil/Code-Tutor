---
type: "EXAMPLE"
title: "Complete Task API"
---

A full CRUD API with FastAPI including all endpoints, models, and proper error handling.

```python
from fastapi import FastAPI, HTTPException, Query
from pydantic import BaseModel, Field
from typing import List, Optional
from datetime import datetime
from enum import Enum

app = FastAPI(
    title="Task Management API",
    description="A complete CRUD API built with FastAPI",
    version="1.0.0"
)

# Enums
class TaskStatus(str, Enum):
    PENDING = "pending"
    IN_PROGRESS = "in_progress"
    COMPLETED = "completed"

# Models
class TaskCreate(BaseModel):
    title: str = Field(min_length=1, max_length=200)
    description: Optional[str] = Field(default=None, max_length=1000)
    status: TaskStatus = TaskStatus.PENDING

class TaskUpdate(BaseModel):
    title: Optional[str] = Field(default=None, min_length=1, max_length=200)
    description: Optional[str] = None
    status: Optional[TaskStatus] = None

class TaskResponse(BaseModel):
    id: int
    title: str
    description: Optional[str]
    status: TaskStatus
    created_at: datetime
    updated_at: datetime

# In-memory database
tasks_db = {}
next_id = 1

# Endpoints
@app.get("/tasks", response_model=List[TaskResponse])
def list_tasks(
    status: Optional[TaskStatus] = None,
    skip: int = Query(default=0, ge=0),
    limit: int = Query(default=10, ge=1, le=100)
):
    """List all tasks with optional filtering and pagination."""
    result = list(tasks_db.values())
    
    if status:
        result = [t for t in result if t["status"] == status]
    
    return result[skip:skip + limit]

@app.get("/tasks/{task_id}", response_model=TaskResponse)
def get_task(task_id: int):
    """Get a single task by ID."""
    if task_id not in tasks_db:
        raise HTTPException(status_code=404, detail="Task not found")
    return tasks_db[task_id]

@app.post("/tasks", response_model=TaskResponse, status_code=201)
def create_task(task: TaskCreate):
    """Create a new task."""
    global next_id
    now = datetime.now()
    new_task = {
        "id": next_id,
        "title": task.title,
        "description": task.description,
        "status": task.status,
        "created_at": now,
        "updated_at": now
    }
    tasks_db[next_id] = new_task
    next_id += 1
    return new_task

@app.put("/tasks/{task_id}", response_model=TaskResponse)
def update_task(task_id: int, task: TaskUpdate):
    """Update an existing task."""
    if task_id not in tasks_db:
        raise HTTPException(status_code=404, detail="Task not found")
    
    existing = tasks_db[task_id]
    update_data = task.model_dump(exclude_unset=True)
    
    for field, value in update_data.items():
        existing[field] = value
    
    existing["updated_at"] = datetime.now()
    return existing

@app.delete("/tasks/{task_id}", status_code=204)
def delete_task(task_id: int):
    """Delete a task."""
    if task_id not in tasks_db:
        raise HTTPException(status_code=404, detail="Task not found")
    del tasks_db[task_id]

@app.get("/tasks/stats/summary")
def get_stats():
    """Get task statistics."""
    all_tasks = list(tasks_db.values())
    return {
        "total": len(all_tasks),
        "pending": len([t for t in all_tasks if t["status"] == TaskStatus.PENDING]),
        "in_progress": len([t for t in all_tasks if t["status"] == TaskStatus.IN_PROGRESS]),
        "completed": len([t for t in all_tasks if t["status"] == TaskStatus.COMPLETED])
    }

print("Task Management API")
print("Run: uvicorn main:app --reload")
print("Docs: http://localhost:8000/docs")
```
