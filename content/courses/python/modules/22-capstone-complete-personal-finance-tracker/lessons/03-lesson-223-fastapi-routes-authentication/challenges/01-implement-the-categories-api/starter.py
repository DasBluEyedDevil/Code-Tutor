from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, Field

from ..models.transaction import Category, TransactionType
from ..services.auth import CurrentUser

router = APIRouter(prefix="/categories", tags=["categories"])


class CategoryCreate(BaseModel):
    """TODO: Schema for creating a category."""
    pass


class CategoryResponse(BaseModel):
    """TODO: Schema for category in response."""
    pass


@router.get("", response_model=list[CategoryResponse])
async def list_categories(current_user: CurrentUser):
    """TODO: List user's categories (default + custom)."""
    pass


@router.post("", response_model=CategoryResponse, status_code=status.HTTP_201_CREATED)
async def create_category(current_user: CurrentUser, data: CategoryCreate):
    """TODO: Create a custom category."""
    pass


@router.delete("/{category_id}", status_code=status.HTTP_204_NO_CONTENT)
async def delete_category(category_id: int, current_user: CurrentUser):
    """TODO: Delete a category (only if custom and no transactions)."""
    pass