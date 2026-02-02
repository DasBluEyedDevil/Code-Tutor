---
type: "EXAMPLE"
title: "Pydantic BaseModel Basics"
---

Pydantic models define data structures with automatic type validation. Use Field() for constraints like min/max values, and EmailStr for email validation. Invalid data raises ValidationError with detailed error messages.

```python
from pydantic import BaseModel, Field, EmailStr, ValidationError
from datetime import datetime
from typing import Optional

class User(BaseModel):
    name: str
    email: EmailStr
    age: int = Field(ge=0, le=150)  # 0-150
    is_active: bool = True
    created_at: datetime = Field(default_factory=datetime.now)
    bio: Optional[str] = None

# Valid data - works!
user = User(name="Alice", email="alice@example.com", age=25)
print(user.model_dump())  # Convert to dict

# Invalid data - raises ValidationError
try:
    bad_user = User(name="Bob", email="not-an-email", age=-5)
except ValidationError as e:
    print(e.errors())  # Detailed error info
```
