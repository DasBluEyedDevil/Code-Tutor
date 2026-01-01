---
type: "THEORY"
title: "CTEs: Common Table Expressions"
---

CTEs (using `WITH`) make complex queries readable and maintainable:

**Basic CTE:**
```sql
WITH monthly_totals AS (
    SELECT 
        DATE_TRUNC('month', transaction_date) AS month,
        SUM(amount) AS total
    FROM transactions
    GROUP BY DATE_TRUNC('month', transaction_date)
)
SELECT * FROM monthly_totals ORDER BY month;
```

**Multiple CTEs:**
```sql
WITH 
    income AS (
        SELECT SUM(amount) AS total FROM transactions WHERE amount > 0
    ),
    expenses AS (
        SELECT SUM(ABS(amount)) AS total FROM transactions WHERE amount < 0
    )
SELECT 
    income.total AS income,
    expenses.total AS expenses,
    income.total - expenses.total AS net
FROM income, expenses;
```

**Benefits:**
- Break complex queries into logical steps
- Reuse the same subquery multiple times
- Self-documenting with descriptive names
- Often more efficient than nested subqueries