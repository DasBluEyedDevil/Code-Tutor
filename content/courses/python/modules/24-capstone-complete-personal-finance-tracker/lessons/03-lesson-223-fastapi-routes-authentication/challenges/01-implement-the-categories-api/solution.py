from typing import Annotated

from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, Field

from ..models.transaction import Category, TransactionType
from ..repositories.category import CategoryRepository
from ..services.auth import CurrentUser

router = APIRouter(prefix="/categories", tags=["categories"])


class CategoryCreate(BaseModel):
    name: str = Field(min_length=1, max_length=100)
    type: TransactionType
    icon: str = Field(default="📁", max_length=10)


class CategoryResponse(BaseModel):
    id: int
    name: str
    type: TransactionType
    icon: str
    is_default: bool
    
    model_config = {"from_attributes": True}


def get_category_repo() -> CategoryRepository:
    return CategoryRepository()


@router.get("", response_model=list[CategoryResponse])
async def list_categories(
    current_user: CurrentUser,
    repo: Annotated[CategoryRepository, Depends(get_category_repo)],
):
    categories = await repo.get_user_categories(current_user.id)
    return [CategoryResponse.model_validate(cat) for cat in categories]


@router.post("", response_model=CategoryResponse, status_code=status.HTTP_201_CREATED)
async def create_category(
    current_user: CurrentUser,
    data: CategoryCreate,
    repo: Annotated[CategoryRepository, Depends(get_category_repo)],
):
    existing = await repo.get_by_name(data.name, current_user.id)
    if existing:
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail=f"Category '{data.name}' already exists",
        )
    
    category = await repo.create(Category(
        id=None,
        name=data.name,
        type=data.type,
        user_id=current_user.id,
        icon=data.icon,
        is_default=False,
    ))
    return CategoryResponse.model_validate(category)


@router.delete("/{category_id}", status_code=status.HTTP_204_NO_CONTENT)
async def delete_category(
    category_id: int,
    current_user: CurrentUser,
    repo: Annotated[CategoryRepository, Depends(get_category_repo)],
):
    category = await repo.get_by_id(category_id, current_user.id)
    
    if not category:
        raise HTTPException(status_code=404, detail="Category not found")
    
    if category.is_default:
        raise HTTPException(status_code=400, detail="Cannot delete default categories")
    
    has_transactions = await repo.has_transactions(category_id)
    if has_transactions:
        raise HTTPException(
            status_code=400,
            detail="Cannot delete category with transactions. Move transactions first.",
        )
    
    await repo.delete(category_id, current_user.id)