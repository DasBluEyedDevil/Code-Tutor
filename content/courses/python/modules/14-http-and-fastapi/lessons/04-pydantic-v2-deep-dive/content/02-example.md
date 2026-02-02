---
type: "EXAMPLE"
title: "BaseModel Patterns"
---

**Core Pydantic Patterns:**

**1. Field Constraints:**
```python
Field(..., gt=0)       # Greater than 0 (required)
Field(default=1, ge=1) # >= 1 with default
Field(min_length=3)    # String min length
Field(pattern=r'^...')  # Regex pattern
```

**2. Type Hints:**
```python
str | None      # Optional string (Python 3.10+)
list[str]       # List of strings
dict[str, int]  # Dict with string keys, int values
```

**3. Enums for Choices:**
```python
class Category(str, Enum):
    INCOME = "income"
    EXPENSE = "expense"
```
Inherit from `str` so JSON serialization works correctly.

**4. model_config:**
```python
model_config = {
    "str_strip_whitespace": True,
    "str_min_length": 1,
    "extra": "forbid"  # Reject unknown fields
}
```

```python
from pydantic import BaseModel, Field
from datetime import datetime
from enum import Enum
from typing import Optional

print("=== Pydantic v2 BaseModel Patterns ===")

# Enum for type safety on categorical values
class Category(str, Enum):
    INCOME = "income"
    EXPENSE = "expense"
    TRANSFER = "transfer"

# Main model with various field patterns
class Transaction(BaseModel):
    """A financial transaction with full validation."""
    
    # Required field with constraint: must be positive
    amount: float = Field(..., gt=0, description="Must be positive")
    
    # Enum field - only valid categories accepted
    category: Category
    
    # Optional field with default None
    description: str | None = None
    
    # Auto-generated timestamp
    created_at: datetime = Field(default_factory=datetime.now)
    
    # Model-level configuration
    model_config = {
        "str_strip_whitespace": True,  # Auto-strip whitespace from strings
        "str_min_length": 1,            # No empty strings allowed
    }

# Create a valid transaction
print("\n1. Creating valid transaction:")
txn = Transaction(
    amount=99.99,
    category=Category.EXPENSE,
    description="  Weekly groceries  "  # Whitespace will be stripped
)
print(f"   Amount: ${txn.amount}")
print(f"   Category: {txn.category.value}")
print(f"   Description: '{txn.description}'")
print(f"   Created: {txn.created_at.strftime('%Y-%m-%d %H:%M')}")

# Convert to dict (v2 method)
print("\n2. Convert to dictionary:")
data = txn.model_dump()
print(f"   {data}")

# Convert to JSON string
print("\n3. Convert to JSON:")
json_str = txn.model_dump_json()
print(f"   {json_str}")

# Create from dict (v2 method)
print("\n4. Create from dictionary:")
raw_data = {
    "amount": 50.0,
    "category": "income",
    "description": "Refund"
}
txn2 = Transaction.model_validate(raw_data)
print(f"   Created: {txn2.category.value} - ${txn2.amount}")

# Validation errors
print("\n5. Validation errors:")
from pydantic import ValidationError

try:
    bad_txn = Transaction(
        amount=-50,  # Negative amount - invalid!
        category="invalid_category"  # Not in enum
    )
except ValidationError as e:
    print(f"   Errors found: {e.error_count()}")
    for error in e.errors():
        print(f"   - {error['loc'][0]}: {error['msg']}")

print("\n6. Model schema (for API docs):")
schema = Transaction.model_json_schema()
print(f"   Title: {schema.get('title')}")
print(f"   Required fields: {schema.get('required')}")
```
