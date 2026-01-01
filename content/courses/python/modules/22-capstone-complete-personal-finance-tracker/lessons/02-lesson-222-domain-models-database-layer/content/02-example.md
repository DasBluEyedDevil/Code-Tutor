---
type: "EXAMPLE"
title: "Domain Models with Dataclasses"
---

Type-safe domain models using modern Python dataclasses:

```python
# src/finance_tracker/models/transaction.py
from dataclasses import dataclass, field
from datetime import date, datetime
from decimal import Decimal
from enum import StrEnum
from typing import Self


class TransactionType(StrEnum):
    """Transaction type - income adds money, expense removes it."""
    INCOME = "income"
    EXPENSE = "expense"


@dataclass(frozen=True, slots=True)
class Category:
    """Transaction category (immutable value object).
    
    Frozen dataclass ensures categories can't be accidentally modified.
    Slots reduce memory usage for many instances.
    """
    id: int
    name: str
    type: TransactionType
    user_id: int
    icon: str = "📁"
    is_default: bool = False
    
    def __str__(self) -> str:
        return f"{self.icon} {self.name}"


@dataclass(slots=True)
class Transaction:
    """Financial transaction entity.
    
    Represents money coming in (income) or going out (expense).
    Amount is always positive - type determines direction.
    """
    id: int | None  # None for new transactions
    amount: Decimal
    description: str
    category_id: int
    user_id: int
    transaction_date: date = field(default_factory=date.today)
    created_at: datetime = field(default_factory=datetime.now)
    
    def __post_init__(self) -> None:
        """Validate transaction data after creation."""
        if self.amount <= 0:
            raise ValueError(f"Amount must be positive, got {self.amount}")
        if not self.description.strip():
            raise ValueError("Description cannot be empty")
    
    @classmethod
    def create(
        cls,
        amount: Decimal | float | str,
        description: str,
        category_id: int,
        user_id: int,
        transaction_date: date | None = None,
    ) -> Self:
        """Factory method for creating new transactions.
        
        Handles type conversion and defaults.
        """
        return cls(
            id=None,
            amount=Decimal(str(amount)),
            description=description.strip(),
            category_id=category_id,
            user_id=user_id,
            transaction_date=transaction_date or date.today(),
        )
    
    def with_amount(self, new_amount: Decimal) -> Self:
        """Return copy with updated amount (immutable pattern)."""
        return Transaction(
            id=self.id,
            amount=new_amount,
            description=self.description,
            category_id=self.category_id,
            user_id=self.user_id,
            transaction_date=self.transaction_date,
            created_at=self.created_at,
        )


@dataclass(slots=True)
class TransactionSummary:
    """Aggregated transaction statistics."""
    total_income: Decimal
    total_expenses: Decimal
    transaction_count: int
    period_start: date
    period_end: date
    
    @property
    def net_balance(self) -> Decimal:
        """Net balance for the period."""
        return self.total_income - self.total_expenses
    
    @property
    def savings_rate(self) -> float:
        """Percentage of income saved."""
        if self.total_income == 0:
            return 0.0
        return float((self.net_balance / self.total_income) * 100)


# Example usage
if __name__ == "__main__":
    # Create a category
    food = Category(
        id=1,
        name="Food & Dining",
        type=TransactionType.EXPENSE,
        user_id=1,
        icon="🍔"
    )
    print(f"Category: {food}")  # 🍔 Food & Dining
    
    # Create a transaction
    tx = Transaction.create(
        amount="45.99",
        description="Grocery shopping",
        category_id=1,
        user_id=1,
    )
    print(f"Transaction: ${tx.amount} - {tx.description}")
```
