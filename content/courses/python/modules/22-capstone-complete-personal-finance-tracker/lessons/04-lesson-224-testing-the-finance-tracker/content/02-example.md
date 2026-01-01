---
type: "EXAMPLE"
title: "Test Configuration with pytest"
---

Set up async testing with proper fixtures:

```python
# tests/conftest.py
import asyncio
from decimal import Decimal
from typing import AsyncGenerator

import pytest
import pytest_asyncio
from httpx import AsyncClient, ASGITransport

from finance_tracker.main import app
from finance_tracker.database import Database
from finance_tracker.models.user import User
from finance_tracker.models.transaction import Category, TransactionType
from finance_tracker.services.auth import create_access_token, hash_password


# Use a test database
TEST_DATABASE_URL = "postgresql://postgres:postgres@localhost/finance_tracker_test"


@pytest.fixture(scope="session")
def event_loop():
    """Create event loop for async tests."""
    loop = asyncio.get_event_loop_policy().new_event_loop()
    yield loop
    loop.close()


@pytest_asyncio.fixture
async def db() -> AsyncGenerator[None, None]:
    """Set up test database with fresh schema."""
    # Override database URL for tests
    import os
    os.environ["DATABASE_URL"] = TEST_DATABASE_URL
    
    await Database.connect()
    
    # Clean tables before each test
    async with Database.connection() as conn:
        await conn.execute("TRUNCATE users, categories, transactions, budgets CASCADE")
    
    yield
    
    await Database.disconnect()


@pytest_asyncio.fixture
async def test_user(db) -> User:
    """Create a test user."""
    async with Database.connection() as conn:
        row = await conn.fetchrow(
            """
            INSERT INTO users (email, hashed_password)
            VALUES ($1, $2)
            RETURNING id, email, hashed_password, created_at
            """,
            "test@example.com",
            hash_password("testpass123"),
        )
        return User(
            id=row["id"],
            email=row["email"],
            hashed_password=row["hashed_password"],
            created_at=row["created_at"],
        )


@pytest_asyncio.fixture
async def test_category(db, test_user) -> Category:
    """Create a test category."""
    async with Database.connection() as conn:
        row = await conn.fetchrow(
            """
            INSERT INTO categories (name, type, user_id, icon)
            VALUES ($1, $2, $3, $4)
            RETURNING id, name, type, user_id, icon, is_default
            """,
            "Groceries",
            "expense",
            test_user.id,
            "🛒",
        )
        return Category(
            id=row["id"],
            name=row["name"],
            type=TransactionType(row["type"]),
            user_id=row["user_id"],
            icon=row["icon"],
            is_default=row["is_default"],
        )


@pytest_asyncio.fixture
async def auth_token(test_user) -> str:
    """Get auth token for test user."""
    return create_access_token(test_user)


@pytest_asyncio.fixture
async def client(db) -> AsyncGenerator[AsyncClient, None]:
    """Async HTTP client for API tests."""
    transport = ASGITransport(app=app)
    async with AsyncClient(transport=transport, base_url="http://test") as ac:
        yield ac


@pytest_asyncio.fixture
async def auth_client(client, auth_token) -> AsyncClient:
    """Client with authentication headers."""
    client.headers["Authorization"] = f"Bearer {auth_token}"
    return client
```
