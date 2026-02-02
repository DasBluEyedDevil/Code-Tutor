import asyncpg
import asyncio
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class MigrationHelper:
    """Safe migration helper for zero-downtime changes."""
    
    def __init__(self, dsn: str):
        self.dsn = dsn
    
    async def add_column(self, table: str, column: str, data_type: str):
        """Add a nullable column."""
        conn = await asyncpg.connect(self.dsn)
        await conn.execute(f'''
            ALTER TABLE {table}
            ADD COLUMN IF NOT EXISTS {column} {data_type}
        ''')
        logger.info(f"Added column: {table}.{column}")
        await conn.close()
    
    async def backfill_column(
        self, 
        table: str, 
        column: str, 
        default_value: str,
        batch_size: int = 1000
    ):
        """Backfill a column in batches."""
        conn = await asyncpg.connect(self.dsn)
        total = 0
        
        while True:
            result = await conn.execute(f'''
                UPDATE {table}
                SET {column} = ____
                WHERE {column} IS ____
                  AND id IN (
                      SELECT id FROM {table}
                      WHERE {column} IS NULL
                      LIMIT {batch_size}
                  )
            ''')
            
            count = int(result.split()[-1])
            total += count
            
            if count ____:
                break
            
            logger.info(f"Backfilled {total} rows...")
            await asyncio.sleep(0.1)
        
        logger.info(f"Backfill complete: {total} rows")
        await conn.close()
    
    async def set_not_null(self, table: str, column: str):
        """Add NOT NULL constraint after backfill."""
        conn = await asyncpg.connect(self.dsn)
        await conn.execute(f'''
            ALTER TABLE {table}
            ALTER COLUMN {column} SET ____
        ''')
        logger.info(f"Set NOT NULL on {table}.{column}")
        await conn.close()

async def main():
    helper = MigrationHelper(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Safe migration: add status column
    await helper.add_column('transactions', 'status', 'VARCHAR(20)')
    await helper.backfill_column('transactions', 'status', "'completed'")
    await helper.set_not_null('transactions', 'status')
    
    print("Migration complete!")

asyncio.run(main())