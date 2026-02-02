---
type: "EXAMPLE"
title: "When to Move Beyond SQLite"
---

**Signs You've Outgrown SQLite:**

SQLite is excellent for development, but consider migrating when:

**1. Multiple Concurrent Users:**
- SQLite locks the entire database for writes
- PostgreSQL/MySQL handle concurrent writes efficiently
- Rule of thumb: > 10 concurrent writers = time to migrate

**2. Need Advanced Features:**
- **JSON operations** - PostgreSQL has native JSONB
- **Full-text search** - Dedicated search engines or PostgreSQL
- **Geospatial queries** - PostGIS extension
- **Complex analytics** - Columnar databases

**3. Production Deployment:**
- Multiple app instances sharing data
- Need for database replication
- High availability requirements
- Horizontal scaling needs

**4. Data Size:**
- SQLite works well up to ~1GB
- Performance degrades with larger datasets
- No query planner optimizations for big data

**Migration Path:**
```python
# Development (SQLite)
DEV_URL = "sqlite+aiosqlite:///./dev.db"

# Production (PostgreSQL)
PROD_URL = "postgresql+asyncpg://user:pass@host/db"

# Use environment variable
import os
DATABASE_URL = os.getenv("DATABASE_URL", DEV_URL)
```

**The Good News:**
With SQLAlchemy, switching databases mostly requires:
1. Change the connection URL
2. Install the new driver (asyncpg for PostgreSQL)
3. Run migrations (if schema changes needed)

```python
import os

# Environment-based database selection
# This pattern lets you use SQLite locally, PostgreSQL in production

# Development: SQLite (zero config)
DEV_DATABASE_URL = "sqlite+aiosqlite:///./dev.db"

# Production: PostgreSQL (scalable)
# Set DATABASE_URL in your production environment
# Example: postgresql+asyncpg://user:password@host:5432/dbname

DATABASE_URL = os.getenv("DATABASE_URL", DEV_DATABASE_URL)

print("=== When to Move Beyond SQLite ===")
print("")
print("Migrate when you have:")
print("  - Multiple concurrent users writing data")
print("  - Need for JSON queries, full-text search")
print("  - Multiple app instances (containers, servers)")
print("  - Data growing beyond a few GB")
print("  - Production high-availability requirements")
print("")
print("Migration is easy with SQLAlchemy:")
print(f"  Current: {DATABASE_URL}")
print("")
print("To switch to PostgreSQL:")
print("  1. uv add asyncpg  # PostgreSQL async driver")
print("  2. Set DATABASE_URL environment variable")
print("  3. Run alembic migrations")
print("")
print("Your SQLAlchemy models stay the same!")
```
