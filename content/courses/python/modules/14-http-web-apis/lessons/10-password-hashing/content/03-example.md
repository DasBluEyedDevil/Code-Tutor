---
type: "EXAMPLE"
title: "User Registration Flow"
---

**Secure Registration Pattern**

When a user registers, you must:
1. Validate the email format (use `EmailStr`)
2. Check if the email is already registered
3. Hash the password before storing
4. Never log or return the password hash

**Important Security Points:**
- Store `hashed_password`, never `password`
- Use `EmailStr` for automatic email validation
- Return minimal user info (no password hash)
- Use proper HTTP status codes (400 for validation errors)

```python
from fastapi import FastAPI, HTTPException, Depends
from pydantic import BaseModel, EmailStr
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from passlib.context import CryptContext

app = FastAPI()

# Password hashing setup
pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

def hash_password(password: str) -> str:
    return pwd_context.hash(password)

# Pydantic models
class UserCreate(BaseModel):
    """User registration request."""
    email: EmailStr  # Automatic email format validation
    password: str    # Plain text from user (will be hashed)

class UserResponse(BaseModel):
    """User response (never includes password!)."""
    id: int
    email: str
    # Note: No password or hashed_password field!

# Example User SQLAlchemy model (simplified)
# class User(Base):
#     __tablename__ = "users"
#     id = Column(Integer, primary_key=True)
#     email = Column(String, unique=True, nullable=False)
#     hashed_password = Column(String, nullable=False)

@app.post("/register", response_model=UserResponse, status_code=201)
async def register(user: UserCreate, db: AsyncSession = Depends(get_db)):
    """Register a new user with secure password hashing."""
    
    # Step 1: Check if user already exists
    result = await db.execute(
        select(User).where(User.email == user.email)
    )
    existing_user = result.scalar_one_or_none()
    
    if existing_user:
        raise HTTPException(
            status_code=400,
            detail="Email already registered"
        )
    
    # Step 2: Hash password BEFORE storing
    # CRITICAL: Never store plain text passwords!
    hashed = hash_password(user.password)
    
    # Step 3: Create user with hashed password
    db_user = User(
        email=user.email,
        hashed_password=hashed  # Store the HASH, not the password
    )
    
    db.add(db_user)
    await db.commit()
    await db.refresh(db_user)
    
    # Step 4: Return user WITHOUT password info
    return db_user

# Demonstration of the flow
print("=== User Registration Flow ===")
print()
print("1. User submits: { email: 'alice@example.com', password: 'secret123' }")
print()
print("2. Server validates email format (EmailStr)")
print()
print("3. Server checks database for existing user")
print()
print("4. Server hashes password:")
example_hash = hash_password("secret123")
print(f"   'secret123' -> '{example_hash[:40]}...'")
print()
print("5. Server stores in database:")
print("   { id: 1, email: 'alice@example.com', hashed_password: '$2b$12...' }")
print()
print("6. Server responds with:")
print("   { id: 1, email: 'alice@example.com' }")
print("   (No password in response!)")
```
