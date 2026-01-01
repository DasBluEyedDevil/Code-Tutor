---
type: "EXAMPLE"
title: "High-Performance Batch Operations"
---

**Optimized bulk insert for the Finance Tracker:**

```python
import asyncpg
import asyncio
from decimal import Decimal
from datetime import date, timedelta
import random
import time

async def benchmark_inserts():
    """Compare different insert strategies."""
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Generate test data
    num_records = 1000
    test_data = [
        (
            1,  # user_id
            1,  # account_id
            Decimal(random.uniform(-500, 500)).quantize(Decimal('0.01')),
            f'Transaction {i}',
            date.today() - timedelta(days=random.randint(0, 365))
        )
        for i in range(num_records)
    ]
    
    # Create temp table for testing
    await conn.execute('''
        CREATE TEMP TABLE bench_transactions (
            id SERIAL PRIMARY KEY,
            user_id INTEGER,
            account_id INTEGER,
            amount DECIMAL(12,2),
            description TEXT,
            tx_date DATE
        )
    ''')
    
    # Method 1: Individual inserts (slow)
    start = time.perf_counter()
    for row in test_data[:100]:  # Only 100 for speed
        await conn.execute('''
            INSERT INTO bench_transactions 
            (user_id, account_id, amount, description, tx_date)
            VALUES ($1, $2, $3, $4, $5)
        ''', *row)
    individual_time = time.perf_counter() - start
    print(f"Individual inserts (100 rows): {individual_time*1000:.1f}ms")
    
    # Method 2: executemany (better)
    await conn.execute('TRUNCATE bench_transactions')
    start = time.perf_counter()
    await conn.executemany('''
        INSERT INTO bench_transactions 
        (user_id, account_id, amount, description, tx_date)
        VALUES ($1, $2, $3, $4, $5)
    ''', test_data)
    executemany_time = time.perf_counter() - start
    print(f"executemany ({num_records} rows): {executemany_time*1000:.1f}ms")
    
    # Method 3: COPY (fastest)
    await conn.execute('TRUNCATE bench_transactions')
    start = time.perf_counter()
    await conn.copy_records_to_table(
        'bench_transactions',
        records=test_data,
        columns=['user_id', 'account_id', 'amount', 'description', 'tx_date']
    )
    copy_time = time.perf_counter() - start
    print(f"COPY ({num_records} rows): {copy_time*1000:.1f}ms")
    
    # Speedup comparison
    print(f"\nSpeedup vs individual inserts:")
    print(f"  executemany: {(individual_time/executemany_time)*10:.1f}x faster")
    print(f"  COPY: {(individual_time/copy_time)*10:.1f}x faster")
    
    await conn.close()

asyncio.run(benchmark_inserts())
```
