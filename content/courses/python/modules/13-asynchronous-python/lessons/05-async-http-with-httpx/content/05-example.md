---
type: "EXAMPLE"
title: "Personal Finance Tracker: Fetching Multiple APIs"
---

**Real-world pattern: Aggregating financial data**

When building a finance tracker, you often need to fetch data from multiple sources:
- Account balances
- Recent transactions
- Budget status
- Exchange rates

**With httpx, fetch them all concurrently:**
```python
import httpx
import asyncio

async def fetch_finance_data():
    """Fetch data from multiple financial APIs concurrently."""
    async with httpx.AsyncClient(timeout=30.0) as client:
        # Concurrent requests with gather
        responses = await asyncio.gather(
            client.get("https://api.example.com/accounts"),
            client.get("https://api.example.com/transactions"),
            client.get("https://api.example.com/budgets"),
        )
        
        accounts, transactions, budgets = [r.json() for r in responses]
        return {
            "accounts": accounts,
            "transactions": transactions,
            "budgets": budgets
        }
```

**httpx advantages over requests:**
- Native async support
- HTTP/2 support
- Same API as requests (easy migration)
- Better timeout handling
- Connection pooling built-in

```python
import asyncio

# Simulating a Personal Finance Tracker API client
class FinanceAPIClient:
    """Mock async client for finance APIs"""
    
    def __init__(self, timeout: float = 30.0):
        self.timeout = timeout
        self._mock_data = {
            "accounts": [
                {"id": 1, "name": "Checking", "balance": 2500.00},
                {"id": 2, "name": "Savings", "balance": 10000.00}
            ],
            "transactions": [
                {"id": 1, "amount": -45.00, "category": "Groceries"},
                {"id": 2, "amount": -12.99, "category": "Entertainment"},
                {"id": 3, "amount": 3000.00, "category": "Income"}
            ],
            "budgets": [
                {"category": "Groceries", "limit": 400, "spent": 245},
                {"category": "Entertainment", "limit": 100, "spent": 62}
            ]
        }
    
    async def __aenter__(self):
        print(f"  [Client] Connected (timeout: {self.timeout}s)")
        return self
    
    async def __aexit__(self, *args):
        print("  [Client] Connection closed")
    
    async def get(self, url: str):
        """Simulate async GET request"""
        endpoint = url.split("/")[-1]
        print(f"  GET /{endpoint}...")
        await asyncio.sleep(0.2)  # Simulate network latency
        return MockResponse(self._mock_data.get(endpoint, {}))

class MockResponse:
    def __init__(self, data):
        self._data = data
        self.status_code = 200
    
    def json(self):
        return self._data
    
    def raise_for_status(self):
        pass

async def fetch_finance_data():
    """Fetch all finance data concurrently."""
    async with FinanceAPIClient(timeout=30.0) as client:
        # Fetch all data concurrently
        responses = await asyncio.gather(
            client.get("https://api.example.com/accounts"),
            client.get("https://api.example.com/transactions"),
            client.get("https://api.example.com/budgets"),
        )
        
        accounts, transactions, budgets = [r.json() for r in responses]
        
        return {
            "accounts": accounts,
            "transactions": transactions,
            "budgets": budgets
        }

async def main():
    print("=== Personal Finance Tracker ===")
    print("Fetching data from multiple APIs concurrently...\n")
    
    import time
    start = time.time()
    
    data = await fetch_finance_data()
    
    elapsed = time.time() - start
    print(f"\n=== Results (fetched in {elapsed:.2f}s) ===")
    
    # Display accounts
    print("\nAccounts:")
    total_balance = 0
    for acc in data["accounts"]:
        print(f"  {acc['name']}: ${acc['balance']:,.2f}")
        total_balance += acc['balance']
    print(f"  Total: ${total_balance:,.2f}")
    
    # Display recent transactions
    print("\nRecent Transactions:")
    for tx in data["transactions"][:3]:
        sign = "+" if tx['amount'] > 0 else ""
        print(f"  {tx['category']}: {sign}${tx['amount']:,.2f}")
    
    # Display budget status
    print("\nBudget Status:")
    for budget in data["budgets"]:
        remaining = budget['limit'] - budget['spent']
        pct = (budget['spent'] / budget['limit']) * 100
        print(f"  {budget['category']}: ${budget['spent']}/${budget['limit']} ({pct:.0f}%)")

print("Finance Tracker with Async HTTP\n")
asyncio.run(main())
```
