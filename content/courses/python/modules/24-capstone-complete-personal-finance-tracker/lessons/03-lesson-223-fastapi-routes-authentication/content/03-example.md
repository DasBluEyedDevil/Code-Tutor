---
type: "EXAMPLE"
title: "Transaction API Routes"
---

FastAPI router for transaction endpoints:

```python
# src/finance_tracker/api/transactions.py
from datetime import date
from decimal import Decimal
from typing import Annotated

from fastapi import APIRouter, Depends, HTTPException, Query, status
from pydantic import BaseModel, Field

from ..models.transaction import Transaction, TransactionSummary
from ..repositories.transaction import TransactionRepository
from ..services.auth import CurrentUser


router = APIRouter(prefix="/transactions", tags=["transactions"])


# Pydantic schemas for API
class TransactionCreate(BaseModel):
    """Schema for creating a transaction."""
    amount: Decimal = Field(gt=0, description="Transaction amount (positive)")
    description: str = Field(min_length=1, max_length=500)
    category_id: int = Field(gt=0)
    transaction_date: date | None = None


class TransactionUpdate(BaseModel):
    """Schema for updating a transaction."""
    amount: Decimal | None = Field(default=None, gt=0)
    description: str | None = Field(default=None, min_length=1, max_length=500)
    category_id: int | None = Field(default=None, gt=0)
    transaction_date: date | None = None


class TransactionResponse(BaseModel):
    """Schema for transaction in response."""
    id: int
    amount: Decimal
    description: str
    category_id: int
    transaction_date: date
    
    model_config = {"from_attributes": True}


class TransactionListResponse(BaseModel):
    """Paginated list of transactions."""
    items: list[TransactionResponse]
    total: int
    limit: int
    offset: int


class SummaryResponse(BaseModel):
    """Transaction summary statistics."""
    total_income: Decimal
    total_expenses: Decimal
    net_balance: Decimal
    savings_rate: float
    transaction_count: int
    period_start: date
    period_end: date


# Dependency for repository
def get_transaction_repo() -> TransactionRepository:
    return TransactionRepository()


@router.get("", response_model=TransactionListResponse)
async def list_transactions(
    current_user: CurrentUser,
    repo: Annotated[TransactionRepository, Depends(get_transaction_repo)],
    start_date: date | None = None,
    end_date: date | None = None,
    category_id: int | None = None,
    limit: int = Query(default=50, le=100),
    offset: int = Query(default=0, ge=0),
):
    """List user's transactions with optional filters."""
    transactions = await repo.get_user_transactions(
        user_id=current_user.id,
        start_date=start_date,
        end_date=end_date,
        category_id=category_id,
        limit=limit,
        offset=offset,
    )
    
    return TransactionListResponse(
        items=[TransactionResponse.model_validate(tx) for tx in transactions],
        total=len(transactions),  # Simplified - real app would do COUNT query
        limit=limit,
        offset=offset,
    )


@router.post("", response_model=TransactionResponse, status_code=status.HTTP_201_CREATED)
async def create_transaction(
    current_user: CurrentUser,
    data: TransactionCreate,
    repo: Annotated[TransactionRepository, Depends(get_transaction_repo)],
):
    """Create a new transaction."""
    transaction = Transaction.create(
        amount=data.amount,
        description=data.description,
        category_id=data.category_id,
        user_id=current_user.id,
        transaction_date=data.transaction_date,
    )
    
    created = await repo.create(transaction)
    return TransactionResponse.model_validate(created)


@router.get("/summary", response_model=SummaryResponse)
async def get_summary(
    current_user: CurrentUser,
    repo: Annotated[TransactionRepository, Depends(get_transaction_repo)],
    start_date: date = Query(..., description="Period start date"),
    end_date: date = Query(..., description="Period end date"),
):
    """Get transaction summary for date range."""
    if end_date < start_date:
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail="end_date must be after start_date",
        )
    
    summary = await repo.get_summary(
        user_id=current_user.id,
        start_date=start_date,
        end_date=end_date,
    )
    
    return SummaryResponse(
        total_income=summary.total_income,
        total_expenses=summary.total_expenses,
        net_balance=summary.net_balance,
        savings_rate=summary.savings_rate,
        transaction_count=summary.transaction_count,
        period_start=summary.period_start,
        period_end=summary.period_end,
    )


@router.get("/{transaction_id}", response_model=TransactionResponse)
async def get_transaction(
    transaction_id: int,
    current_user: CurrentUser,
    repo: Annotated[TransactionRepository, Depends(get_transaction_repo)],
):
    """Get a specific transaction."""
    transaction = await repo.get_by_id(transaction_id, current_user.id)
    
    if not transaction:
        raise HTTPException(
            status_code=status.HTTP_404_NOT_FOUND,
            detail="Transaction not found",
        )
    
    return TransactionResponse.model_validate(transaction)


@router.delete("/{transaction_id}", status_code=status.HTTP_204_NO_CONTENT)
async def delete_transaction(
    transaction_id: int,
    current_user: CurrentUser,
    repo: Annotated[TransactionRepository, Depends(get_transaction_repo)],
):
    """Delete a transaction."""
    deleted = await repo.delete(transaction_id, current_user.id)
    
    if not deleted:
        raise HTTPException(
            status_code=status.HTTP_404_NOT_FOUND,
            detail="Transaction not found",
        )
```
