# Example implementation: Recurring Transactions

from dataclasses import dataclass, field
from datetime import date, timedelta
from decimal import Decimal
from enum import StrEnum
from typing import Sequence

from ..database import Database
from .transaction import Transaction


class RecurrenceFrequency(StrEnum):
    DAILY = "daily"
    WEEKLY = "weekly"
    MONTHLY = "monthly"
    YEARLY = "yearly"


@dataclass(slots=True)
class RecurringTransaction:
    id: int | None
    amount: Decimal
    description: str
    category_id: int
    user_id: int
    frequency: RecurrenceFrequency
    start_date: date
    end_date: date | None = None
    last_created: date | None = None
    is_active: bool = True
    
    def should_create_today(self) -> bool:
        today = date.today()
        
        if not self.is_active:
            return False
        if today < self.start_date:
            return False
        if self.end_date and today > self.end_date:
            return False
        if self.last_created and self.last_created >= today:
            return False
        
        return self.next_occurrence() <= today
    
    def next_occurrence(self) -> date:
        base = self.last_created or self.start_date
        
        match self.frequency:
            case RecurrenceFrequency.DAILY:
                return base + timedelta(days=1)
            case RecurrenceFrequency.WEEKLY:
                return base + timedelta(weeks=1)
            case RecurrenceFrequency.MONTHLY:
                month = base.month + 1
                year = base.year
                if month > 12:
                    month = 1
                    year += 1
                return base.replace(year=year, month=month)
            case RecurrenceFrequency.YEARLY:
                return base.replace(year=base.year + 1)
    
    def create_transaction(self) -> Transaction:
        return Transaction.create(
            amount=self.amount,
            description=f"{self.description} (recurring)",
            category_id=self.category_id,
            user_id=self.user_id,
        )


class RecurringTransactionRepository:
    async def get_due_recurring(self) -> Sequence[RecurringTransaction]:
        async with Database.connection() as conn:
            rows = await conn.fetch(
                """
                SELECT * FROM recurring_transactions
                WHERE is_active = true
                  AND start_date <= CURRENT_DATE
                  AND (end_date IS NULL OR end_date >= CURRENT_DATE)
                  AND (last_created IS NULL OR last_created < CURRENT_DATE)
                """
            )
            return [self._row_to_model(r) for r in rows]
    
    async def mark_created(self, id: int) -> None:
        async with Database.connection() as conn:
            await conn.execute(
                "UPDATE recurring_transactions SET last_created = CURRENT_DATE WHERE id = $1",
                id
            )
    
    @staticmethod
    def _row_to_model(row) -> RecurringTransaction:
        return RecurringTransaction(
            id=row["id"],
            amount=Decimal(row["amount"]),
            description=row["description"],
            category_id=row["category_id"],
            user_id=row["user_id"],
            frequency=RecurrenceFrequency(row["frequency"]),
            start_date=row["start_date"],
            end_date=row["end_date"],
            last_created=row["last_created"],
            is_active=row["is_active"],
        )


# Scheduler service (run daily via cron/celery)
async def process_recurring_transactions() -> int:
    from .transaction import TransactionRepository
    
    recurring_repo = RecurringTransactionRepository()
    tx_repo = TransactionRepository()
    
    due = await recurring_repo.get_due_recurring()
    created = 0
    
    for recurring in due:
        if recurring.should_create_today():
            tx = recurring.create_transaction()
            await tx_repo.create(tx)
            await recurring_repo.mark_created(recurring.id)
            created += 1
    
    return created