---
type: "EXAMPLE"
title: "TaskGroup with Error Handling"
---

**Basic TaskGroup pattern:**
```python
import asyncio

async def fetch_all_data():
    results = {}
    
    async with asyncio.TaskGroup() as tg:
        accounts_task = tg.create_task(
            fetch_accounts(),
            name="accounts"
        )
        transactions_task = tg.create_task(
            fetch_transactions(),
            name="transactions"
        )
    
    # If any task fails, all are cancelled
    # and ExceptionGroup is raised
    results["accounts"] = accounts_task.result()
    results["transactions"] = transactions_task.result()
    return results
```

**Handling ExceptionGroup (Python 3.11+):**
```python
try:
    data = await fetch_all_data()
except* HTTPError as eg:
    for exc in eg.exceptions:
        print(f"HTTP error: {exc}")
except* TimeoutError as eg:
    print("Some requests timed out")
```

**The `except*` syntax handles multiple exceptions!**

```python
import asyncio
from dataclasses import dataclass
from typing import Optional

# Simulated API responses
@dataclass
class APIResult:
    endpoint: str
    data: dict
    success: bool = True
    error: Optional[str] = None

async def fetch_accounts():
    """Simulate fetching account data"""
    print("  [accounts] Fetching...")
    await asyncio.sleep(0.3)
    return {"accounts": [{"id": 1, "balance": 2500}, {"id": 2, "balance": 10000}]}

async def fetch_transactions():
    """Simulate fetching transaction data"""
    print("  [transactions] Fetching...")
    await asyncio.sleep(0.2)
    return {"transactions": [{"id": 1, "amount": -45}, {"id": 2, "amount": 3000}]}

async def fetch_budgets():
    """Simulate fetching budget data"""
    print("  [budgets] Fetching...")
    await asyncio.sleep(0.25)
    return {"budgets": [{"category": "Food", "limit": 400, "spent": 245}]}

async def fetch_failing():
    """Simulate a failing API call"""
    print("  [failing] Fetching...")
    await asyncio.sleep(0.1)
    raise ConnectionError("API server unreachable")

async def demo_taskgroup_success():
    """Demo: TaskGroup with all successful tasks"""
    print("=== TaskGroup: All Tasks Succeed ===")
    
    async with asyncio.TaskGroup() as tg:
        accounts = tg.create_task(fetch_accounts(), name="accounts")
        transactions = tg.create_task(fetch_transactions(), name="transactions")
        budgets = tg.create_task(fetch_budgets(), name="budgets")
    
    # All tasks completed successfully
    print("\n  Results:")
    print(f"    Accounts: {accounts.result()}")
    print(f"    Transactions: {transactions.result()}")
    print(f"    Budgets: {budgets.result()}")
    return True

async def demo_taskgroup_failure():
    """Demo: TaskGroup with one failing task"""
    print("\n=== TaskGroup: One Task Fails ===")
    
    try:
        async with asyncio.TaskGroup() as tg:
            accounts = tg.create_task(fetch_accounts(), name="accounts")
            failing = tg.create_task(fetch_failing(), name="failing")
            transactions = tg.create_task(fetch_transactions(), name="transactions")
        
        # Never reached if any task fails
        print("  All tasks completed!")
    except* ConnectionError as eg:
        print(f"\n  Caught {len(eg.exceptions)} ConnectionError(s):")
        for exc in eg.exceptions:
            print(f"    - {exc}")
        print("  (Other tasks were automatically cancelled)")
    except* Exception as eg:
        print(f"\n  Caught {len(eg.exceptions)} other error(s):")
        for exc in eg.exceptions:
            print(f"    - {type(exc).__name__}: {exc}")

async def demo_taskgroup_multiple_failures():
    """Demo: TaskGroup with multiple failing tasks"""
    print("\n=== TaskGroup: Multiple Failures ===")
    
    async def fail_timeout():
        await asyncio.sleep(0.1)
        raise TimeoutError("Request timed out")
    
    async def fail_connection():
        await asyncio.sleep(0.15)
        raise ConnectionError("Server unavailable")
    
    try:
        async with asyncio.TaskGroup() as tg:
            tg.create_task(fail_timeout(), name="timeout_task")
            tg.create_task(fail_connection(), name="connection_task")
            tg.create_task(fetch_accounts(), name="accounts")
    except* TimeoutError as eg:
        print(f"  Timeout errors: {len(eg.exceptions)}")
    except* ConnectionError as eg:
        print(f"  Connection errors: {len(eg.exceptions)}")
    except* Exception as eg:
        print(f"  Other errors: {len(eg.exceptions)}")

async def main():
    print("TaskGroup Demo (Python 3.11+)\n")
    
    await demo_taskgroup_success()
    await demo_taskgroup_failure()
    await demo_taskgroup_multiple_failures()
    
    print("\n=== Key Takeaways ===")
    print("  1. TaskGroup ensures all tasks complete or all are cancelled")
    print("  2. ExceptionGroup captures ALL exceptions that occurred")
    print("  3. Use except* to handle specific exception types")
    print("  4. Named tasks help with debugging")

asyncio.run(main())
```
