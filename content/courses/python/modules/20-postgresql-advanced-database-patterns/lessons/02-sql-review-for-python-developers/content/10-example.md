---
type: "EXAMPLE"
title: "Running Balance with Window Functions"
---

**Calculate running balance for each account:**

```python
import asyncpg
import asyncio

async def running_balance():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    query = '''
        SELECT 
            t.transaction_date,
            t.description,
            t.amount,
            -- Running balance for this account
            SUM(t.amount) OVER (
                PARTITION BY t.account_id
                ORDER BY t.transaction_date, t.id
                ROWS UNBOUNDED PRECEDING
            ) AS running_balance,
            -- Transaction rank within the month
            ROW_NUMBER() OVER (
                PARTITION BY DATE_TRUNC('month', t.transaction_date)
                ORDER BY ABS(t.amount) DESC
            ) AS amount_rank_in_month,
            -- Previous transaction amount
            LAG(t.amount) OVER (
                PARTITION BY t.account_id
                ORDER BY t.transaction_date, t.id
            ) AS prev_amount
        FROM transactions t
        WHERE t.account_id = $1
        ORDER BY t.transaction_date DESC, t.id DESC
        LIMIT 15
    '''
    
    transactions = await conn.fetch(query, 1)  # account_id = 1
    
    print("=== Account Transaction History ===")
    print(f"{'Date':<12} {'Description':<20} {'Amount':>10} {'Balance':>12} {'Rank':>5}")
    print("-" * 65)
    
    for tx in transactions:
        print(f"{tx['transaction_date']} {tx['description']:<20} "
              f"${tx['amount']:>9.2f} ${tx['running_balance']:>11.2f} "
              f"#{tx['amount_rank_in_month']}")
    
    await conn.close()

asyncio.run(running_balance())
```
