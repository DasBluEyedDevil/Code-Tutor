---
type: "KEY_POINT"
title: "Nested Models and Lists"
---

**Nested Pydantic models:**
```python
class Address(BaseModel):
    street: str
    city: str
    country: str = "USA"

class User(BaseModel):
    name: str
    addresses: List[Address]  # List of nested models

# Request body:
{
    "name": "Alice",
    "addresses": [
        {"street": "123 Main", "city": "Boston"},
        {"street": "456 Oak", "city": "NYC"}
    ]
}
```

**Response with list:**
```python
from typing import List

@app.get("/users/", response_model=List[UserResponse])
def list_users():
    return get_all_users()  # Returns list
```