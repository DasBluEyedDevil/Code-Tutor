---
type: "EXAMPLE"
title: "Ranked Search with Highlighting"
---

**Production-ready search with ranking and snippets:**

```python
import asyncpg
import asyncio
from typing import List, Dict, Any

async def search_transactions(
    conn,
    query_text: str,
    limit: int = 20
) -> List[Dict[str, Any]]:
    """Search transactions with ranking and highlighting."""
    
    # Use websearch_to_tsquery for user-friendly input
    # Handles: "exact phrase", OR, -exclude
    results = await conn.fetch('''
        SELECT 
            t.id,
            t.description,
            t.amount,
            t.transaction_date,
            c.name AS category,
            ts_rank(t.search_vector, query) AS rank,
            ts_headline(
                'english',
                t.description,
                query,
                'StartSel=<b>, StopSel=</b>, MaxWords=35, MinWords=15'
            ) AS headline
        FROM transactions t
        LEFT JOIN categories c ON t.category_id = c.id,
             websearch_to_tsquery('english', $1) AS query
        WHERE t.search_vector @@ query
        ORDER BY rank DESC, t.transaction_date DESC
        LIMIT $2
    ''', query_text, limit)
    
    return [dict(r) for r in results]

async def demo_search():
    """Demonstrate ranked search."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Test various search patterns
    searches = [
        'grocery',                    # Single word
        'coffee shop',                # Multiple words (AND)
        '"whole foods"',              # Exact phrase
        'grocery OR restaurant',      # OR search
        'food -fast',                 # Exclude
    ]
    
    for query in searches:
        print(f"\n=== Search: '{query}' ===")
        results = await search_transactions(conn, query, limit=3)
        
        if not results:
            print("  No results found")
            continue
        
        for r in results:
            print(f"  [{r['rank']:.4f}] ${r['amount']}: {r['category']}")
            print(f"           {r['headline']}")
    
    await conn.close()

asyncio.run(demo_search())
```
