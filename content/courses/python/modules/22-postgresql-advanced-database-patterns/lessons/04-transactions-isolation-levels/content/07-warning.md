---
type: "WARNING"
title: "Deadlocks and How to Avoid Them"
---

### What is a Deadlock?

A deadlock occurs when two transactions wait for each other's locks:

```
Transaction A: Locks account 1, wants account 2
Transaction B: Locks account 2, wants account 1
→ Both stuck forever!
```

PostgreSQL detects deadlocks and aborts one transaction with an error.

### Prevention Strategies:

**1. Consistent Lock Order**
```python
# Always lock accounts in ID order
ids = sorted([from_account, to_account])
for account_id in ids:
    await conn.fetchrow(
        'SELECT * FROM accounts WHERE id = $1 FOR UPDATE',
        account_id
    )
```

**2. Lock Timeout**
```sql
SET lock_timeout = '5s';  -- Give up after 5 seconds
```

**3. Use NOWAIT**
```sql
SELECT * FROM accounts WHERE id = 1 FOR UPDATE NOWAIT;
-- Fails immediately if locked, instead of waiting
```

**4. Minimize Transaction Duration**
- Do calculations BEFORE the transaction
- Don't hold transactions open during I/O or user input
- Batch small, quick transactions

**5. Retry Logic**
```python
for attempt in range(3):
    try:
        async with conn.transaction():
            # ... operations
            break
    except asyncpg.DeadlockDetectedError:
        await asyncio.sleep(random.uniform(0.1, 0.5))
```