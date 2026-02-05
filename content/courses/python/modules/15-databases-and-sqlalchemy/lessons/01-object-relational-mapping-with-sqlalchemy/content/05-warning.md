---
type: "WARNING"
title: "Common ORM Pitfalls"
---

**SQLAlchemy Mistakes to Avoid:**

❌ **Don't use `session.query()` in SQLAlchemy 2.0**

```python
# OLD (SQLAlchemy 1.x - deprecated)
users = session.query(User).filter(User.active == True).all()

# NEW (SQLAlchemy 2.0)
from sqlalchemy import select

stmt = select(User).where(User.active == True)
users = session.scalars(stmt).all()
```

❌ **Don't forget session management**

```python
# WRONG: Session never committed or rolled back
user = User(name="Alice")
session.add(user)
# Forgot to commit! Changes lost!

# RIGHT: Always handle the session
try:
    user = User(name="Alice")
    session.add(user)
    session.commit()
except Exception:
    session.rollback()
    raise
finally:
    session.close()

# BEST: Use context manager
from contextlib import contextmanager

@contextmanager
def get_session():
    session = Session()
    try:
        yield session
        session.commit()
    except Exception:
        session.rollback()
        raise
    finally:
        session.close()
```

❌ **Don't let lazy loading hurt performance**

```python
# SLOW: N+1 query problem
users = session.scalars(select(User)).all()
for user in users:
    print(user.posts)  # Each access = separate query!

# FAST: Eager load relationships
from sqlalchemy.orm import selectinload

stmt = select(User).options(selectinload(User.posts))
users = session.scalars(stmt).all()
# All data loaded in 2 queries
```

❌ **Don't ignore detached instance errors**

```python
# WRONG: Using object after session closes
with get_session() as session:
    user = session.get(User, 1)

print(user.posts)  # DetachedInstanceError!

# RIGHT: Load data while session is open
with get_session() as session:
    user = session.get(User, 1, options=[selectinload(User.posts)])
    posts = list(user.posts)  # Loaded while session open
print(posts)  # Safe to use
```
