---
type: "EXAMPLE"
title: "Soft Deletes Pattern"
---

**Never lose data - mark as deleted instead of removing:**

```python
import asyncpg
import asyncio
from datetime import datetime
from typing import Optional, List

async def setup_soft_delete_schema():
    """Create schema with soft delete support."""
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Transactions with soft delete
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS transactions_v2 (
            id SERIAL PRIMARY KEY,
            user_id INTEGER NOT NULL,
            account_id INTEGER NOT NULL,
            amount DECIMAL(12, 2) NOT NULL,
            description TEXT,
            category_id INTEGER,
            
            -- Soft delete: NULL = active, timestamp = deleted
            deleted_at TIMESTAMP DEFAULT NULL,
            
            -- Audit trail
            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            created_by INTEGER,
            
            -- Partial index for active records only
            CONSTRAINT chk_amount_nonzero CHECK (amount != 0)
        );
        
        -- View for active transactions (most common query)
        CREATE OR REPLACE VIEW active_transactions AS
        SELECT * FROM transactions_v2 WHERE deleted_at IS NULL;
        
        -- Index only active records for faster queries
        CREATE INDEX IF NOT EXISTS idx_transactions_active
        ON transactions_v2 (user_id, created_at DESC)
        WHERE deleted_at IS NULL;
    ''')
    
    print("Soft delete schema created!")
    await conn.close()

class TransactionRepository:
    """Repository with soft delete support."""
    
    def __init__(self, conn):
        self.conn = conn
    
    async def create(self, user_id: int, account_id: int, 
                     amount: float, description: str) -> int:
        """Create a new transaction."""
        return await self.conn.fetchval('''
            INSERT INTO transactions_v2 
                (user_id, account_id, amount, description, created_by)
            VALUES ($1, $2, $3, $4, $1)
            RETURNING id
        ''', user_id, account_id, amount, description)
    
    async def get_active(self, user_id: int, limit: int = 50) -> List[dict]:
        """Get active (non-deleted) transactions."""
        rows = await self.conn.fetch('''
            SELECT * FROM active_transactions
            WHERE user_id = $1
            ORDER BY created_at DESC
            LIMIT $2
        ''', user_id, limit)
        return [dict(r) for r in rows]
    
    async def soft_delete(self, tx_id: int, user_id: int) -> bool:
        """Soft delete a transaction."""
        result = await self.conn.execute('''
            UPDATE transactions_v2
            SET deleted_at = CURRENT_TIMESTAMP,
                updated_at = CURRENT_TIMESTAMP
            WHERE id = $1 AND user_id = $2 AND deleted_at IS NULL
        ''', tx_id, user_id)
        return result == 'UPDATE 1'
    
    async def restore(self, tx_id: int, user_id: int) -> bool:
        """Restore a soft-deleted transaction."""
        result = await self.conn.execute('''
            UPDATE transactions_v2
            SET deleted_at = NULL,
                updated_at = CURRENT_TIMESTAMP
            WHERE id = $1 AND user_id = $2 AND deleted_at IS NOT NULL
        ''', tx_id, user_id)
        return result == 'UPDATE 1'
    
    async def get_deleted(self, user_id: int) -> List[dict]:
        """Get deleted transactions (for recovery UI)."""
        rows = await self.conn.fetch('''
            SELECT * FROM transactions_v2
            WHERE user_id = $1 AND deleted_at IS NOT NULL
            ORDER BY deleted_at DESC
        ''', user_id)
        return [dict(r) for r in rows]
    
    async def hard_delete_old(self, days: int = 90) -> int:
        """Permanently delete records soft-deleted more than N days ago."""
        result = await self.conn.execute('''
            DELETE FROM transactions_v2
            WHERE deleted_at IS NOT NULL
              AND deleted_at < CURRENT_TIMESTAMP - INTERVAL '%s days'
        ''' % days)
        return int(result.split()[-1])

async def main():
    await setup_soft_delete_schema()

asyncio.run(main())
```
