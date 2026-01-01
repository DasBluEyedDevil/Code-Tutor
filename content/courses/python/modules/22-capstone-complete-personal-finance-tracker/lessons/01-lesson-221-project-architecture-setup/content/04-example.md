---
type: "EXAMPLE"
title: "Configuration with Pydantic Settings"
---

Create type-safe configuration that loads from environment variables:

```python
# src/finance_tracker/config.py
from functools import lru_cache
from pathlib import Path

from pydantic_settings import BaseSettings, SettingsConfigDict


class Settings(BaseSettings):
    """Application settings loaded from environment variables.
    
    All sensitive values come from .env file or environment.
    """
    model_config = SettingsConfigDict(
        env_file=".env",
        env_file_encoding="utf-8",
        case_sensitive=False,
    )
    
    # Application
    app_name: str = "Finance Tracker"
    debug: bool = False
    
    # Database
    database_url: str = "postgresql://localhost/finance_tracker"
    db_pool_min_size: int = 5
    db_pool_max_size: int = 20
    
    # Security
    secret_key: str  # Required - no default for security
    algorithm: str = "HS256"
    access_token_expire_minutes: int = 30
    
    # Paths
    base_dir: Path = Path(__file__).parent.parent.parent
    
    @property
    def data_dir(self) -> Path:
        """Directory for data files (exports, reports)."""
        path = self.base_dir / "data"
        path.mkdir(exist_ok=True)
        return path


@lru_cache
def get_settings() -> Settings:
    """Get cached settings instance.
    
    Uses lru_cache to ensure settings are loaded only once.
    """
    return Settings()


# Usage example
if __name__ == "__main__":
    settings = get_settings()
    print(f"App: {settings.app_name}")
    print(f"Debug: {settings.debug}")
    print(f"Data dir: {settings.data_dir}")
```
