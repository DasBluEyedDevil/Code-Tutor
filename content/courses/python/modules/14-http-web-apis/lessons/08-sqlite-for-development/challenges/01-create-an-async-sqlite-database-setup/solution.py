# database.py - Async SQLite setup for FastAPI
from sqlalchemy.ext.asyncio import (
    create_async_engine,
    async_sessionmaker,
    AsyncSession
)
from sqlalchemy.orm import declarative_base

# SQLite URL with aiosqlite driver
DATABASE_URL = "sqlite+aiosqlite:///./dev.db"

# Create async engine with SQLite-specific settings
engine = create_async_engine(
    DATABASE_URL,
    echo=True,  # Log SQL statements (disable in production)
    connect_args={"check_same_thread": False}  # Required for SQLite
)

# Create async session factory
async_session = async_sessionmaker(
    engine,
    expire_on_commit=False,
    class_=AsyncSession
)

# Base class for models
Base = declarative_base()

async def init_db():
    """Create all database tables.
    
    Call this at FastAPI startup.
    """
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)

async def get_db():
    """FastAPI dependency for database sessions.
    
    Provides an async session with automatic commit/rollback.
    """
    async with async_session() as session:
        try:
            yield session
            await session.commit()
        except Exception:
            await session.rollback()
            raise
        finally:
            await session.close()

# Test the setup
if __name__ == "__main__":
    import asyncio
    
    async def test():
        await init_db()
        print("Database initialized successfully!")
        print(f"Using: {DATABASE_URL}")
    
    asyncio.run(test())