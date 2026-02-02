---
type: "EXAMPLE"
title: "Production Database Manager"
---

**A universal database manager for any cloud provider:**

```python
import asyncpg
import asyncio
import os
import ssl
from typing import Optional
from contextlib import asynccontextmanager
from urllib.parse import urlparse
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class CloudDatabaseManager:
    """Universal database manager for cloud PostgreSQL providers."""
    
    PROVIDERS = {
        'supabase.com': 'supabase',
        'neon.tech': 'neon',
        'railway.app': 'railway',
        'render.com': 'render',
        'amazonaws.com': 'aws_rds',
        'digitalocean.com': 'digitalocean',
    }
    
    def __init__(self, database_url: Optional[str] = None):
        self.database_url = database_url or os.environ.get('DATABASE_URL')
        self.provider = self._detect_provider()
        self._pool: Optional[asyncpg.Pool] = None
        
        logger.info(f"Database provider detected: {self.provider}")
    
    def _detect_provider(self) -> str:
        """Detect cloud provider from URL."""
        if not self.database_url:
            return 'local'
        
        for domain, provider in self.PROVIDERS.items():
            if domain in self.database_url:
                return provider
        
        return 'unknown'
    
    def _get_pool_config(self) -> dict:
        """Get provider-optimized pool configuration."""
        base_config = {
            'dsn': self.database_url,
            'command_timeout': 30,
        }
        
        if self.provider == 'local':
            return {
                **base_config,
                'ssl': 'prefer',
                'min_size': 2,
                'max_size': 10,
            }
        
        if self.provider == 'neon':
            # Neon is serverless, optimize for cold starts
            return {
                **base_config,
                'ssl': 'require',
                'min_size': 1,
                'max_size': 10,
                'max_inactive_connection_lifetime': 60,
            }
        
        if self.provider == 'supabase':
            # Supabase has connection pooling via Supavisor
            return {
                **base_config,
                'ssl': 'require',
                'min_size': 2,
                'max_size': 10,
            }
        
        # Default cloud config
        return {
            **base_config,
            'ssl': 'require',
            'min_size': 2,
            'max_size': 10,
        }
    
    async def connect(self):
        """Initialize connection pool."""
        config = self._get_pool_config()
        
        try:
            self._pool = await asyncpg.create_pool(**config)
            
            # Verify connection
            async with self._pool.acquire() as conn:
                version = await conn.fetchval('SELECT version()')
                logger.info(f"Connected! PostgreSQL: {version[:40]}...")
            
        except Exception as e:
            logger.error(f"Connection failed: {e}")
            raise
    
    async def disconnect(self):
        """Close connection pool."""
        if self._pool:
            await self._pool.close()
            logger.info("Disconnected from database")
    
    @asynccontextmanager
    async def connection(self):
        """Get a connection from the pool."""
        async with self._pool.acquire() as conn:
            yield conn
    
    @asynccontextmanager
    async def transaction(self):
        """Get a connection with transaction."""
        async with self._pool.acquire() as conn:
            async with conn.transaction():
                yield conn
    
    async def health_check(self) -> dict:
        """Check database health."""
        try:
            async with self._pool.acquire() as conn:
                result = await conn.fetchrow('''
                    SELECT 
                        current_database() as database,
                        current_user as user,
                        pg_database_size(current_database()) as size_bytes,
                        (SELECT count(*) FROM pg_stat_activity) as connections
                ''')
                
                return {
                    'status': 'healthy',
                    'provider': self.provider,
                    'database': result['database'],
                    'size_mb': result['size_bytes'] / (1024 * 1024),
                    'connections': result['connections'],
                }
        except Exception as e:
            return {
                'status': 'unhealthy',
                'error': str(e),
            }

# Usage example
async def main():
    # Uses DATABASE_URL from environment
    db = CloudDatabaseManager()
    await db.connect()
    
    # Health check
    health = await db.health_check()
    print(f"Database health: {health}")
    
    # Use connection
    async with db.connection() as conn:
        tables = await conn.fetch('''
            SELECT tablename 
            FROM pg_tables 
            WHERE schemaname = 'public'
        ''')
        print(f"Tables: {[t['tablename'] for t in tables]}")
    
    await db.disconnect()

asyncio.run(main())
```
