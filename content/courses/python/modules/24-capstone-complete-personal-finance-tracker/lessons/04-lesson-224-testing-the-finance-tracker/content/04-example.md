---
type: "EXAMPLE"
title: "API Integration Tests"
---

Test HTTP endpoints with async client:

```python
# tests/test_api.py
from datetime import date
from decimal import Decimal

import pytest
from httpx import AsyncClient


@pytest.mark.asyncio
class TestTransactionsAPI:
    """Test transaction API endpoints."""
    
    async def test_list_transactions_requires_auth(self, client: AsyncClient):
        """GET /transactions returns 401 without token."""
        response = await client.get("/transactions")
        
        assert response.status_code == 401
    
    async def test_list_transactions_empty(self, auth_client: AsyncClient):
        """GET /transactions returns empty list for new user."""
        response = await auth_client.get("/transactions")
        
        assert response.status_code == 200
        data = response.json()
        assert data["items"] == []
        assert data["total"] == 0
    
    async def test_create_transaction(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """POST /transactions creates transaction."""
        response = await auth_client.post("/transactions", json={
            "amount": "45.99",
            "description": "Grocery shopping",
            "category_id": test_category.id,
        })
        
        assert response.status_code == 201
        data = response.json()
        assert data["amount"] == "45.99"
        assert data["description"] == "Grocery shopping"
        assert "id" in data
    
    async def test_create_transaction_validation_error(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """POST /transactions rejects invalid data."""
        response = await auth_client.post("/transactions", json={
            "amount": "-50",  # Invalid: negative
            "description": "Test",
            "category_id": test_category.id,
        })
        
        assert response.status_code == 422  # Validation error
    
    async def test_get_transaction(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """GET /transactions/{id} returns specific transaction."""
        # Create transaction first
        create_response = await auth_client.post("/transactions", json={
            "amount": "25.00",
            "description": "Coffee",
            "category_id": test_category.id,
        })
        tx_id = create_response.json()["id"]
        
        # Get it
        response = await auth_client.get(f"/transactions/{tx_id}")
        
        assert response.status_code == 200
        assert response.json()["id"] == tx_id
    
    async def test_get_nonexistent_transaction(
        self,
        auth_client: AsyncClient,
    ):
        """GET /transactions/{id} returns 404 for missing."""
        response = await auth_client.get("/transactions/99999")
        
        assert response.status_code == 404
    
    async def test_delete_transaction(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """DELETE /transactions/{id} removes transaction."""
        # Create
        create_response = await auth_client.post("/transactions", json={
            "amount": "10.00",
            "description": "To delete",
            "category_id": test_category.id,
        })
        tx_id = create_response.json()["id"]
        
        # Delete
        response = await auth_client.delete(f"/transactions/{tx_id}")
        assert response.status_code == 204
        
        # Verify gone
        get_response = await auth_client.get(f"/transactions/{tx_id}")
        assert get_response.status_code == 404
    
    async def test_get_summary(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """GET /transactions/summary returns statistics."""
        # Create some transactions
        await auth_client.post("/transactions", json={
            "amount": "100.00",
            "description": "Expense 1",
            "category_id": test_category.id,
            "transaction_date": "2025-01-15",
        })
        await auth_client.post("/transactions", json={
            "amount": "50.00",
            "description": "Expense 2",
            "category_id": test_category.id,
            "transaction_date": "2025-01-20",
        })
        
        # Get summary
        response = await auth_client.get(
            "/transactions/summary",
            params={"start_date": "2025-01-01", "end_date": "2025-01-31"},
        )
        
        assert response.status_code == 200
        data = response.json()
        assert data["total_expenses"] == "150.00"
        assert data["transaction_count"] == 2


@pytest.mark.asyncio
class TestAuthAPI:
    """Test authentication endpoints."""
    
    async def test_register_user(self, client: AsyncClient, db):
        """POST /auth/register creates new user."""
        response = await client.post("/auth/register", json={
            "email": "newuser@example.com",
            "password": "securepass123",
        })
        
        assert response.status_code == 201
        data = response.json()
        assert data["email"] == "newuser@example.com"
        assert "id" in data
        assert "password" not in data  # Never expose password
    
    async def test_register_duplicate_email(self, client: AsyncClient, test_user):
        """POST /auth/register rejects duplicate email."""
        response = await client.post("/auth/register", json={
            "email": "test@example.com",  # Already exists
            "password": "anotherpass",
        })
        
        assert response.status_code == 400
        assert "already registered" in response.json()["detail"].lower()
    
    async def test_login_success(self, client: AsyncClient, test_user):
        """POST /auth/login returns access token."""
        response = await client.post("/auth/login", data={
            "username": "test@example.com",
            "password": "testpass123",
        })
        
        assert response.status_code == 200
        data = response.json()
        assert "access_token" in data
        assert data["token_type"] == "bearer"
    
    async def test_login_wrong_password(self, client: AsyncClient, test_user):
        """POST /auth/login rejects wrong password."""
        response = await client.post("/auth/login", data={
            "username": "test@example.com",
            "password": "wrongpassword",
        })
        
        assert response.status_code == 401
```
