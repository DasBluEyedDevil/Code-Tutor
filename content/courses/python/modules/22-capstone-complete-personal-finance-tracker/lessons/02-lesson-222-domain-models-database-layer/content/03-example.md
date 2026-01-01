---
type: "EXAMPLE"
title: "Async Database Layer with asyncpg"
---

Connection pool and repository pattern for PostgreSQL:

```python
# src/finance_tracker/database.py
import asyncio
from contextlib import asynccontextmanager
from typing import AsyncGenerator

import asyncpg
from asyncpg import Pool, Connection

from .config import get_settings


class Database:
    """Async database connection manager.
    
    Uses asyncpg connection pool for efficient async operations.
    """
    
    _pool: Pool | None = None
    
    @classmethod
    async def connect(cls) -> None:
        """Initialize connection pool."""
        if cls._pool is not None:
            return
        
        settings = get_settings()
        cls._pool = await asyncpg.create_pool(
            settings.database_url,
            min_size=settings.db_pool_min_size,
            max_size=settings.db_pool_max_size,
        )
    
    @classmethod
    async def disconnect(cls) -> None:
        """Close connection pool."""
        if cls._pool:
            await cls._pool.close()
            cls._pool = None
    
    @classmethod
    def get_pool(cls) -> Pool:
        """Get the connection pool (must be connected first)."""
        if cls._pool is None:
            raise RuntimeError("Database not connected. Call connect() first.")
        return cls._pool
    
    @classmethod
    @asynccontextmanager
    async def connection(cls) -> AsyncGenerator[Connection, None]:
        """Get a connection from the pool.
        
        Usage:
            async with Database.connection() as conn:
                result = await conn.fetch('SELECT * FROM users')
        """
        pool = cls.get_pool()
        async with pool.acquire() as conn:
            yield conn
    
    @classmethod
    @asynccontextmanager
    async def transaction(cls) -> AsyncGenerator[Connection, None]:
        """Get a connection with transaction.
        
        Automatically commits on success, rolls back on exception.
        
        Usage:
            async with Database.transaction() as conn:
                await conn.execute('INSERT INTO ...')
                await conn.execute('UPDATE ...')
                # Commits automatically if no exception
        """
        async with cls.connection() as conn:
            async with conn.transaction():
                yield conn


# Repository base class
from abc import ABC, abstractmethod
from typing import Generic, TypeVar

T = TypeVar("T")


class BaseRepository(ABC, Generic[T]):
    """Abstract base repository for data access.
    
    Provides common patterns for CRUD operations.
    """
    
    @abstractmethod
    async def get_by_id(self, id: int) -> T | None:
        """Get entity by ID."""
        ...
    
    @abstractmethod
    async def create(self, entity: T) -> T:
        """Create new entity."""
        ...
    
    @abstractmethod
    async def update(self, entity: T) -> T:
        """Update existing entity."""
        ...
    
    @abstractmethod
    async def delete(self, id: int) -> bool:
        """Delete entity by ID."""
        ...
```
