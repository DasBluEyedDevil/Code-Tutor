from fastapi import FastAPI, Query
from pydantic import BaseModel
from typing import List, Optional

app = FastAPI()

tasks = [
    {"id": 1, "title": "Learn Python", "description": "Complete the basics"},
    {"id": 2, "title": "Build API", "description": "Create REST endpoints"},
    {"id": 3, "title": "Write tests", "description": "Add pytest tests"},
    {"id": 4, "title": "Deploy app", "description": "Push to production"},
]

class TaskResponse(BaseModel):
    id: int
    title: str
    description: Optional[str]

@app.get("/tasks/search", response_model=List[TaskResponse])
def search_tasks(
    q: str = Query(min_length=1, description="Search term"),
    limit: int = Query(default=10, ge=1, le=50)
):
    """Search tasks by title or description."""
    query = q.lower()
    results = []
    
    for task in tasks:
        title_match = query in task["title"].lower()
        desc_match = task["description"] and query in task["description"].lower()
        
        if title_match or desc_match:
            results.append(task)
    
    return results[:limit]

# Test the search
print("Search API ready!")
print("Example: /tasks/search?q=python")
print("Example: /tasks/search?q=test&limit=5")