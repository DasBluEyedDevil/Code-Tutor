---
type: "EXAMPLE"
title: "Unit Tests for Models"
---

Test domain model validation and behavior:

```python
# tests/test_models.py
from datetime import date
from decimal import Decimal

import pytest

from finance_tracker.models.transaction import (
    Transaction,
    TransactionType,
    Category,
    TransactionSummary,
)


class TestTransaction:
    """Test Transaction model."""
    
    def test_create_valid_transaction(self):
        """Transaction.create() builds valid transaction."""
        tx = Transaction.create(
            amount="50.99",
            description="Grocery shopping",
            category_id=1,
            user_id=1,
        )
        
        assert tx.id is None  # New transaction
        assert tx.amount == Decimal("50.99")
        assert tx.description == "Grocery shopping"
        assert tx.transaction_date == date.today()
    
    def test_create_strips_description_whitespace(self):
        """Description whitespace is trimmed."""
        tx = Transaction.create(
            amount="10",
            description="  Coffee  ",
            category_id=1,
            user_id=1,
        )
        
        assert tx.description == "Coffee"
    
    def test_negative_amount_raises_error(self):
        """Negative amounts are rejected."""
        with pytest.raises(ValueError, match="positive"):
            Transaction.create(
                amount="-50",
                description="Invalid",
                category_id=1,
                user_id=1,
            )
    
    def test_zero_amount_raises_error(self):
        """Zero amount is rejected."""
        with pytest.raises(ValueError, match="positive"):
            Transaction.create(
                amount="0",
                description="Invalid",
                category_id=1,
                user_id=1,
            )
    
    def test_empty_description_raises_error(self):
        """Empty description is rejected."""
        with pytest.raises(ValueError, match="empty"):
            Transaction.create(
                amount="50",
                description="   ",  # Whitespace only
                category_id=1,
                user_id=1,
            )
    
    def test_with_amount_returns_copy(self):
        """with_amount() returns new instance."""
        tx = Transaction.create(
            amount="50",
            description="Test",
            category_id=1,
            user_id=1,
        )
        
        updated = tx.with_amount(Decimal("75"))
        
        assert updated.amount == Decimal("75")
        assert tx.amount == Decimal("50")  # Original unchanged
        assert updated.description == tx.description


class TestTransactionSummary:
    """Test TransactionSummary calculations."""
    
    def test_net_balance_calculation(self):
        """Net balance is income minus expenses."""
        summary = TransactionSummary(
            total_income=Decimal("1000"),
            total_expenses=Decimal("600"),
            transaction_count=10,
            period_start=date(2025, 1, 1),
            period_end=date(2025, 1, 31),
        )
        
        assert summary.net_balance == Decimal("400")
    
    def test_savings_rate_calculation(self):
        """Savings rate is percentage of income saved."""
        summary = TransactionSummary(
            total_income=Decimal("1000"),
            total_expenses=Decimal("800"),
            transaction_count=10,
            period_start=date(2025, 1, 1),
            period_end=date(2025, 1, 31),
        )
        
        assert summary.savings_rate == 20.0  # 200/1000 * 100
    
    def test_savings_rate_zero_income(self):
        """Savings rate is 0 when no income."""
        summary = TransactionSummary(
            total_income=Decimal("0"),
            total_expenses=Decimal("100"),
            transaction_count=5,
            period_start=date(2025, 1, 1),
            period_end=date(2025, 1, 31),
        )
        
        assert summary.savings_rate == 0.0


class TestCategory:
    """Test Category model."""
    
    def test_str_representation(self):
        """String includes icon and name."""
        cat = Category(
            id=1,
            name="Food",
            type=TransactionType.EXPENSE,
            user_id=1,
            icon="🍔",
        )
        
        assert str(cat) == "🍔 Food"
    
    def test_category_is_frozen(self):
        """Category instances are immutable."""
        cat = Category(
            id=1,
            name="Food",
            type=TransactionType.EXPENSE,
            user_id=1,
        )
        
        with pytest.raises(AttributeError):
            cat.name = "New Name"  # type: ignore
```
