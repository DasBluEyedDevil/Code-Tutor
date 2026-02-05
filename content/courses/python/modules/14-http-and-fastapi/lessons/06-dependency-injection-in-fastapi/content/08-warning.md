---
type: "WARNING"
title: "Dependency Injection Pitfalls"
---

**Common DI Mistakes in FastAPI:**

❌ **Don't forget to use `Depends()` wrapper**

```python
# WRONG: Function called immediately, not injected
@app.get("/items")
def get_items(db = get_database()):  # Called once at startup!
    ...

# RIGHT: Wrapped in Depends()
@app.get("/items")
def get_items(db = Depends(get_database)):  # Called per request
    ...
```

❌ **Don't create dependencies that never close**

```python
# WRONG: Connection never closed
def get_db():
    db = DatabaseConnection()
    return db  # Leaks connection!

# RIGHT: Use yield for cleanup
def get_db():
    db = DatabaseConnection()
    try:
        yield db
    finally:
        db.close()
```

❌ **Don't nest dependencies too deeply**

```python
# HARD TO DEBUG
def get_setting():
    return Settings()

def get_db(settings = Depends(get_setting)):
    return Database(settings)

def get_user_repo(db = Depends(get_db)):
    return UserRepo(db)

def get_auth(repo = Depends(get_user_repo)):
    return AuthService(repo)

# 4 levels deep - hard to trace issues!
```

❌ **Don't forget dependency caching**

```python
# Dependencies are called ONCE per request by default
# If you need fresh instances:

from fastapi import Depends

def get_timestamp():
    return datetime.now()

@app.get("/test")
def test(
    t1 = Depends(get_timestamp),
    t2 = Depends(get_timestamp)  # Same as t1!
):
    print(t1 == t2)  # True

# For fresh instances:
@app.get("/test")
def test(
    t1 = Depends(get_timestamp, use_cache=False),
    t2 = Depends(get_timestamp, use_cache=False)  # Different!
):
    print(t1 == t2)  # False
```

**Best Practice:**
Keep dependencies simple, single-purpose, and always clean up resources with `yield` + `try/finally`.
