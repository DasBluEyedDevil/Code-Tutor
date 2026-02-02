---
type: "EXAMPLE"
title: "Request and Response Models"
---

Separate models for input (Create) and output (Response) provide clean API contracts. The response_model parameter filters output to only include specified fields - preventing accidental data leaks like exposing passwords.

```python
from pydantic import BaseModel
from typing import Optional

# Request model (what client sends)
class UserCreate(BaseModel):
    email: str
    password: str
    name: Optional[str] = None

# Response model (what we return)
class UserResponse(BaseModel):
    id: int
    email: str
    name: Optional[str]
    # Note: no password!

@app.post("/users/", response_model=UserResponse)
def create_user(user: UserCreate):
    # user is validated UserCreate
    # Return is validated against UserResponse
    db_user = create_user_in_db(user)
    return db_user  # Password filtered out!
```
