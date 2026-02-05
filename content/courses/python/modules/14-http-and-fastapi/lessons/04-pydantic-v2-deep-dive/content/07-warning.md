---
type: "WARNING"
title: "Pydantic v2 Migration Pitfalls"
---

**If You're Coming from Pydantic v1:**

❌ **Don't use deprecated methods**

| v1 (DEPRECATED) | v2 (CURRENT) |
|-----------------|--------------|
| `.dict()` | `.model_dump()` |
| `.json()` | `.model_dump_json()` |
| `.parse_obj()` | `.model_validate()` |
| `.parse_raw()` | `.model_validate_json()` |
| `schema()` | `.model_json_schema()` |
| `__fields__` | `.model_fields` |

❌ **Don't use old Config class**

```python
# v1 (DEPRECATED)
class User(BaseModel):
    name: str
    
    class Config:
        str_strip_whitespace = True

# v2 (CURRENT)
from pydantic import ConfigDict

class User(BaseModel):
    model_config = ConfigDict(str_strip_whitespace=True)
    name: str
```

❌ **Don't use old validator decorators**

```python
# v1 (DEPRECATED)
from pydantic import validator

class User(BaseModel):
    age: int
    
    @validator('age')
    def check_age(cls, v):
        ...

# v2 (CURRENT)
from pydantic import field_validator

class User(BaseModel):
    age: int
    
    @field_validator('age')
    @classmethod
    def check_age(cls, v):
        ...
```

❌ **Don't ignore strict mode differences**

```python
# v2 is stricter by default with some types
# Test thoroughly when migrating!

# v1: float accepted for int fields
# v2: depends on strict mode setting
```

**Migration Checklist:**
- [ ] Replace all `.dict()` with `.model_dump()`
- [ ] Replace all `parse_obj()` with `model_validate()`
- [ ] Move Config to `model_config = ConfigDict(...)`
- [ ] Update `@validator` to `@field_validator` with `@classmethod`
- [ ] Run tests to catch behavior changes
