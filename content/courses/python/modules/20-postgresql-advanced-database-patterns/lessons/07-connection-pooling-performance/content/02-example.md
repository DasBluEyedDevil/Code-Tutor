---
type: "EXAMPLE"
title: "asyncpg Pool Configuration"
---

**Create a production-ready connection pool:**

**Expected Output:**
```
Pool created: 5 connections (min), 20 max
Borrowed connection, pool size: 5
Returned connection, pool size: 5
Query executed in 2.3ms
```

```python
import asyncpg
import asyncio
from contextlib import asynccontextmanager
import time

class DatabasePool:
    """Production-ready PostgreSQL connection pool."""
    
    def __init__(
        self,
        dsn: str = None,
        min_size: int = 5,
        max_size: int = 20,
        max_inactive_connection_lifetime: float = 300.0,
        command_timeout: float = 60.0
    ):
        self.dsn = dsn
        self.min_size = min_size
        self.max_size = max_size
        self.max_inactive = max_inactive_connection_lifetime
        self.command_timeout = command_timeout
        self._pool = None
    
    async def connect(self):
        """Initialize the connection pool."""
        self._pool = await asyncpg.create_pool(
            self.dsn or 'postgresql://finance_user:secure_password@localhost/finance_tracker',
            min_size=self.min_size,
            max_size=self.max_size,
            max_inactive_connection_lifetime=self.max_inactive,
            command_timeout=self.command_timeout,
        )
        print(f"Pool created: {self.min_size} connections (min), {self.max_size} max")
    
    async def disconnect(self):
        """Close all connections in the pool."""
        if self._pool:
            await self._pool.close()
            print("Pool closed gracefully")
    
    @asynccontextmanager
    async def acquire(self):
        """Borrow a connection from the pool."""
        async with self._pool.acquire() as conn:
            print(f"Borrowed connection, pool size: {self._pool.get_size()}")
            yield conn
        print(f"Returned connection, pool size: {self._pool.get_size()}")
    
    async def execute(self, query: str, *args):
        """Execute a query using a pooled connection."""
        start = time.perf_counter()
        async with self._pool.acquire() as conn:
            result = await conn.execute(query, *args)
        elapsed = (time.perf_counter() - start) * 1000
        print(f"Query executed in {elapsed:.1f}ms")
        return result
    
    async def fetch(self, query: str, *args):
        """Fetch rows using a pooled connection."""
        async with self._pool.acquire() as conn:
            return await conn.fetch(query, *args)
    
    async def fetchrow(self, query: str, *args):
        """Fetch a single row using a pooled connection."""
        async with self._pool.acquire() as conn:
            return await conn.fetchrow(query, *args)

async def main():
    db = DatabasePool(min_size=5, max_size=20)
    await db.connect()
    
    # Simple query
    accounts = await db.fetch('SELECT * FROM accounts LIMIT 5')
    print(f"Found {len(accounts)} accounts")
    
    await db.disconnect()

asyncio.run(main())
```
