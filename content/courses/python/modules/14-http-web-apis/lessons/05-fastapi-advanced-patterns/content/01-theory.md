---
type: "THEORY"
title: "Dependency Injection"
---

**What is Dependency Injection?**

Instead of creating dependencies inside functions, you *inject* them:

```python
from fastapi import Depends

# Dependency function
def get_db():
    db = DatabaseSession()
    try:
        yield db  # Provide to endpoint
    finally:
        db.close()  # Cleanup after

# Use the dependency
@app.get("/users/")
def get_users(db: Session = Depends(get_db)):
    return db.query(User).all()
```

**Benefits:**
- Reusable logic (auth, db, logging)
- Automatic cleanup
- Easy testing (swap dependencies)
- Clear separation of concerns