# Choose a feature and implement it!
# 
# Example: Recurring Transactions
# 
# 1. Create RecurringTransaction model
# 2. Add repository methods
# 3. Create scheduler service
# 4. Add API endpoints
# 5. Write tests

from dataclasses import dataclass
from datetime import date
from decimal import Decimal
from enum import StrEnum


class RecurrenceFrequency(StrEnum):
    DAILY = "daily"
    WEEKLY = "weekly"
    MONTHLY = "monthly"
    YEARLY = "yearly"


@dataclass(slots=True)
class RecurringTransaction:
    """Template for automatically created transactions."""
    id: int | None
    amount: Decimal
    description: str
    category_id: int
    user_id: int
    frequency: RecurrenceFrequency
    start_date: date
    end_date: date | None = None
    last_created: date | None = None
    is_active: bool = True
    
    # TODO: Implement methods:
    # - should_create_today() -> bool
    # - next_occurrence() -> date
    # - create_transaction() -> Transaction