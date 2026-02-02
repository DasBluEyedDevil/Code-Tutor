import asyncio
from decimal import Decimal

async def fetch_accounts():
    await asyncio.sleep(0.1)
    return [{"id": 1, "name": "Checking", "balance": Decimal("1500.00")}]

async def fetch_transactions():
    await asyncio.sleep(0.1)
    return [{"id": 1, "amount": Decimal("-50.00"), "category": "groceries"}]

async def fetch_budgets():
    await asyncio.sleep(0.1)
    return [{"category": "groceries", "limit": Decimal("500.00")}]

async def load_dashboard():
    """Load all finance data concurrently."""
    try:
        async with asyncio.____() as tg:
            accounts_task = tg.____(fetch_accounts())
            transactions_task = tg.____(fetch_transactions())
            budgets_task = tg.____(fetch_budgets())
        
        return {
            "accounts": accounts_task.____(),
            "transactions": transactions_task.____(),
            "budgets": budgets_task.____()
        }
    except* ValueError as eg:
        print(f"Validation errors: {len(eg.exceptions)}")
        return None
    except* ConnectionError as eg:
        print(f"Network errors: {len(eg.exceptions)}")
        return None

# Test it
data = asyncio.run(load_dashboard())
print(f"Loaded {len(data['accounts'])} accounts")
