---
type: "EXAMPLE"
title: "Request Body with Pydantic"
---

**Pydantic Models for Request Bodies:**

Pydantic provides automatic validation for complex data:

```python
from pydantic import BaseModel, Field

class Item(BaseModel):
    name: str
    price: float = Field(..., gt=0)  # Required, > 0
    quantity: int = Field(default=1, ge=1)
```

**Using in Endpoints:**
```python
@app.post("/items/")
async def create_item(item: Item):
    return item  # FastAPI validates automatically
```

**Validation Features:**
- `Field(...)` - Required field
- `Field(default=X)` - Default value
- `gt`, `ge`, `lt`, `le` - Numeric bounds
- `min_length`, `max_length` - String length
- `regex` - Pattern matching

**Optional Fields:**
```python
class Item(BaseModel):
    name: str
    description: str | None = None  # Optional
```

**Nested Models:**
```python
class Address(BaseModel):
    city: str
    country: str

class User(BaseModel):
    name: str
    address: Address  # Nested validation
```

```python
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field, field_validator
from typing import Optional
from datetime import datetime
from enum import Enum

app = FastAPI(title="Transaction API with Pydantic")

print("=== Pydantic Models ===")

# Enum for transaction categories
class Category(str, Enum):
    food = "Food"
    transport = "Transport"
    entertainment = "Entertainment"
    shopping = "Shopping"
    utilities = "Utilities"
    other = "Other"

# Request model for creating transactions
class TransactionCreate(BaseModel):
    """Model for creating a new transaction."""
    amount: float = Field(
        ...,
        gt=0,
        description="Transaction amount (must be positive)"
    )
    category: Category = Field(
        ...,
        description="Transaction category"
    )
    description: Optional[str] = Field(
        default=None,
        max_length=200,
        description="Optional description"
    )
    
    # Custom validator
    @field_validator('description')
    @classmethod
    def clean_description(cls, v):
        if v:
            return v.strip()
        return v

# Response model (includes generated fields)
class Transaction(BaseModel):
    """Full transaction model with all fields."""
    id: int
    amount: float
    category: Category
    description: Optional[str]
    created_at: datetime

# Example instance
example = TransactionCreate(
    amount=99.99,
    category=Category.food,
    description="Weekly groceries"
)
print(f"Example TransactionCreate:")
print(f"  {example.model_dump()}")

print("\n=== Validation in Action ===")

# In-memory storage
transactions_db: dict[int, Transaction] = {}
next_id = 1

@app.post("/transactions/", response_model=Transaction)
async def create_transaction(transaction: TransactionCreate):
    """Create a new transaction."""
    global next_id
    
    # Create full transaction with generated fields
    new_transaction = Transaction(
        id=next_id,
        amount=transaction.amount,
        category=transaction.category,
        description=transaction.description,
        created_at=datetime.now()
    )
    
    transactions_db[next_id] = new_transaction
    next_id += 1
    
    return new_transaction

# Demonstrate validation
print("Valid request body:")
print('''  {
    "amount": 50.00,
    "category": "Food",
    "description": "Lunch"
  }''')

print("\nInvalid request (amount <= 0):")
print('''  {
    "amount": -10,
    "category": "Food"
  }''')
print("  -> 422 Validation Error")

print("\n=== Update Models ===")

# Partial update model (all fields optional)
class TransactionUpdate(BaseModel):
    """Model for updating a transaction (all optional)."""
    amount: Optional[float] = Field(default=None, gt=0)
    category: Optional[Category] = None
    description: Optional[str] = Field(default=None, max_length=200)

@app.patch("/transactions/{transaction_id}", response_model=Transaction)
async def update_transaction(
    transaction_id: int,
    updates: TransactionUpdate
):
    """Partially update a transaction."""
    if transaction_id not in transactions_db:
        raise HTTPException(status_code=404, detail="Transaction not found")
    
    stored = transactions_db[transaction_id]
    update_data = updates.model_dump(exclude_unset=True)
    
    # Apply only provided updates
    for field, value in update_data.items():
        setattr(stored, field, value)
    
    return stored

print("PATCH /transactions/1")
print("Body: {\"amount\": 75.00}  # Only update amount")

print("\n=== Nested Models ===")

class Merchant(BaseModel):
    name: str
    category: str
    location: Optional[str] = None

class DetailedTransaction(BaseModel):
    amount: float = Field(..., gt=0)
    merchant: Merchant  # Nested model
    notes: Optional[str] = None

example_nested = {
    "amount": 42.50,
    "merchant": {
        "name": "Coffee Shop",
        "category": "Food & Drink",
        "location": "Downtown"
    },
    "notes": "Morning coffee"
}

print(f"Nested model example:")
print(f"  {example_nested}")

# Validate the nested structure
validated = DetailedTransaction(**example_nested)
print(f"\nValidated merchant: {validated.merchant.name}")
```
