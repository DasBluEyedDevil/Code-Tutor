---
type: "EXAMPLE"
title: "Production-Ready Connection Pool"
---

**A reusable database manager for the Finance Tracker:**

```python
import asyncpg
import asyncio
from contextlib import asynccontextmanager
from typing import Optional

class DatabaseManager:
    """Manages PostgreSQL connections for the Finance Tracker."""
    
    def __init__(self, dsn: Optional[str] = None, **kwargs):
        self.dsn = dsn
        self.kwargs = kwargs
        self.pool: Optional[asyncpg.Pool] = None
    
    async def connect(self):
        """Initialize the connection pool."""
        if self.dsn:
            self.pool = await asyncpg.create_pool(self.dsn, **self.kwargs)
        else:
            self.pool = await asyncpg.create_pool(
                host='localhost',
                port=5432,
                user='finance_user',
                password='secure_password',
                database='finance_tracker',
                min_size=2,
                max_size=10,
                **self.kwargs
            )
        print(f"Pool created: {self.pool.get_size()} connections")
    
    async def disconnect(self):
        """Close all connections in the pool."""
        if self.pool:
            await self.pool.close()
            print("Pool closed.")
    
    @asynccontextmanager
    async def transaction(self):
        """Context manager for database transactions."""
        async with self.pool.acquire() as conn:
            async with conn.transaction():
                yield conn
    
    async def fetch(self, query: str, *args):
        """Execute a query and return all rows."""
        async with self.pool.acquire() as conn:
            return await conn.fetch(query, *args)
    
    async def fetchrow(self, query: str, *args):
        """Execute a query and return one row."""
        async with self.pool.acquire() as conn:
            return await conn.fetchrow(query, *args)
    
    async def execute(self, query: str, *args):
        """Execute a query without returning results."""
        async with self.pool.acquire() as conn:
            return await conn.execute(query, *args)

# Usage example
async def main():
    db = DatabaseManager()
    await db.connect()
    
    # Simple query
    accounts = await db.fetch('SELECT * FROM accounts')
    print(f"Found {len(accounts)} accounts")
    
    # Transaction example
    async with db.transaction() as conn:
        await conn.execute(
            'UPDATE accounts SET balance = balance + $1 WHERE id = $2',
            100.00, 1
        )
        print("Balance updated within transaction")
    
    await db.disconnect()

asyncio.run(main())
```
