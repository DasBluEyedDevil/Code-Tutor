import asyncpg
import asyncio
from decimal import Decimal
from dataclasses import dataclass
from typing import List, Tuple

@dataclass
class Payment:
    from_account: int
    to_account: int
    amount: Decimal
    description: str

async def process_payments(pool, payments: List[Payment]) -> Tuple[int, int]:
    """Process batch of payments with savepoints for error handling."""
    processed = 0
    failed = 0
    
    async with pool.acquire() as conn:
        async with conn.____():  # Start transaction
            for payment in payments:
                # Create savepoint before each payment
                sp = await conn.____()
                
                try:
                    # Check source has funds
                    balance = await conn.fetchval(
                        'SELECT balance FROM accounts WHERE id = $1',
                        payment.from_account
                    )
                    
                    if balance is None or balance < payment.amount:
                        raise ValueError("Insufficient funds")
                    
                    # Deduct from source
                    await conn.execute('''
                        UPDATE accounts SET balance = balance - $1 WHERE id = $2
                    ''', payment.amount, payment.from_account)
                    
                    # Add to destination
                    await conn.execute('''
                        UPDATE accounts SET balance = balance + $1 WHERE id = $2
                    ''', payment.amount, payment.to_account)
                    
                    processed += 1
                    
                except Exception as e:
                    # Rollback just this payment
                    await sp.____()
                    failed += 1
    
    return processed, failed

async def main():
    pool = await asyncpg.create_pool(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    payments = [
        Payment(1, 2, Decimal('50.00'), 'Payment 1'),
        Payment(1, 2, Decimal('99999.00'), 'Will fail'),
        Payment(1, 3, Decimal('25.00'), 'Payment 3'),
    ]
    
    ok, fail = await process_payments(pool, payments)
    print(f"Processed: {ok}, Failed: {fail}")
    
    await pool.close()

asyncio.run(main())