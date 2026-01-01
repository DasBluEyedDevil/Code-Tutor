---
type: "EXAMPLE"
title: "Finance Tracker: Transaction Report with JOINs"
---

**Generate a transaction report joining accounts and categories:**

```python
import asyncpg
import asyncio

async def transaction_report():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Join transactions with accounts and categories
    query = '''
        SELECT 
            t.id,
            t.amount,
            t.description,
            t.transaction_date,
            a.name AS account_name,
            c.name AS category_name,
            c.icon AS category_icon
        FROM transactions t
        INNER JOIN accounts a ON t.account_id = a.id
        LEFT JOIN categories c ON t.category_id = c.id
        WHERE t.transaction_date >= CURRENT_DATE - INTERVAL '30 days'
        ORDER BY t.transaction_date DESC
        LIMIT 10
    '''
    
    transactions = await conn.fetch(query)
    
    print("=== Last 30 Days Transactions ===")
    for tx in transactions:
        icon = tx['category_icon'] or '📝'
        print(f"{icon} {tx['account_name']}: ${tx['amount']:>10.2f} - {tx['category_name']}")
        print(f"   {tx['description']}")
    
    await conn.close()

asyncio.run(transaction_report())
```
