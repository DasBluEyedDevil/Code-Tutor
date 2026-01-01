---
type: "EXAMPLE"
title: "Connecting with asyncpg"
---

**asyncpg: The Fastest PostgreSQL Driver for Python**

For async FastAPI applications, `asyncpg` provides excellent performance.

**Installation:**
```bash
# Using uv (recommended)
uv add asyncpg

# Or using pip
pip install asyncpg
```

**Connection URL Format:**
```
postgresql+asyncpg://username:password@host:port/database
```

**Key Configuration Options:**
- `pool_size`: Number of connections to keep open (default: 5)
- `max_overflow`: Extra connections allowed above pool_size (default: 10)
- `pool_timeout`: Seconds to wait for a connection (default: 30)
- `echo`: Log all SQL statements (useful for debugging)

```python
# PostgreSQL connection with asyncpg
# Install: uv add asyncpg

from sqlalchemy.ext.asyncio import create_async_engine, async_sessionmaker

# PostgreSQL URL format
# postgresql+asyncpg://username:password@host:port/database
DATABASE_URL = "postgresql+asyncpg://myuser:secret@localhost:5432/mydb"

# Create async engine with connection pooling
engine = create_async_engine(
    DATABASE_URL,
    pool_size=10,        # Keep 10 connections open
    max_overflow=20,     # Allow 20 extra connections if needed
    pool_timeout=30,     # Wait 30 seconds for connection
    echo=True            # Log SQL statements (disable in production)
)

# Create async session factory
async_session = async_sessionmaker(
    engine,
    expire_on_commit=False
)

print("=== PostgreSQL + asyncpg Configuration ===")
print(f"")
print(f"Database URL format:")
print(f"  postgresql+asyncpg://user:pass@host:5432/db")
print(f"")
print(f"Connection Pool Settings:")
print(f"  pool_size=10      # Base connections")
print(f"  max_overflow=20   # Extra connections allowed")
print(f"  pool_timeout=30   # Seconds to wait")
print(f"")
print(f"Why connection pooling matters:")
print(f"  - Creating connections is expensive")
print(f"  - Pool reuses existing connections")
print(f"  - Prevents connection exhaustion")
print(f"  - Handles concurrent requests efficiently")
```
