import asyncpg
import asyncio

async def optimize_schema():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Add CHECK constraint for non-zero amount
    await conn.execute('''
        ALTER TABLE transactions
        ADD CONSTRAINT chk_amount_nonzero
        CHECK (amount ____ 0)
    ''')
    print("Added: CHECK constraint for non-zero amount")
    
    # Add foreign key with cascade delete
    await conn.execute('''
        ALTER TABLE transactions
        ADD CONSTRAINT fk_transactions_user
        FOREIGN KEY (user_id) 
        REFERENCES users(id)
        ON DELETE ____
    ''')
    print("Added: Foreign key with cascade delete")
    
    # Create composite index for common query pattern
    await conn.execute('''
        CREATE INDEX idx_tx_user_date
        ON transactions(____, transaction_date DESC)
    ''')
    print("Created: Composite index on user_id, transaction_date")
    
    # Create partial index for large transactions (> $1000)
    await conn.execute('''
        CREATE INDEX idx_large_transactions
        ON transactions(amount)
        WHERE ____(amount) > 1000
    ''')
    print("Created: Partial index for large transactions")
    
    print("\nSchema optimization complete!")
    await conn.close()

asyncio.run(optimize_schema())