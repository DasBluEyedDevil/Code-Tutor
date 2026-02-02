---
type: "EXAMPLE"
title: "Savepoints in the Finance Tracker"
---

**Process a batch of transactions, skipping failures:**

```python
import asyncpg
import asyncio
from decimal import Decimal
from dataclasses import dataclass
from typing import List

@dataclass
class PendingTransaction:
    account_id: int
    amount: Decimal
    description: str

async def process_batch_with_savepoints():
    """Process multiple transactions, continuing on individual failures."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Batch of transactions to process
    batch = [
        PendingTransaction(1, Decimal('-50.00'), 'Coffee shop'),
        PendingTransaction(1, Decimal('-10000.00'), 'Luxury item'),  # Will fail
        PendingTransaction(1, Decimal('-25.00'), 'Groceries'),
        PendingTransaction(999, Decimal('-15.00'), 'Invalid account'),  # Will fail
        PendingTransaction(1, Decimal('-30.00'), 'Gas station'),
    ]
    
    processed = 0
    failed = 0
    
    async with conn.transaction():
        # Log batch start
        batch_id = await conn.fetchval('''
            INSERT INTO batch_logs (status, started_at)
            VALUES ('processing', NOW())
            RETURNING id
        ''')
        
        for tx in batch:
            # Create savepoint before each transaction
            savepoint = await conn.savepoint()
            
            try:
                # Check sufficient funds
                balance = await conn.fetchval(
                    'SELECT balance FROM accounts WHERE id = $1',
                    tx.account_id
                )
                
                if balance is None:
                    raise ValueError(f"Account {tx.account_id} not found")
                
                if balance + tx.amount < 0:  # amount is negative for expenses
                    raise ValueError(f"Insufficient funds: ${balance} available")
                
                # Process the transaction
                await conn.execute('''
                    UPDATE accounts SET balance = balance + $1 WHERE id = $2
                ''', tx.amount, tx.account_id)
                
                await conn.execute('''
                    INSERT INTO transactions (account_id, amount, description)
                    VALUES ($1, $2, $3)
                ''', tx.account_id, tx.amount, tx.description)
                
                processed += 1
                print(f"OK: {tx.description} (${tx.amount})")
                
            except Exception as e:
                # Rollback just this transaction
                await savepoint.rollback()
                failed += 1
                print(f"FAILED: {tx.description} - {e}")
                
                # Log the failure (this is AFTER rollback, so it sticks)
                await conn.execute('''
                    INSERT INTO batch_log_errors (batch_id, message)
                    VALUES ($1, $2)
                ''', batch_id, f"{tx.description}: {e}")
        
        # Update batch status
        await conn.execute('''
            UPDATE batch_logs 
            SET status = 'completed', 
                processed_count = $1, 
                failed_count = $2,
                completed_at = NOW()
            WHERE id = $3
        ''', processed, failed, batch_id)
    
    print(f"\nBatch complete: {processed} processed, {failed} failed")
    await conn.close()

asyncio.run(process_batch_with_savepoints())
```
