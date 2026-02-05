---
type: "WARNING"
title: "FastAPI Routes Common Mistakes"
---

**Routing Pitfalls to Avoid:**

❌ **Don't put specific routes after generic ones**

```python
# WRONG: /users/me never matches!
@app.get("/users/{user_id}")
def get_user(user_id: str):
    ...

@app.get("/users/me")  # Never reached - "me" matches {user_id}
def get_current_user():
    ...

# RIGHT: Specific routes first
@app.get("/users/me")  # Matches first
def get_current_user():
    ...

@app.get("/users/{user_id}")  # Then parameterized
def get_user(user_id: str):
    ...
```

❌ **Don't forget path parameter types**

```python
# WRONG: user_id is a string by default
@app.get("/users/{user_id}")
def get_user(user_id):  # "abc" is valid!
    db.get_user(user_id)  # Crash!

# RIGHT: Type hint for validation
@app.get("/users/{user_id}")
def get_user(user_id: int):  # Only integers
    db.get_user(user_id)
```

❌ **Don't confuse path and query parameters**

```python
# Path parameter: /users/42
@app.get("/users/{user_id}")
def get_user(user_id: int):  # In the URL path
    ...

# Query parameter: /users?skip=0&limit=10
@app.get("/users")
def list_users(skip: int = 0, limit: int = 10):  # After ?
    ...
```

❌ **Don't use wrong HTTP methods**

| Method | Use For |
|--------|---------|
| GET | Read data (no side effects) |
| POST | Create new resource |
| PUT | Update entire resource |
| PATCH | Update part of resource |
| DELETE | Remove resource |

```python
# WRONG: POST for reading
@app.post("/users")
def get_users():
    return db.get_all_users()

# RIGHT: GET for reading
@app.get("/users")
def get_users():
    return db.get_all_users()
```
