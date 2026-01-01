---
type: "EXAMPLE"
title: "Retry Pattern with TaskGroup"
---

**Common pattern: Fetch with automatic retry**

Combine TaskGroup with exponential backoff for robust API calls:

```python
async def fetch_with_retry(url: str, retries: int = 3):
    """Fetch with automatic retry on failure."""
    async with httpx.AsyncClient() as client:
        for attempt in range(retries):
            try:
                response = await client.get(url)
                response.raise_for_status()
                return response.json()
            except httpx.HTTPError as e:
                if attempt == retries - 1:
                    raise  # Final attempt failed
                # Exponential backoff: 1s, 2s, 4s...
                await asyncio.sleep(2 ** attempt)
```

**Use with TaskGroup for parallel fetches:**
```python
async with asyncio.TaskGroup() as tg:
    accounts = tg.create_task(
        fetch_with_retry("https://api.example.com/accounts")
    )
    transactions = tg.create_task(
        fetch_with_retry("https://api.example.com/transactions")
    )
```

```python
import asyncio
import random

class MockHTTPError(Exception):
    """Simulates an HTTP error"""
    pass

async def fetch_with_retry(endpoint: str, retries: int = 3):
    """Fetch with automatic retry and exponential backoff."""
    
    for attempt in range(retries):
        try:
            print(f"  [{endpoint}] Attempt {attempt + 1}/{retries}...")
            await asyncio.sleep(0.2)  # Simulate network delay
            
            # Simulate random failures (50% chance on first attempt)
            if attempt == 0 and random.random() < 0.5:
                raise MockHTTPError(f"{endpoint}: Server error 503")
            
            # Success!
            return {"endpoint": endpoint, "data": f"Data from {endpoint}"}
            
        except MockHTTPError as e:
            if attempt == retries - 1:
                print(f"  [{endpoint}] All retries exhausted!")
                raise
            
            backoff = 2 ** attempt * 0.1  # 0.1s, 0.2s, 0.4s
            print(f"  [{endpoint}] Failed, retrying in {backoff}s...")
            await asyncio.sleep(backoff)

async def fetch_all_with_retry():
    """Fetch multiple endpoints with retry logic."""
    print("=== Fetch with Retry Pattern ===")
    print("(Each endpoint has 50% chance of initial failure)\n")
    
    results = {}
    
    try:
        async with asyncio.TaskGroup() as tg:
            accounts_task = tg.create_task(
                fetch_with_retry("accounts"),
                name="accounts"
            )
            transactions_task = tg.create_task(
                fetch_with_retry("transactions"),
                name="transactions"
            )
            budgets_task = tg.create_task(
                fetch_with_retry("budgets"),
                name="budgets"
            )
        
        results["accounts"] = accounts_task.result()
        results["transactions"] = transactions_task.result()
        results["budgets"] = budgets_task.result()
        
        print("\n=== All Fetches Succeeded ===")
        for key, value in results.items():
            print(f"  {key}: {value}")
        
    except* MockHTTPError as eg:
        print(f"\n=== Some Fetches Failed ===")
        print(f"  {len(eg.exceptions)} endpoint(s) failed after retries:")
        for exc in eg.exceptions:
            print(f"    - {exc}")

async def main():
    # Run multiple times to see both success and failure
    random.seed(42)  # For reproducible demo
    await fetch_all_with_retry()

print("Retry Pattern with TaskGroup\n")
asyncio.run(main())
```
