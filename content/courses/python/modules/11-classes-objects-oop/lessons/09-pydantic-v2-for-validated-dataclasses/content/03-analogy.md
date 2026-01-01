---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Pydantic v2 Model:**
```python
from pydantic import BaseModel, Field, field_validator, computed_field
from pydantic import ConfigDict

class MyModel(BaseModel):
    # Configuration
    model_config = ConfigDict(
        frozen=True,           # Immutable
        str_strip_whitespace=True,  # Strip strings
        validate_assignment=True,   # Validate on assignment
    )
    
    # Fields with constraints
    name: str = Field(min_length=1, max_length=100)
    amount: float = Field(gt=0, le=1000000)
    email: str = Field(pattern=r'^[\w.-]+@[\w.-]+\.\w+$')
    
    # Optional with default
    description: str | None = None
    
    # Custom validator
    @field_validator('name')
    @classmethod
    def validate_name(cls, v: str) -> str:
        return v.title()  # Capitalize
    
    # Computed property
    @computed_field
    @property
    def display_name(self) -> str:
        return f"{self.name} (${self.amount})"
```

**Field constraints:**
```python
Field(
    gt=0,              # Greater than
    ge=0,              # Greater than or equal
    lt=100,            # Less than
    le=100,            # Less than or equal
    min_length=1,      # String min length
    max_length=100,    # String max length
    pattern=r'regex',  # Regex pattern
    default=None,      # Default value
    default_factory=list,  # Factory for mutable
)
```

**Model methods:**
```python
# Create instance
obj = MyModel(name="test", amount=100)

# Serialize
obj.model_dump()       # To dict
obj.model_dump_json()  # To JSON string

# Validate data
MyModel.model_validate({"name": "test", "amount": 100})
MyModel.model_validate_json('{"name": "test", "amount": 100}')
```