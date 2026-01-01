from fastapi import FastAPI, HTTPException, Query
from pydantic import BaseModel, Field
from typing import Optional

app = FastAPI(
    title="Budget Tracker API",
    description="Track spending across budget categories",
    version="1.0.0"
)

# In-memory storage
categories_db: dict[int, "Category"] = {}
next_id = 1

# Pydantic Models
class CategoryCreate(BaseModel):
    """Model for creating a new category."""
    name: str = Field(..., min_length=1, max_length=50, description="Category name")
    budget_limit: float = Field(..., gt=0, description="Monthly budget limit")
    spent: float = Field(default=0, ge=0, description="Amount already spent")

class Category(BaseModel):
    """Full category model with ID."""
    id: int
    name: str
    budget_limit: float
    spent: float

class RemainingBudget(BaseModel):
    """Response model for remaining budget."""
    category_id: int
    category_name: str
    budget_limit: float
    spent: float
    remaining: float
    percent_used: float

# Endpoints
@app.get("/categories/", response_model=list[Category])
async def list_categories(
    skip: int = Query(default=0, ge=0),
    limit: int = Query(default=10, le=50)
):
    """List all budget categories with pagination."""
    categories = list(categories_db.values())
    return categories[skip:skip + limit]

@app.get("/categories/{category_id}", response_model=Category)
async def get_category(category_id: int):
    """Get a specific category by ID."""
    if category_id not in categories_db:
        raise HTTPException(
            status_code=404,
            detail=f"Category with ID {category_id} not found"
        )
    return categories_db[category_id]

@app.post("/categories/", response_model=Category, status_code=201)
async def create_category(category: CategoryCreate):
    """Create a new budget category."""
    global next_id
    
    # Check for duplicate names
    for existing in categories_db.values():
        if existing.name.lower() == category.name.lower():
            raise HTTPException(
                status_code=400,
                detail=f"Category '{category.name}' already exists"
            )
    
    # Create new category
    new_category = Category(
        id=next_id,
        name=category.name,
        budget_limit=category.budget_limit,
        spent=category.spent
    )
    
    categories_db[next_id] = new_category
    next_id += 1
    
    return new_category

@app.get("/categories/{category_id}/remaining", response_model=RemainingBudget)
async def get_remaining_budget(category_id: int):
    """Calculate remaining budget for a category."""
    if category_id not in categories_db:
        raise HTTPException(
            status_code=404,
            detail=f"Category with ID {category_id} not found"
        )
    
    category = categories_db[category_id]
    remaining = category.budget_limit - category.spent
    percent_used = (category.spent / category.budget_limit) * 100 if category.budget_limit > 0 else 0
    
    return RemainingBudget(
        category_id=category.id,
        category_name=category.name,
        budget_limit=category.budget_limit,
        spent=category.spent,
        remaining=remaining,
        percent_used=round(percent_used, 1)
    )

# Demo: Add sample data and show structure
print("=== Budget Tracker API ===")
print("\nEndpoints:")
for route in app.routes:
    if hasattr(route, 'methods'):
        methods = ', '.join(route.methods - {'HEAD', 'OPTIONS'})
        if methods:
            print(f"  {methods:6} {route.path}")

print("\nExample requests:")
print("  POST /categories/")
print('  Body: {"name": "Groceries", "budget_limit": 500}')
print("\n  GET /categories/1/remaining")
print('  Response: {"remaining": 350, "percent_used": 30.0}')

print("\nRun with: uvicorn main:app --reload")