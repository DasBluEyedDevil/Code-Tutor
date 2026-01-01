---
type: "THEORY"
title: "Resource Cleanup in Async Code"
---

**The Problem: Cleaning Up Async Resources**

Just like regular code needs `with` statements for proper cleanup, async code needs `async with` for async resources.

**Regular context manager:**
```python
with open('file.txt') as f:
    data = f.read()
# File automatically closed
```

**Async context manager:**
```python
async with aiofiles.open('file.txt') as f:
    data = await f.read()
# File automatically closed
```

**Why async context managers?**
- Some resources need async setup/teardown
- Network connections, database connections, file handles
- Ensures cleanup even if errors occur

**Common async context managers:**
- `aiofiles.open()` - Async file I/O
- `httpx.AsyncClient()` - HTTP connection pooling
- `asyncpg.create_pool()` - PostgreSQL connections
- Database sessions and transactions