---
type: "EXAMPLE"
title: "FastAPI Testing: Personal Finance API"
---

Complete example testing a FastAPI finance tracker with async client.

```python
# app/main.py
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from decimal import Decimal
from datetime import date

app = FastAPI(title="Personal Finance Tracker")

class Transaction(BaseModel):
    id: int | None = None
    account_id: int
    amount: Decimal
    category: str
    description: str
    date: date

# In-memory storage for demo
transactions_db: list[Transaction] = []

@app.post("/transactions", response_model=Transaction)
async def create_transaction(tx: Transaction) -> Transaction:
    tx.id = len(transactions_db) + 1
    transactions_db.append(tx)
    return tx

@app.get("/transactions/{account_id}")
async def get_transactions(account_id: int) -> list[Transaction]:
    return [tx for tx in transactions_db if tx.account_id == account_id]

@app.get("/balance/{account_id}")
async def get_balance(account_id: int) -> dict:
    txs = [tx for tx in transactions_db if tx.account_id == account_id]
    total = sum(tx.amount for tx in txs)
    return {"account_id": account_id, "balance": total}

# tests/conftest.py
import pytest
from httpx import AsyncClient, ASGITransport
from app.main import app, transactions_db

@pytest.fixture
async def async_client():
    """Async HTTP client for testing FastAPI."""
    # Clear database before each test
    transactions_db.clear()
    
    transport = ASGITransport(app=app)
    async with AsyncClient(transport=transport, base_url="http://test") as client:
        yield client

@pytest.fixture
def sample_transaction():
    """Sample transaction data."""
    from datetime import date
    return {
        "account_id": 1,
        "amount": "-45.99",
        "category": "groceries",
        "description": "Weekly shopping",
        "date": str(date.today())
    }

# tests/test_api.py
import pytest
from decimal import Decimal

async def test_create_transaction(async_client, sample_transaction):
    """Test creating a new transaction."""
    response = await async_client.post("/transactions", json=sample_transaction)
    
    assert response.status_code == 200
    data = response.json()
    assert data["id"] == 1
    assert data["category"] == "groceries"
    assert Decimal(data["amount"]) == Decimal("-45.99")

async def test_get_transactions(async_client, sample_transaction):
    """Test retrieving transactions for an account."""
    # Create two transactions
    await async_client.post("/transactions", json=sample_transaction)
    await async_client.post("/transactions", json={
        **sample_transaction, 
        "amount": "100.00",
        "category": "income"
    })
    
    response = await async_client.get("/transactions/1")
    assert response.status_code == 200
    transactions = response.json()
    assert len(transactions) == 2

async def test_get_balance(async_client, sample_transaction):
    """Test balance calculation."""
    await async_client.post("/transactions", json=sample_transaction)  # -45.99
    await async_client.post("/transactions", json={
        **sample_transaction,
        "amount": "200.00"
    })  # +200.00
    
    response = await async_client.get("/balance/1")
    assert response.status_code == 200
    data = response.json()
    assert Decimal(str(data["balance"])) == Decimal("154.01")

```
