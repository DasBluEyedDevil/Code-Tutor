import pytest
from httpx import AsyncClient
from datetime import date


@pytest.mark.asyncio
class TestBudgetsAPI:
    async def test_create_budget(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        response = await auth_client.post("/budgets", json={
            "category_id": test_category.id,
            "amount_limit": "500.00",
            "period": "monthly",
            "start_date": str(date.today()),
        })
        
        assert response.status_code == 201
        data = response.json()
        assert data["amount_limit"] == "500.00"
        assert data["period"] == "monthly"
    
    async def test_create_budget_negative_amount(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        response = await auth_client.post("/budgets", json={
            "category_id": test_category.id,
            "amount_limit": "-100.00",
            "period": "monthly",
            "start_date": str(date.today()),
        })
        
        assert response.status_code == 422
    
    async def test_get_budget_status_under_limit(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        # Create budget
        budget_resp = await auth_client.post("/budgets", json={
            "category_id": test_category.id,
            "amount_limit": "500.00",
            "period": "monthly",
            "start_date": str(date.today()),
        })
        budget_id = budget_resp.json()["id"]
        
        # Create transaction under limit
        await auth_client.post("/transactions", json={
            "amount": "100.00",
            "description": "Test expense",
            "category_id": test_category.id,
        })
        
        # Check status
        response = await auth_client.get(f"/budgets/{budget_id}/status")
        
        assert response.status_code == 200
        data = response.json()
        assert data["spent"] == "100.00"
        assert data["remaining"] == "400.00"
        assert data["percentage_used"] == 20.0
        assert data["is_over_budget"] is False
    
    async def test_get_budget_status_over_limit(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        # Create small budget
        budget_resp = await auth_client.post("/budgets", json={
            "category_id": test_category.id,
            "amount_limit": "100.00",
            "period": "monthly",
            "start_date": str(date.today()),
        })
        budget_id = budget_resp.json()["id"]
        
        # Create transactions over limit
        await auth_client.post("/transactions", json={
            "amount": "80.00",
            "description": "Expense 1",
            "category_id": test_category.id,
        })
        await auth_client.post("/transactions", json={
            "amount": "50.00",
            "description": "Expense 2",
            "category_id": test_category.id,
        })
        
        response = await auth_client.get(f"/budgets/{budget_id}/status")
        
        data = response.json()
        assert data["spent"] == "130.00"
        assert data["remaining"] == "-30.00"
        assert data["percentage_used"] == 130.0
        assert data["is_over_budget"] is True