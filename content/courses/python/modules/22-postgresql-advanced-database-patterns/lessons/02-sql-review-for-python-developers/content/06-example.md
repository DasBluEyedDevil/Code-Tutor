---
type: "EXAMPLE"
title: "Monthly Spending by Category"
---

**Aggregate spending for the Finance Tracker dashboard:**

```python
import asyncpg
import asyncio

async def monthly_spending_report():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    query = '''
        SELECT 
            c.name AS category,
            c.icon,
            COUNT(*) AS transaction_count,
            SUM(ABS(t.amount)) AS total_spent,
            AVG(ABS(t.amount))::DECIMAL(10,2) AS avg_transaction,
            MIN(t.transaction_date) AS first_tx,
            MAX(t.transaction_date) AS last_tx
        FROM transactions t
        JOIN categories c ON t.category_id = c.id
        WHERE 
            t.amount < 0  -- Only expenses
            AND t.transaction_date >= DATE_TRUNC('month', CURRENT_DATE)
        GROUP BY c.id, c.name, c.icon
        HAVING SUM(ABS(t.amount)) > 10  -- Ignore tiny categories
        ORDER BY total_spent DESC
    '''
    
    categories = await conn.fetch(query)
    
    print("=== This Month's Spending by Category ===")
    print(f"{'Category':<20} {'Count':>6} {'Total':>12} {'Average':>10}")
    print("-" * 50)
    
    total = 0
    for cat in categories:
        icon = cat['icon'] or '📦'
        print(f"{icon} {cat['category']:<17} {cat['transaction_count']:>6} "
              f"${cat['total_spent']:>10.2f} ${cat['avg_transaction']:>8.2f}")
        total += cat['total_spent']
    
    print("-" * 50)
    print(f"{'Total':<20} {'':<6} ${total:>10.2f}")
    
    await conn.close()

asyncio.run(monthly_spending_report())
```
