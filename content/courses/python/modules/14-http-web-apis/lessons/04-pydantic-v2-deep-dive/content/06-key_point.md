---
type: "KEY_POINT"
title: "Pydantic v2 Key Takeaways"
---

**Core Concepts:**
- **BaseModel** - Define data structures with type hints
- **Field()** - Add constraints and metadata
- **@field_validator** - Custom single-field validation
- **@model_validator** - Cross-field validation
- **pydantic-settings** - Configuration management

**v2 Syntax Summary:**
```python
# Models
model.model_dump()        # Convert to dict
model.model_dump_json()   # Convert to JSON string
Model.model_validate(data) # Create from dict
Model.model_json_schema() # Get JSON schema

# Validators
@field_validator('field')
@classmethod
def validate(cls, v): ...

@model_validator(mode='after')
def validate(self) -> Self: ...

# Configuration
model_config = {
    'str_strip_whitespace': True,
    'extra': 'forbid'
}
```

**Best Practices:**
- Use enums for categorical values
- Return modified values from validators
- Use `mode='before'` for preprocessing
- Keep validation logic simple and focused
- Use Field descriptions for API documentation
- Never commit secrets in settings files