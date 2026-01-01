from sqlalchemy.ext.asyncio import create_async_engine, AsyncSession, async_sessionmaker
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from sqlalchemy import ForeignKey, String, select
from sqlalchemy.orm import selectinload
from fastapi import FastAPI, Depends, HTTPException
from pydantic import BaseModel
from datetime import datetime
from typing import List

# Database setup
DATABASE_URL = "sqlite+aiosqlite:///./tasks.db"

# TODO: Create engine and async_session

# TODO: Create Base class

# TODO: Create Project model
# - id: primary key
# - name: required string
# - description: optional string
# - created_at: datetime with default
# - tasks: relationship to Task

# TODO: Create Task model
# - id: primary key
# - title: required string
# - completed: bool, default False
# - project_id: foreign key
# - created_at: datetime with default
# - project: relationship back to Project

# Pydantic Schemas
class ProjectCreate(BaseModel):
    name: str
    description: str | None = None

class TaskCreate(BaseModel):
    title: str

class TaskResponse(BaseModel):
    id: int
    title: str
    completed: bool
    
    class Config:
        from_attributes = True

class ProjectResponse(BaseModel):
    id: int
    name: str
    description: str | None
    tasks: List[TaskResponse]
    
    class Config:
        from_attributes = True

class ProjectSummary(BaseModel):
    id: int
    name: str
    task_count: int
    
    class Config:
        from_attributes = True

# TODO: Create get_db dependency

app = FastAPI()

# TODO: Create startup event to initialize database

# TODO: POST /projects/ - Create project

# TODO: GET /projects/{project_id} - Get project with tasks

# TODO: POST /projects/{project_id}/tasks/ - Add task to project

# TODO: PUT /tasks/{task_id}/complete - Mark task completed

# TODO: GET /projects/ - List all projects with task counts