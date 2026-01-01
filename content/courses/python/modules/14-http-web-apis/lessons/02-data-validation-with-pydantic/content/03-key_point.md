---
type: "KEY_POINT"
title: "Field Validators and Constraints"
---

**Built-in constraints:**
```python
from pydantic import Field

class Product(BaseModel):
    name: str = Field(min_length=1, max_length=100)
    price: float = Field(gt=0)  # greater than 0
    quantity: int = Field(ge=0)  # >= 0
    sku: str = Field(pattern=r'^[A-Z]{3}-\d{4}$')  # regex
```

**Custom validators:**
```python
from pydantic import field_validator

class User(BaseModel):
    username: str
    
    @field_validator('username')
    @classmethod
    def username_alphanumeric(cls, v):
        if not v.isalnum():
            raise ValueError('must be alphanumeric')
        return v.lower()  # normalize
```

**Pydantic v2 syntax:**
- `model_dump()` replaces `.dict()`
- `model_validate()` replaces `.parse_obj()`
- `@field_validator` replaces `@validator`