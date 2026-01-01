import asyncpg
import asyncio
from datetime import datetime
from typing import List, Optional

class TransactionRepo:
    """Transaction repository with soft delete support."""
    
    def __init__(self, conn):
        self.conn = conn
    
    async def get_active(self, user_id: int) -> List[dict]:
        """Get active (non-deleted) transactions."""
        rows = await self.conn.fetch('''
            SELECT id, amount, description, created_at
            FROM transactions
            WHERE user_id = $1 AND deleted_at ____ NULL
            ORDER BY created_at DESC
        ''', user_id)
        return [dict(r) for r in rows]
    
    async def soft_delete(self, tx_id: int) -> bool:
        """Mark a transaction as deleted."""
        result = await self.conn.execute('''
            UPDATE transactions
            SET deleted_at = ____,
                updated_at = CURRENT_TIMESTAMP
            WHERE id = $1 AND deleted_at IS NULL
        ''', tx_id)
        return result == 'UPDATE 1'
    
    async def restore(self, tx_id: int) -> bool:
        """Restore a soft-deleted transaction."""
        result = await self.conn.execute('''
            UPDATE transactions
            SET deleted_at = ____,
                updated_at = CURRENT_TIMESTAMP
            WHERE id = $1 AND deleted_at IS NOT NULL
        ''', tx_id)
        return result == 'UPDATE 1'
    
    async def get_deleted(self, user_id: int) -> List[dict]:
        """Get deleted transactions for recovery."""
        rows = await self.conn.fetch('''
            SELECT id, amount, description, deleted_at
            FROM transactions
            WHERE user_id = $1 AND deleted_at ____ NULL
            ORDER BY deleted_at DESC
        ''', user_id)
        return [dict(r) for r in rows]

async def main():
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    repo = TransactionRepo(conn)
    
    active = await repo.get_active(1)
    print(f"Active transactions: {len(active)}")
    
    deleted = await repo.get_deleted(1)
    print(f"Deleted transactions: {len(deleted)}")
    
    await conn.close()

asyncio.run(main())