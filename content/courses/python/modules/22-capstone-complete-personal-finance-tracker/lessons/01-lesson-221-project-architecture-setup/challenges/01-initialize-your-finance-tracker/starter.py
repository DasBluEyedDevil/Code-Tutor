# src/finance_tracker/config.py
from pathlib import Path
from pydantic_settings import BaseSettings


class Settings(BaseSettings):
    """TODO: Implement application settings.
    
    Required fields:
    - app_name: str (default: 'Finance Tracker')
    - debug: bool (default: False)
    - database_url: str (required)
    - secret_key: str (required)
    - access_token_expire_minutes: int (default: 30)
    
    Add model_config for loading from .env file.
    """
    pass


def get_settings() -> Settings:
    """Return settings instance (hint: use lru_cache for efficiency)."""
    pass