---
type: "EXAMPLE"
title: "pydantic-settings for Configuration"
---

**pydantic-settings** provides type-safe, validated configuration from environment variables:

**Installation:**
```bash
pip install pydantic-settings
```

```python
from pydantic_settings import BaseSettings, SettingsConfigDict
from pydantic import Field, SecretStr, PostgresDsn, field_validator
from typing import Optional, List
from functools import lru_cache

class Settings(BaseSettings):
    """
    Application settings loaded from environment variables.
    SecretStr prevents accidental logging of sensitive values.
    """
    
    # Database configuration
    database_url: PostgresDsn = Field(
        ...,  # Required
        description="PostgreSQL connection string"
    )
    database_pool_size: int = Field(
        default=10,
        ge=1,
        le=100,
        description="Database connection pool size"
    )
    
    # Security settings - use SecretStr for sensitive data
    jwt_secret: SecretStr = Field(
        ...,
        min_length=32,
        description="JWT signing secret (min 32 chars)"
    )
    jwt_algorithm: str = Field(
        default="HS256",
        pattern="^(HS256|HS384|HS512|RS256|RS384|RS512)$"
    )
    jwt_expiry_minutes: int = Field(default=15, ge=1, le=60)
    
    # API keys - SecretStr hides value in logs/repr
    stripe_api_key: Optional[SecretStr] = None
    sendgrid_api_key: Optional[SecretStr] = None
    
    # Application settings
    app_name: str = Field(default="Finance Tracker")
    debug: bool = Field(default=False)
    allowed_origins: List[str] = Field(default=["http://localhost:3000"])
    
    # Environment
    environment: str = Field(
        default="development",
        pattern="^(development|staging|production)$"
    )
    
    @field_validator('allowed_origins', mode='before')
    @classmethod
    def parse_origins(cls, v):
        """Parse comma-separated origins from env var."""
        if isinstance(v, str):
            return [origin.strip() for origin in v.split(',')]
        return v
    
    model_config = SettingsConfigDict(
        env_file=".env",              # Load from .env file
        env_file_encoding="utf-8",
        case_sensitive=False,          # DATABASE_URL = database_url
        extra="ignore"                 # Ignore unknown env vars
    )

# Singleton pattern - load settings once
@lru_cache()
def get_settings() -> Settings:
    """Get cached settings instance."""
    return Settings()

# Demo with mock environment
import os
os.environ.update({
    "DATABASE_URL": "postgresql://user:pass@localhost:5432/financedb",
    "JWT_SECRET": "this-is-a-very-long-secret-key-for-jwt-signing",
    "ENVIRONMENT": "development",
    "STRIPE_API_KEY": "sk_test_abcdef123456",
    "ALLOWED_ORIGINS": "http://localhost:3000,https://app.example.com"
})

print("Configuration Management with pydantic-settings")
print("=" * 50)

settings = get_settings()

print(f"\nApp Name: {settings.app_name}")
print(f"Environment: {settings.environment}")
print(f"Debug: {settings.debug}")
print(f"Database Pool Size: {settings.database_pool_size}")
print(f"JWT Algorithm: {settings.jwt_algorithm}")
print(f"Allowed Origins: {settings.allowed_origins}")

# SecretStr hides values when printed
print(f"\nJWT Secret (repr): {settings.jwt_secret}")  # Shows SecretStr('**********')
print(f"JWT Secret (value): {settings.jwt_secret.get_secret_value()[:10]}...")  # Actual value

print(f"\nStripe API Key: {settings.stripe_api_key}")  # Hidden

# Validation example
print("\n--- Validation Demo ---")
try:
    # This would fail - JWT secret too short
    os.environ["JWT_SECRET"] = "short"
    Settings._settings_build_values.cache_clear() if hasattr(Settings, '_settings_build_values') else None
    # Uncommenting would raise: Settings(jwt_secret="short")
    print("JWT secret must be at least 32 characters!")
except Exception as e:
    print(f"Validation error: {e}")

print("\nConfiguration loaded securely!")
```
