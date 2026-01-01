---
type: "EXAMPLE"
title: "Basic Transaction Control: BEGIN, COMMIT, ROLLBACK"
---

**Transaction control in asyncpg:**

**Expected Output:**
```
Initial balance: $1000.00
After deposit: $1100.00
After rollback: $1100.00 (second deposit rolled back)
```

```python
import asyncpg
import asyncio
from decimal import Decimal

async def transaction_basics():
    """Demonstrate basic transaction control."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Reset for demo
    await conn.execute('UPDATE accounts SET balance = 1000 WHERE id = 1')
    
    # Method 1: Context manager (recommended)
    async with conn.transaction():
        await conn.execute('''
            UPDATE accounts SET balance = balance + $1 WHERE id = $2
        ''', Decimal('100.00'), 1)
        # Auto-commits when context exits successfully
    
    balance = await conn.fetchval(
        'SELECT balance FROM accounts WHERE id = $1', 1
    )
    print(f"After deposit: ${balance}")
    
    # Method 2: Manual control with try/except
    tr = conn.transaction()
    await tr.start()
    try:
        await conn.execute('''
            UPDATE accounts SET balance = balance + $1 WHERE id = $2
        ''', Decimal('500.00'), 1)
        
        # Simulate an error - maybe validation failed
        raise ValueError("Business rule violation!")
        
        await tr.commit()
    except Exception as e:
        await tr.rollback()
        print(f"Transaction rolled back: {e}")
    
    # Verify rollback worked
    balance = await conn.fetchval(
        'SELECT balance FROM accounts WHERE id = $1', 1
    )
    print(f"After rollback: ${balance} (second deposit rolled back)")
    
    await conn.close()

asyncio.run(transaction_basics())
```
