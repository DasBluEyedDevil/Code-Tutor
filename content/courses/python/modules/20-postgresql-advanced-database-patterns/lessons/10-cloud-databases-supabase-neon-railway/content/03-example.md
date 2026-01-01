---
type: "EXAMPLE"
title: "Connecting to Neon"
---

**Set up Neon serverless PostgreSQL:**

```python
import asyncpg
import asyncio
import os

# Neon connection string format:
# postgresql://[user]:[password]@ep-[name]-[id].[region].aws.neon.tech/neondb?sslmode=require

# Get from Neon Console > Connection Details
NEON_URL = os.environ.get(
    'DATABASE_URL',
    'postgresql://finance_user:password@ep-cool-branch-123456.us-east-2.aws.neon.tech/neondb?sslmode=require'
)

async def connect_neon():
    """Connect to Neon serverless PostgreSQL."""
    
    # Neon with connection pooling
    pool = await asyncpg.create_pool(
        NEON_URL,
        ssl='require',  # Required for Neon
        min_size=1,     # Serverless: keep min low
        max_size=10,
        # Neon-specific: connections may be closed by pooler
        max_inactive_connection_lifetime=60,
    )
    
    print("Connected to Neon!")
    
    async with pool.acquire() as conn:
        # Verify connection
        result = await conn.fetchrow('''
            SELECT 
                current_database() as db,
                current_user as user,
                version() as version
        ''')
        print(f"Database: {result['db']}")
        print(f"User: {result['user']}")
        
        # Create tables
        await conn.execute('''
            CREATE TABLE IF NOT EXISTS accounts (
                id SERIAL PRIMARY KEY,
                user_id INTEGER NOT NULL,
                name VARCHAR(100) NOT NULL,
                balance DECIMAL(12, 2) DEFAULT 0.00,
                created_at TIMESTAMPTZ DEFAULT NOW()
            );
            
            CREATE TABLE IF NOT EXISTS transactions (
                id SERIAL PRIMARY KEY,
                account_id INTEGER REFERENCES accounts(id),
                amount DECIMAL(12, 2) NOT NULL,
                description TEXT,
                transaction_date DATE DEFAULT CURRENT_DATE,
                created_at TIMESTAMPTZ DEFAULT NOW()
            );
            
            CREATE INDEX IF NOT EXISTS idx_tx_account 
            ON transactions(account_id, transaction_date DESC);
        ''')
        print("Schema created!")
    
    # Demonstrate Neon branching concept
    print("\nNeon Branching (via Console):")
    print("  1. Create branch from main for testing")
    print("  2. Run migrations on branch")
    print("  3. Test thoroughly")
    print("  4. Merge branch to main (or discard)")
    
    await pool.close()

asyncio.run(connect_neon())
```
