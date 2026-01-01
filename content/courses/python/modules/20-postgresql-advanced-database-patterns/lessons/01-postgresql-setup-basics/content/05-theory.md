---
type: "THEORY"
title: "Connection Pooling for Production"
---

**Why Connection Pools?**

Creating database connections is expensive (~10-50ms). In a web app handling 100 requests/second, creating fresh connections would be disastrous.

**Connection Pool Benefits:**
- Reuse existing connections
- Limit maximum connections (prevent overload)
- Automatic connection health checks
- Built-in timeout handling

**Pool Configuration:**
```python
pool = await asyncpg.create_pool(
    host='localhost',
    port=5432,
    user='finance_user',
    password='secure_password',
    database='finance_tracker',
    min_size=5,      # Minimum connections to maintain
    max_size=20,     # Maximum connections allowed
    max_inactive_connection_lifetime=300  # Close idle after 5 min
)
```

**Usage Pattern:**
```python
async with pool.acquire() as conn:
    # Connection is borrowed from pool
    result = await conn.fetch('SELECT * FROM accounts')
# Connection automatically returned to pool
```