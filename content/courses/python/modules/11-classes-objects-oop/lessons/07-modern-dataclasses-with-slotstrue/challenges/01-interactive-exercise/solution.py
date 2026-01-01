from dataclasses import dataclass, field
from datetime import date
from enum import Enum
from typing import Dict, List

class Period(Enum):
    WEEKLY = "weekly"
    MONTHLY = "monthly"
    YEARLY = "yearly"

@dataclass(slots=True, frozen=True)
class Budget:
    """Immutable budget definition."""
    name: str
    limit: float
    period: Period
    
    def __post_init__(self):
        if self.limit <= 0:
            raise ValueError("Budget limit must be positive")

@dataclass(slots=True, frozen=True)
class Expense:
    """Immutable expense record."""
    amount: float
    category: str
    date: date = field(default_factory=date.today)
    
    def __post_init__(self):
        if self.amount <= 0:
            raise ValueError("Expense amount must be positive")

@dataclass(slots=True)
class BudgetTracker:
    """Mutable budget tracker."""
    budgets: Dict[str, Budget] = field(default_factory=dict)
    expenses: Dict[str, List[Expense]] = field(default_factory=dict)
    
    def add_budget(self, budget: Budget) -> None:
        """Add a budget to track."""
        self.budgets[budget.name] = budget
        self.expenses[budget.name] = []
        print(f"Added budget: {budget.name} (${budget.limit:.2f}/{budget.period.value})")
    
    def add_expense(self, budget_name: str, expense: Expense) -> None:
        """Add an expense to a budget."""
        if budget_name not in self.budgets:
            raise KeyError(f"Budget '{budget_name}' not found")
        self.expenses[budget_name].append(expense)
        print(f"Added expense: ${expense.amount:.2f} to {budget_name}")
    
    def get_spent(self, budget_name: str) -> float:
        """Get total spent for a budget."""
        return sum(e.amount for e in self.expenses.get(budget_name, []))
    
    def get_remaining(self, budget_name: str) -> float:
        """Get remaining budget."""
        if budget_name not in self.budgets:
            raise KeyError(f"Budget '{budget_name}' not found")
        return self.budgets[budget_name].limit - self.get_spent(budget_name)
    
    def get_summary(self) -> str:
        """Get budget summary."""
        lines = ["Budget Summary:"]
        for name, budget in self.budgets.items():
            spent = self.get_spent(name)
            remaining = budget.limit - spent
            pct = (spent / budget.limit) * 100
            status = "OVER" if remaining < 0 else "OK"
            lines.append(f"  {name}: ${spent:.2f}/${budget.limit:.2f} ({pct:.1f}%) [{status}]")
        return "\n".join(lines)

# Test the implementation
print("=== Budget Tracker Demo ===")

tracker = BudgetTracker()
tracker.add_budget(Budget("Groceries", 500.0, Period.MONTHLY))
tracker.add_budget(Budget("Entertainment", 200.0, Period.MONTHLY))

print()
tracker.add_expense("Groceries", Expense(75.50, "food"))
tracker.add_expense("Groceries", Expense(42.30, "food"))
tracker.add_expense("Entertainment", Expense(15.99, "streaming"))

print(f"\nGroceries remaining: ${tracker.get_remaining('Groceries'):.2f}")
print(f"Entertainment remaining: ${tracker.get_remaining('Entertainment'):.2f}")
print()
print(tracker.get_summary())