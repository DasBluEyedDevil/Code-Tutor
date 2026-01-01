---
type: "EXAMPLE"
title: "Updating JSONB Data"
---

**Modify JSONB columns without replacing entire document:**

```python
import asyncpg
import asyncio
import json

async def update_jsonb_data():
    """Update JSONB fields efficiently."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # 1. Set a single key (creates or updates)
    await conn.execute('''
        UPDATE transactions
        SET metadata = jsonb_set(metadata, '{reviewed}', 'true')
        WHERE id = $1
    ''', 1)
    print("Set 'reviewed' = true")
    
    # 2. Set nested key
    await conn.execute('''
        UPDATE transactions
        SET metadata = jsonb_set(
            metadata, 
            '{merchant,verified}', 
            'true'
        )
        WHERE id = $1
    ''', 1)
    print("Set 'merchant.verified' = true")
    
    # 3. Add to array
    await conn.execute('''
        UPDATE transactions
        SET metadata = jsonb_set(
            metadata,
            '{tags}',
            (COALESCE(metadata->'tags', '[]'::jsonb) || '"reviewed"'::jsonb)
        )
        WHERE id = $1
    ''', 1)
    print("Added 'reviewed' to tags array")
    
    # 4. Remove a key
    await conn.execute('''
        UPDATE transactions
        SET metadata = metadata - 'receipt_url'
        WHERE id = $1
    ''', 1)
    print("Removed 'receipt_url' key")
    
    # 5. Remove from nested path
    await conn.execute('''
        UPDATE transactions
        SET metadata = metadata #- '{merchant,location}'
        WHERE id = $1
    ''', 1)
    print("Removed 'merchant.location' path")
    
    # 6. Merge JSONB objects
    new_data = {"notes": "Double-check receipt", "priority": "high"}
    await conn.execute('''
        UPDATE transactions
        SET metadata = metadata || $1::jsonb
        WHERE id = $2
    ''', json.dumps(new_data), 1)
    print(f"Merged new data: {new_data}")
    
    # 7. Update user preferences (practical example)
    await conn.execute('''
        UPDATE users
        SET preferences = jsonb_set(
            preferences,
            '{notifications,push}',
            'true'
        )
        WHERE id = $1
    ''', 1)
    print("Enabled push notifications for user")
    
    # View final result
    result = await conn.fetchrow(
        'SELECT metadata FROM transactions WHERE id = $1', 1
    )
    print(f"\nFinal metadata: {json.dumps(result['metadata'], indent=2)}")
    
    await conn.close()

asyncio.run(update_jsonb_data())
```
