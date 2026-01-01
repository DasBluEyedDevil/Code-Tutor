import asyncpg
import asyncio

async def category_report():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    query = '''
        WITH monthly_spending AS (
            SELECT 
                DATE_TRUNC('month', transaction_date) AS month,
                category_id,
                SUM(ABS(amount)) AS total
            FROM transactions
            WHERE amount < 0
            GROUP BY ____, category_id
        )
        SELECT 
            TO_CHAR(ms.month, 'Mon YYYY') AS month_name,
            c.name AS category,
            ms.total,
            SUM(ms.total) OVER (
                PARTITION BY ms.category_id
                ORDER BY ms.month
                ____ UNBOUNDED PRECEDING
            ) AS running_total
        FROM monthly_spending ms
        JOIN categories c ON ms.category_id = c.id
        ORDER BY ms.month DESC, ms.total DESC
    '''
    
    results = await conn.fetch(query)
    
    for row in results:
        print(f"{row['month_name']}: {row['category']:<15} "
              f"${row['total']:>8.2f} (Running: ${row['running_total']:>10.2f})")
    
    await conn.close()

asyncio.run(category_report())