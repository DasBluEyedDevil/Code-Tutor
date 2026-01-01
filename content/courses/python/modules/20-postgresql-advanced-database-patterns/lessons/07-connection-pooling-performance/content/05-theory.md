---
type: "THEORY"
title: "Query Optimization Patterns"
---

Beyond pooling, optimize your queries for performance:

**1. Use EXPLAIN ANALYZE**
```sql
EXPLAIN (ANALYZE, BUFFERS)
SELECT * FROM transactions WHERE user_id = 1;
```

**2. Batch Operations**
```python
# BAD: N individual inserts
for tx in transactions:
    await conn.execute('INSERT ...', tx)

# GOOD: Single batch insert
await conn.executemany(
    'INSERT INTO transactions VALUES ($1, $2, $3)',
    [(tx.id, tx.amount, tx.desc) for tx in transactions]
)
```

**3. Prepared Statements**
```python
# Prepare once, execute many times
stmt = await conn.prepare('SELECT * FROM accounts WHERE id = $1')
for account_id in account_ids:
    result = await stmt.fetch(account_id)
```

**4. Streaming Large Results**
```python
# Don't load everything into memory
async with conn.transaction():
    async for record in conn.cursor('SELECT * FROM transactions'):
        process(record)
```

**5. Copy for Bulk Loading**
```python
# Fastest way to insert large datasets
await conn.copy_records_to_table(
    'transactions',
    records=data,
    columns=['amount', 'description', 'date']
)
```