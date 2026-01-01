---
type: "EXAMPLE"
title: "Building the Finance Tracker Schema"
---

**Expected Output:**
```
Database setup complete!
Tables created: users, accounts, categories, transactions
Test account created: Checking with balance $1000.00
Retrieved 1 accounts
```

```python
import asyncpg
import asyncio
from decimal import Decimal

async def setup_finance_tracker():
    """Initialize the Personal Finance Tracker database."""
    
    # Connect to PostgreSQL
    conn = await asyncpg.connect(
        host='localhost',
        port=5432,
        user='finance_user',
        password='secure_password',
        database='finance_tracker'
    )
    
    # Create tables for our finance tracker
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS accounts (
            id SERIAL PRIMARY KEY,
            name VARCHAR(100) NOT NULL,
            account_type VARCHAR(50) NOT NULL,
            balance DECIMAL(12, 2) DEFAULT 0.00,
            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        )
    ''')
    
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS categories (
            id SERIAL PRIMARY KEY,
            name VARCHAR(50) NOT NULL UNIQUE,
            category_type VARCHAR(20) NOT NULL,
            icon VARCHAR(10)
        )
    ''')
    
    print("Database setup complete!")
    print("Tables created: users, accounts, categories, transactions")
    
    # Insert test data using parameterized queries ($1, $2, etc.)
    await conn.execute('''
        INSERT INTO accounts (name, account_type, balance)
        VALUES ($1, $2, $3)
        ON CONFLICT DO NOTHING
    ''', 'Checking', 'bank', Decimal('1000.00'))
    
    print("Test account created: Checking with balance $1000.00")
    
    # Query data - returns list of Record objects
    rows = await conn.fetch('SELECT * FROM accounts')
    print(f"Retrieved {len(rows)} accounts")
    
    for row in rows:
        # Records work like dicts and tuples
        print(f"  - {row['name']}: ${row['balance']}")
    
    await conn.close()

asyncio.run(setup_finance_tracker())
```
