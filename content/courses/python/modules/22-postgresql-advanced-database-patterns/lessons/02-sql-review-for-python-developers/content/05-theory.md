---
type: "THEORY"
title: "Aggregations: GROUP BY and HAVING"
---

Aggregations summarize data - essential for financial reports:

**Common Aggregate Functions:**
- `SUM(amount)` - Total
- `AVG(amount)` - Average
- `COUNT(*)` - Row count
- `MIN(amount)`, `MAX(amount)` - Extremes
- `ARRAY_AGG(name)` - Collect into array (PostgreSQL)

**GROUP BY** - Group rows for aggregation
```sql
SELECT category, SUM(amount) AS total
FROM transactions
GROUP BY category
```

**HAVING** - Filter groups (like WHERE for aggregates)
```sql
SELECT category, SUM(amount) AS total
FROM transactions
GROUP BY category
HAVING SUM(amount) > 100  -- Only categories with total > 100
```

**Order of Operations:**
1. FROM/JOIN - Get the data
2. WHERE - Filter rows
3. GROUP BY - Group rows
4. HAVING - Filter groups
5. SELECT - Choose columns
6. ORDER BY - Sort results
7. LIMIT/OFFSET - Paginate