---
type: "EXAMPLE"
title: "Atomic Fund Transfer with Deadlock Prevention"
---

**A production-ready fund transfer that handles all edge cases:**

```python
import asyncpg
import asyncio
from decimal import Decimal
from typing import Optional
import random

class TransferError(Exception):
    """Custom exception for transfer failures."""
    pass

async def transfer_funds(
    pool: asyncpg.Pool,
    from_account: int,
    to_account: int,
    amount: Decimal,
    description: Optional[str] = None,
    max_retries: int = 3
) -> dict:
    """Transfer funds between accounts atomically.
    
    Features:
    - Deadlock prevention via consistent lock order
    - Serializable isolation for correctness
    - Automatic retry on conflicts
    - Comprehensive validation
    """
    if amount <= 0:
        raise TransferError("Amount must be positive")
    
    if from_account == to_account:
        raise TransferError("Cannot transfer to same account")
    
    # Sort account IDs to prevent deadlock
    lock_order = sorted([from_account, to_account])
    
    for attempt in range(max_retries):
        try:
            async with pool.acquire() as conn:
                async with conn.transaction(isolation='serializable'):
                    # Lock accounts in consistent order
                    accounts = {}
                    for acc_id in lock_order:
                        row = await conn.fetchrow('''
                            SELECT id, name, balance 
                            FROM accounts 
                            WHERE id = $1 
                            FOR UPDATE
                        ''', acc_id)
                        if row is None:
                            raise TransferError(f"Account {acc_id} not found")
                        accounts[acc_id] = dict(row)
                    
                    # Validate sufficient funds
                    if accounts[from_account]['balance'] < amount:
                        raise TransferError(
                            f"Insufficient funds: ${accounts[from_account]['balance']} "
                            f"available, ${amount} required"
                        )
                    
                    # Perform the transfer
                    await conn.execute('''
                        UPDATE accounts SET balance = balance - $1 WHERE id = $2
                    ''', amount, from_account)
                    
                    await conn.execute('''
                        UPDATE accounts SET balance = balance + $1 WHERE id = $2
                    ''', amount, to_account)
                    
                    # Log both sides of the transfer
                    desc = description or f"Transfer to account {to_account}"
                    await conn.execute('''
                        INSERT INTO transactions 
                            (account_id, amount, type, description, related_account_id)
                        VALUES 
                            ($1, $2, 'transfer_out', $3, $4),
                            ($4, $5, 'transfer_in', $6, $1)
                    ''', 
                        from_account, -amount, desc, to_account,
                        amount, f"Transfer from account {from_account}"
                    )
                    
                    # Return success details
                    return {
                        'success': True,
                        'from_account': from_account,
                        'to_account': to_account,
                        'amount': amount,
                        'from_new_balance': accounts[from_account]['balance'] - amount,
                        'to_new_balance': accounts[to_account]['balance'] + amount,
                        'attempt': attempt + 1
                    }
        
        except (asyncpg.SerializationError, asyncpg.DeadlockDetectedError) as e:
            if attempt < max_retries - 1:
                # Exponential backoff with jitter
                delay = (2 ** attempt) * 0.1 + random.uniform(0, 0.1)
                print(f"Retry {attempt + 1}: {type(e).__name__}, waiting {delay:.2f}s")
                await asyncio.sleep(delay)
            else:
                raise TransferError(f"Transfer failed after {max_retries} attempts")
    
    raise TransferError("Unexpected: transfer loop exited without result")

# Usage example
async def main():
    pool = await asyncpg.create_pool(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker',
        min_size=2, max_size=10
    )
    
    try:
        result = await transfer_funds(
            pool,
            from_account=1,
            to_account=2,
            amount=Decimal('150.00'),
            description='Rent payment'
        )
        
        print(f"Transfer successful!")
        print(f"From account new balance: ${result['from_new_balance']}")
        print(f"To account new balance: ${result['to_new_balance']}")
        
    except TransferError as e:
        print(f"Transfer failed: {e}")
    
    await pool.close()

asyncio.run(main())
```
