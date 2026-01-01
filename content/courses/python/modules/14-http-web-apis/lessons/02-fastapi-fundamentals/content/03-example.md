---
type: "EXAMPLE"
title: "Path and Query Parameters"
---

**Path Parameters:**
```python
@app.get("/items/{item_id}")
async def get_item(item_id: int):  # Extracted from URL
    return {"item_id": item_id}
```
- Defined in the path with `{parameter}`
- Type hints enable automatic validation
- Invalid types return 422 Validation Error

**Query Parameters:**
```python
@app.get("/items/")
async def list_items(skip: int = 0, limit: int = 10):
    return {"skip": skip, "limit": limit}
```
- Parameters not in path become query params
- Default values make them optional
- Access via `?skip=0&limit=10`

**Query with Validation:**
```python
from fastapi import Query

@app.get("/search/")
async def search(
    q: str = Query(..., min_length=3),  # Required, min 3 chars
    limit: int = Query(default=10, le=100)  # Max 100
):
    return {"query": q, "limit": limit}
```
- `Query(...)` means required
- `Query(default=X)` sets default
- Add constraints: `min_length`, `max_length`, `le`, `ge`

```python
from fastapi import FastAPI, Query, Path, HTTPException
from typing import Optional

app = FastAPI(title="Transaction API")

# Sample data
transactions = {
    1: {"id": 1, "amount": 100.00, "category": "Food", "description": "Groceries"},
    2: {"id": 2, "amount": 50.00, "category": "Transport", "description": "Uber"},
    3: {"id": 3, "amount": 200.00, "category": "Food", "description": "Restaurant"},
    4: {"id": 4, "amount": 75.00, "category": "Entertainment", "description": "Movies"},
    5: {"id": 5, "amount": 300.00, "category": "Shopping", "description": "Clothes"},
}

print("=== Path Parameters ===")

@app.get("/transactions/{transaction_id}")
async def get_transaction(
    transaction_id: int = Path(..., title="Transaction ID", ge=1)
):
    """Get a specific transaction by ID."""
    if transaction_id not in transactions:
        raise HTTPException(status_code=404, detail="Transaction not found")
    return transactions[transaction_id]

# Demonstrate path parameter extraction
print("Path: /transactions/{transaction_id}")
print("Example: /transactions/1 -> Returns transaction with ID 1")
print("Type validation: /transactions/abc -> 422 Error (not an integer)")

print("\n=== Query Parameters ===")

@app.get("/transactions/")
async def list_transactions(
    skip: int = Query(default=0, ge=0, description="Records to skip"),
    limit: int = Query(default=10, le=100, description="Max records to return"),
    category: Optional[str] = Query(default=None, description="Filter by category")
):
    """List transactions with pagination and filtering."""
    results = list(transactions.values())
    
    # Filter by category if provided
    if category:
        results = [t for t in results if t["category"].lower() == category.lower()]
    
    # Apply pagination
    paginated = results[skip:skip + limit]
    
    return {
        "transactions": paginated,
        "total": len(results),
        "skip": skip,
        "limit": limit,
        "category_filter": category
    }

print("Path: /transactions/")
print("Query params: skip, limit, category")
print("Example: /transactions/?skip=0&limit=5")
print("Example: /transactions/?category=Food")

print("\n=== Query Validation ===")

@app.get("/search/")
async def search_transactions(
    q: str = Query(
        ...,  # Required
        min_length=2,
        max_length=50,
        description="Search query"
    ),
    min_amount: Optional[float] = Query(default=None, ge=0),
    max_amount: Optional[float] = Query(default=None, ge=0)
):
    """Search transactions by description."""
    results = [
        t for t in transactions.values()
        if q.lower() in t["description"].lower()
    ]
    
    if min_amount is not None:
        results = [t for t in results if t["amount"] >= min_amount]
    if max_amount is not None:
        results = [t for t in results if t["amount"] <= max_amount]
    
    return {"query": q, "results": results, "count": len(results)}

print("Required query: q (2-50 chars)")
print("Optional: min_amount, max_amount")
print("Example: /search/?q=food&min_amount=50")

print("\n=== Combined Parameters ===")

@app.get("/users/{user_id}/transactions/")
async def get_user_transactions(
    user_id: int = Path(..., ge=1),
    skip: int = 0,
    limit: int = Query(default=10, le=50)
):
    """Get transactions for a specific user (demo)."""
    # In real app, would filter by user_id
    return {
        "user_id": user_id,
        "transactions": list(transactions.values())[skip:skip + limit]
    }

print("Path + Query: /users/{user_id}/transactions/?skip=0&limit=5")
```
