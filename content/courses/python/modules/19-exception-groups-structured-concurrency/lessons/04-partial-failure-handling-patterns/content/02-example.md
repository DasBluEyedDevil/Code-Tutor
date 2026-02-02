---
type: "EXAMPLE"
title: "Finance Tracker: Syncing Multiple Bank Accounts"
---

**Real-world scenario:** When syncing multiple bank accounts, some may fail (expired credentials, API downtime) while others succeed. The user wants to see all synced data AND know which accounts failed.

**Expected Output:**
```
Successfully synced 3 accounts:
  - Checking: $2,500.00
  - Savings: $10,000.00
  - Investment: $45,000.00
Failed to sync 2 accounts:
  - Credit Union: API timeout
  - Brokerage: Authentication expired
```

```python
import asyncio
from decimal import Decimal
from dataclasses import dataclass

@dataclass
class AccountSync:
    name: str
    balance: Decimal | None = None
    error: str | None = None
    
    @property
    def success(self) -> bool:
        return self.error is None

class SyncError(Exception):
    def __init__(self, account: str, reason: str):
        self.account = account
        self.reason = reason
        super().__init__(f"{account}: {reason}")

async def sync_account(name: str, should_fail: bool = False) -> AccountSync:
    """Simulate syncing a bank account."""
    await asyncio.sleep(0.1)  # API latency
    
    if should_fail:
        raise SyncError(name, "API timeout" if "Union" in name else "Authentication expired")
    
    # Simulate fetched balances
    balances = {
        "Checking": Decimal("2500.00"),
        "Savings": Decimal("10000.00"),
        "Investment": Decimal("45000.00"),
    }
    return AccountSync(name=name, balance=balances.get(name, Decimal("0")))

async def sync_all_accounts_safe(accounts: list[tuple[str, bool]]):
    """Sync accounts with partial failure handling."""
    successes: list[AccountSync] = []
    failures: list[SyncError] = []
    
    async def safe_sync(name: str, should_fail: bool):
        try:
            result = await sync_account(name, should_fail)
            return ("success", result)
        except SyncError as e:
            return ("failure", e)
    
    async with asyncio.TaskGroup() as tg:
        tasks = [tg.create_task(safe_sync(name, fail)) for name, fail in accounts]
    
    for task in tasks:
        status, result = task.result()
        if status == "success":
            successes.append(result)
        else:
            failures.append(result)
    
    # Report results
    if successes:
        print(f"Successfully synced {len(successes)} accounts:")
        for acc in successes:
            print(f"  - {acc.name}: ${acc.balance:,.2f}")
    
    if failures:
        print(f"Failed to sync {len(failures)} accounts:")
        for err in failures:
            print(f"  - {err.account}: {err.reason}")
    
    return successes, failures

# Sync 5 accounts - 2 will fail
asyncio.run(sync_all_accounts_safe([
    ("Checking", False),
    ("Savings", False),
    ("Credit Union", True),   # Will fail
    ("Investment", False),
    ("Brokerage", True),      # Will fail
]))

```
