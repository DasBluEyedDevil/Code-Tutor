---
type: "EXAMPLE"
title: "Model Validators"
---

**Cross-Field Validation with @model_validator:**

When you need to validate relationships BETWEEN fields, use model validators.

**Key Differences from field_validator:**
- Runs after all individual fields are validated
- Has access to ALL fields at once
- Returns the entire model instance (or values dict)

**Modes:**
```python
@model_validator(mode='before')  # Gets raw input dict
@model_validator(mode='after')   # Gets validated model instance
```

**Common Use Cases:**
- Password confirmation matching
- Date range validation (start < end)
- Conditional requirements (if field A, then field B required)
- Business rule validation (spent <= limit)

```python
from pydantic import BaseModel, model_validator, ValidationError
from typing import Self

print("=== Pydantic v2 Model Validators ===")

# Example 1: Budget validation
class Budget(BaseModel):
    """Budget with cross-field validation."""
    
    monthly_limit: float
    spent: float
    category: str
    
    @model_validator(mode="after")
    def check_not_overspent(self) -> Self:
        """Ensure spent doesn't exceed the limit."""
        if self.spent > self.monthly_limit:
            raise ValueError(
                f"Spent (${self.spent}) cannot exceed "
                f"monthly limit (${self.monthly_limit})"
            )
        return self

print("\n1. Valid budget:")
budget = Budget(monthly_limit=1000, spent=750, category="Food")
print(f"   Limit: ${budget.monthly_limit}")
print(f"   Spent: ${budget.spent}")
print(f"   Remaining: ${budget.monthly_limit - budget.spent}")

print("\n2. Overspent budget (validation error):")
try:
    bad_budget = Budget(monthly_limit=500, spent=750, category="Shopping")
except ValidationError as e:
    for error in e.errors():
        print(f"   Error: {error['msg']}")

# Example 2: Date range validation
from datetime import date

class DateRange(BaseModel):
    """Date range with start before end validation."""
    
    start_date: date
    end_date: date
    description: str | None = None
    
    @model_validator(mode="after")
    def check_dates(self) -> Self:
        """Ensure start_date is before end_date."""
        if self.start_date >= self.end_date:
            raise ValueError("start_date must be before end_date")
        return self

print("\n3. Valid date range:")
dr = DateRange(
    start_date=date(2024, 1, 1),
    end_date=date(2024, 12, 31),
    description="Year 2024"
)
print(f"   Range: {dr.start_date} to {dr.end_date}")

print("\n4. Invalid date range:")
try:
    bad_range = DateRange(
        start_date=date(2024, 12, 31),
        end_date=date(2024, 1, 1)
    )
except ValidationError as e:
    for error in e.errors():
        print(f"   Error: {error['msg']}")

# Example 3: mode='before' for preprocessing
class UserRegistration(BaseModel):
    """Registration with password confirmation."""
    
    username: str
    password: str
    
    @model_validator(mode="before")
    @classmethod
    def check_passwords_match(cls, data: dict) -> dict:
        """Verify password and confirm_password match before validation."""
        password = data.get("password")
        confirm = data.get("confirm_password")
        
        if password and confirm and password != confirm:
            raise ValueError("Passwords do not match")
        
        # Remove confirm_password from data (not in model)
        data.pop("confirm_password", None)
        return data

print("\n5. Valid registration:")
user = UserRegistration.model_validate({
    "username": "alice",
    "password": "secret123",
    "confirm_password": "secret123"
})
print(f"   Username: {user.username}")
print(f"   Password set: {'*' * len(user.password)}")

print("\n6. Password mismatch:")
try:
    bad_user = UserRegistration.model_validate({
        "username": "bob",
        "password": "secret123",
        "confirm_password": "different"
    })
except ValidationError as e:
    for error in e.errors():
        print(f"   Error: {error['msg']}")
```
