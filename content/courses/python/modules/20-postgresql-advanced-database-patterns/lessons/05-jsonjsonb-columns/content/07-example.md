---
type: "EXAMPLE"
title: "Creating and Using JSONB Indexes"
---

**Optimize JSONB queries with proper indexing:**

```python
import asyncpg
import asyncio

async def setup_jsonb_indexes():
    """Create and test JSONB indexes."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # 1. Full GIN index on metadata
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_metadata_gin
        ON transactions USING GIN (metadata)
    ''')
    print("Created: Full GIN index on metadata")
    
    # 2. Optimized path_ops index for containment
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_metadata_pathops
        ON transactions USING GIN (metadata jsonb_path_ops)
    ''')
    print("Created: pathops GIN index (smaller, @> only)")
    
    # 3. Expression index for specific field
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_transactions_merchant_name
        ON transactions ((metadata->'merchant'->>'name'))
        WHERE metadata->'merchant'->>'name' IS NOT NULL
    ''')
    print("Created: B-tree index on merchant name")
    
    # 4. Check index usage with EXPLAIN
    print("\n=== Query Plan Analysis ===")
    
    # Query using containment (uses GIN)
    plan = await conn.fetch('''
        EXPLAIN (ANALYZE, FORMAT TEXT)
        SELECT * FROM transactions
        WHERE metadata @> '{"merchant": {"category": "grocery"}}'
    ''')
    print("\nContainment query (@>):")
    for row in plan[:3]:  # First few lines
        print(f"  {row[0]}")
    
    # Query using extraction (uses expression index)
    plan = await conn.fetch('''
        EXPLAIN (ANALYZE, FORMAT TEXT)
        SELECT * FROM transactions
        WHERE metadata->'merchant'->>'name' = 'Whole Foods'
    ''')
    print("\nEquality query (->>'name' = ...):")
    for row in plan[:3]:
        print(f"  {row[0]}")
    
    # Check index sizes
    print("\n=== Index Sizes ===")
    sizes = await conn.fetch('''
        SELECT 
            indexname,
            pg_size_pretty(pg_relation_size(indexname::regclass)) as size
        FROM pg_indexes
        WHERE tablename = 'transactions'
          AND indexname LIKE '%metadata%'
        ORDER BY pg_relation_size(indexname::regclass) DESC
    ''')
    for idx in sizes:
        print(f"  {idx['indexname']}: {idx['size']}")
    
    await conn.close()

asyncio.run(setup_jsonb_indexes())
```
