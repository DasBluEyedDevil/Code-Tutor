---
type: "EXAMPLE"
title: "The RETURNING Clause"
---

PostgreSQL's `RETURNING` is incredibly useful - get the inserted/updated row immediately:

**Expected Output:**
```
Created transaction #1: $-45.99 for groceries
Updated account balance: $954.01
```

```python
import asyncpg
import asyncio
from decimal import Decimal

async def returning_demo():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # INSERT with RETURNING - get the new row's ID and timestamp
    new_tx = await conn.fetchrow('''
        INSERT INTO transactions (account_id, amount, category, description)
        VALUES ($1, $2, $3, $4)
        RETURNING id, amount, created_at
    ''', 1, Decimal('-45.99'), 'groceries', 'Supermarket')
    
    print(f"Created transaction #{new_tx['id']}: ${new_tx['amount']} for groceries")
    
    # UPDATE with RETURNING - verify the update
    updated = await conn.fetchrow('''
        UPDATE accounts 
        SET balance = balance + $1
        WHERE id = $2
        RETURNING id, name, balance
    ''', Decimal('-45.99'), 1)
    
    print(f"Updated account balance: ${updated['balance']}")
    
    await conn.close()

asyncio.run(returning_demo())
```
