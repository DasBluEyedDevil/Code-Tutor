---
type: "EXAMPLE"
title: "Field Validators"
---

**Custom Validation with @field_validator:**

Field validators let you add custom logic beyond built-in constraints.

**Key Rules:**
1. Use `@field_validator('field_name')` decorator
2. Always add `@classmethod` decorator below it
3. First parameter is `cls`, second is the value `v`
4. Raise `ValueError` for validation failures
5. Return the (possibly modified) value

**Validator Modes:**
```python
@field_validator('field', mode='before')  # Before Pydantic validates
@field_validator('field', mode='after')   # After Pydantic validates (default)
```

**Common Use Cases:**
- Normalizing data (lowercase, strip whitespace)
- Complex validation (business rules)
- Data transformation (parse strings to numbers)
- Cross-referencing external data

```python
from pydantic import BaseModel, field_validator, ValidationError

print("=== Pydantic v2 Field Validators ===")

class TransactionCreate(BaseModel):
    """Transaction model with custom validators."""
    
    amount: float
    category: str
    reference_code: str | None = None
    
    @field_validator("amount")
    @classmethod
    def amount_must_be_positive(cls, v: float) -> float:
        """Ensure amount is positive and round to 2 decimals."""
        if v <= 0:
            raise ValueError("Amount must be positive")
        return round(v, 2)  # Normalize to 2 decimal places
    
    @field_validator("category")
    @classmethod
    def category_must_be_valid(cls, v: str) -> str:
        """Validate and normalize category."""
        valid = ["income", "expense", "transfer"]
        normalized = v.lower().strip()
        if normalized not in valid:
            raise ValueError(f"Category must be one of: {valid}")
        return normalized  # Return lowercase version
    
    @field_validator("reference_code")
    @classmethod
    def validate_reference_code(cls, v: str | None) -> str | None:
        """Validate reference code format if provided."""
        if v is None:
            return v
        # Must be format: TXN-XXXXX (TXN- followed by 5 digits)
        if not v.startswith("TXN-") or len(v) != 9:
            raise ValueError("Reference code must be format TXN-XXXXX")
        if not v[4:].isdigit():
            raise ValueError("Reference code must end with 5 digits")
        return v.upper()

# Test valid transaction
print("\n1. Valid transaction:")
txn = TransactionCreate(
    amount=99.999,      # Will be rounded to 99.99
    category="EXPENSE", # Will be normalized to "expense"
    reference_code="TXN-12345"
)
print(f"   Amount: ${txn.amount}")  # 99.99
print(f"   Category: {txn.category}")  # expense
print(f"   Reference: {txn.reference_code}")

# Test validation errors
print("\n2. Invalid amount:")
try:
    bad = TransactionCreate(amount=-50, category="expense")
except ValidationError as e:
    for error in e.errors():
        print(f"   Error: {error['msg']}")

print("\n3. Invalid category:")
try:
    bad = TransactionCreate(amount=50, category="invalid")
except ValidationError as e:
    for error in e.errors():
        print(f"   Error: {error['msg']}")

print("\n4. Invalid reference code:")
try:
    bad = TransactionCreate(
        amount=50,
        category="expense",
        reference_code="INVALID"
    )
except ValidationError as e:
    for error in e.errors():
        print(f"   Error: {error['msg']}")

# Validator that runs before type coercion
print("\n5. Mode='before' validator example:")

class FlexibleAmount(BaseModel):
    """Accept amount as string or number."""
    amount: float
    
    @field_validator("amount", mode="before")
    @classmethod
    def parse_amount(cls, v):
        """Handle string amounts like '$50' or '50.00'."""
        if isinstance(v, str):
            # Remove currency symbols and commas
            cleaned = v.replace("$", "").replace(",", "").strip()
            return float(cleaned)
        return v

print("   From string '$1,234.56':")
result = FlexibleAmount(amount="$1,234.56")
print(f"   Parsed amount: {result.amount}")
```
