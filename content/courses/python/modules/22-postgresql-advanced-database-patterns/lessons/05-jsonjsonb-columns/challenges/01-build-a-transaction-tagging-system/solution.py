import asyncpg
import asyncio
from typing import List

async def add_tag(conn, transaction_id: int, tag: str):
    """Add a tag to transaction metadata."""
    # Use jsonb_set to add tag to the 'tags' array
    await conn.execute('''
        UPDATE transactions
        SET metadata = jsonb_set(
            COALESCE(metadata, '{}'::jsonb),
            '{tags}',
            COALESCE(metadata->'tags', '[]'::jsonb) || $1::jsonb
        )
        WHERE id = $2
    ''', f'"{tag}"', transaction_id)

async def remove_tag(conn, transaction_id: int, tag: str):
    """Remove a tag from transaction metadata."""
    # Remove the tag from the array
    await conn.execute('''
        UPDATE transactions
        SET metadata = jsonb_set(
            metadata,
            '{tags}',
            (
                SELECT jsonb_agg(elem)
                FROM jsonb_array_elements(metadata->'tags') elem
                WHERE elem <> $1::jsonb
            )
        )
        WHERE id = $2
          AND metadata->'tags' ? $1
    ''', tag, transaction_id)

async def find_by_tag(conn, tag: str) -> List[dict]:
    """Find all transactions with a specific tag."""
    results = await conn.fetch('''
        SELECT id, amount, description, metadata->'tags' as tags
        FROM transactions
        WHERE metadata->'tags' ? $1
    ''', tag)
    return [dict(r) for r in results]

async def main():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    await add_tag(conn, 1, 'important')
    print("Added 'important' tag")
    
    results = await find_by_tag(conn, 'important')
    print(f"Found {len(results)} transactions with 'important' tag")
    
    await remove_tag(conn, 1, 'important')
    print("Removed 'important' tag")
    
    await conn.close()

asyncio.run(main())