---
type: "KEY_POINT"
title: "Dependency Injection Key Takeaways"
---

**Core Concepts:**
- **Depends()** - FastAPI's DI mechanism
- **Function dependencies** - Return values are injected
- **Yield dependencies** - For cleanup (db connections, files)
- **Chained dependencies** - Dependencies can use other dependencies
- **Class dependencies** - For configurable, stateful dependencies

**Patterns Summary:**
```python
# Simple function
def get_db():
    return Database()

# With cleanup
def get_db():
    db = Database()
    try:
        yield db
    finally:
        db.close()

# Chained
def get_user(db = Depends(get_db)):
    return db.get_user()

# Class-based
class Pagination:
    def __init__(self, skip: int = 0, limit: int = 10):
        self.skip = skip
        self.limit = limit
```

**Best Practices:**
- Keep dependencies focused on one task
- Use `yield` for resources needing cleanup
- Chain dependencies for authentication flows
- Use classes for configurable dependencies
- Dependencies are great for cross-cutting concerns