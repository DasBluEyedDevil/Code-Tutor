---
type: "EXAMPLE"
title: "Adding Full-Text Search to Transactions"
---

**Create a searchable transactions table:**

```python
import asyncpg
import asyncio

async def setup_fts():
    """Set up full-text search on transactions."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Add generated search vector column
    # Combines description + category for comprehensive search
    await conn.execute('''
        ALTER TABLE transactions 
        ADD COLUMN IF NOT EXISTS search_vector tsvector
        GENERATED ALWAYS AS (
            setweight(to_tsvector('english', COALESCE(description, '')), 'A') ||
            setweight(to_tsvector('english', COALESCE(category, '')), 'B')
        ) STORED
    ''')
    print("Added search_vector column with weights")
    
    # Create GIN index for fast searches
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_search
        ON transactions USING GIN (search_vector)
    ''')
    print("Created GIN index on search_vector")
    
    # Verify it's working
    result = await conn.fetchrow('''
        SELECT 
            description,
            category,
            search_vector
        FROM transactions
        WHERE search_vector IS NOT NULL
        LIMIT 1
    ''')
    
    if result:
        print(f"\nSample search vector:")
        print(f"  Description: {result['description']}")
        print(f"  Category: {result['category']}")
        print(f"  Vector: {result['search_vector'][:100]}...")
    
    # Check index size
    size = await conn.fetchval('''
        SELECT pg_size_pretty(pg_relation_size('idx_transactions_search'))
    ''')
    print(f"\nSearch index size: {size}")
    
    await conn.close()

asyncio.run(setup_fts())
```
