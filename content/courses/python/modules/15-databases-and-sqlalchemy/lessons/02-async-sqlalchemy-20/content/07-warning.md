---
type: "WARNING"
title: "Async SQLAlchemy Gotchas"
---

**Async-Specific Pitfalls:**

❌ **Don't mix sync and async APIs**

```python
# WRONG: Using sync session in async code
async def get_user(user_id: int):
    with Session() as session:  # Sync session blocks!
        return session.get(User, user_id)

# RIGHT: Use async session
from sqlalchemy.ext.asyncio import async_sessionmaker

async def get_user(user_id: int):
    async with AsyncSession() as session:
        return await session.get(User, user_id)
```

❌ **Don't forget await on session operations**

```python
# WRONG: Missing await
async def create_user(name: str):
    async with AsyncSession() as session:
        user = User(name=name)
        session.add(user)
        session.commit()  # Missing await!

# RIGHT: Await all async operations
async def create_user(name: str):
    async with AsyncSession() as session:
        user = User(name=name)
        session.add(user)
        await session.commit()  # ✓
```

❌ **Don't use lazy loading with async**

```python
# WRONG: Lazy loading doesn't work async
async def get_user_posts():
    async with AsyncSession() as session:
        user = await session.get(User, 1)
        return user.posts  # MissingGreenlet error!

# RIGHT: Always eager load
from sqlalchemy.orm import selectinload

async def get_user_posts():
    async with AsyncSession() as session:
        stmt = select(User).options(selectinload(User.posts)).where(User.id == 1)
        user = await session.scalar(stmt)
        return user.posts  # Already loaded
```

❌ **Don't create engine at import time**

```python
# WRONG: Engine created at import (blocks startup)
engine = create_async_engine("postgresql+asyncpg://...")
AsyncSession = async_sessionmaker(engine)

# RIGHT: Create engine in async context
async def init_db():
    global engine, AsyncSession
    engine = create_async_engine("postgresql+asyncpg://...")
    AsyncSession = async_sessionmaker(engine)
    
# Or use dependency injection
async def get_session():
    async with AsyncSession() as session:
        yield session
```

**Remember:** Async SQLAlchemy requires async drivers:
- SQLite: `aiosqlite`
- PostgreSQL: `asyncpg`
- MySQL: `asyncmy`
