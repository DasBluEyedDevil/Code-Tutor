---
type: "EXAMPLE"
title: "Setting Up Async Engine"
---

**Creating an async database engine for FastAPI:**

The async engine and session maker are the foundation of async SQLAlchemy.

**Key Components:**
- `create_async_engine()` - Creates the async database engine
- `async_sessionmaker` - Factory for creating async sessions
- `AsyncSession` - The async session type for type hints
- `get_db()` - Dependency that yields sessions with cleanup

**Important Settings:**
- `echo=True` - Log all SQL queries (useful for development)
- `expire_on_commit=False` - Keep object attributes after commit

```python
from sqlalchemy.ext.asyncio import (
    create_async_engine,
    AsyncSession,
    async_sessionmaker
)
from fastapi import FastAPI, Depends

# For development with SQLite (requires: pip install aiosqlite)
DATABASE_URL = "sqlite+aiosqlite:///./finance.db"

# For production with PostgreSQL (requires: pip install asyncpg)
# DATABASE_URL = "postgresql+asyncpg://user:pass@localhost/finance"

# Create the async engine
engine = create_async_engine(
    DATABASE_URL,
    echo=True  # Log SQL queries (disable in production)
)

# Create async session factory
async_session = async_sessionmaker(
    engine,
    expire_on_commit=False  # Keep attributes accessible after commit
)

# Dependency for FastAPI
async def get_db():
    """Yield a database session with automatic cleanup."""
    async with async_session() as session:
        yield session

# Initialize database tables (call on startup)
async def init_db():
    """Create all tables."""
    async with engine.begin() as conn:
        # Import your Base here
        # await conn.run_sync(Base.metadata.create_all)
        pass

app = FastAPI()

@app.on_event("startup")
async def startup():
    await init_db()

# Demonstration
print("=== Async SQLAlchemy Engine Setup ===")
print(f"\nDatabase URL: {DATABASE_URL}")
print("\nKey components:")
print("  1. create_async_engine() - Async connection pool")
print("  2. async_sessionmaker - Creates AsyncSession instances")
print("  3. get_db() - FastAPI dependency with yield")
print("\nUsage in endpoint:")
print("  @app.get('/users')")
print("  async def get_users(db: AsyncSession = Depends(get_db)):")
print("      result = await db.execute(select(User))")
print("      return result.scalars().all()")
```
