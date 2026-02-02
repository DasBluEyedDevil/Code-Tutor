---
type: "WARNING"
title: "Common Performance Pitfalls"
---

### Avoid These Common Mistakes:

**1. N+1 Query Problem**
```python
# BAD - 1 query + N queries for each user
users = await conn.fetch('SELECT * FROM users')
for user in users:
    accounts = await conn.fetch(
        'SELECT * FROM accounts WHERE user_id = $1', user['id']
    )  # Runs N times!

# GOOD - Single query with JOIN
data = await conn.fetch('''
    SELECT u.*, a.name as account_name, a.balance
    FROM users u
    LEFT JOIN accounts a ON u.id = a.user_id
''')
```

**2. SELECT * in Production**
```python
# BAD - fetches all columns including blobs
await conn.fetch('SELECT * FROM users')

# GOOD - only what you need
await conn.fetch('SELECT id, name, email FROM users')
```

**3. Missing LIMIT on Large Tables**
```python
# BAD - could return millions of rows
await conn.fetch('SELECT * FROM transactions')

# GOOD - always paginate
await conn.fetch('SELECT * FROM transactions LIMIT 100 OFFSET 0')
```

**4. Functions in WHERE Defeating Indexes**
```sql
-- BAD - can't use index on created_at
WHERE DATE(created_at) = '2024-01-15'

-- GOOD - uses index
WHERE created_at >= '2024-01-15' 
  AND created_at < '2024-01-16'
```

**5. LIKE with Leading Wildcard**
```sql
-- BAD - can't use B-tree index
WHERE name LIKE '%smith'

-- OK - can use index
WHERE name LIKE 'smith%'

-- For full-text, use GIN index with tsvector
```