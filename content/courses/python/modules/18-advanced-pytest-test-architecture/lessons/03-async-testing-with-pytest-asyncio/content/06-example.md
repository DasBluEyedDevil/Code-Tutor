---
type: "EXAMPLE"
title: "Database Fixtures for Finance Tracker"
---

Production-ready async database testing pattern.

```python
# tests/conftest.py
import pytest
from sqlalchemy.ext.asyncio import (
    create_async_engine,
    AsyncSession,
    async_sessionmaker,
)
from sqlalchemy.orm import DeclarativeBase
from sqlalchemy import Column, Integer, String, Numeric, Date, ForeignKey
from decimal import Decimal
from datetime import date

# Models
class Base(DeclarativeBase):
    pass

class Account(Base):
    __tablename__ = "accounts"
    id = Column(Integer, primary_key=True)
    name = Column(String(100), nullable=False)
    account_type = Column(String(20), nullable=False)
    balance = Column(Numeric(12, 2), default=Decimal("0.00"))

class Transaction(Base):
    __tablename__ = "transactions"
    id = Column(Integer, primary_key=True)
    account_id = Column(Integer, ForeignKey("accounts.id"), nullable=False)
    amount = Column(Numeric(12, 2), nullable=False)
    category = Column(String(50))
    description = Column(String(255))
    transaction_date = Column(Date, default=date.today)

# Database fixtures
@pytest.fixture
async def db_engine():
    """Create async engine with in-memory SQLite."""
    engine = create_async_engine(
        "sqlite+aiosqlite:///:memory:",
        echo=False,  # Set True for SQL debugging
    )
    
    # Create all tables
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)
    
    yield engine
    
    # Cleanup
    await engine.dispose()

@pytest.fixture
async def db_session(db_engine):
    """Provide async session for each test."""
    async_session = async_sessionmaker(
        db_engine,
        class_=AsyncSession,
        expire_on_commit=False,
    )
    
    async with async_session() as session:
        yield session
        # Rollback any uncommitted changes
        await session.rollback()

@pytest.fixture
async def sample_account(db_session):
    """Create a sample account for testing."""
    account = Account(
        name="Checking",
        account_type="checking",
        balance=Decimal("1000.00")
    )
    db_session.add(account)
    await db_session.commit()
    await db_session.refresh(account)
    return account

# tests/test_transactions.py
from sqlalchemy import select
from decimal import Decimal

async def test_create_transaction(db_session, sample_account):
    """Test creating a transaction in the database."""
    tx = Transaction(
        account_id=sample_account.id,
        amount=Decimal("-50.00"),
        category="groceries",
        description="Weekly shopping"
    )
    db_session.add(tx)
    await db_session.commit()
    
    # Verify transaction was saved
    result = await db_session.execute(
        select(Transaction).where(Transaction.account_id == sample_account.id)
    )
    transactions = result.scalars().all()
    assert len(transactions) == 1
    assert transactions[0].amount == Decimal("-50.00")

async def test_account_balance_update(db_session, sample_account):
    """Test updating account balance after transaction."""
    # Record expense
    tx = Transaction(
        account_id=sample_account.id,
        amount=Decimal("-150.00"),
        category="utilities"
    )
    db_session.add(tx)
    
    # Update balance
    sample_account.balance += tx.amount
    await db_session.commit()
    
    # Verify new balance
    await db_session.refresh(sample_account)
    assert sample_account.balance == Decimal("850.00")

async def test_multiple_transactions(db_session, sample_account):
    """Test multiple transactions on same account."""
    transactions = [
        Transaction(account_id=sample_account.id, amount=Decimal("500.00"), category="income"),
        Transaction(account_id=sample_account.id, amount=Decimal("-100.00"), category="food"),
        Transaction(account_id=sample_account.id, amount=Decimal("-50.00"), category="transport"),
    ]
    db_session.add_all(transactions)
    await db_session.commit()
    
    # Calculate total
    result = await db_session.execute(
        select(Transaction).where(Transaction.account_id == sample_account.id)
    )
    all_tx = result.scalars().all()
    total = sum(tx.amount for tx in all_tx)
    
    assert total == Decimal("350.00")  # 500 - 100 - 50

```
