from typing import List, Optional, Dict
from dataclasses import dataclass
from datetime import datetime

# TODO: Define Task model
# Fields: id, title, description, completed, user_id, created_at

# TODO: Define TaskRepository
# Methods: create, find_by_id, find_by_user, list_all, update_status

# TODO: Define TaskService  
# Methods: create_task, get_user_tasks, mark_completed
# Include validation (title min 3 chars, description min 5 chars)

# Test your implementation
repo = TaskRepository()
service = TaskService(repo)

# Create tasks
result = service.create_task(
    title="Learn Python",
    description="Complete Module 14",
    user_id=1
)
print(result)

# List tasks
tasks = service.get_user_tasks(1)
for task in tasks:
    print(f"- {task['title']}")