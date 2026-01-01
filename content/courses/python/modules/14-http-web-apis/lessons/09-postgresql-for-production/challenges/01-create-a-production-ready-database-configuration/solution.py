# database.py - Production-ready database configuration
from pydantic_settings import BaseSettings
from sqlalchemy.ext.asyncio import (
    create_async_engine,
    async_sessionmaker,
    AsyncSession
)
from sqlalchemy.orm import declarative_base

class Settings(BaseSettings):
    # Default to SQLite for local development
    database_url: str = "sqlite+aiosqlite:///./dev.db"
    
    model_config = {"env_file": ".env"}

settings = Settings()

def get_engine_kwargs() -> dict:
    """Return appropriate engine kwargs based on database type."""
    if "sqlite" in settings.database_url:
        # SQLite-specific settings
        return {
            "connect_args": {"check_same_thread": False}
        }
    else:
        # PostgreSQL settings with connection pooling
        return {
            "pool_size": 10,
            "max_overflow": 20,
            "pool_timeout": 30
        }

# Create async engine with appropriate settings
engine = create_async_engine(
    settings.database_url,
    echo=True,  # Set to False in production
    **get_engine_kwargs()
)

# Create async session factory
async_session = async_sessionmaker(
    engine,
    expire_on_commit=False,
    class_=AsyncSession
)

# Base class for models
Base = declarative_base()

# Test the configuration
if __name__ == "__main__":
    print(f"Database URL: {settings.database_url}")
    print(f"Engine kwargs: {get_engine_kwargs()}")
    print("Configuration loaded successfully!")