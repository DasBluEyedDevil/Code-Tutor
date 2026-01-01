from dataclasses import dataclass, field
from datetime import date
from enum import Enum

class Period(Enum):
    WEEKLY = "weekly"
    MONTHLY = "monthly"
    YEARLY = "yearly"

# TODO: Create frozen Budget dataclass with slots
# Fields: name (str), limit (float), period (Period)

# TODO: Create frozen Expense dataclass with slots
# Fields: amount (float), category (str), date (date, default=today)

# TODO: Create mutable BudgetTracker dataclass with slots
# Fields: budgets (dict), expenses (list)
# Methods: add_budget, add_expense, get_remaining(budget_name)

# Test your implementation
tracker = BudgetTracker()
tracker.add_budget(Budget("Groceries", 500.0, Period.MONTHLY))
tracker.add_expense("Groceries", Expense(75.50, "food"))
print(f"Remaining: ${tracker.get_remaining('Groceries'):.2f}")