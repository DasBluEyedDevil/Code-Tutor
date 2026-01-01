---
type: "EXAMPLE"
title: "Connection Health Checks"
---

**Ensure connections are healthy before use:**

```python
import asyncpg
import asyncio
from typing import Optional
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class HealthyPool:
    """Pool with connection health monitoring."""
    
    def __init__(self, dsn: str):
        self.dsn = dsn
        self._pool: Optional[asyncpg.Pool] = None
        self._healthy = False
    
    async def connect(self):
        """Create pool with health check on connection init."""
        self._pool = await asyncpg.create_pool(
            self.dsn,
            min_size=2,
            max_size=10,
            # Run this query when connection is created
            init=self._init_connection,
            # Connection lifetime settings
            max_inactive_connection_lifetime=300,
        )
        self._healthy = True
        logger.info("Pool initialized with health checks")
    
    async def _init_connection(self, conn: asyncpg.Connection):
        """Called for each new connection - set up and verify."""
        # Verify connection works
        await conn.execute('SELECT 1')
        
        # Set session-level configurations
        await conn.execute("SET timezone TO 'UTC'")
        await conn.execute("SET statement_timeout TO '30s'")
        
        logger.debug("Connection initialized and verified")
    
    async def health_check(self) -> bool:
        """Check if pool is healthy."""
        if not self._pool:
            return False
        
        try:
            async with self._pool.acquire() as conn:
                result = await conn.fetchval('SELECT 1')
                self._healthy = result == 1
                return self._healthy
        except Exception as e:
            logger.error(f"Health check failed: {e}")
            self._healthy = False
            return False
    
    async def get_pool_stats(self) -> dict:
        """Get current pool statistics."""
        if not self._pool:
            return {'status': 'disconnected'}
        
        return {
            'status': 'healthy' if self._healthy else 'unhealthy',
            'size': self._pool.get_size(),
            'min_size': self._pool.get_min_size(),
            'max_size': self._pool.get_max_size(),
            'free_size': self._pool.get_idle_size(),
        }
    
    async def close(self):
        """Gracefully close the pool."""
        if self._pool:
            await self._pool.close()
            self._healthy = False
            logger.info("Pool closed")

async def main():
    pool = HealthyPool(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    await pool.connect()
    
    # Check health
    is_healthy = await pool.health_check()
    print(f"Pool healthy: {is_healthy}")
    
    # Get stats
    stats = await pool.get_pool_stats()
    print(f"Pool stats: {stats}")
    
    await pool.close()

asyncio.run(main())
```
