---
type: "KEY_POINT"
title: "SQLite Development Takeaways"
---

**Core Concepts:**

1. **Zero Configuration** - SQLite requires no server, just a file
2. **Ships with Python** - sqlite3 is built-in, add aiosqlite for async
3. **Perfect for Development** - Quick setup, easy reset, portable

**Setup Checklist:**
```bash
uv add aiosqlite  # Async SQLite driver
```

**Connection URL:**
```python
"sqlite+aiosqlite:///./dev.db"
```

**Required for SQLite:**
```python
connect_args={"check_same_thread": False}
```

**Limitations to Remember:**
- Single writer at a time
- No concurrent writes
- Best for data under a few GB
- File-based (no network access)

**When to Migrate:**
- Multiple concurrent users
- Need advanced features (JSON, full-text search)
- Production with multiple instances
- Data exceeding a few GB

**Migration is Simple:**
- SQLAlchemy abstracts the database
- Change URL, install driver, run migrations
- Your models stay the same