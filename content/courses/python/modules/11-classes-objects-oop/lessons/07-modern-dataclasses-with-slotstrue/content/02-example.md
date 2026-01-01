---
type: "EXAMPLE"
title: "Code Example: Building a Finance Tracker with Dataclasses"
---

**Key dataclass features:**

**1. slots=True (Python 3.10+):**
- Memory efficient - no __dict__
- Faster attribute access
- Prevents accidental attribute creation

**2. frozen=True:**
- Immutable instances
- Hashable (can use in sets/dicts)
- Thread-safe

**3. Field options:**
- `field(default=...)` - Default value
- `field(default_factory=...)` - For mutable defaults
- `field(compare=False)` - Exclude from comparisons
- `field(repr=False)` - Exclude from repr

**4. Post-init processing:**
- `__post_init__()` - Validate or compute fields

```python
from dataclasses import dataclass, field
from datetime import datetime, date
from typing import Optional
from enum import Enum
import sys

class Category(Enum):
    INCOME = "income"
    GROCERIES = "groceries"
    UTILITIES = "utilities"
    ENTERTAINMENT = "entertainment"
    TRANSPORT = "transport"
    OTHER = "other"

# Modern dataclass with slots for memory efficiency
@dataclass(slots=True, frozen=True)
class Transaction:
    """Immutable transaction record for finance tracking."""
    id: int
    amount: float
    category: Category
    description: str = ""
    date: date = field(default_factory=date.today)
    
    def __post_init__(self):
        """Validate transaction data."""
        if self.amount == 0:
            raise ValueError("Amount cannot be zero")

    @property
    def is_expense(self) -> bool:
        return self.amount < 0
    
    @property
    def is_income(self) -> bool:
        return self.amount > 0

@dataclass(slots=True)
class Account:
    """Mutable account with transaction history."""
    name: str
    account_type: str = "checking"
    transactions: list = field(default_factory=list, repr=False)
    _transaction_counter: int = field(default=0, repr=False, compare=False)
    
    @property
    def balance(self) -> float:
        return sum(t.amount for t in self.transactions)
    
    def add_transaction(self, amount: float, category: Category, 
                        description: str = "") -> Transaction:
        """Add a transaction and return it."""
        self._transaction_counter += 1
        txn = Transaction(
            id=self._transaction_counter,
            amount=amount,
            category=category,
            description=description
        )
        self.transactions.append(txn)
        return txn
    
    def get_expenses_by_category(self) -> dict:
        """Group expenses by category."""
        expenses = {}
        for txn in self.transactions:
            if txn.is_expense:
                cat = txn.category.value
                expenses[cat] = expenses.get(cat, 0) + abs(txn.amount)
        return expenses

print("=== Personal Finance Tracker with Dataclasses ===")
print()

# Create account
account = Account(name="Main Checking")
print(f"Created: {account}")
print(f"Starting balance: ${account.balance:.2f}")

# Add transactions
print("\n=== Adding Transactions ===")
account.add_transaction(3000, Category.INCOME, "Salary")
account.add_transaction(-150, Category.GROCERIES, "Weekly groceries")
account.add_transaction(-80, Category.UTILITIES, "Electric bill")
account.add_transaction(-50, Category.ENTERTAINMENT, "Movie night")
account.add_transaction(-35, Category.TRANSPORT, "Gas")

print(f"Current balance: ${account.balance:.2f}")

# Show transactions (immutable!)
print("\n=== Transaction History ===")
for txn in account.transactions:
    sign = '+' if txn.is_income else '-'
    print(f"  {txn.id}: {sign}${abs(txn.amount):.2f} [{txn.category.value}] {txn.description}")

# Expenses by category
print("\n=== Expenses by Category ===")
for cat, total in account.get_expenses_by_category().items():
    print(f"  {cat}: ${total:.2f}")

# Demonstrate immutability
print("\n=== Immutability Demo ===")
txn = account.transactions[0]
print(f"Transaction: {txn}")
try:
    txn.amount = 5000  # Try to modify
except Exception as e:
    print(f"Cannot modify frozen dataclass: {type(e).__name__}")

# Memory comparison: slots vs no-slots
print("\n=== Memory Efficiency (slots=True) ===")

@dataclass
class TransactionNoSlots:
    id: int
    amount: float
    category: str

@dataclass(slots=True)
class TransactionWithSlots:
    id: int
    amount: float
    category: str

no_slots = TransactionNoSlots(1, 100.0, "test")
with_slots = TransactionWithSlots(1, 100.0, "test")

print(f"Without slots: has __dict__ = {hasattr(no_slots, '__dict__')}")
print(f"With slots: has __dict__ = {hasattr(with_slots, '__dict__')}")
print(f"With slots: has __slots__ = {hasattr(TransactionWithSlots, '__slots__')}")
print(f"Slots defined: {TransactionWithSlots.__slots__}")
```
