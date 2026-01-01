---
type: "EXAMPLE"
title: "Authentication Dependency"
---

Dependencies with Depends() inject shared logic like authentication. The verify_token function extracts and validates JWT tokens from headers, and can be reused across multiple endpoints. Invalid tokens raise HTTPException(401).

```python
from fastapi import Depends, HTTPException, Header

async def verify_token(authorization: str = Header(...)):
    """Verify JWT token from Authorization header"""
    if not authorization.startswith("Bearer "):
        raise HTTPException(401, "Invalid auth header")
    
    token = authorization.split(" ")[1]
    try:
        payload = jwt.decode(token, SECRET_KEY)
        return payload["user_id"]
    except jwt.JWTError:
        raise HTTPException(401, "Invalid token")

@app.get("/me/")
def get_current_user(user_id: int = Depends(verify_token)):
    # Only runs if token is valid!
    return {"user_id": user_id}

@app.get("/admin/")
def admin_only(
    user_id: int = Depends(verify_token),
    is_admin: bool = Depends(check_admin)
):
    # Multiple dependencies!
    return {"admin": True}
```
