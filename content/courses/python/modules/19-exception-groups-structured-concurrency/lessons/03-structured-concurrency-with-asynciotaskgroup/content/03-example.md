---
type: "EXAMPLE"
title: "Handling Multiple Exception Types"
---

When fetching financial data, different types of errors can occur simultaneously. The `except*` syntax lets you handle each type appropriately.

**Expected Output:**
```
Data fetch errors:
  Validation errors: 1 - will retry with defaults
  Network errors: 1 - will use cached data
```

```python
import asyncio
from decimal import Decimal

class ValidationError(Exception):
    """Invalid data format from API."""
    pass

async def fetch_accounts_flaky():
    await asyncio.sleep(0.1)
    raise ConnectionError("API server unreachable")

async def fetch_transactions_flaky():
    await asyncio.sleep(0.1)
    raise ValidationError("Invalid transaction format in response")

async def fetch_budgets_ok():
    await asyncio.sleep(0.1)
    return [{"category": "groceries", "limit": 500}]

async def fetch_finance_data_with_errors():
    """Demonstrates handling multiple exception types."""
    try:
        async with asyncio.TaskGroup() as tg:
            tg.create_task(fetch_accounts_flaky())
            tg.create_task(fetch_transactions_flaky())
            tg.create_task(fetch_budgets_ok())
    except* ValueError as eg:
        print(f"Validation errors: {len(eg.exceptions)} - will retry with defaults")
    except* ConnectionError as eg:
        print(f"Network errors: {len(eg.exceptions)} - will use cached data")
    except* ValidationError as eg:
        print(f"Data format errors: {len(eg.exceptions)} - logging for review")

# Run the example
asyncio.run(fetch_finance_data_with_errors())

```
