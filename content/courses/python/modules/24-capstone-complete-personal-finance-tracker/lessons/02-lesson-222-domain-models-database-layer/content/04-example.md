---
type: "EXAMPLE"
title: "Transaction Repository Implementation"
---

Complete repository for transaction CRUD operations:

```python
# src/finance_tracker/repositories/transaction.py
from datetime import date
from decimal import Decimal
from typing import Sequence

from asyncpg import Connection

from ..database import Database
from ..models.transaction import Transaction, TransactionSummary


class TransactionRepository:
    """Repository for Transaction data access.
    
    All database queries for transactions are centralized here.
    """
    
    async def get_by_id(self, id: int, user_id: int) -> Transaction | None:
        """Get transaction by ID (with ownership check)."""
        async with Database.connection() as conn:
            row = await conn.fetchrow(
                """
                SELECT id, amount, description, category_id, user_id,
                       transaction_date, created_at
                FROM transactions
                WHERE id = $1 AND user_id = $2
                """,
                id, user_id
            )
            return self._row_to_transaction(row) if row else None
    
    async def get_user_transactions(
        self,
        user_id: int,
        start_date: date | None = None,
        end_date: date | None = None,
        category_id: int | None = None,
        limit: int = 100,
        offset: int = 0,
    ) -> Sequence[Transaction]:
        """Get transactions for user with optional filters."""
        query = """
            SELECT id, amount, description, category_id, user_id,
                   transaction_date, created_at
            FROM transactions
            WHERE user_id = $1
        """
        params: list = [user_id]
        param_num = 2
        
        if start_date:
            query += f" AND transaction_date >= ${param_num}"
            params.append(start_date)
            param_num += 1
        
        if end_date:
            query += f" AND transaction_date <= ${param_num}"
            params.append(end_date)
            param_num += 1
        
        if category_id:
            query += f" AND category_id = ${param_num}"
            params.append(category_id)
            param_num += 1
        
        query += f" ORDER BY transaction_date DESC LIMIT ${param_num} OFFSET ${param_num + 1}"
        params.extend([limit, offset])
        
        async with Database.connection() as conn:
            rows = await conn.fetch(query, *params)
            return [self._row_to_transaction(row) for row in rows]
    
    async def create(self, transaction: Transaction) -> Transaction:
        """Create a new transaction."""
        async with Database.connection() as conn:
            row = await conn.fetchrow(
                """
                INSERT INTO transactions 
                    (amount, description, category_id, user_id, transaction_date)
                VALUES ($1, $2, $3, $4, $5)
                RETURNING id, amount, description, category_id, user_id,
                          transaction_date, created_at
                """,
                transaction.amount,
                transaction.description,
                transaction.category_id,
                transaction.user_id,
                transaction.transaction_date,
            )
            return self._row_to_transaction(row)
    
    async def update(self, transaction: Transaction) -> Transaction:
        """Update an existing transaction."""
        if transaction.id is None:
            raise ValueError("Cannot update transaction without ID")
        
        async with Database.connection() as conn:
            row = await conn.fetchrow(
                """
                UPDATE transactions
                SET amount = $1, description = $2, category_id = $3,
                    transaction_date = $4
                WHERE id = $5 AND user_id = $6
                RETURNING id, amount, description, category_id, user_id,
                          transaction_date, created_at
                """,
                transaction.amount,
                transaction.description,
                transaction.category_id,
                transaction.transaction_date,
                transaction.id,
                transaction.user_id,
            )
            if not row:
                raise ValueError(f"Transaction {transaction.id} not found")
            return self._row_to_transaction(row)
    
    async def delete(self, id: int, user_id: int) -> bool:
        """Delete a transaction (with ownership check)."""
        async with Database.connection() as conn:
            result = await conn.execute(
                "DELETE FROM transactions WHERE id = $1 AND user_id = $2",
                id, user_id
            )
            return result == "DELETE 1"
    
    async def get_summary(
        self,
        user_id: int,
        start_date: date,
        end_date: date,
    ) -> TransactionSummary:
        """Get aggregated summary for date range."""
        async with Database.connection() as conn:
            row = await conn.fetchrow(
                """
                SELECT
                    COALESCE(SUM(CASE WHEN c.type = 'income' THEN t.amount ELSE 0 END), 0) as total_income,
                    COALESCE(SUM(CASE WHEN c.type = 'expense' THEN t.amount ELSE 0 END), 0) as total_expenses,
                    COUNT(*) as transaction_count
                FROM transactions t
                JOIN categories c ON t.category_id = c.id
                WHERE t.user_id = $1
                  AND t.transaction_date BETWEEN $2 AND $3
                """,
                user_id, start_date, end_date
            )
            return TransactionSummary(
                total_income=Decimal(row["total_income"]),
                total_expenses=Decimal(row["total_expenses"]),
                transaction_count=row["transaction_count"],
                period_start=start_date,
                period_end=end_date,
            )
    
    @staticmethod
    def _row_to_transaction(row) -> Transaction:
        """Convert database row to Transaction object."""
        return Transaction(
            id=row["id"],
            amount=Decimal(row["amount"]),
            description=row["description"],
            category_id=row["category_id"],
            user_id=row["user_id"],
            transaction_date=row["transaction_date"],
            created_at=row["created_at"],
        )
```
