---
type: "THEORY"
title: "What is Dependency Injection?"
---

**Dependency Injection (DI)** is a design pattern where objects receive their dependencies from external sources rather than creating them internally.

**Why FastAPI Uses Dependency Injection:**

1. **Reusability** - Write once, use in many endpoints
2. **Testing** - Easy to swap real dependencies with mocks
3. **Separation of Concerns** - Each function does one thing well
4. **Clean Code** - No repetitive setup code in every endpoint

**The Depends() Function:**

FastAPI's `Depends()` is the key to DI:

```python
from fastapi import Depends

def get_database():
    return DatabaseConnection()

@app.get('/items')
def read_items(db = Depends(get_database)):
    return db.get_all_items()
```

**How It Works:**
1. FastAPI sees `Depends(get_database)`
2. Before calling your endpoint, it calls `get_database()`
3. The return value is injected as `db`
4. Your endpoint uses `db` without knowing how it was created

**Common Use Cases:**
- Database connections
- Authentication/Authorization
- Configuration/Settings
- Logging
- Rate limiting
- Pagination parameters