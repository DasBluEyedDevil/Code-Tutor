---
type: "EXAMPLE"
title: "Connecting to Railway"
---

**Deploy with Railway PostgreSQL:**

```python
import asyncpg
import asyncio
import os

# Railway injects DATABASE_URL automatically when you add PostgreSQL
# Format: postgresql://postgres:[password]@[host].railway.app:5432/railway

RAILWAY_URL = os.environ.get('DATABASE_URL')

if not RAILWAY_URL:
    print("Running locally - using local database")
    RAILWAY_URL = 'postgresql://finance_user:secure_password@localhost/finance_tracker'

async def connect_railway():
    """Connect to Railway PostgreSQL."""
    
    # Railway uses standard PostgreSQL
    pool = await asyncpg.create_pool(
        RAILWAY_URL,
        ssl='require' if 'railway.app' in RAILWAY_URL else 'prefer',
        min_size=2,
        max_size=10,
    )
    
    print("Connected to Railway PostgreSQL!")
    
    async with pool.acquire() as conn:
        # Check connection
        db_name = await conn.fetchval('SELECT current_database()')
        print(f"Database: {db_name}")
        
        # Initialize schema
        await conn.execute('''
            CREATE TABLE IF NOT EXISTS app_config (
                key VARCHAR(100) PRIMARY KEY,
                value JSONB,
                updated_at TIMESTAMPTZ DEFAULT NOW()
            );
            
            -- Track deployments
            INSERT INTO app_config (key, value)
            VALUES ('last_migration', '{"version": "1.0.0", "timestamp": null}'::jsonb)
            ON CONFLICT (key) DO UPDATE 
            SET value = jsonb_set(
                app_config.value, 
                '{timestamp}', 
                to_jsonb(NOW()::text)
            ),
            updated_at = NOW();
        ''')
        
        config = await conn.fetchrow(
            "SELECT * FROM app_config WHERE key = 'last_migration'"
        )
        print(f"Migration config: {config['value']}")
    
    await pool.close()

# Production-ready configuration
class DatabaseConfig:
    """Environment-aware database configuration."""
    
    @staticmethod
    def get_pool_config() -> dict:
        """Get pool configuration based on environment."""
        database_url = os.environ.get('DATABASE_URL', '')
        
        # Detect provider and adjust settings
        if 'supabase.com' in database_url:
            return {
                'dsn': database_url,
                'ssl': 'require',
                'min_size': 2,
                'max_size': 10,
                'command_timeout': 30,
            }
        elif 'neon.tech' in database_url:
            return {
                'dsn': database_url,
                'ssl': 'require',
                'min_size': 1,  # Serverless
                'max_size': 10,
                'max_inactive_connection_lifetime': 60,
            }
        elif 'railway.app' in database_url:
            return {
                'dsn': database_url,
                'ssl': 'require',
                'min_size': 2,
                'max_size': 10,
            }
        else:
            # Local development
            return {
                'dsn': database_url or 'postgresql://localhost/finance_tracker',
                'ssl': 'prefer',
                'min_size': 2,
                'max_size': 5,
            }

asyncio.run(connect_railway())
```
