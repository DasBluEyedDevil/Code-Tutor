import pytest
from httpx import AsyncClient
from datetime import date


@pytest.mark.asyncio
class TestBudgetsAPI:
    """Test budget API endpoints."""
    
    async def test_create_budget(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """TODO: POST /budgets creates budget."""
        pass
    
    async def test_create_budget_negative_amount(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """TODO: POST /budgets rejects negative amount."""
        pass
    
    async def test_get_budget_status_under_limit(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """TODO: GET /budgets/{id}/status shows correct spending."""
        pass
    
    async def test_get_budget_status_over_limit(
        self,
        auth_client: AsyncClient,
        test_category,
    ):
        """TODO: Budget status shows warning when over limit."""
        pass