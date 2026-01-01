---
type: "EXAMPLE"
title: "Code Example: Finance Tracker with Protocols"
---

**Protocol for decoupled design:**

**Benefits:**
1. **Testability** - Easy to mock
2. **Flexibility** - Swap implementations
3. **Decoupling** - No hard dependencies
4. **Type safety** - Caught by type checkers

**Key patterns:**
- Repository pattern for data access
- Strategy pattern for calculations
- Dependency injection via constructor

**Runtime checking:**
- Use `runtime_checkable` decorator
- Enables isinstance() checks
- Slight performance cost

```python
from dataclasses import dataclass, field
from typing import Protocol, runtime_checkable, Optional
from abc import ABC, abstractmethod
from datetime import date
from enum import Enum

# === Domain Models ===

class Category(Enum):
    INCOME = "income"
    EXPENSE = "expense"

@dataclass(slots=True, frozen=True)
class Transaction:
    id: int
    amount: float
    category: Category
    description: str
    date: date = field(default_factory=date.today)

# === Protocol Definitions ===

@runtime_checkable
class Repository(Protocol):
    """Protocol for data persistence - no inheritance needed!"""
    
    def save(self, item: Transaction) -> None:
        """Save a transaction."""
        ...
    
    def get(self, id: int) -> Optional[Transaction]:
        """Get transaction by ID."""
        ...
    
    def get_all(self) -> list[Transaction]:
        """Get all transactions."""
        ...

@runtime_checkable
class TaxCalculator(Protocol):
    """Protocol for tax calculation strategies."""
    
    def calculate(self, income: float, expenses: float) -> float:
        """Calculate tax owed."""
        ...

# === Abstract Base Class Example ===

class ReportGenerator(ABC):
    """ABC for report generation - provides shared implementation."""
    
    @abstractmethod
    def generate(self, transactions: list[Transaction]) -> str:
        """Generate report content."""
        pass
    
    def save_report(self, content: str, filename: str) -> None:
        """Shared implementation for saving reports."""
        print(f"Saving report to {filename}...")
        # In real code: write to file

# === Concrete Implementations ===

class InMemoryRepository:
    """In-memory storage - implements Repository protocol implicitly!"""
    
    def __init__(self):
        self._storage: dict[int, Transaction] = {}
    
    def save(self, item: Transaction) -> None:
        self._storage[item.id] = item
        print(f"  Saved: Transaction #{item.id}")
    
    def get(self, id: int) -> Optional[Transaction]:
        return self._storage.get(id)
    
    def get_all(self) -> list[Transaction]:
        return list(self._storage.values())

class SQLiteRepository:
    """SQLite storage - also implements Repository protocol!"""
    
    def __init__(self, db_path: str = ":memory:"):
        self.db_path = db_path
        self._data: dict[int, Transaction] = {}  # Simplified
    
    def save(self, item: Transaction) -> None:
        self._data[item.id] = item
        print(f"  [SQLite] Saved: Transaction #{item.id}")
    
    def get(self, id: int) -> Optional[Transaction]:
        return self._data.get(id)
    
    def get_all(self) -> list[Transaction]:
        return list(self._data.values())

class SimpleTaxCalculator:
    """Simple flat tax rate."""
    
    def __init__(self, rate: float = 0.25):
        self.rate = rate
    
    def calculate(self, income: float, expenses: float) -> float:
        taxable = max(0, income - expenses)
        return taxable * self.rate

class ProgressiveTaxCalculator:
    """Progressive tax brackets."""
    
    def __init__(self):
        self.brackets = [
            (10000, 0.10),
            (40000, 0.22),
            (90000, 0.32),
            (float('inf'), 0.37)
        ]
    
    def calculate(self, income: float, expenses: float) -> float:
        taxable = max(0, income - expenses)
        tax = 0.0
        prev_limit = 0
        
        for limit, rate in self.brackets:
            if taxable <= 0:
                break
            bracket_income = min(taxable, limit - prev_limit)
            tax += bracket_income * rate
            taxable -= bracket_income
            prev_limit = limit
        
        return tax

class MonthlyReportGenerator(ReportGenerator):
    """Monthly summary report - inherits from ABC."""
    
    def generate(self, transactions: list[Transaction]) -> str:
        income = sum(t.amount for t in transactions if t.category == Category.INCOME)
        expenses = sum(t.amount for t in transactions if t.category == Category.EXPENSE)
        
        return f"Monthly Report\n" \
               f"  Income: ${income:.2f}\n" \
               f"  Expenses: ${expenses:.2f}\n" \
               f"  Net: ${income - expenses:.2f}"

# === Finance Tracker using Dependency Injection ===

class FinanceTracker:
    """Finance tracker with pluggable components."""
    
    def __init__(self, 
                 repository: Repository,  # Protocol type hint!
                 tax_calculator: TaxCalculator):
        self.repo = repository
        self.tax_calc = tax_calculator
        self._next_id = 1
    
    def add_income(self, amount: float, description: str) -> Transaction:
        txn = Transaction(self._next_id, amount, Category.INCOME, description)
        self._next_id += 1
        self.repo.save(txn)
        return txn
    
    def add_expense(self, amount: float, description: str) -> Transaction:
        txn = Transaction(self._next_id, amount, Category.EXPENSE, description)
        self._next_id += 1
        self.repo.save(txn)
        return txn
    
    def calculate_taxes(self) -> float:
        transactions = self.repo.get_all()
        income = sum(t.amount for t in transactions if t.category == Category.INCOME)
        expenses = sum(t.amount for t in transactions if t.category == Category.EXPENSE)
        return self.tax_calc.calculate(income, expenses)
    
    def get_summary(self) -> dict:
        transactions = self.repo.get_all()
        income = sum(t.amount for t in transactions if t.category == Category.INCOME)
        expenses = sum(t.amount for t in transactions if t.category == Category.EXPENSE)
        return {
            "income": income,
            "expenses": expenses,
            "net": income - expenses,
            "tax_owed": self.calculate_taxes()
        }

# === Demo ===

print("=== Protocol-Based Finance Tracker ===")
print()

# Protocol runtime checking
print("=== Protocol Checking ===")
print(f"InMemoryRepository implements Repository: {isinstance(InMemoryRepository(), Repository)}")
print(f"SimpleTaxCalculator implements TaxCalculator: {isinstance(SimpleTaxCalculator(), TaxCalculator)}")
print()

# Create tracker with in-memory storage and simple tax
print("=== Using InMemory + Simple Tax ===")
tracker1 = FinanceTracker(
    repository=InMemoryRepository(),
    tax_calculator=SimpleTaxCalculator(rate=0.20)
)

tracker1.add_income(5000, "Salary")
tracker1.add_expense(1500, "Rent")
tracker1.add_expense(300, "Utilities")

summary1 = tracker1.get_summary()
print(f"Net Income: ${summary1['net']:.2f}")
print(f"Tax (20% flat): ${summary1['tax_owed']:.2f}")
print()

# Same tracker, different implementations!
print("=== Using SQLite + Progressive Tax ===")
tracker2 = FinanceTracker(
    repository=SQLiteRepository(),
    tax_calculator=ProgressiveTaxCalculator()
)

tracker2.add_income(5000, "Salary")
tracker2.add_expense(1500, "Rent")
tracker2.add_expense(300, "Utilities")

summary2 = tracker2.get_summary()
print(f"Net Income: ${summary2['net']:.2f}")
print(f"Tax (progressive): ${summary2['tax_owed']:.2f}")
print()

# ABC with shared implementation
print("=== ABC Report Generator ===")
reporter = MonthlyReportGenerator()
transactions = tracker2.repo.get_all()
report = reporter.generate(transactions)
print(report)
reporter.save_report(report, "monthly_report.txt")
```
