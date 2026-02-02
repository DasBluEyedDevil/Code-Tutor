---
type: "THEORY"
title: "Path and Query Parameters"
---

**Path parameters** - part of the URL:
```python
@app.get("/users/{user_id}")
def get_user(user_id: int):  # Automatically validated as int
    return {"user_id": user_id}
```

**Query parameters** - after the ?:
```python
@app.get("/items/")
def list_items(
    skip: int = 0,
    limit: int = 10,
    search: str = None
):
    # /items/?skip=10&limit=5&search=laptop
    return {"skip": skip, "limit": limit, "search": search}
```

**Combining both:**
```python
@app.get("/users/{user_id}/items/")
def get_user_items(
    user_id: int,  # Path param
    skip: int = 0,  # Query param
    limit: int = 10  # Query param
):
    pass
```