---
type: "THEORY"
title: "CRUD Operations Refresher"
---

Before diving into advanced patterns, let's solidify the fundamentals with asyncpg:

**CREATE (INSERT)**
```python
await conn.execute('''
    INSERT INTO transactions (user_id, amount, category, description)
    VALUES ($1, $2, $3, $4)
''', user_id, Decimal('50.00'), 'groceries', 'Weekly shopping')
```

**READ (SELECT)**
```python
# Single row
user = await conn.fetchrow('SELECT * FROM users WHERE id = $1', user_id)

# Multiple rows
transactions = await conn.fetch(
    'SELECT * FROM transactions WHERE user_id = $1 ORDER BY date DESC',
    user_id
)

# Single value
count = await conn.fetchval('SELECT COUNT(*) FROM transactions')
```

**UPDATE**
```python
await conn.execute('''
    UPDATE accounts SET balance = balance - $1 WHERE id = $2
''', amount, account_id)
```

**DELETE**
```python
await conn.execute('DELETE FROM transactions WHERE id = $1', tx_id)
```

**Key Differences from SQLite:**
- Use `$1, $2, $3` not `?` for parameters
- `RETURNING` clause to get inserted/updated rows
- `ON CONFLICT` for upserts