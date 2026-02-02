---
type: "EXAMPLE"
title: "Connecting to Supabase"
---

**Set up Supabase and connect with asyncpg:**

```python
import asyncpg
import asyncio
import os

# Supabase connection string format:
# postgresql://postgres.[project-ref]:[password]@aws-0-[region].pooler.supabase.com:5432/postgres

# Get from Supabase Dashboard > Settings > Database
SUPABASE_URL = os.environ.get(
    'DATABASE_URL',
    'postgresql://postgres.xxxxxxxxxxxx:your-password@aws-0-us-east-1.pooler.supabase.com:5432/postgres'
)

async def connect_supabase():
    """Connect to Supabase PostgreSQL."""
    
    # Supabase requires SSL
    pool = await asyncpg.create_pool(
        SUPABASE_URL,
        ssl='require',  # Required for Supabase
        min_size=2,
        max_size=10,
        command_timeout=30,
    )
    
    print("Connected to Supabase!")
    
    async with pool.acquire() as conn:
        # Verify connection
        version = await conn.fetchval('SELECT version()')
        print(f"PostgreSQL version: {version[:50]}...")
        
        # Create Finance Tracker tables
        await conn.execute('''
            CREATE TABLE IF NOT EXISTS transactions (
                id SERIAL PRIMARY KEY,
                user_id UUID NOT NULL,  -- Supabase uses UUID for auth.users
                amount DECIMAL(12, 2) NOT NULL,
                description TEXT,
                category VARCHAR(50),
                created_at TIMESTAMPTZ DEFAULT NOW()
            )
        ''')
        print("Created transactions table")
        
        # Enable Row Level Security (Supabase best practice)
        await conn.execute('''
            ALTER TABLE transactions ENABLE ROW LEVEL SECURITY;
            
            -- Policy: Users can only see their own transactions
            CREATE POLICY IF NOT EXISTS "Users can view own transactions"
            ON transactions FOR SELECT
            USING (auth.uid() = user_id);
            
            -- Policy: Users can insert own transactions
            CREATE POLICY IF NOT EXISTS "Users can insert own transactions"
            ON transactions FOR INSERT
            WITH CHECK (auth.uid() = user_id);
        ''')
        print("RLS policies configured")
    
    await pool.close()

asyncio.run(connect_supabase())
```
