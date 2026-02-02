---
type: "EXAMPLE"
title: "CRUD Operations"
---

**Async CRUD operations with SQLAlchemy 2.0:**

All database operations are now async and use the `select()` function.

**Pattern Changes from 1.x:**
```python
# OLD: session.query(User).filter(...)
# NEW: await session.execute(select(User).where(...))

# OLD: session.query(User).get(id)
# NEW: await session.get(User, id)
```

**Result Processing:**
- `result.scalars()` - Get ORM objects (not rows)
- `result.scalar_one()` - Get exactly one result (raises if 0 or 2+)
- `result.scalar_one_or_none()` - Get one or None
- `result.all()` - Get all as list

```python
from sqlalchemy import select
from sqlalchemy.ext.asyncio import AsyncSession
from fastapi import FastAPI, Depends, HTTPException
from pydantic import BaseModel

# Pydantic schemas for request/response
class UserCreate(BaseModel):
    email: str
    name: str

class UserResponse(BaseModel):
    id: int
    email: str
    name: str
    
    class Config:
        from_attributes = True  # Enable ORM mode

app = FastAPI()

# CREATE - Add new user
@app.post("/users/", response_model=UserResponse)
async def create_user(user: UserCreate, db: AsyncSession = Depends(get_db)):
    """Create a new user."""
    db_user = User(**user.model_dump())
    db.add(db_user)
    await db.commit()
    await db.refresh(db_user)  # Reload to get generated id
    return db_user

# READ - Get single user
@app.get("/users/{user_id}", response_model=UserResponse)
async def get_user(user_id: int, db: AsyncSession = Depends(get_db)):
    """Get user by ID."""
    result = await db.execute(
        select(User).where(User.id == user_id)
    )
    user = result.scalar_one_or_none()
    
    if not user:
        raise HTTPException(status_code=404, detail="User not found")
    return user

# READ - Get all users
@app.get("/users/", response_model=list[UserResponse])
async def get_users(
    skip: int = 0,
    limit: int = 100,
    db: AsyncSession = Depends(get_db)
):
    """Get all users with pagination."""
    result = await db.execute(
        select(User).offset(skip).limit(limit)
    )
    return result.scalars().all()

# UPDATE - Modify user
@app.put("/users/{user_id}", response_model=UserResponse)
async def update_user(
    user_id: int,
    user_update: UserCreate,
    db: AsyncSession = Depends(get_db)
):
    """Update user by ID."""
    result = await db.execute(
        select(User).where(User.id == user_id)
    )
    user = result.scalar_one_or_none()
    
    if not user:
        raise HTTPException(status_code=404, detail="User not found")
    
    user.email = user_update.email
    user.name = user_update.name
    await db.commit()
    await db.refresh(user)
    return user

# DELETE - Remove user
@app.delete("/users/{user_id}")
async def delete_user(user_id: int, db: AsyncSession = Depends(get_db)):
    """Delete user by ID."""
    result = await db.execute(
        select(User).where(User.id == user_id)
    )
    user = result.scalar_one_or_none()
    
    if not user:
        raise HTTPException(status_code=404, detail="User not found")
    
    await db.delete(user)
    await db.commit()
    return {"message": "User deleted"}

# Demonstration
print("=== Async CRUD Operations ===")
print("\nEndpoints:")
print("  POST   /users/         - Create user")
print("  GET    /users/{id}     - Get single user")
print("  GET    /users/         - Get all users")
print("  PUT    /users/{id}     - Update user")
print("  DELETE /users/{id}     - Delete user")

print("\nKey patterns:")
print("  - select(Model).where(...) for queries")
print("  - result.scalar_one_or_none() for single row")
print("  - result.scalars().all() for multiple rows")
print("  - await db.commit() after changes")
print("  - await db.refresh(obj) to reload")
```
