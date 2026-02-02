---
type: "EXAMPLE"
title: "Safe Migration Script"
---

**Zero-downtime migration implementation:**

```python
import asyncpg
import asyncio
from typing import Callable, Awaitable
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class SafeMigration:
    """Execute database migrations safely without downtime."""
    
    def __init__(self, dsn: str):
        self.dsn = dsn
    
    async def add_nullable_column(
        self, 
        table: str, 
        column: str, 
        data_type: str
    ):
        """Add a nullable column (instant, safe)."""
        async with asyncpg.create_pool(self.dsn) as pool:
            async with pool.acquire() as conn:
                await conn.execute(f'''
                    ALTER TABLE {table}
                    ADD COLUMN IF NOT EXISTS {column} {data_type}
                ''')
                logger.info(f"Added column {table}.{column}")
    
    async def backfill_in_batches(
        self,
        table: str,
        update_query: str,
        batch_size: int = 1000,
        delay: float = 0.1
    ):
        """Backfill data in small batches to avoid locking."""
        async with asyncpg.create_pool(self.dsn) as pool:
            total_updated = 0
            
            while True:
                async with pool.acquire() as conn:
                    # Update a batch
                    result = await conn.execute(f'''
                        WITH batch AS (
                            SELECT id FROM {table}
                            WHERE {update_query.split('SET')[0].replace('UPDATE ' + table, '').strip() or '1=1'}
                            LIMIT {batch_size}
                            FOR UPDATE SKIP LOCKED
                        )
                        UPDATE {table} SET {update_query.split('SET')[1]}
                        WHERE id IN (SELECT id FROM batch)
                    ''')
                    
                    count = int(result.split()[-1])
                    total_updated += count
                    
                    if count == 0:
                        break
                    
                    logger.info(f"Backfilled {total_updated} rows...")
                    await asyncio.sleep(delay)  # Be gentle on the database
            
            logger.info(f"Backfill complete: {total_updated} rows updated")
    
    async def add_index_concurrently(
        self,
        table: str,
        index_name: str,
        columns: list,
        where: str = None
    ):
        """Add an index without blocking writes."""
        async with asyncpg.create_pool(self.dsn) as pool:
            async with pool.acquire() as conn:
                # Must run outside transaction for CONCURRENTLY
                await conn.execute('COMMIT')
                
                where_clause = f"WHERE {where}" if where else ""
                await conn.execute(f'''
                    CREATE INDEX CONCURRENTLY IF NOT EXISTS {index_name}
                    ON {table}({', '.join(columns)})
                    {where_clause}
                ''')
                logger.info(f"Created index {index_name} concurrently")
    
    async def expand_contract_rename(
        self,
        table: str,
        old_column: str,
        new_column: str,
        data_type: str
    ):
        """Rename column using expand-contract pattern."""
        # Step 1: Add new column
        await self.add_nullable_column(table, new_column, data_type)
        
        # Step 2: Copy data
        async with asyncpg.create_pool(self.dsn) as pool:
            async with pool.acquire() as conn:
                await conn.execute(f'''
                    UPDATE {table}
                    SET {new_column} = {old_column}
                    WHERE {new_column} IS NULL
                ''')
                logger.info(f"Copied data from {old_column} to {new_column}")
        
        logger.info(f"Expand complete. Update app to use {new_column}, then drop {old_column}")

async def main():
    migration = SafeMigration(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Example: Add a new column safely
    await migration.add_nullable_column(
        'transactions',
        'merchant_name',
        'VARCHAR(200)'
    )
    
    # Example: Add index without blocking
    await migration.add_index_concurrently(
        'transactions',
        'idx_transactions_merchant',
        ['merchant_name'],
        where="merchant_name IS NOT NULL"
    )

asyncio.run(main())
```
