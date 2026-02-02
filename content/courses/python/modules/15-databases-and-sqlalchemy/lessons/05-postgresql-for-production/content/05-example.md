---
type: "EXAMPLE"
title: "Environment-Based Configuration"
---

**Use Environment Variables for Database URLs**

The best practice is to use different databases for different environments:
- **Development**: SQLite (zero config, easy reset)
- **Testing**: SQLite in-memory or Docker PostgreSQL
- **Production**: Managed PostgreSQL (Supabase, Neon, RDS)

**Pydantic Settings** makes this easy by automatically reading from environment variables and `.env` files.

**Key Principle:** Your code should work identically regardless of which database is configured. SQLAlchemy abstracts the differences.

```python
# Environment-based database configuration
# Works with both SQLite (dev) and PostgreSQL (prod)

from pydantic_settings import BaseSettings
from sqlalchemy.ext.asyncio import create_async_engine

class Settings(BaseSettings):
    """Application settings with environment variable support"""
    
    # Default to SQLite for local development
    database_url: str = "sqlite+aiosqlite:///./dev.db"
    
    # Load from .env file automatically
    model_config = {"env_file": ".env"}

settings = Settings()

# Engine configuration based on database type
def get_engine_kwargs():
    """Return appropriate engine kwargs based on database type"""
    if "sqlite" in settings.database_url:
        return {
            "connect_args": {"check_same_thread": False}
        }
    else:
        # PostgreSQL settings
        return {
            "pool_size": 10,
            "max_overflow": 20,
            "pool_timeout": 30
        }

engine = create_async_engine(
    settings.database_url,
    echo=True,
    **get_engine_kwargs()
)

print("=== Environment-Based Configuration ===")
print("")
print(".env for Development:")
print("-" * 40)
print("# No DATABASE_URL needed - uses SQLite default")
print("# Or explicitly:")
print("DATABASE_URL=sqlite+aiosqlite:///./dev.db")
print("")
print(".env for Production:")
print("-" * 40)
print("DATABASE_URL=postgresql+asyncpg://user:pass@host:5432/db")
print("")
print(f"Current URL: {settings.database_url}")
print("")
print("Benefits:")
print("  - Same code works in all environments")
print("  - No secrets in source code")
print("  - Easy to switch databases")
print("  - SQLAlchemy handles the differences")
```
