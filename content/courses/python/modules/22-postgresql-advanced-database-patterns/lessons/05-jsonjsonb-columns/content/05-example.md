---
type: "EXAMPLE"
title: "Querying JSONB in the Finance Tracker"
---

**Powerful queries using JSONB operators:**

```python
import asyncpg
import asyncio

async def query_jsonb_data():
    """Query transactions using JSONB operators."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # 1. Extract specific fields
    print("=== Transactions with Merchant Info ===")
    results = await conn.fetch('''
        SELECT 
            id,
            amount,
            metadata->>'merchant' as merchant_json,
            metadata->'merchant'->>'name' as merchant_name,
            metadata#>>'{merchant,location,city}' as city
        FROM transactions
        WHERE metadata ? 'merchant'
        LIMIT 5
    ''')
    for r in results:
        print(f"  ${r['amount']}: {r['merchant_name']} ({r['city']})")
    
    # 2. Filter by nested value
    print("\n=== Grocery Transactions ===")
    results = await conn.fetch('''
        SELECT id, amount, description
        FROM transactions
        WHERE metadata @> '{"merchant": {"category": "grocery"}}'
    ''')
    for r in results:
        print(f"  ${r['amount']}: {r['description']}")
    
    # 3. Filter by tag in array
    print("\n=== Tagged 'organic' ===")
    results = await conn.fetch('''
        SELECT id, amount, metadata->'tags' as tags
        FROM transactions
        WHERE metadata->'tags' ? 'organic'
    ''')
    for r in results:
        print(f"  ${r['amount']}: tags = {r['tags']}")
    
    # 4. Aggregate by JSON field
    print("\n=== Spending by Merchant Category ===")
    results = await conn.fetch('''
        SELECT 
            metadata->'merchant'->>'category' as category,
            COUNT(*) as count,
            SUM(ABS(amount)) as total
        FROM transactions
        WHERE metadata->'merchant'->>'category' IS NOT NULL
        GROUP BY metadata->'merchant'->>'category'
        ORDER BY total DESC
    ''')
    for r in results:
        print(f"  {r['category']}: {r['count']} transactions, ${r['total']}")
    
    # 5. User preferences query
    print("\n=== Users with Dark Theme ===")
    results = await conn.fetch('''
        SELECT id, name, preferences->>'theme' as theme
        FROM users
        WHERE preferences->>'theme' = 'dark'
    ''')
    for r in results:
        print(f"  {r['name']}: {r['theme']} theme")
    
    await conn.close()

asyncio.run(query_jsonb_data())
```
