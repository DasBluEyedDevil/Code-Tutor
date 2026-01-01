from fastapi import FastAPI, Query
from pydantic import BaseModel
from typing import List, Optional

app = FastAPI()

# Sample data
tasks = [
    {"id": 1, "title": "Learn Python", "description": "Complete the basics"},
    {"id": 2, "title": "Build API", "description": "Create REST endpoints"},
    {"id": 3, "title": "Write tests", "description": "Add pytest tests"},
]

class TaskResponse(BaseModel):
    id: int
    title: str
    description: Optional[str]

# TODO: Add search endpoint
# GET /tasks/search?q=python
# Should search in both title and description (case-insensitive)
# Return matching tasks

@app.get("/tasks/search", response_model=List[TaskResponse])
def search_tasks(q: str = Query(min_length=1)):
    # Your code here
    pass

print("Search endpoint added!")