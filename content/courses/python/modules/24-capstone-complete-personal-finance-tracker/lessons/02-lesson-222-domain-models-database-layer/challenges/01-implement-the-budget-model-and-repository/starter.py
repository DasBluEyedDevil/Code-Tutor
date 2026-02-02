from dataclasses import dataclass
from datetime import date
from decimal import Decimal
from enum import StrEnum


class BudgetPeriod(StrEnum):
    """TODO: Define budget period options."""
    pass


@dataclass(slots=True)
class Budget:
    """TODO: Implement budget model.
    
    Fields:
    - id: int | None
    - category_id: int
    - user_id: int
    - amount_limit: Decimal
    - period: BudgetPeriod
    - start_date: date
    - end_date: date | None
    
    Validation:
    - amount_limit must be positive
    - end_date must be after start_date (if provided)
    """
    pass


class BudgetRepository:
    """TODO: Implement budget data access."""
    
    async def get_active_budgets(self, user_id: int) -> list[Budget]:
        """Get all active budgets for user."""
        pass
    
    async def check_budget_status(
        self, 
        budget_id: int,
        user_id: int,
    ) -> dict:
        """Check spending against budget limit.
        
        Returns dict with:
        - budget: Budget
        - spent: Decimal
        - remaining: Decimal  
        - percentage_used: float
        """
        pass