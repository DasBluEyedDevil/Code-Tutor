---
type: "EXAMPLE"
title: "Strategic Indexes for Finance Tracker"
---

**Create indexes based on common query patterns:**

```python
import asyncpg
import asyncio

async def create_indexes():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Index for user's transactions by date (most common query)
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_user_date 
        ON transactions(user_id, transaction_date DESC)
    ''')
    print("Created: idx_transactions_user_date")
    
    # Index for category filtering
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_category 
        ON transactions(category_id)
        WHERE category_id IS NOT NULL
    ''')
    print("Created: idx_transactions_category (partial)")
    
    # Composite index for account balance queries
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_account_amount
        ON transactions(account_id, amount)
    ''')
    print("Created: idx_transactions_account_amount")
    
    # Index for date range queries (e.g., monthly reports)
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_date_range
        ON transactions(transaction_date)
        WHERE transaction_date >= CURRENT_DATE - INTERVAL '1 year'
    ''')
    print("Created: idx_transactions_date_range (partial, last year)")
    
    # Covering index for common SELECT (includes data in index)
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_covering
        ON transactions(user_id, transaction_date DESC)
        INCLUDE (amount, category_id, description)
    ''')
    print("Created: idx_transactions_covering (covering index)")
    
    # Check index sizes
    sizes = await conn.fetch('''
        SELECT 
            indexname,
            pg_size_pretty(pg_relation_size(indexname::regclass)) as size
        FROM pg_indexes 
        WHERE tablename = 'transactions'
        ORDER BY pg_relation_size(indexname::regclass) DESC
    ''')
    
    print("\nIndex sizes:")
    for idx in sizes:
        print(f"  {idx['indexname']}: {idx['size']}")
    
    await conn.close()

asyncio.run(create_indexes())
```
