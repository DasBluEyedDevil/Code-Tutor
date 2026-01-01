---
type: "EXAMPLE"
title: "Creating Tables with JSONB Columns"
---

**Add flexible metadata to the Finance Tracker:**

```python
import asyncpg
import asyncio
import json
from decimal import Decimal

async def setup_jsonb_tables():
    """Create tables with JSONB columns for flexible data."""
    conn = await asyncpg.connect(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Enhanced transactions table with JSONB metadata
    await conn.execute('''
        ALTER TABLE transactions 
        ADD COLUMN IF NOT EXISTS metadata JSONB DEFAULT '{}'::jsonb
    ''')
    
    # User preferences stored as JSONB
    await conn.execute('''
        ALTER TABLE users
        ADD COLUMN IF NOT EXISTS preferences JSONB DEFAULT '{}'::jsonb
    ''')
    
    print("JSONB columns added!")
    
    # Insert transaction with rich metadata
    metadata = {
        "merchant": {
            "name": "Whole Foods",
            "category": "grocery",
            "location": {
                "city": "San Francisco",
                "state": "CA"
            }
        },
        "tags": ["organic", "weekly-shopping", "essentials"],
        "receipt_url": "https://receipts.example.com/abc123",
        "items_count": 15
    }
    
    await conn.execute('''
        INSERT INTO transactions (account_id, amount, description, metadata)
        VALUES ($1, $2, $3, $4)
    ''', 1, Decimal('-127.43'), 'Weekly groceries', json.dumps(metadata))
    
    print("Transaction with metadata inserted!")
    
    # Set user preferences
    preferences = {
        "theme": "dark",
        "currency": "USD",
        "notifications": {
            "email": True,
            "push": False,
            "weekly_summary": True
        },
        "dashboard_widgets": ["spending_chart", "recent_transactions", "budgets"]
    }
    
    await conn.execute('''
        UPDATE users SET preferences = $1 WHERE id = $2
    ''', json.dumps(preferences), 1)
    
    print("User preferences saved!")
    
    await conn.close()

asyncio.run(setup_jsonb_tables())
```
