import asyncpg
import asyncio

async def setup_search(conn):
    """Set up full-text search on transactions."""
    # Add search vector column
    await conn.execute('''
        ALTER TABLE transactions
        ADD COLUMN IF NOT EXISTS search_vector tsvector
        GENERATED ALWAYS AS (
            to_tsvector('english', COALESCE(description, '') || ' ' || COALESCE(category, ''))
        ) STORED
    ''')
    
    # Create GIN index
    await conn.execute('''
        CREATE INDEX IF NOT EXISTS idx_tx_fts
        ON transactions USING GIN(search_vector)
    ''')

async def search(conn, query_text: str, limit: int = 10):
    """Search transactions with ranking and highlighting."""
    results = await conn.fetch('''
        SELECT 
            id,
            description,
            amount,
            ts_rank(search_vector, query) AS rank,
            ts_headline('english', description, query) AS headline
        FROM transactions,
             to_tsquery('english', $1) AS query
        WHERE search_vector @@ query
        ORDER BY rank DESC
        LIMIT $2
    ''', query_text, limit)
    return results

async def main():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    await setup_search(conn)
    print("Search setup complete!")
    
    results = await search(conn, 'grocery')
    print(f"Found {len(results)} results:")
    for r in results:
        print(f"  [{r['rank']:.3f}] {r['headline']}")
    
    await conn.close()

asyncio.run(main())