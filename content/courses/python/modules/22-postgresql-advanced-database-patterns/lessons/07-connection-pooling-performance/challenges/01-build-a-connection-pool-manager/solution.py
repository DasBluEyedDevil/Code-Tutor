import asyncpg
import asyncio
from contextlib import asynccontextmanager
from typing import Optional

class FinanceDBPool:
    """Connection pool manager for Finance Tracker."""
    
    def __init__(self, dsn: str, min_size: int = 5, max_size: int = 20):
        self.dsn = dsn
        self.min_size = min_size
        self.max_size = max_size
        self._pool: Optional[asyncpg.Pool] = None
    
    async def connect(self):
        """Initialize the pool."""
        self._pool = await asyncpg.create_pool(
            self.dsn,
            min_size=self.min_size,
            max_size=self.max_size,
            max_inactive_connection_lifetime=300,
            command_timeout=60,
        )
        print(f"Pool created: {self._pool.get_size()} connections")
    
    async def health_check(self) -> bool:
        """Check if pool is healthy."""
        try:
            async with self._pool.acquire() as conn:
                result = await conn.fetchval('SELECT 1')
                return result == 1
        except Exception:
            return False
    
    def get_stats(self) -> dict:
        """Get pool statistics."""
        return {
            'size': self._pool.get_size(),
            'idle': self._pool.get_idle_size(),
            'min': self._pool.get_min_size(),
            'max': self._pool.get_max_size(),
        }
    
    @asynccontextmanager
    async def acquire(self):
        """Context manager to borrow a connection."""
        async with self._pool.acquire() as conn:
            yield conn
    
    async def close(self):
        """Close the pool."""
        if self._pool:
            await self._pool.close()

async def main():
    pool = FinanceDBPool(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    await pool.connect()
    
    is_healthy = await pool.health_check()
    print(f"Health check: {'passed' if is_healthy else 'failed'}")
    
    stats = pool.get_stats()
    print(f"Pool stats: {stats}")
    
    async with pool.acquire() as conn:
        count = await conn.fetchval('SELECT COUNT(*) FROM accounts')
        print(f"Accounts: {count}")
    
    await pool.close()

asyncio.run(main())