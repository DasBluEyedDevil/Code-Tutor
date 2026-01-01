---
type: "THEORY"
title: "asyncpg: The Fastest PostgreSQL Driver"
---

**Why asyncpg over psycopg2?**

`asyncpg` is a high-performance async PostgreSQL driver:
- **3-5x faster** than psycopg2 for most operations
- **Native async/await** - perfect for FastAPI, aiohttp
- **Built-in connection pooling** via `asyncpg.create_pool()`
- **Automatic type conversion** - Python types <-> PostgreSQL types

**Installation:**
```bash
pip install asyncpg
```

**Connection Patterns:**

1. **Single Connection** - Simple scripts, one-off tasks
2. **Connection Pool** - Web applications, high concurrency

**Type Mapping:**
- `INTEGER` -> `int`
- `TEXT/VARCHAR` -> `str`
- `DECIMAL/NUMERIC` -> `decimal.Decimal`
- `TIMESTAMP` -> `datetime.datetime`
- `JSONB` -> `dict`/`list`
- `BOOLEAN` -> `bool`