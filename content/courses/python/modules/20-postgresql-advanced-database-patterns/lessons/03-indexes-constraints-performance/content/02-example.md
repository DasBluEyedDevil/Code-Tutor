---
type: "EXAMPLE"
title: "Finance Tracker Schema with Constraints"
---

**A production-ready schema with proper constraints:**

```python
import asyncpg
import asyncio

async def create_schema():
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Users table
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS users (
            id SERIAL PRIMARY KEY,
            email VARCHAR(255) UNIQUE NOT NULL,
            password_hash VARCHAR(255) NOT NULL,
            name VARCHAR(100) NOT NULL,
            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            is_active BOOLEAN DEFAULT true
        )
    ''')
    
    # Categories with type constraint
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS categories (
            id SERIAL PRIMARY KEY,
            user_id INTEGER REFERENCES users(id) ON DELETE CASCADE,
            name VARCHAR(50) NOT NULL,
            category_type VARCHAR(20) NOT NULL 
                CHECK (category_type IN ('income', 'expense', 'transfer')),
            icon VARCHAR(10),
            UNIQUE (user_id, name)  -- No duplicate names per user
        )
    ''')
    
    # Accounts with balance constraint
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS accounts (
            id SERIAL PRIMARY KEY,
            user_id INTEGER REFERENCES users(id) ON DELETE CASCADE,
            name VARCHAR(100) NOT NULL,
            account_type VARCHAR(20) NOT NULL
                CHECK (account_type IN ('checking', 'savings', 'credit', 'investment')),
            balance DECIMAL(12, 2) DEFAULT 0.00,
            credit_limit DECIMAL(12, 2),
            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            UNIQUE (user_id, name),
            -- Credit accounts can go negative up to limit
            CHECK (
                account_type != 'credit' 
                OR balance >= -COALESCE(credit_limit, 0)
            )
        )
    ''')
    
    # Transactions with referential integrity
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS transactions (
            id SERIAL PRIMARY KEY,
            user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
            account_id INTEGER NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
            category_id INTEGER REFERENCES categories(id) ON DELETE SET NULL,
            amount DECIMAL(12, 2) NOT NULL CHECK (amount != 0),
            description TEXT,
            transaction_date DATE NOT NULL DEFAULT CURRENT_DATE,
            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        )
    ''')
    
    print("Schema created with all constraints!")
    await conn.close()

asyncio.run(create_schema())
```
