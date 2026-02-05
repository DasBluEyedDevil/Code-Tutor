---
type: "KEY_POINT"
title: "FastAPI is Built on Async - Your M13 Skills Transfer Directly"
---

**Core Takeaway:**

Everything you learned about `async`/`await` in Module 13 applies directly to FastAPI web development:

✅ `async def` functions — used for route handlers  
✅ `await` — used for database queries, HTTP calls, file I/O  
✅ `async with` — used for database connections and HTTP clients  
✅ `asyncio.gather()` — used for parallel operations  
✅ `TaskGroup` — used for structured concurrent operations  

**What You'll Learn in M14:**

| Concept | Purpose |
|---------|---------|
| HTTP Methods | GET, POST, PUT, DELETE for different operations |
| Pydantic Models | Type-safe request/response validation |
| Dependency Injection | Clean, testable code organization |
| OpenAPI | Automatic API documentation |

**The Mental Model:**

Think of FastAPI as "async Python with HTTP routing added on top." The async foundation you built in M13 is exactly what you need.

**Preview: Your First Real API Pattern**

```python
from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

# Pydantic model = type-safe data
class User(BaseModel):
    name: str
    email: str

@app.post("/users")
async def create_user(user: User):
    # async + type safety = powerful combination
    await save_to_database(user)
    return {"created": True}
```

**Ready for Module 14?** You already have the hardest part—the async mindset.
