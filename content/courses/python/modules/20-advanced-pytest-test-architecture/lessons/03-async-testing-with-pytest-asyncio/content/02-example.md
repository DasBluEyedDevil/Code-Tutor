---
type: "EXAMPLE"
title: "Auto Mode: Clean Async Tests"
---

With `asyncio_mode = "auto"`, async tests just work - no decorators needed.

**pyproject.toml configuration:**
```toml
[tool.pytest.ini_options]
asyncio_mode = "auto"
```

```python
# test_finance_async.py
# With auto mode, no @pytest.mark.asyncio needed!
import pytest
import asyncio
from decimal import Decimal

# Finance Tracker: Async transaction fetching
async def fetch_transactions(account_id: int, limit: int = 10) -> list[dict]:
    """Simulate fetching transactions from async database."""
    await asyncio.sleep(0.01)  # Simulate I/O
    return [
        {"id": i, "account_id": account_id, "amount": Decimal("50.00") * i}
        for i in range(1, limit + 1)
    ]

async def calculate_balance(transactions: list[dict]) -> Decimal:
    """Calculate balance from transactions."""
    await asyncio.sleep(0.001)  # Simulate processing
    return sum(tx["amount"] for tx in transactions)

# Test 1: Basic async test - just define as async!
async def test_fetch_transactions():
    transactions = await fetch_transactions(account_id=1, limit=5)
    assert len(transactions) == 5
    assert all(tx["account_id"] == 1 for tx in transactions)

# Test 2: Async fixture - also just works
@pytest.fixture
async def sample_transactions():
    """Fixture that fetches test data asynchronously."""
    return await fetch_transactions(account_id=42, limit=3)

async def test_with_async_fixture(sample_transactions):
    assert len(sample_transactions) == 3
    balance = await calculate_balance(sample_transactions)
    assert balance == Decimal("300.00")  # 50 + 100 + 150

# Test 3: Concurrent operations
async def test_concurrent_account_fetches():
    """Fetch multiple accounts in parallel."""
    results = await asyncio.gather(
        fetch_transactions(account_id=1, limit=2),
        fetch_transactions(account_id=2, limit=3),
        fetch_transactions(account_id=3, limit=1),
    )
    assert len(results) == 3
    assert len(results[0]) == 2
    assert len(results[1]) == 3
    assert len(results[2]) == 1

# Test 4: Exception handling
async def test_async_error_handling():
    async def validate_amount(amount: Decimal) -> None:
        await asyncio.sleep(0.001)
        if amount <= 0:
            raise ValueError("Amount must be positive")
    
    with pytest.raises(ValueError, match="must be positive"):
        await validate_amount(Decimal("-50.00"))

```
