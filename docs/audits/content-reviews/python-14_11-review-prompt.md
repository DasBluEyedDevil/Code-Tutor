# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** HTTP & Web APIs
- **Lesson:** Mini-Project: FastAPI CRUD API (ID: 14_11)
- **Difficulty:** advanced
- **Estimated Time:** 50 minutes

## Current Lesson Content

{
    "id":  "14_11",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Building a Complete API",
                                "content":  "**Project: Task Management API**\n\nWe\u0027ll build the same API as the Flask project, but with FastAPI:\n\n**Features:**\n- Create, read, update, delete tasks\n- Filter by status (pending/completed)\n- Pagination\n- Input validation\n- Auto-generated docs\n\n**Structure:**\n```\napp/\n├── main.py          # FastAPI app\n├── models.py        # Pydantic models\n├── database.py      # DB connection\n└── routers/\n    └── tasks.py     # Task endpoints\n```\n\n**Comparison with Flask:**\n| Flask | FastAPI |\n|-------|----------|\n| `@app.route(\u0027/tasks\u0027, methods=[\u0027GET\u0027])` | `@app.get(\u0027/tasks\u0027)` |\n| `request.get_json()` | `def create(task: TaskCreate)` |\n| Manual validation | Automatic via Pydantic |\n| No built-in docs | Swagger at /docs |"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Complete Task API",
                                "content":  "A full CRUD API with FastAPI including all endpoints, models, and proper error handling.",
                                "code":  "from fastapi import FastAPI, HTTPException, Query\nfrom pydantic import BaseModel, Field\nfrom typing import List, Optional\nfrom datetime import datetime\nfrom enum import Enum\n\napp = FastAPI(\n    title=\"Task Management API\",\n    description=\"A complete CRUD API built with FastAPI\",\n    version=\"1.0.0\"\n)\n\n# Enums\nclass TaskStatus(str, Enum):\n    PENDING = \"pending\"\n    IN_PROGRESS = \"in_progress\"\n    COMPLETED = \"completed\"\n\n# Models\nclass TaskCreate(BaseModel):\n    title: str = Field(min_length=1, max_length=200)\n    description: Optional[str] = Field(default=None, max_length=1000)\n    status: TaskStatus = TaskStatus.PENDING\n\nclass TaskUpdate(BaseModel):\n    title: Optional[str] = Field(default=None, min_length=1, max_length=200)\n    description: Optional[str] = None\n    status: Optional[TaskStatus] = None\n\nclass TaskResponse(BaseModel):\n    id: int\n    title: str\n    description: Optional[str]\n    status: TaskStatus\n    created_at: datetime\n    updated_at: datetime\n\n# In-memory database\ntasks_db = {}\nnext_id = 1\n\n# Endpoints\n@app.get(\"/tasks\", response_model=List[TaskResponse])\ndef list_tasks(\n    status: Optional[TaskStatus] = None,\n    skip: int = Query(default=0, ge=0),\n    limit: int = Query(default=10, ge=1, le=100)\n):\n    \"\"\"List all tasks with optional filtering and pagination.\"\"\"\n    result = list(tasks_db.values())\n    \n    if status:\n        result = [t for t in result if t[\"status\"] == status]\n    \n    return result[skip:skip + limit]\n\n@app.get(\"/tasks/{task_id}\", response_model=TaskResponse)\ndef get_task(task_id: int):\n    \"\"\"Get a single task by ID.\"\"\"\n    if task_id not in tasks_db:\n        raise HTTPException(status_code=404, detail=\"Task not found\")\n    return tasks_db[task_id]\n\n@app.post(\"/tasks\", response_model=TaskResponse, status_code=201)\ndef create_task(task: TaskCreate):\n    \"\"\"Create a new task.\"\"\"\n    global next_id\n    now = datetime.now()\n    new_task = {\n        \"id\": next_id,\n        \"title\": task.title,\n        \"description\": task.description,\n        \"status\": task.status,\n        \"created_at\": now,\n        \"updated_at\": now\n    }\n    tasks_db[next_id] = new_task\n    next_id += 1\n    return new_task\n\n@app.put(\"/tasks/{task_id}\", response_model=TaskResponse)\ndef update_task(task_id: int, task: TaskUpdate):\n    \"\"\"Update an existing task.\"\"\"\n    if task_id not in tasks_db:\n        raise HTTPException(status_code=404, detail=\"Task not found\")\n    \n    existing = tasks_db[task_id]\n    update_data = task.model_dump(exclude_unset=True)\n    \n    for field, value in update_data.items():\n        existing[field] = value\n    \n    existing[\"updated_at\"] = datetime.now()\n    return existing\n\n@app.delete(\"/tasks/{task_id}\", status_code=204)\ndef delete_task(task_id: int):\n    \"\"\"Delete a task.\"\"\"\n    if task_id not in tasks_db:\n        raise HTTPException(status_code=404, detail=\"Task not found\")\n    del tasks_db[task_id]\n\n@app.get(\"/tasks/stats/summary\")\ndef get_stats():\n    \"\"\"Get task statistics.\"\"\"\n    all_tasks = list(tasks_db.values())\n    return {\n        \"total\": len(all_tasks),\n        \"pending\": len([t for t in all_tasks if t[\"status\"] == TaskStatus.PENDING]),\n        \"in_progress\": len([t for t in all_tasks if t[\"status\"] == TaskStatus.IN_PROGRESS]),\n        \"completed\": len([t for t in all_tasks if t[\"status\"] == TaskStatus.COMPLETED])\n    }\n\nprint(\"Task Management API\")\nprint(\"Run: uvicorn main:app --reload\")\nprint(\"Docs: http://localhost:8000/docs\")",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "FastAPI vs Flask Summary",
                                "content":  "**What we gained with FastAPI:**\n\n1. **Automatic validation** - No manual checking\n2. **Type safety** - IDE catches errors\n3. **Auto documentation** - /docs always current\n4. **Cleaner code** - Less boilerplate\n5. **Better performance** - Async support\n\n**When to migrate:**\n- New project → Use FastAPI\n- Existing Flask → Keep if working\n- Need async → FastAPI\n- Need auto-docs → FastAPI\n\n**Learning path:**\n1. Flask basics (done!)\n2. FastAPI basics (this lesson)\n3. SQLAlchemy + FastAPI\n4. Authentication patterns\n5. Testing with pytest"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "14_11-challenge",
                           "title":  "Add Search to Task API",
                           "description":  "Extend the Task API with search functionality.",
                           "instructions":  "Add a search endpoint that finds tasks by title or description containing a search term.",
                           "starterCode":  "from fastapi import FastAPI, Query\nfrom pydantic import BaseModel\nfrom typing import List, Optional\n\napp = FastAPI()\n\n# Sample data\ntasks = [\n    {\"id\": 1, \"title\": \"Learn Python\", \"description\": \"Complete the basics\"},\n    {\"id\": 2, \"title\": \"Build API\", \"description\": \"Create REST endpoints\"},\n    {\"id\": 3, \"title\": \"Write tests\", \"description\": \"Add pytest tests\"},\n]\n\nclass TaskResponse(BaseModel):\n    id: int\n    title: str\n    description: Optional[str]\n\n# TODO: Add search endpoint\n# GET /tasks/search?q=python\n# Should search in both title and description (case-insensitive)\n# Return matching tasks\n\n@app.get(\"/tasks/search\", response_model=List[TaskResponse])\ndef search_tasks(q: str = Query(min_length=1)):\n    # Your code here\n    pass\n\nprint(\"Search endpoint added!\")",
                           "solution":  "from fastapi import FastAPI, Query\nfrom pydantic import BaseModel\nfrom typing import List, Optional\n\napp = FastAPI()\n\ntasks = [\n    {\"id\": 1, \"title\": \"Learn Python\", \"description\": \"Complete the basics\"},\n    {\"id\": 2, \"title\": \"Build API\", \"description\": \"Create REST endpoints\"},\n    {\"id\": 3, \"title\": \"Write tests\", \"description\": \"Add pytest tests\"},\n    {\"id\": 4, \"title\": \"Deploy app\", \"description\": \"Push to production\"},\n]\n\nclass TaskResponse(BaseModel):\n    id: int\n    title: str\n    description: Optional[str]\n\n@app.get(\"/tasks/search\", response_model=List[TaskResponse])\ndef search_tasks(\n    q: str = Query(min_length=1, description=\"Search term\"),\n    limit: int = Query(default=10, ge=1, le=50)\n):\n    \"\"\"Search tasks by title or description.\"\"\"\n    query = q.lower()\n    results = []\n    \n    for task in tasks:\n        title_match = query in task[\"title\"].lower()\n        desc_match = task[\"description\"] and query in task[\"description\"].lower()\n        \n        if title_match or desc_match:\n            results.append(task)\n    \n    return results[:limit]\n\n# Test the search\nprint(\"Search API ready!\")\nprint(\"Example: /tasks/search?q=python\")\nprint(\"Example: /tasks/search?q=test\u0026limit=5\")",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Search finds matching tasks",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use .lower() for case-insensitive matching. Check if query is in title or description."
                                         }
                                     ],
                           "commonMistakes":  [

                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Mini-Project: FastAPI CRUD API",
    "estimatedMinutes":  50
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
- Search for "python Mini-Project: FastAPI CRUD API 2024 2025" to find latest practices
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
  "lessonId": "14_11",
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

