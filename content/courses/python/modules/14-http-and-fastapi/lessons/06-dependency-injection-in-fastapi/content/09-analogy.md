---
type: "ANALOGY"
title: "Dependency Injection as a Butler"
---

**Understanding DI Through a Butler Service**

Imagine you're a guest at a grand estate. Instead of getting things yourself, you have a butler.

**Without DI (No Butler):**

```python
@app.get("/dinner")
def serve_dinner():
    # You do everything yourself
    db = DatabaseConnection(host="...", port=5432)
    db.connect()
    food = db.query("SELECT * FROM dinner")
    db.close()
    return food
```

Every time you want dinner, you:
1. Go to the kitchen yourself
2. Find the ingredients
3. Cook the food
4. Clean up after

**With DI (Butler Service):**

```python
def get_database():
    db = DatabaseConnection(host="...", port=5432)
    db.connect()
    try:
        yield db  # Butler brings the connection
    finally:
        db.close()  # Butler cleans up

@app.get("/dinner")
def serve_dinner(db = Depends(get_database)):
    # Butler brings everything you need!
    return db.query("SELECT * FROM dinner")
```

**The Butler (DI) Does:**

| Task | Without Butler | With Butler |
|------|---------------|-------------|
| Get database connection | Write connection code | `Depends(get_database)` |
| Check authentication | Manually verify token | `Depends(get_current_user)` |
| Load settings | Read env vars | `Depends(get_settings)` |
| Clean up | Remember to close | Automatic via `yield` |

**The Key Insight:**

Dependency injection is like having a butler who:
- **Prepares** what you need before you ask
- **Delivers** it when you need it
- **Cleans up** when you're done

You focus on the dinner (business logic). The butler handles the setup and cleanup (dependencies).
