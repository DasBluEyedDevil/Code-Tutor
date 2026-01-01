import asyncio
from dataclasses import dataclass
from typing import Optional, Dict, Any

@dataclass
class FetchResult:
    endpoint: str
    data: Optional[Dict[str, Any]] = None
    error: Optional[str] = None

async def fetch_accounts():
    print("  [accounts] Fetching...")
    await asyncio.sleep(0.2)
    return {"accounts": [{"id": 1, "balance": 2500}]}

async def fetch_transactions():
    print("  [transactions] Fetching...")
    await asyncio.sleep(0.15)
    # Simulate occasional failure
    if False:  # Change to True to test error handling
        raise ConnectionError("Transaction API unavailable")
    return {"transactions": [{"id": 1, "amount": -45}]}

async def fetch_budgets():
    print("  [budgets] Fetching...")
    await asyncio.sleep(0.25)
    return {"budgets": [{"category": "Food", "limit": 400}]}

async def fetch_all_finance_data() -> Dict[str, FetchResult]:
    """Fetch all finance data using TaskGroup."""
    results = {
        "accounts": FetchResult(endpoint="accounts"),
        "transactions": FetchResult(endpoint="transactions"),
        "budgets": FetchResult(endpoint="budgets")
    }
    
    try:
        async with asyncio.TaskGroup() as tg:
            accounts_task = tg.create_task(fetch_accounts(), name="accounts")
            transactions_task = tg.create_task(fetch_transactions(), name="transactions")
            budgets_task = tg.create_task(fetch_budgets(), name="budgets")
        
        # All succeeded
        results["accounts"].data = accounts_task.result()
        results["transactions"].data = transactions_task.result()
        results["budgets"].data = budgets_task.result()
        
    except* ConnectionError as eg:
        # Handle connection errors
        for exc in eg.exceptions:
            # Find which endpoint failed based on error message
            error_msg = str(exc)
            if "Transaction" in error_msg:
                results["transactions"].error = error_msg
            elif "Account" in error_msg:
                results["accounts"].error = error_msg
            elif "Budget" in error_msg:
                results["budgets"].error = error_msg
            else:
                results["unknown"] = FetchResult(endpoint="unknown", error=error_msg)
    
    except* Exception as eg:
        # Handle other errors
        for exc in eg.exceptions:
            print(f"  Unexpected error: {type(exc).__name__}: {exc}")
    
    return results

async def main():
    print("=== Finance Data Fetcher with TaskGroup ===")
    print("Fetching all data concurrently...\n")
    
    results = await fetch_all_finance_data()
    
    print("\n=== Results ===")
    for endpoint, result in results.items():
        if result.error:
            print(f"  {endpoint}: ERROR - {result.error}")
        else:
            print(f"  {endpoint}: {result.data}")

asyncio.run(main())