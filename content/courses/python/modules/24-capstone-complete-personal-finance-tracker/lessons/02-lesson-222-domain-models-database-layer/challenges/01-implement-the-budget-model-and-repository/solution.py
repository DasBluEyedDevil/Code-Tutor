from dataclasses import dataclass, field
from datetime import date
from decimal import Decimal
from enum import StrEnum
from typing import Sequence

from ..database import Database


class BudgetPeriod(StrEnum):
    WEEKLY = "weekly"
    MONTHLY = "monthly"
    YEARLY = "yearly"


@dataclass(slots=True)
class Budget:
    id: int | None
    category_id: int
    user_id: int
    amount_limit: Decimal
    period: BudgetPeriod
    start_date: date
    end_date: date | None = None
    
    def __post_init__(self) -> None:
        if self.amount_limit <= 0:
            raise ValueError("Budget limit must be positive")
        if self.end_date and self.end_date <= self.start_date:
            raise ValueError("End date must be after start date")


class BudgetRepository:
    async def get_active_budgets(self, user_id: int) -> Sequence[Budget]:
        async with Database.connection() as conn:
            rows = await conn.fetch(
                """
                SELECT id, category_id, user_id, amount_limit, period, start_date, end_date
                FROM budgets
                WHERE user_id = $1 AND (end_date IS NULL OR end_date >= CURRENT_DATE)
                """,
                user_id
            )
            return [Budget(
                id=row["id"],
                category_id=row["category_id"],
                user_id=row["user_id"],
                amount_limit=Decimal(row["amount_limit"]),
                period=BudgetPeriod(row["period"]),
                start_date=row["start_date"],
                end_date=row["end_date"],
            ) for row in rows]
    
    async def check_budget_status(self, budget_id: int, user_id: int) -> dict:
        async with Database.connection() as conn:
            row = await conn.fetchrow(
                """
                SELECT b.*, 
                    COALESCE(SUM(t.amount), 0) as spent
                FROM budgets b
                LEFT JOIN transactions t ON t.category_id = b.category_id
                    AND t.user_id = b.user_id
                    AND t.transaction_date >= b.start_date
                    AND (b.end_date IS NULL OR t.transaction_date <= b.end_date)
                WHERE b.id = $1 AND b.user_id = $2
                GROUP BY b.id
                """,
                budget_id, user_id
            )
            if not row:
                raise ValueError(f"Budget {budget_id} not found")
            
            budget = Budget(
                id=row["id"],
                category_id=row["category_id"],
                user_id=row["user_id"],
                amount_limit=Decimal(row["amount_limit"]),
                period=BudgetPeriod(row["period"]),
                start_date=row["start_date"],
                end_date=row["end_date"],
            )
            spent = Decimal(row["spent"])
            
            return {
                "budget": budget,
                "spent": spent,
                "remaining": budget.amount_limit - spent,
                "percentage_used": float(spent / budget.amount_limit * 100),
            }