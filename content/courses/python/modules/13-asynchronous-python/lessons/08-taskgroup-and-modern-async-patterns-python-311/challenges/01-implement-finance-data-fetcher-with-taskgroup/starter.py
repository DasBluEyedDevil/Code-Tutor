import asyncio
from dataclasses import dataclass
from typing import Optional, Dict, Any

@dataclass
class FetchResult:
    endpoint: str
    data: Optional[Dict[str, Any]] = None
    error: Optional[str] = None

async def fetch_accounts():
    await asyncio.sleep(0.2)
    return {"accounts": [{"id": 1, "balance": 2500}]}

async def fetch_transactions():
    await asyncio.sleep(0.15)
    # Simulate occasional failure
    if False:  # Change to True to test error handling
        raise ConnectionError("Transaction API unavailable")
    return {"transactions": [{"id": 1, "amount": -45}]}

async def fetch_budgets():
    await asyncio.sleep(0.25)
    return {"budgets": [{"category": "Food", "limit": 400}]}

async def fetch_all_finance_data() -> Dict[str, FetchResult]:
    """Fetch all finance data using TaskGroup.
    
    Returns dict with results for each endpoint.
    If an endpoint fails, its result contains the error.
    """
    results = {}
    
    # TODO: Use asyncio.TaskGroup to fetch all data
    # TODO: Handle ExceptionGroup if any tasks fail
    # TODO: Return results dict with FetchResult for each endpoint
    
    pass

async def main():
    print("=== Finance Data Fetcher ===")
    results = await fetch_all_finance_data()
    
    for endpoint, result in results.items():
        if result.error:
            print(f"  {endpoint}: ERROR - {result.error}")
        else:
            print(f"  {endpoint}: {result.data}")

asyncio.run(main())