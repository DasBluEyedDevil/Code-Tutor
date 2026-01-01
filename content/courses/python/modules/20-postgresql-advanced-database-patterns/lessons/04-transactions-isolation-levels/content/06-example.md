---
type: "EXAMPLE"
title: "Setting Isolation Levels in asyncpg"
---

**Configure isolation levels for different scenarios:**

```python
import asyncpg
import asyncio
from decimal import Decimal

async def isolation_level_demo():
    """Demonstrate different isolation levels."""
    pool = await asyncpg.create_pool(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker',
        min_size=2, max_size=5
    )
    
    # 1. READ COMMITTED (default) - for normal operations
    async with pool.acquire() as conn:
        async with conn.transaction():
            # Default isolation
            balance = await conn.fetchval(
                'SELECT balance FROM accounts WHERE id = 1'
            )
            print(f"Read Committed - Balance: ${balance}")
    
    # 2. REPEATABLE READ - for consistent reports
    async with pool.acquire() as conn:
        async with conn.transaction(
            isolation='repeatable_read'
        ):
            # Snapshot taken at transaction start
            totals = await conn.fetch('''
                SELECT category_id, SUM(amount) as total
                FROM transactions
                GROUP BY category_id
            ''')
            
            # Even if another transaction commits here,
            # this query sees the same data
            grand_total = await conn.fetchval('''
                SELECT SUM(amount) FROM transactions
            ''')
            
            print(f"Repeatable Read - Grand total: ${grand_total}")
    
    # 3. SERIALIZABLE - for critical operations
    async with pool.acquire() as conn:
        max_retries = 3
        for attempt in range(max_retries):
            try:
                async with conn.transaction(
                    isolation='serializable'
                ):
                    # Read current balance
                    balance = await conn.fetchval(
                        'SELECT balance FROM accounts WHERE id = 1'
                    )
                    
                    # Calculate and update
                    interest = balance * Decimal('0.001')  # 0.1% interest
                    
                    await conn.execute('''
                        UPDATE accounts 
                        SET balance = balance + $1
                        WHERE id = 1
                    ''', interest)
                    
                    print(f"Serializable - Added ${interest:.2f} interest")
                    break  # Success!
                    
            except asyncpg.SerializationError:
                if attempt < max_retries - 1:
                    print(f"Serialization conflict, retry {attempt + 1}")
                    await asyncio.sleep(0.1)  # Brief delay
                else:
                    raise
    
    await pool.close()

asyncio.run(isolation_level_demo())
```
