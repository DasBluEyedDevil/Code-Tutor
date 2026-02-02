---
type: "EXAMPLE"
title: "Relationships and Eager Loading"
---

**Loading related data efficiently with selectinload:**

**The N+1 Problem:**
Without eager loading, accessing relationships causes extra queries:
```python
users = await get_all_users()  # 1 query
for user in users:
    print(user.transactions)   # N additional queries!
```

**Solution: Eager Loading Strategies:**
- `selectinload()` - Separate SELECT IN query (best for collections)
- `joinedload()` - JOIN in same query (best for single objects)
- `subqueryload()` - Subquery (alternative to selectinload)

**When to Use Each:**
```python
# For one-to-many (User -> Transactions)
selectinload(User.transactions)

# For many-to-one (Transaction -> User)
joinedload(Transaction.user)
```

```python
from sqlalchemy import select
from sqlalchemy.orm import selectinload, joinedload
from sqlalchemy.ext.asyncio import AsyncSession
from fastapi import FastAPI, Depends
from pydantic import BaseModel
from typing import List

# Response schemas with nested data
class TransactionResponse(BaseModel):
    id: int
    amount: float
    category: str
    
    class Config:
        from_attributes = True

class UserWithTransactions(BaseModel):
    id: int
    name: str
    email: str
    transactions: List[TransactionResponse]
    
    class Config:
        from_attributes = True

app = FastAPI()

# Get user with all transactions (eager loaded)
@app.get("/users/{user_id}/transactions", response_model=UserWithTransactions)
async def get_user_with_transactions(
    user_id: int,
    db: AsyncSession = Depends(get_db)
):
    """Get user with all their transactions in ONE query."""
    result = await db.execute(
        select(User)
        .options(selectinload(User.transactions))  # Eager load!
        .where(User.id == user_id)
    )
    user = result.scalar_one_or_none()
    
    if not user:
        raise HTTPException(status_code=404, detail="User not found")
    
    return user

# Get all users with their transactions
@app.get("/users/with-transactions", response_model=List[UserWithTransactions])
async def get_all_users_with_transactions(
    db: AsyncSession = Depends(get_db)
):
    """Get all users with transactions - avoids N+1 queries."""
    result = await db.execute(
        select(User).options(selectinload(User.transactions))
    )
    return result.scalars().all()

# Get transaction with user info
@app.get("/transactions/{tx_id}")
async def get_transaction_with_user(
    tx_id: int,
    db: AsyncSession = Depends(get_db)
):
    """Get transaction with user - uses joinedload for single object."""
    result = await db.execute(
        select(Transaction)
        .options(joinedload(Transaction.user))  # JOIN for single object
        .where(Transaction.id == tx_id)
    )
    transaction = result.scalar_one_or_none()
    
    if not transaction:
        raise HTTPException(status_code=404, detail="Transaction not found")
    
    return {
        "id": transaction.id,
        "amount": transaction.amount,
        "category": transaction.category,
        "user_name": transaction.user.name
    }

# Demonstration
print("=== Eager Loading Strategies ===")

print("\nWithout eager loading (N+1 problem):")
print("  Query 1: SELECT * FROM users")
print("  Query 2: SELECT * FROM transactions WHERE user_id = 1")
print("  Query 3: SELECT * FROM transactions WHERE user_id = 2")
print("  ... (one query per user!)")

print("\nWith selectinload (2 queries total):")
print("  Query 1: SELECT * FROM users")
print("  Query 2: SELECT * FROM transactions WHERE user_id IN (1, 2, ...)")

print("\nWith joinedload (1 query total):")
print("  Query: SELECT * FROM users JOIN transactions ON ...")

print("\nBest practices:")
print("  - selectinload() for one-to-many (collections)")
print("  - joinedload() for many-to-one (single objects)")
print("  - Always eager load when you need related data")
```
