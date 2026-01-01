---
type: "EXAMPLE"
title: "Connecting to the Database from Your App"
---

**Updating your FastAPI app to use the Compose database:**

```python
# src/config.py
import os
from pydantic_settings import BaseSettings


class Settings(BaseSettings):
    """Application settings loaded from environment variables."""
    
    # Database URL from docker-compose environment
    database_url: str = "sqlite+aiosqlite:///./dev.db"
    
    # App settings
    debug: bool = False
    log_level: str = "INFO"
    
    class Config:
        env_file = ".env"  # Load from .env file if present


settings = Settings()


# src/database.py
from sqlalchemy.ext.asyncio import create_async_engine, AsyncSession
from sqlalchemy.orm import sessionmaker
from src.config import settings

# Create async engine from DATABASE_URL
engine = create_async_engine(
    settings.database_url,
    echo=settings.debug,  # Log SQL queries in debug mode
)

# Session factory
AsyncSessionLocal = sessionmaker(
    engine,
    class_=AsyncSession,
    expire_on_commit=False
)


async def get_db():
    """Dependency for FastAPI routes."""
    async with AsyncSessionLocal() as session:
        try:
            yield session
        finally:
            await session.close()


# src/main.py
from fastapi import FastAPI, Depends
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import text

from src.database import get_db
from src.config import settings

app = FastAPI(
    title="Personal Finance Tracker",
    debug=settings.debug
)


@app.get("/health")
async def health_check(db: AsyncSession = Depends(get_db)):
    """Health check endpoint - verifies database connection."""
    try:
        await db.execute(text("SELECT 1"))
        return {"status": "healthy", "database": "connected"}
    except Exception as e:
        return {"status": "unhealthy", "database": str(e)}
```
