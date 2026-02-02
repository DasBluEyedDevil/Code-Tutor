---
type: "EXAMPLE"
title: "Settings Management"
---

**Pydantic Settings for Configuration:**

The `pydantic-settings` package handles application configuration:
- Read from environment variables
- Load from `.env` files
- Type validation on config values
- Default values with overrides

**Installation:**
```bash
uv add pydantic-settings
# or: pip install pydantic-settings
```

**Key Features:**
- Automatic env var reading (case-insensitive)
- Nested settings with prefixes
- Multiple .env file support
- Secrets file support

**Best Practices:**
- Never commit `.env` to git
- Provide `.env.example` with placeholder values
- Use type hints for validation
- Set sensible defaults for development

```python
# Note: pydantic-settings is a separate package
# Install with: uv add pydantic-settings

from pydantic import Field
import os

print("=== Pydantic Settings Management ===")

# Simulate pydantic-settings behavior
# In real code, you'd use: from pydantic_settings import BaseSettings

# First, let's simulate environment variables
os.environ["DATABASE_URL"] = "postgresql://user:pass@localhost/mydb"
os.environ["SECRET_KEY"] = "super-secret-key-12345"
os.environ["DEBUG"] = "true"
os.environ["MAX_CONNECTIONS"] = "100"

print("\n1. Basic Settings Pattern:")
print("""
from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    database_url: str
    secret_key: str
    debug: bool = False
    max_connections: int = 10
    
    model_config = {
        "env_file": ".env",
        "env_file_encoding": "utf-8"
    }

settings = Settings()  # Reads from environment
""")

# Demonstrate the concept with a regular BaseModel
from pydantic import BaseModel

class Settings(BaseModel):
    """Application settings (simulated)."""
    
    database_url: str
    secret_key: str
    debug: bool = False
    max_connections: int = 10
    api_version: str = "v1"
    
    @classmethod
    def from_env(cls) -> "Settings":
        """Load settings from environment variables."""
        return cls(
            database_url=os.environ.get("DATABASE_URL", ""),
            secret_key=os.environ.get("SECRET_KEY", ""),
            debug=os.environ.get("DEBUG", "false").lower() == "true",
            max_connections=int(os.environ.get("MAX_CONNECTIONS", "10")),
            api_version=os.environ.get("API_VERSION", "v1")
        )

settings = Settings.from_env()

print("\n2. Loaded settings:")
print(f"   Database: {settings.database_url[:30]}...")
print(f"   Debug mode: {settings.debug}")
print(f"   Max connections: {settings.max_connections}")
print(f"   API version: {settings.api_version}")

# Nested settings example
print("\n3. Nested Settings Pattern:")

class DatabaseSettings(BaseModel):
    """Database configuration."""
    url: str
    pool_size: int = 5
    echo: bool = False

class CacheSettings(BaseModel):
    """Cache configuration."""
    redis_url: str = "redis://localhost:6379"
    ttl_seconds: int = 3600

class AppSettings(BaseModel):
    """Main application settings with nested configs."""
    
    app_name: str = "My App"
    debug: bool = False
    database: DatabaseSettings
    cache: CacheSettings = CacheSettings()

app_settings = AppSettings(
    app_name="Finance Tracker",
    debug=True,
    database=DatabaseSettings(
        url="postgresql://localhost/finance",
        pool_size=10
    )
)

print(f"   App: {app_settings.app_name}")
print(f"   Debug: {app_settings.debug}")
print(f"   DB Pool: {app_settings.database.pool_size}")
print(f"   Cache TTL: {app_settings.cache.ttl_seconds}s")

# Example .env file
print("\n4. Example .env file:")
env_example = """
# .env.example - Copy to .env and fill in values
DATABASE_URL=postgresql://user:password@localhost/dbname
SECRET_KEY=your-secret-key-here
DEBUG=false
MAX_CONNECTIONS=50
REDIS_URL=redis://localhost:6379
"""
print(env_example)

print("5. Real pydantic-settings usage:")
print("""
from pydantic_settings import BaseSettings, SettingsConfigDict

class Settings(BaseSettings):
    model_config = SettingsConfigDict(
        env_file='.env',
        env_file_encoding='utf-8',
        case_sensitive=False,
        extra='ignore'
    )
    
    # All fields auto-read from environment
    database_url: str
    secret_key: str
    debug: bool = False

# Usage in FastAPI:
from functools import lru_cache

@lru_cache  # Cache settings for performance
def get_settings():
    return Settings()
""")
```
