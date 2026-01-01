---
type: "EXAMPLE"
title: "Code Example: Validated Finance Tracker with Pydantic"
---

**Pydantic v2 features:**

**1. Model definition:**
```python
class MyModel(BaseModel):
    field: type = default
```

**2. Field validation:**
```python
from pydantic import Field

amount: float = Field(gt=0, description="Positive amount")
```

**3. Custom validators:**
```python
from pydantic import field_validator

@field_validator('field_name')
def validate_field(cls, v):
    # Validate and return
    return v
```

**4. Computed fields:**
```python
from pydantic import computed_field

@computed_field
@property
def total(self) -> float:
    return self.price * self.quantity
```

```python
# Note: This example shows Pydantic v2 patterns
# Install: pip install pydantic>=2.0

from dataclasses import dataclass, field
from datetime import date, datetime
from enum import Enum
from typing import Optional, Annotated

# Simulating Pydantic v2 for demonstration
# In real code: from pydantic import BaseModel, Field, field_validator, computed_field

print("=== Pydantic v2 Finance Tracker Patterns ===")
print()

# === Pydantic-style Model Simulation ===
# (Actual Pydantic code shown in comments)

class Category(Enum):
    INCOME = "income"
    GROCERIES = "groceries"
    UTILITIES = "utilities"
    ENTERTAINMENT = "entertainment"
    TRANSPORT = "transport"
    SAVINGS = "savings"

# Simulating Pydantic's validation behavior
class ValidatedModel:
    """Base class simulating Pydantic BaseModel behavior."""
    
    def __init__(self, **data):
        annotations = getattr(self.__class__, '__annotations__', {})
        for field_name, field_type in annotations.items():
            value = data.get(field_name)
            # Simulate type coercion
            if value is not None:
                if field_type == float and isinstance(value, (int, str)):
                    try:
                        value = float(value)
                    except ValueError:
                        raise ValueError(f"Cannot convert {value!r} to float for {field_name}")
                elif field_type == int and isinstance(value, str):
                    value = int(value)
            setattr(self, field_name, value)
    
    def model_dump(self) -> dict:
        """Convert to dictionary."""
        return {k: getattr(self, k) for k in self.__annotations__}
    
    def __repr__(self):
        fields = ', '.join(f"{k}={getattr(self, k)!r}" for k in self.__annotations__)
        return f"{self.__class__.__name__}({fields})"

# === Pydantic v2 Patterns (as comments with simulation) ===

# Real Pydantic v2 code:
# from pydantic import BaseModel, Field, field_validator, computed_field, ConfigDict
#
# class Transaction(BaseModel):
#     model_config = ConfigDict(frozen=True)  # Immutable
#     
#     id: int = Field(gt=0, description="Positive transaction ID")
#     amount: float = Field(description="Transaction amount")
#     category: Category
#     description: str = Field(min_length=1, max_length=200)
#     date: date = Field(default_factory=date.today)
#     
#     @field_validator('amount')
#     @classmethod
#     def validate_amount(cls, v: float) -> float:
#         if v == 0:
#             raise ValueError('Amount cannot be zero')
#         return round(v, 2)  # Round to 2 decimal places
#     
#     @computed_field
#     @property
#     def is_expense(self) -> bool:
#         return self.amount < 0

# Simulated Transaction model
@dataclass(slots=True, frozen=True)
class Transaction:
    """Transaction with validation (simulating Pydantic)."""
    id: int
    amount: float
    category: Category
    description: str
    date: date = field(default_factory=date.today)
    
    def __post_init__(self):
        # Simulate Pydantic validation
        if self.id <= 0:
            raise ValueError("id must be greater than 0")
        if self.amount == 0:
            raise ValueError("amount cannot be zero")
        if not (1 <= len(self.description) <= 200):
            raise ValueError("description must be 1-200 characters")
        # Round amount to 2 decimal places
        object.__setattr__(self, 'amount', round(self.amount, 2))
    
    @property
    def is_expense(self) -> bool:
        return self.amount < 0

# Real Pydantic v2 Account:
# class Account(BaseModel):
#     name: str = Field(min_length=1)
#     balance: float = Field(default=0.0)
#     transactions: list[Transaction] = Field(default_factory=list)
#     
#     @computed_field
#     @property
#     def transaction_count(self) -> int:
#         return len(self.transactions)
#     
#     @computed_field  
#     @property
#     def total_income(self) -> float:
#         return sum(t.amount for t in self.transactions if t.amount > 0)
#     
#     @computed_field
#     @property
#     def total_expenses(self) -> float:
#         return abs(sum(t.amount for t in self.transactions if t.amount < 0))

@dataclass(slots=True)
class Account:
    """Account with computed properties (simulating Pydantic)."""
    name: str
    balance: float = 0.0
    transactions: list = field(default_factory=list)
    _next_id: int = field(default=1, repr=False)
    
    @property
    def transaction_count(self) -> int:
        return len(self.transactions)
    
    @property
    def total_income(self) -> float:
        return sum(t.amount for t in self.transactions if t.amount > 0)
    
    @property
    def total_expenses(self) -> float:
        return abs(sum(t.amount for t in self.transactions if t.amount < 0))
    
    def add_transaction(self, amount: float, category: Category, 
                        description: str) -> Transaction:
        txn = Transaction(
            id=self._next_id,
            amount=amount,
            category=category,
            description=description
        )
        self.transactions.append(txn)
        self.balance += txn.amount
        self._next_id += 1
        return txn
    
    def to_dict(self) -> dict:
        """Simulating model_dump()."""
        return {
            "name": self.name,
            "balance": self.balance,
            "transaction_count": self.transaction_count,
            "total_income": self.total_income,
            "total_expenses": self.total_expenses,
            "transactions": [
                {
                    "id": t.id,
                    "amount": t.amount,
                    "category": t.category.value,
                    "description": t.description,
                    "date": t.date.isoformat()
                }
                for t in self.transactions
            ]
        }

# === Demo ===

print("=== Creating Account ===")
account = Account(name="Main Checking")
print(f"Created: {account.name}")
print(f"Starting balance: ${account.balance:.2f}")

print("\n=== Adding Transactions with Validation ===")
account.add_transaction(5000.00, Category.INCOME, "Monthly salary")
account.add_transaction(-1234.567, Category.UTILITIES, "Rent payment")  # Will be rounded
account.add_transaction(-89.99, Category.GROCERIES, "Weekly groceries")
account.add_transaction(-45.00, Category.ENTERTAINMENT, "Streaming services")
account.add_transaction(200.00, Category.INCOME, "Freelance work")

print(f"\nCurrent balance: ${account.balance:.2f}")
print(f"Transaction count: {account.transaction_count}")
print(f"Total income: ${account.total_income:.2f}")
print(f"Total expenses: ${account.total_expenses:.2f}")

print("\n=== Transaction Details ===")
for txn in account.transactions:
    sign = '+' if not txn.is_expense else '-'
    print(f"  #{txn.id}: {sign}${abs(txn.amount):.2f} [{txn.category.value}] {txn.description}")

print("\n=== Validation Errors ===")

# Test validation
try:
    bad_txn = Transaction(id=0, amount=100, category=Category.INCOME, description="Test")
except ValueError as e:
    print(f"Caught: {e}")

try:
    bad_txn = Transaction(id=1, amount=0, category=Category.INCOME, description="Test")
except ValueError as e:
    print(f"Caught: {e}")

try:
    bad_txn = Transaction(id=1, amount=100, category=Category.INCOME, description="")
except ValueError as e:
    print(f"Caught: {e}")

print("\n=== Serialization (model_dump) ===")
import json
account_dict = account.to_dict()
print(json.dumps(account_dict, indent=2)[:500] + "...")

print("\n=== Pydantic v2 Installation ===")
print("To use real Pydantic v2:")
print("  pip install 'pydantic>=2.0'")
print("\nThen replace this simulation with:")
print("  from pydantic import BaseModel, Field, field_validator, computed_field")
```
