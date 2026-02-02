---
type: "EXAMPLE"
title: "Streaming Large Result Sets"
---

**Process millions of rows without memory issues:**

```python
import asyncpg
import asyncio
from decimal import Decimal

async def process_large_dataset():
    """Stream large result sets efficiently."""
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Method 1: Cursor for row-by-row processing
    print("=== Cursor Streaming ===")
    total = Decimal('0')
    count = 0
    
    async with conn.transaction():
        # Cursor fetches in batches internally
        async for record in conn.cursor(
            'SELECT amount FROM transactions WHERE user_id = $1',
            1
        ):
            total += record['amount']
            count += 1
            
            if count % 10000 == 0:
                print(f"Processed {count} records...")
    
    print(f"Total: ${total} from {count} transactions")
    
    # Method 2: Chunked fetching with LIMIT/OFFSET
    print("\n=== Chunked Fetching ===")
    chunk_size = 1000
    offset = 0
    
    while True:
        chunk = await conn.fetch('''
            SELECT id, amount, description
            FROM transactions
            WHERE user_id = $1
            ORDER BY id
            LIMIT $2 OFFSET $3
        ''', 1, chunk_size, offset)
        
        if not chunk:
            break
        
        # Process chunk
        print(f"Processing chunk at offset {offset}: {len(chunk)} rows")
        
        offset += chunk_size
        
        # Safety limit for demo
        if offset >= 5000:
            print("Demo limit reached")
            break
    
    # Method 3: Keyset pagination (more efficient for large offsets)
    print("\n=== Keyset Pagination ===")
    last_id = 0
    
    while True:
        chunk = await conn.fetch('''
            SELECT id, amount, description
            FROM transactions
            WHERE user_id = $1 AND id > $2
            ORDER BY id
            LIMIT $3
        ''', 1, last_id, chunk_size)
        
        if not chunk:
            break
        
        last_id = chunk[-1]['id']
        print(f"Fetched {len(chunk)} rows, last_id={last_id}")
        
        # Safety limit
        if last_id >= 5000:
            break
    
    await conn.close()

asyncio.run(process_large_dataset())
```
