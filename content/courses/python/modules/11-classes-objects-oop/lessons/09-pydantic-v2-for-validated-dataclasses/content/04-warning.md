---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using Pydantic v1 Syntax with v2**
```python
# WRONG - v1 syntax
from pydantic import BaseModel, validator

class Model(BaseModel):
    name: str
    
    @validator('name')  # v1 decorator
    def validate_name(cls, v):
        return v.upper()

# CORRECT - v2 syntax
from pydantic import BaseModel, field_validator

class Model(BaseModel):
    name: str
    
    @field_validator('name')
    @classmethod  # Required in v2!
    def validate_name(cls, v: str) -> str:
        return v.upper()
```

**2. Forgetting @classmethod on Validators**
```python
# WRONG - Missing @classmethod in v2
class Model(BaseModel):
    amount: float
    
    @field_validator('amount')
    def validate_amount(cls, v):  # Won't work!
        return round(v, 2)

# CORRECT - Include @classmethod
class Model(BaseModel):
    amount: float
    
    @field_validator('amount')
    @classmethod
    def validate_amount(cls, v: float) -> float:
        return round(v, 2)
```

**3. Mutating Frozen Models**
```python
# WRONG - Trying to mutate frozen model
class Transaction(BaseModel):
    model_config = ConfigDict(frozen=True)
    amount: float

t = Transaction(amount=100)
t.amount = 200  # ValidationError: Instance is frozen

# SOLUTION - Create new instance
new_t = t.model_copy(update={"amount": 200})
```

**4. Not Handling ValidationError**
```python
# WRONG - Validation error crashes app
from pydantic import BaseModel

class User(BaseModel):
    age: int

user = User(age="not a number")  # Crashes!

# CORRECT - Handle validation errors
from pydantic import BaseModel, ValidationError

try:
    user = User(age="not a number")
except ValidationError as e:
    print(f"Validation failed: {e}")
```

**5. Confusing model_dump() with dict()**
```python
# v1 style (deprecated)
data = model.dict()  # Works but deprecated

# v2 style (correct)
data = model.model_dump()  # Use this!
json_str = model.model_dump_json()  # For JSON
```