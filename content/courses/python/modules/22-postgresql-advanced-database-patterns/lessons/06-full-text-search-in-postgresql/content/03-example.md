---
type: "EXAMPLE"
title: "Basic Full-Text Search"
---

**Search transaction descriptions:**

```python
import asyncpg
import asyncio

async def basic_fts_demo():
    """Demonstrate basic full-text search."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Simple search using to_tsvector and to_tsquery
    print("=== Search for 'grocery' ===")
    results = await conn.fetch('''
        SELECT id, description, amount
        FROM transactions
        WHERE to_tsvector('english', description) 
              @@ to_tsquery('english', 'grocery')
        LIMIT 10
    ''')
    for r in results:
        print(f"  ${r['amount']}: {r['description']}")
    
    # Search with stemming - 'shopping' matches 'shop'
    print("\n=== Search 'shop' (matches 'shopping') ===")
    results = await conn.fetch('''
        SELECT description
        FROM transactions
        WHERE to_tsvector('english', description)
              @@ to_tsquery('english', 'shop')
        LIMIT 5
    ''')
    for r in results:
        print(f"  {r['description']}")
    
    # Boolean search: coffee OR tea
    print("\n=== Search 'coffee | tea' ===")
    results = await conn.fetch('''
        SELECT description, amount
        FROM transactions
        WHERE to_tsvector('english', description)
              @@ to_tsquery('english', 'coffee | tea')
    ''')
    for r in results:
        print(f"  ${r['amount']}: {r['description']}")
    
    # Phrase search: exact phrase "whole foods"
    print("\n=== Phrase search 'whole <-> foods' ===")
    results = await conn.fetch('''
        SELECT description
        FROM transactions
        WHERE to_tsvector('english', description)
              @@ to_tsquery('english', 'whole <-> foods')
    ''')
    for r in results:
        print(f"  {r['description']}")
    
    await conn.close()

asyncio.run(basic_fts_demo())
```
