---
type: "EXAMPLE"
title: "SQLite with aiosqlite"
---

**Async SQLite for FastAPI:**

FastAPI is async by default, so we need async database access. The `aiosqlite` package provides async support for SQLite.

**Installation:**
```bash
# Using uv (recommended)
uv add aiosqlite

# Or using pip
pip install aiosqlite
```

**Key Concepts:**

1. **Async Engine Creation:**
```python
from sqlalchemy.ext.asyncio import create_async_engine

engine = create_async_engine(
    "sqlite+aiosqlite:///./app.db",
    echo=True  # Log SQL statements
)
```

2. **SQLite-specific Connection Args:**
```python
connect_args={"check_same_thread": False}
```
This is required because SQLite by default only allows access from the creating thread.

3. **The URL Format:**
- `sqlite+aiosqlite:///./app.db` - Relative path
- `sqlite+aiosqlite:////absolute/path/app.db` - Absolute path
- `sqlite+aiosqlite:///:memory:` - In-memory database

```python
# For async FastAPI with SQLite
# Install: uv add aiosqlite

from sqlalchemy.ext.asyncio import create_async_engine

# SQLite URL for async
DATABASE_URL = "sqlite+aiosqlite:///./app.db"

engine = create_async_engine(
    DATABASE_URL,
    echo=True,  # Log SQL for debugging
    connect_args={"check_same_thread": False}  # Required for SQLite
)

print("=== SQLite + aiosqlite Configuration ===")
print(f"Database URL: {DATABASE_URL}")
print("")
print("Key points:")
print("  - 'sqlite+aiosqlite' = async SQLite driver")
print("  - '///./app.db' = relative path (3 slashes)")
print("  - 'echo=True' = log all SQL (great for debugging)")
print("  - 'check_same_thread=False' = allow multi-thread access")
print("")
print("Alternative URLs:")
print("  sqlite+aiosqlite:///:memory:      # In-memory (tests)")
print("  sqlite+aiosqlite:////tmp/app.db   # Absolute path")
```
