---
type: "EXAMPLE"
title: "Development Database Setup"
---

**Complete async database setup for FastAPI:**

This pattern sets up:
1. Async engine for SQLite
2. Async session factory
3. Database initialization function
4. FastAPI dependency for sessions

**Project Structure:**
```
project/
├── app/
│   ├── database.py    # This file
│   ├── models.py      # SQLAlchemy models
│   └── main.py        # FastAPI app
└── dev.db             # Created automatically
```

```python
# database.py - Complete async SQLite setup
from sqlalchemy.ext.asyncio import (
    create_async_engine,
    async_sessionmaker,
    AsyncSession
)
from sqlalchemy.orm import declarative_base

# Database URL - file in current directory
DATABASE_URL = "sqlite+aiosqlite:///./dev.db"

# Create async engine
engine = create_async_engine(
    DATABASE_URL,
    echo=True,  # Set to False in production
    connect_args={"check_same_thread": False}
)

# Create async session factory
async_session = async_sessionmaker(
    engine,
    expire_on_commit=False,  # Keep objects usable after commit
    class_=AsyncSession
)

# Base class for models
Base = declarative_base()

async def init_db():
    """Create all tables from models.
    
    Call this at app startup:
        @app.on_event("startup")
        async def startup():
            await init_db()
    """
    async with engine.begin() as conn:
        # Import models here to ensure they're registered
        # from app.models import User, Post  # noqa
        await conn.run_sync(Base.metadata.create_all)

async def get_db():
    """FastAPI dependency for database sessions.
    
    Usage:
        @app.get("/users")
        async def get_users(db: AsyncSession = Depends(get_db)):
            result = await db.execute(select(User))
            return result.scalars().all()
    """
    async with async_session() as session:
        try:
            yield session
            await session.commit()
        except Exception:
            await session.rollback()
            raise
        finally:
            await session.close()

# Demonstration
print("=== Development Database Setup ===")
print("")
print("Files created:")
print("  database.py - This configuration")
print("  dev.db      - SQLite database file (auto-created)")
print("")
print("Usage in FastAPI:")
print('''
from fastapi import FastAPI, Depends
from sqlalchemy.ext.asyncio import AsyncSession
from app.database import get_db, init_db

app = FastAPI()

@app.on_event("startup")
async def startup():
    await init_db()

@app.get("/items")
async def get_items(db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Item))
    return result.scalars().all()
''')
```
