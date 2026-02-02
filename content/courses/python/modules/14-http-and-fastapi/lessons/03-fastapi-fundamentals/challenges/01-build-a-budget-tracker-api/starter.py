from fastapi import FastAPI, HTTPException, Query
from pydantic import BaseModel, Field
from typing import Optional

app = FastAPI(title="Budget Tracker API")

# In-memory storage
categories_db = {}
next_id = 1

# TODO: Create Pydantic models
class CategoryCreate(BaseModel):
    pass  # Add fields: name (str), budget_limit (float > 0), spent (float >= 0, default 0)

class Category(BaseModel):
    pass  # Add fields: id, name, budget_limit, spent

# TODO: Implement endpoints

@app.get("/categories/")
async def list_categories():
    # Return all categories
    pass

@app.get("/categories/{category_id}")
async def get_category(category_id: int):
    # Return specific category or 404
    pass

@app.post("/categories/")
async def create_category(category: CategoryCreate):
    # Create and return new category
    pass

@app.get("/categories/{category_id}/remaining")
async def get_remaining_budget(category_id: int):
    # Return remaining budget (limit - spent)
    pass

# Test the API structure
print("Budget Tracker API")
print("Run with: uvicorn main:app --reload")