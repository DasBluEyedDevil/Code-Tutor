import pytest
from httpx import AsyncClient, ASGITransport
from decimal import Decimal

# Assume app is imported from main module
# from app.main import app

@pytest.fixture
async def async_client(app):
    transport = ASGITransport(app=app)
    async with AsyncClient(transport=transport, base_url="http://test") as client:
        yield client

async def test_create_transaction(async_client):
    transaction_data = {
        "account_id": 1,
        "amount": "-75.50",
        "category": "dining",
        "description": "Restaurant dinner"
    }
    
    response = await async_client.post("/transactions", json=transaction_data)
    
    assert response.status_code == 200
    data = response.json()
    assert data["category"] == "dining"
    assert Decimal(data["amount"]) == Decimal("-75.50")
