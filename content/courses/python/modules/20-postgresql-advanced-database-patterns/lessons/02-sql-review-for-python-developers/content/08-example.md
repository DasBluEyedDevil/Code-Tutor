---
type: "EXAMPLE"
title: "Finance Tracker: Monthly Trend with CTE"
---

**Calculate month-over-month spending trends:**

```python
import asyncpg
import asyncio

async def spending_trends():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    query = '''
        WITH monthly_totals AS (
            SELECT 
                DATE_TRUNC('month', transaction_date) AS month,
                category_id,
                SUM(amount) AS total
            FROM transactions
            WHERE user_id = $1
            GROUP BY DATE_TRUNC('month', transaction_date), category_id
        ),
        with_categories AS (
            SELECT 
                mt.month,
                c.name AS category,
                mt.total,
                LAG(mt.total) OVER (
                    PARTITION BY mt.category_id 
                    ORDER BY mt.month
                ) AS prev_month_total
            FROM monthly_totals mt
            JOIN categories c ON mt.category_id = c.id
        )
        SELECT 
            TO_CHAR(month, 'Mon YYYY') AS month_name,
            category,
            total,
            prev_month_total,
            CASE 
                WHEN prev_month_total IS NULL THEN NULL
                WHEN prev_month_total = 0 THEN 100.0
                ELSE ROUND(((total - prev_month_total) / ABS(prev_month_total)) * 100, 1)
            END AS percent_change
        FROM with_categories
        ORDER BY month DESC, ABS(total) DESC
        LIMIT 20
    '''
    
    trends = await conn.fetch(query, 1)  # user_id = 1
    
    print("=== Spending Trends (Month over Month) ===")
    for row in trends:
        change = row['percent_change']
        if change is None:
            indicator = "  (first month)"
        elif change > 0:
            indicator = f"  ↑ {change}%"
        elif change < 0:
            indicator = f"  ↓ {abs(change)}%"
        else:
            indicator = "  → 0%"
        
        print(f"{row['month_name']}: {row['category']:<15} ${row['total']:>10.2f}{indicator}")
    
    await conn.close()

asyncio.run(spending_trends())
```
