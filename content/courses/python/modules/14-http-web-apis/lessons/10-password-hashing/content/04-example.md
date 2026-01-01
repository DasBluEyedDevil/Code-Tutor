---
type: "EXAMPLE"
title: "Login Verification"
---

**Secure Login Pattern**

During login, verify the password against the stored hash:
1. Find the user by email
2. Use `verify_password()` to compare
3. Return generic error messages (don't reveal which field was wrong)
4. Never log password attempts

**Security Best Practice:**
Always return the same error message for "user not found" and "wrong password". This prevents attackers from discovering valid emails.

```python
from fastapi import FastAPI, HTTPException, Depends
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from passlib.context import CryptContext

app = FastAPI()

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

def verify_password(plain_password: str, hashed_password: str) -> bool:
    """Verify a password against its hash."""
    return pwd_context.verify(plain_password, hashed_password)

async def get_user_by_email(db: AsyncSession, email: str):
    """Retrieve user from database by email."""
    result = await db.execute(
        select(User).where(User.email == email)
    )
    return result.scalar_one_or_none()

@app.post("/login")
async def login(
    email: str,
    password: str,
    db: AsyncSession = Depends(get_db)
):
    """Authenticate user with email and password."""
    
    # Step 1: Find user by email
    user = await get_user_by_email(db, email)
    
    # Step 2: Verify password (also handles user not found)
    # SECURITY: Use same error message for both cases!
    # This prevents email enumeration attacks.
    if not user or not verify_password(password, user.hashed_password):
        raise HTTPException(
            status_code=401,
            detail="Invalid credentials"  # Generic message!
        )
    
    # Step 3: Login successful
    # In a real app, you'd create a JWT token here
    return {
        "message": "Login successful",
        "user_id": user.id
    }

# Demonstration
print("=== Login Verification Flow ===")
print()

# Simulate stored hash
stored_hash = pwd_context.hash("correctpassword")

print("Stored in database:")
print(f"  email: 'alice@example.com'")
print(f"  hashed_password: '{stored_hash[:40]}...'")
print()

print("Login attempt 1: Correct password")
result = verify_password("correctpassword", stored_hash)
print(f"  verify_password('correctpassword', hash) = {result}")
print("  Response: { message: 'Login successful' }")
print()

print("Login attempt 2: Wrong password")
result = verify_password("wrongpassword", stored_hash)
print(f"  verify_password('wrongpassword', hash) = {result}")
print("  Response: 401 'Invalid credentials'")
print()

print("SECURITY NOTE:")
print("  Same error for 'user not found' and 'wrong password'")
print("  Prevents attackers from discovering valid emails!")
```
