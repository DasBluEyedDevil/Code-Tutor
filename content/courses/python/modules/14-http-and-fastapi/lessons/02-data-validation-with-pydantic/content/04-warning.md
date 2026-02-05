---
type: "WARNING"
title: "Pydantic Validation Gotchas"
---

**Common Mistakes with Pydantic:**

❌ **Don't assume Pydantic catches everything**

```python
# Pydantic validates types, but not business logic
class User(BaseModel):
    age: int

# This is valid to Pydantic (it's an int!)
User(age=-5)  # Negative age makes no sense

# Add custom validation:
from pydantic import field_validator

class User(BaseModel):
    age: int
    
    @field_validator('age')
    @classmethod
    def age_must_be_positive(cls, v):
        if v < 0:
            raise ValueError('Age must be positive')
        return v
```

❌ **Don't mix Pydantic v1 and v2 patterns**

```python
# v1 (DEPRECATED)
user.dict()
User.parse_obj(data)
Config class inside model

# v2 (CURRENT)
user.model_dump()
User.model_validate(data)
model_config = ConfigDict(...)
```

❌ **Don't ignore strict mode**

```python
# By default, Pydantic coerces types
class Item(BaseModel):
    price: int

Item(price="42")  # Works! "42" → 42

# This can hide bugs. Use strict mode:
from pydantic import ConfigDict

class Item(BaseModel):
    model_config = ConfigDict(strict=True)
    price: int

Item(price="42")  # ValidationError!
```

❌ **Don't forget Optional fields need defaults**

```python
# WRONG - Optional without default
class User(BaseModel):
    name: str
    nickname: str | None  # Required field!

User(name="Alice")  # ValidationError: nickname required

# RIGHT
class User(BaseModel):
    name: str
    nickname: str | None = None  # Now truly optional
```
