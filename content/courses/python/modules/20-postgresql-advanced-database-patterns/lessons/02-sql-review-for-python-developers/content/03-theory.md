---
type: "THEORY"
title: "JOINs: Combining Related Data"
---

The Finance Tracker has related tables - we need JOINs to query across them:

**INNER JOIN** - Only matching rows from both tables
```sql
SELECT t.amount, c.name AS category
FROM transactions t
INNER JOIN categories c ON t.category_id = c.id
```

**LEFT JOIN** - All from left table, matching from right (NULL if no match)
```sql
SELECT a.name, COALESCE(SUM(t.amount), 0) AS total
FROM accounts a
LEFT JOIN transactions t ON a.id = t.account_id
GROUP BY a.id, a.name
```

**RIGHT JOIN** - All from right table, matching from left
```sql
SELECT c.name, COUNT(t.id) AS tx_count
FROM transactions t
RIGHT JOIN categories c ON t.category_id = c.id
GROUP BY c.id, c.name
```

**FULL OUTER JOIN** - All rows from both tables
```sql
SELECT *
FROM accounts a
FULL OUTER JOIN users u ON a.user_id = u.id
```

**Pro Tip:** Use table aliases (t, c, a) for cleaner queries.