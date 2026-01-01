# database.py - Async SQLite setup for FastAPI
from sqlalchemy.ext.asyncio import (
    create_async_engine,
    async_sessionmaker,
    AsyncSession
)
from sqlalchemy.orm import declarative_base

# TODO: Define DATABASE_URL for SQLite with aiosqlite
DATABASE_URL = ""

# TODO: Create async engine with SQLite-specific settings
engine = None

# TODO: Create async session factory
async_session = None

# Base class for models
Base = declarative_base()

# TODO: Implement async init_db function
async def init_db():
    """Create all database tables."""
    pass

# TODO: Implement async get_db dependency
async def get_db():
    """FastAPI dependency for database sessions."""
    pass

# Test the setup
if __name__ == "__main__":
    import asyncio
    
    async def test():
        await init_db()
        print("Database initialized successfully!")
    
    asyncio.run(test())