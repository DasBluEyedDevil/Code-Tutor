---
type: "EXAMPLE"
title: "asyncio.timeout() Patterns for Finance Tracker"
---

**Basic timeout pattern:**
```python
async def fetch_with_timeout():
    try:
        async with asyncio.timeout(10.0):  # 10 second timeout
            async with httpx.AsyncClient() as client:
                response = await client.get("https://api.example.com/slow")
                return response.json()
    except TimeoutError:
        return None  # Handle gracefully
```

**Combining timeout with TaskGroup:**
```python
async def fetch_all_with_timeout():
    try:
        async with asyncio.timeout(30.0):
            async with asyncio.TaskGroup() as tg:
                task1 = tg.create_task(fetch_accounts())
                task2 = tg.create_task(fetch_transactions())
    except TimeoutError:
        print("Operation timed out - some data may be missing")
```

```python
import asyncio
import time

async def slow_fetch(name: str, delay: float):
    """Simulates a slow API fetch"""
    print(f"  [{name}] Starting (will take {delay}s)...")
    await asyncio.sleep(delay)
    print(f"  [{name}] Completed!")
    return {"name": name, "data": f"Result from {name}"}

async def demo_basic_timeout():
    """Basic asyncio.timeout() usage"""
    print("=== Basic asyncio.timeout() ===")
    
    # This will succeed
    try:
        async with asyncio.timeout(2.0):
            result = await slow_fetch("fast_api", 0.5)
            print(f"  Success: {result}")
    except TimeoutError:
        print("  Timed out!")
    
    print()
    
    # This will timeout
    try:
        async with asyncio.timeout(0.5):
            result = await slow_fetch("slow_api", 2.0)
            print(f"  Success: {result}")
    except TimeoutError:
        print("  Timed out after 0.5s!")

async def demo_multiple_operations():
    """Timeout spanning multiple operations"""
    print("\n=== Timeout for Multiple Operations ===")
    
    try:
        async with asyncio.timeout(2.0):
            # Both operations share the 2s timeout
            result1 = await slow_fetch("accounts", 0.5)
            result2 = await slow_fetch("transactions", 0.5)
            result3 = await slow_fetch("budgets", 0.5)
            print(f"  All completed within timeout!")
    except TimeoutError:
        print("  Operations exceeded 2s total!")

async def demo_timeout_at():
    """Using timeout_at for absolute deadlines"""
    print("\n=== asyncio.timeout_at() for Deadlines ===")
    
    loop = asyncio.get_running_loop()
    deadline = loop.time() + 1.5  # 1.5 seconds from now
    
    print(f"  Deadline set: 1.5s from now")
    
    try:
        async with asyncio.timeout_at(deadline):
            # These run sequentially, sharing the deadline
            await slow_fetch("task1", 0.5)
            await slow_fetch("task2", 0.5)
            await slow_fetch("task3", 0.5)  # Should timeout here
    except TimeoutError:
        print("  Deadline exceeded!")

async def demo_timeout_with_taskgroup():
    """Combining timeout with TaskGroup"""
    print("\n=== Timeout with TaskGroup ===")
    
    try:
        async with asyncio.timeout(1.0):
            async with asyncio.TaskGroup() as tg:
                tg.create_task(slow_fetch("api1", 0.3))
                tg.create_task(slow_fetch("api2", 0.4))
                tg.create_task(slow_fetch("api3", 2.0))  # Too slow!
    except TimeoutError:
        print("  TaskGroup timed out! All tasks cancelled.")
    except* Exception as eg:
        print(f"  Errors: {eg.exceptions}")

async def fetch_finance_data_with_timeout():
    """Real-world pattern: Finance tracker with timeout"""
    print("\n=== Finance Tracker with Timeout ===")
    
    results = {}
    
    try:
        async with asyncio.timeout(2.0):
            # Fetch all data with a 2-second deadline
            accounts = await slow_fetch("accounts", 0.3)
            transactions = await slow_fetch("transactions", 0.4)
            budgets = await slow_fetch("budgets", 0.3)
            
            results = {
                "accounts": accounts,
                "transactions": transactions,
                "budgets": budgets
            }
            print(f"  All data fetched successfully!")
            
    except TimeoutError:
        print("  Finance data fetch timed out!")
        results = {"error": "timeout", "partial_data": results}
    
    return results

async def main():
    print("asyncio.timeout() Demo (Python 3.11+)\n")
    
    await demo_basic_timeout()
    await demo_multiple_operations()
    await demo_timeout_at()
    await demo_timeout_with_taskgroup()
    
    data = await fetch_finance_data_with_timeout()
    print(f"\n  Final result: {list(data.keys())}")

asyncio.run(main())
```
