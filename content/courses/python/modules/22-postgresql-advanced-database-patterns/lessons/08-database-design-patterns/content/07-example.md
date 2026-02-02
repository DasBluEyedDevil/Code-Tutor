---
type: "EXAMPLE"
title: "Finance Tracker Complete Schema"
---

**Production-ready schema with all patterns:**

```python
import asyncpg
import asyncio

async def create_complete_schema():
    """Create the complete Finance Tracker schema."""
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    schema = '''
    -- Users table with preferences
    CREATE TABLE IF NOT EXISTS users (
        id SERIAL PRIMARY KEY,
        email VARCHAR(255) UNIQUE NOT NULL,
        password_hash VARCHAR(255) NOT NULL,
        name VARCHAR(100) NOT NULL,
        preferences JSONB DEFAULT '{}',
        
        -- Audit fields
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        last_login_at TIMESTAMP,
        
        -- Soft delete
        deleted_at TIMESTAMP DEFAULT NULL,
        is_active BOOLEAN DEFAULT true
    );

    -- Categories (per-user or global)
    CREATE TABLE IF NOT EXISTS categories (
        id SERIAL PRIMARY KEY,
        user_id INTEGER REFERENCES users(id) ON DELETE CASCADE,
        name VARCHAR(50) NOT NULL,
        category_type VARCHAR(20) CHECK (category_type IN ('income', 'expense', 'transfer')),
        icon VARCHAR(10),
        color VARCHAR(7),  -- Hex color
        
        -- Budget settings
        monthly_budget DECIMAL(12, 2),
        
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        deleted_at TIMESTAMP DEFAULT NULL,
        
        UNIQUE (user_id, name) WHERE deleted_at IS NULL
    );

    -- Accounts (bank accounts, credit cards, etc.)
    CREATE TABLE IF NOT EXISTS accounts (
        id SERIAL PRIMARY KEY,
        user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
        name VARCHAR(100) NOT NULL,
        account_type VARCHAR(30) CHECK (account_type IN 
            ('checking', 'savings', 'credit_card', 'investment', 'cash', 'other')),
        institution VARCHAR(100),
        balance DECIMAL(12, 2) DEFAULT 0.00,
        currency VARCHAR(3) DEFAULT 'USD',
        
        -- Credit card specific
        credit_limit DECIMAL(12, 2),
        due_day INTEGER CHECK (due_day BETWEEN 1 AND 31),
        
        -- Display settings
        icon VARCHAR(10),
        color VARCHAR(7),
        display_order INTEGER DEFAULT 0,
        is_hidden BOOLEAN DEFAULT false,
        
        -- Audit
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        deleted_at TIMESTAMP DEFAULT NULL,
        
        UNIQUE (user_id, name) WHERE deleted_at IS NULL
    );

    -- Transactions with comprehensive fields
    CREATE TABLE IF NOT EXISTS transactions (
        id SERIAL PRIMARY KEY,
        user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
        account_id INTEGER NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
        category_id INTEGER REFERENCES categories(id) ON DELETE SET NULL,
        
        -- Transaction details
        amount DECIMAL(12, 2) NOT NULL CHECK (amount != 0),
        description TEXT,
        notes TEXT,
        transaction_date DATE NOT NULL DEFAULT CURRENT_DATE,
        
        -- Transfer support
        transfer_account_id INTEGER REFERENCES accounts(id),
        transfer_transaction_id INTEGER REFERENCES transactions(id),
        
        -- Metadata (merchant info, receipt, tags)
        metadata JSONB DEFAULT '{}',
        tags TEXT[],
        
        -- Reconciliation
        is_reconciled BOOLEAN DEFAULT false,
        reconciled_at TIMESTAMP,
        
        -- Recurring transaction reference
        recurring_id INTEGER,
        
        -- Full-text search
        search_vector tsvector GENERATED ALWAYS AS (
            setweight(to_tsvector('english', COALESCE(description, '')), 'A') ||
            setweight(to_tsvector('english', COALESCE(notes, '')), 'B')
        ) STORED,
        
        -- Audit
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        deleted_at TIMESTAMP DEFAULT NULL
    );

    -- Budgets
    CREATE TABLE IF NOT EXISTS budgets (
        id SERIAL PRIMARY KEY,
        user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
        category_id INTEGER REFERENCES categories(id) ON DELETE CASCADE,
        
        amount DECIMAL(12, 2) NOT NULL,
        period VARCHAR(20) DEFAULT 'monthly' CHECK (period IN ('weekly', 'monthly', 'yearly')),
        start_date DATE NOT NULL,
        end_date DATE,
        
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        deleted_at TIMESTAMP DEFAULT NULL
    );

    -- Recurring transactions template
    CREATE TABLE IF NOT EXISTS recurring_transactions (
        id SERIAL PRIMARY KEY,
        user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
        account_id INTEGER NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
        category_id INTEGER REFERENCES categories(id),
        
        amount DECIMAL(12, 2) NOT NULL,
        description TEXT,
        
        frequency VARCHAR(20) CHECK (frequency IN ('daily', 'weekly', 'biweekly', 'monthly', 'yearly')),
        next_date DATE NOT NULL,
        end_date DATE,
        
        is_active BOOLEAN DEFAULT true,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

    -- Create indexes
    CREATE INDEX IF NOT EXISTS idx_transactions_user_date 
        ON transactions(user_id, transaction_date DESC) WHERE deleted_at IS NULL;
    CREATE INDEX IF NOT EXISTS idx_transactions_account 
        ON transactions(account_id, transaction_date DESC) WHERE deleted_at IS NULL;
    CREATE INDEX IF NOT EXISTS idx_transactions_category 
        ON transactions(category_id) WHERE deleted_at IS NULL;
    CREATE INDEX IF NOT EXISTS idx_transactions_search 
        ON transactions USING GIN(search_vector);
    CREATE INDEX IF NOT EXISTS idx_transactions_tags 
        ON transactions USING GIN(tags);
    CREATE INDEX IF NOT EXISTS idx_transactions_metadata 
        ON transactions USING GIN(metadata jsonb_path_ops);
    ''';
    
    await conn.execute(schema)
    print("Complete Finance Tracker schema created!")
    
    await conn.close()

asyncio.run(create_complete_schema())
```
