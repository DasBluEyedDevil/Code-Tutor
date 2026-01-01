from functools import lru_cache
from pathlib import Path

from pydantic_settings import BaseSettings, SettingsConfigDict


class Settings(BaseSettings):
    model_config = SettingsConfigDict(
        env_file=".env",
        env_file_encoding="utf-8",
    )
    
    app_name: str = "Finance Tracker"
    debug: bool = False
    database_url: str
    secret_key: str
    access_token_expire_minutes: int = 30
    base_dir: Path = Path(__file__).parent.parent.parent


@lru_cache
def get_settings() -> Settings:
    return Settings()