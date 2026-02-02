---
type: "THEORY"
title: "Savepoints: Partial Rollbacks"
---

Sometimes you need to undo part of a transaction without aborting the whole thing. **Savepoints** create checkpoints within a transaction:

```sql
BEGIN;
    INSERT INTO log (message) VALUES ('Starting batch');
    
    SAVEPOINT before_risky_operation;
    
    -- Try something that might fail
    UPDATE accounts SET balance = balance - 1000 WHERE id = 1;
    
    -- Oops, not enough funds
    ROLLBACK TO SAVEPOINT before_risky_operation;
    
    -- Continue with the rest of the transaction
    INSERT INTO log (message) VALUES ('Skipped transfer, insufficient funds');
    
COMMIT;  -- Log entries are saved, transfer was undone
```

**Use Cases:**
- Processing batches where some items may fail
- Trying operations with fallback behavior
- Complex workflows with optional steps
- Testing changes within a transaction before committing

**In asyncpg:**
```python
async with conn.transaction():
    await conn.execute('INSERT INTO log ...')
    
    # Create savepoint
    savepoint = await conn.savepoint()
    try:
        await conn.execute('risky operation...')
    except:
        await savepoint.rollback()
        # Transaction continues
```