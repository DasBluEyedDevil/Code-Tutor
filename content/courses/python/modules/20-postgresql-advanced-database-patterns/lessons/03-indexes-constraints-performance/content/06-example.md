---
type: "EXAMPLE"
title: "EXPLAIN ANALYZE: Query Optimization"
---

**Use EXPLAIN ANALYZE to understand and optimize queries:**

```python
import asyncpg
import asyncio

async def analyze_queries():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Analyze a query - see execution plan
    explain_query = '''
        EXPLAIN (ANALYZE, BUFFERS, FORMAT TEXT)
        SELECT 
            t.id, t.amount, t.description,
            c.name as category
        FROM transactions t
        LEFT JOIN categories c ON t.category_id = c.id
        WHERE t.user_id = $1
          AND t.transaction_date >= $2
        ORDER BY t.transaction_date DESC
        LIMIT 50
    '''
    
    from datetime import date, timedelta
    start_date = date.today() - timedelta(days=30)
    
    plan = await conn.fetch(explain_query, 1, start_date)
    
    print("=== Query Execution Plan ===")
    for row in plan:
        print(row[0])
    
    print("\n=== Key Metrics to Watch ===")
    print("""
    GOOD signs:
    - "Index Scan" or "Index Only Scan"
    - "Bitmap Index Scan" for multiple conditions
    - Low "actual time" numbers
    - "Buffers: shared hit" (data in cache)
    
    BAD signs:
    - "Seq Scan" on large tables
    - "Sort" with high rows (missing index)
    - "Buffers: shared read" (disk I/O)
    - "Nested Loop" with large outer table
    """)
    
    # Check for missing indexes
    missing = await conn.fetch('''
        SELECT 
            schemaname, tablename,
            seq_scan, seq_tup_read,
            idx_scan, idx_tup_fetch
        FROM pg_stat_user_tables
        WHERE seq_scan > idx_scan  -- More seq scans than index scans
          AND seq_tup_read > 10000  -- Reading lots of rows
        ORDER BY seq_tup_read DESC
        LIMIT 5
    ''')
    
    if missing:
        print("\n=== Tables Needing Indexes ===")
        for t in missing:
            print(f"{t['tablename']}: {t['seq_scan']} seq scans, "
                  f"{t['seq_tup_read']} rows read")
    
    await conn.close()

asyncio.run(analyze_queries())
```
