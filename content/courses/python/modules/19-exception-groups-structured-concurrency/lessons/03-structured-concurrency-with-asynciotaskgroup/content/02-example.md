---
type: "EXAMPLE"
title: "Finance Tracker: Concurrent Data Fetching"
---

**Real-world scenario:** Loading a financial dashboard requires fetching multiple data sources. TaskGroup ensures all fetches complete together, and any failures are collected in an ExceptionGroup.

**Expected Output (all succeed):**
```
Fetched accounts: 3 items
Fetched transactions: 150 items  
Fetched budgets: 5 items
Dashboard data ready!
```

**Expected Output (partial failure):**
```
Fetched accounts: 3 items
Data fetch errors:
  Validation errors: 1
  Network errors: 1
```

```python
import asyncio
from decimal import Decimal
from dataclasses import dataclass

@dataclass
class Account:
    id: int
    name: str
    balance: Decimal

@dataclass
class Transaction:
    id: int
    amount: Decimal
    category: str

@dataclass
class Budget:
    category: str
    limit: Decimal
    spent: Decimal

# Simulated async API calls
async def fetch_accounts() -> list[Account]:
    await asyncio.sleep(0.1)  # Simulate API latency
    return [
        Account(1, "Checking", Decimal("2500.00")),
        Account(2, "Savings", Decimal("10000.00")),
        Account(3, "Credit Card", Decimal("-450.00")),
    ]

async def fetch_transactions() -> list[Transaction]:
    await asyncio.sleep(0.15)
    return [Transaction(i, Decimal("50.00"), "groceries") for i in range(150)]

async def fetch_budgets() -> list[Budget]:
    await asyncio.sleep(0.08)
    return [
        Budget("groceries", Decimal("500.00"), Decimal("320.00")),
        Budget("dining", Decimal("200.00"), Decimal("180.00")),
    ]

async def fetch_finance_data():
    """Fetch multiple financial data sources concurrently."""
    async with asyncio.TaskGroup() as tg:
        accounts_task = tg.create_task(fetch_accounts())
        transactions_task = tg.create_task(fetch_transactions())
        budgets_task = tg.create_task(fetch_budgets())
    
    # All tasks completed - only reached if ALL succeed
    return {
        "accounts": accounts_task.result(),
        "transactions": transactions_task.result(),
        "budgets": budgets_task.result()
    }

# Success case
async def main():
    try:
        data = await fetch_finance_data()
        print(f"Fetched accounts: {len(data['accounts'])} items")
        print(f"Fetched transactions: {len(data['transactions'])} items")
        print(f"Fetched budgets: {len(data['budgets'])} items")
        print("Dashboard data ready!")
    except* ValueError as eg:
        print(f"Validation errors: {len(eg.exceptions)}")
    except* ConnectionError as eg:
        print(f"Network errors: {len(eg.exceptions)}")

asyncio.run(main())

```
