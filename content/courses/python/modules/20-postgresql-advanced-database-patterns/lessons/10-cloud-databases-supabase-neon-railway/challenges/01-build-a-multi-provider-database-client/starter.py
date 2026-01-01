import asyncpg
import asyncio
import os
from typing import Optional

class UniversalDBClient:
    """Database client that works with any PostgreSQL provider."""
    
    def __init__(self, database_url: str = None):
        self.url = database_url or os.environ.get('DATABASE_URL', '')
        self.provider = self._detect_provider()
        self._pool = None
    
    def _detect_provider(self) -> str:
        """Detect provider from URL."""
        if 'supabase' in self.url:
            return '____'
        elif 'neon' in self.url:
            return 'neon'
        elif 'railway' in self.url:
            return 'railway'
        return 'local'
    
    def _get_ssl_mode(self) -> str:
        """Get SSL mode for provider."""
        if self.provider == 'local':
            return '____'
        return 'require'  # Cloud providers need SSL
    
    def _get_pool_config(self) -> dict:
        """Get pool config for provider."""
        config = {'ssl': self._get_ssl_mode()}
        
        if self.provider == 'neon':
            # Serverless - minimal connections
            config['min_size'] = ____
            config['max_size'] = 10
        else:
            config['min_size'] = 2
            config['max_size'] = 10
        
        return config
    
    async def connect(self):
        """Connect to database."""
        config = self._get_pool_config()
        self._pool = await asyncpg.create_pool(self.url, **config)
        print(f"Connected to {self.provider} database!")
    
    async def health_check(self) -> bool:
        """Check if database is healthy."""
        try:
            async with self._pool.____() as conn:
                result = await conn.fetchval('SELECT 1')
                return result == 1
        except:
            return False
    
    async def close(self):
        """Close connection pool."""
        if self._pool:
            await self._pool.close()

async def main():
    client = UniversalDBClient(
        'postgresql://finance_user:pass@localhost/finance_tracker'
    )
    await client.connect()
    
    is_healthy = await client.health_check()
    print(f"Provider: {client.provider}")
    print(f"Healthy: {is_healthy}")
    
    await client.close()

asyncio.run(main())