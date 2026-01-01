---
type: "EXAMPLE"
title: "Row-Level Security for User Isolation"
---

**Automatic user data isolation with RLS:**

```python
import asyncpg
import asyncio

async def setup_rls():
    """Configure Row-Level Security for user isolation."""
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Enable RLS on transactions table
    await conn.execute('''
        ALTER TABLE transactions ENABLE ROW LEVEL SECURITY;
        ALTER TABLE transactions FORCE ROW LEVEL SECURITY;
    ''')
    
    # Create policy: users can only see their own transactions
    await conn.execute('''
        DROP POLICY IF EXISTS user_isolation ON transactions;
        CREATE POLICY user_isolation ON transactions
            FOR ALL
            USING (user_id = current_setting('app.current_user_id', true)::int)
            WITH CHECK (user_id = current_setting('app.current_user_id', true)::int);
    ''')
    
    print("RLS configured!")
    await conn.close()

async def query_with_rls(user_id: int):
    """Execute queries with RLS user context."""
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Set the current user context
    await conn.execute(
        f"SET app.current_user_id = '{user_id}'"
    )
    
    # This query automatically filters by user_id
    # No need to add WHERE user_id = ... - RLS handles it!
    transactions = await conn.fetch('''
        SELECT id, amount, description
        FROM transactions
        ORDER BY created_at DESC
        LIMIT 10
    ''')
    
    print(f"User {user_id} sees {len(transactions)} transactions")
    for tx in transactions:
        print(f"  ${tx['amount']}: {tx['description']}")
    
    await conn.close()

class SecureConnection:
    """Connection wrapper that enforces user context."""
    
    def __init__(self, conn, user_id: int):
        self.conn = conn
        self.user_id = user_id
    
    async def __aenter__(self):
        await self.conn.execute(
            f"SET app.current_user_id = '{self.user_id}'"
        )
        return self.conn
    
    async def __aexit__(self, *args):
        await self.conn.execute("RESET app.current_user_id")

async def main():
    await setup_rls()
    
    # Query as different users
    await query_with_rls(1)
    await query_with_rls(2)

asyncio.run(main())
```
