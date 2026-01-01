---
type: "EXAMPLE"
title: "Code Example: Modern Type Hints (Python 3.9+, 3.10+, 3.12+)"
---

**Evolution of Type Hints:**

**Python 3.9+: Built-in Generics**
- Use `list[str]` instead of `List[str]`
- Use `dict[str, int]` instead of `Dict[str, int]`
- No need to import from typing for basic types!

**Python 3.10+: Union Operator**
- Use `X | None` instead of `Optional[X]`
- Use `int | str` instead of `Union[int, str]`
- Cleaner, more readable syntax

**Python 3.12+: type Statement**
- Use `type Alias = ...` for type aliases
- Cleaner than `Alias = ...` assignment
- Makes type aliases explicit

**Personal Finance Tracker Example:**
We'll define types for transactions, accounts, and budgets.

```python
from typing import TypedDict, Protocol, NotRequired
from datetime import date
from decimal import Decimal

# ================================================
# Personal Finance Tracker - Modern Type Hints
# ================================================

print("=== Modern vs Legacy Type Hints ===")
print("""
# LEGACY (Python < 3.9) - Requires imports
from typing import List, Dict, Optional, Union

def process(items: List[str]) -> Dict[str, int]:
    pass

def find(id: int) -> Optional[User]:
    pass

def format(val: Union[int, float]) -> str:
    pass

# MODERN (Python 3.9+, 3.10+) - No imports needed!
def process(items: list[str]) -> dict[str, int]:
    pass

def find(id: int) -> User | None:  # 3.10+
    pass

def format(val: int | float) -> str:  # 3.10+
    pass
""")

print("\n=== TypedDict for Structured Data ===")

# TypedDict - Define exact structure of dictionaries
# Perfect for JSON data, API responses, config files

class TransactionDict(TypedDict):
    """A financial transaction in the Finance Tracker."""
    id: int
    amount: float
    category: str
    description: str
    date: str
    is_expense: bool

# TypedDict with optional fields (Python 3.11+)
class TransactionDictFull(TypedDict, total=False):
    """Transaction with optional fields."""
    id: int  # Required
    amount: float  # Required
    category: str  # Required
    description: str  # Optional
    date: str  # Optional
    tags: list[str]  # Optional

# Using TypedDict
def process_transaction(txn: TransactionDict) -> str:
    """Process a single transaction."""
    sign = "-" if txn['is_expense'] else "+"
    return f"{sign}${txn['amount']:.2f} [{txn['category']}] {txn['description']}"

def calculate_balance(transactions: list[TransactionDict]) -> float:
    """Calculate total balance from transactions."""
    total = 0.0
    for txn in transactions:
        if txn['is_expense']:
            total -= txn['amount']
        else:
            total += txn['amount']
    return total

# Sample transactions
transactions: list[TransactionDict] = [
    {
        'id': 1,
        'amount': 5000.00,
        'category': 'Income',
        'description': 'Salary',
        'date': '2024-01-15',
        'is_expense': False
    },
    {
        'id': 2,
        'amount': 150.00,
        'category': 'Food',
        'description': 'Grocery shopping',
        'date': '2024-01-16',
        'is_expense': True
    },
    {
        'id': 3,
        'amount': 50.00,
        'category': 'Transport',
        'description': 'Gas',
        'date': '2024-01-17',
        'is_expense': True
    }
]

print("Transactions:")
for txn in transactions:
    print(f"  {process_transaction(txn)}")

balance = calculate_balance(transactions)
print(f"\nBalance: ${balance:,.2f}")

print("\n=== Protocol for Structural Typing ===")

# Protocol - Define interfaces based on structure, not inheritance
# "If it looks like a duck and quacks like a duck..."

class Exportable(Protocol):
    """Any object that can be exported."""
    def to_dict(self) -> dict: ...
    def to_json(self) -> str: ...

class Summarizable(Protocol):
    """Any object with a summary method."""
    def summary(self) -> str: ...

# Classes don't need to explicitly inherit from Protocol!
class Account:
    """Bank account that happens to match Exportable protocol."""
    def __init__(self, name: str, balance: float):
        self.name = name
        self.balance = balance
    
    def to_dict(self) -> dict:
        return {'name': self.name, 'balance': self.balance}
    
    def to_json(self) -> str:
        import json
        return json.dumps(self.to_dict())
    
    def summary(self) -> str:
        return f"{self.name}: ${self.balance:,.2f}"

class Budget:
    """Budget category that also matches Exportable."""
    def __init__(self, category: str, limit: float, spent: float):
        self.category = category
        self.limit = limit
        self.spent = spent
    
    def to_dict(self) -> dict:
        return {
            'category': self.category,
            'limit': self.limit,
            'spent': self.spent,
            'remaining': self.limit - self.spent
        }
    
    def to_json(self) -> str:
        import json
        return json.dumps(self.to_dict())
    
    def summary(self) -> str:
        pct = (self.spent / self.limit) * 100
        return f"{self.category}: ${self.spent:.2f}/${self.limit:.2f} ({pct:.0f}%)"

# Function accepts ANY object matching Exportable protocol
def export_to_file(item: Exportable, filename: str) -> None:
    """Export any Exportable item to JSON file."""
    print(f"  Would export to {filename}: {item.to_json()}")

def print_summaries(items: list[Summarizable]) -> None:
    """Print summary of any Summarizable items."""
    for item in items:
        print(f"  {item.summary()}")

# Works with Account AND Budget - both match the protocols!
account = Account("Checking", 2500.00)
budget = Budget("Food", 500.00, 350.00)

print("Exporting (Protocol in action):")
export_to_file(account, "account.json")
export_to_file(budget, "budget.json")

print("\nSummaries (another Protocol):")
print_summaries([account, budget])

print("\n=== Type Aliases (Python 3.12+ syntax) ===")

# Old way (still works)
TransactionList = list[TransactionDict]
CategoryTotals = dict[str, float]

# Python 3.12+ way - explicit type alias
# type TransactionList = list[TransactionDict]
# type CategoryTotals = dict[str, float]
# type MaybeFloat = float | None

print("""
# Python 3.12+ type statement:
type TransactionList = list[TransactionDict]
type CategoryTotals = dict[str, float]
type AccountBalance = float | Decimal
type MaybeAmount = float | None

# Benefits:
# - Explicit that it's a type alias
# - Works better with forward references
# - Cleaner syntax than assignment
""")

# Using type aliases
def categorize_spending(
    transactions: list[TransactionDict]
) -> dict[str, float]:
    """Group spending by category."""
    totals: dict[str, float] = {}
    for txn in transactions:
        if txn['is_expense']:
            cat = txn['category']
            totals[cat] = totals.get(cat, 0.0) + txn['amount']
    return totals

spending = categorize_spending(transactions)
print("Spending by category:")
for cat, total in spending.items():
    print(f"  {cat}: ${total:,.2f}")
```
