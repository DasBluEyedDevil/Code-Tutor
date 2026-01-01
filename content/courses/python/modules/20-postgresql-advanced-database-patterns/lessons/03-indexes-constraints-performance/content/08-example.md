---
type: "EXAMPLE"
title: "Production Performance Monitoring"
---

**Set up performance monitoring queries:**

```python
import asyncpg
import asyncio

async def performance_check():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # 1. Check table sizes
    print("=== Table Sizes ===")
    sizes = await conn.fetch('''
        SELECT 
            relname as table_name,
            pg_size_pretty(pg_total_relation_size(relid)) as total_size,
            pg_size_pretty(pg_relation_size(relid)) as data_size,
            pg_size_pretty(pg_indexes_size(relid)) as index_size,
            n_live_tup as row_count
        FROM pg_stat_user_tables
        ORDER BY pg_total_relation_size(relid) DESC
        LIMIT 10
    ''')
    for t in sizes:
        print(f"{t['table_name']}: {t['total_size']} "
              f"({t['row_count']} rows, indexes: {t['index_size']})")
    
    # 2. Check unused indexes
    print("\n=== Unused Indexes (candidates for removal) ===")
    unused = await conn.fetch('''
        SELECT 
            schemaname || '.' || relname as table,
            indexrelname as index,
            pg_size_pretty(pg_relation_size(indexrelid)) as size,
            idx_scan as scans
        FROM pg_stat_user_indexes
        WHERE idx_scan = 0
          AND indexrelname NOT LIKE '%_pkey'
        ORDER BY pg_relation_size(indexrelid) DESC
        LIMIT 5
    ''')
    for idx in unused:
        print(f"{idx['index']} on {idx['table']}: {idx['size']} (0 scans)")
    
    # 3. Check for bloat (dead tuples)
    print("\n=== Table Bloat (needs VACUUM) ===")
    bloat = await conn.fetch('''
        SELECT 
            relname as table,
            n_live_tup as live_rows,
            n_dead_tup as dead_rows,
            CASE WHEN n_live_tup > 0 
                THEN round(100.0 * n_dead_tup / n_live_tup, 1)
                ELSE 0 
            END as dead_pct,
            last_vacuum,
            last_autovacuum
        FROM pg_stat_user_tables
        WHERE n_dead_tup > 1000
        ORDER BY n_dead_tup DESC
        LIMIT 5
    ''')
    for t in bloat:
        print(f"{t['table']}: {t['dead_rows']} dead rows ({t['dead_pct']}%)")
    
    # 4. Connection stats
    print("\n=== Connection Stats ===")
    conn_stats = await conn.fetchrow('''
        SELECT 
            max_conn,
            used,
            reserved,
            max_conn - used - reserved as available
        FROM (
            SELECT 
                setting::int as max_conn
            FROM pg_settings 
            WHERE name = 'max_connections'
        ) mc,
        (
            SELECT 
                count(*) as used
            FROM pg_stat_activity
        ) used,
        (
            SELECT 
                setting::int as reserved
            FROM pg_settings
            WHERE name = 'superuser_reserved_connections'
        ) reserved
    ''')
    print(f"Max: {conn_stats['max_conn']}, Used: {conn_stats['used']}, "
          f"Available: {conn_stats['available']}")
    
    await conn.close()

asyncio.run(performance_check())
```
