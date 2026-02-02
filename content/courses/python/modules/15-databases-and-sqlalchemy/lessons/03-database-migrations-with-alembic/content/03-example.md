---
type: "EXAMPLE"
title: "Configuring env.py for Async"
---

**Configuring Alembic for async SQLAlchemy 2.0:**

The `env.py` file needs to be configured to:
1. Import your models (so Alembic sees them)
2. Use async engine creation
3. Connect to your database URL from config

**Key Imports:**
- `async_engine_from_config` - Creates async engine from alembic.ini
- `target_metadata` - Set to `Base.metadata` so autogenerate works

**Important:** All your models must be imported before `target_metadata` is set!

```python
# migrations/env.py

from logging.config import fileConfig
from sqlalchemy import pool
from sqlalchemy.ext.asyncio import async_engine_from_config
from alembic import context
import asyncio

# Import your models - REQUIRED for autogenerate to work!
# This imports Base.metadata with all model information
from app.models import Base
from app.config import settings

# Alembic Config object
config = context.config

# Set database URL from your app settings
config.set_main_option("sqlalchemy.url", settings.database_url)

# Set up logging
if config.config_file_name is not None:
    fileConfig(config.config_file_name)

# This is the metadata Alembic uses to detect changes
# It MUST reference your models' Base.metadata
target_metadata = Base.metadata

def run_migrations_offline() -> None:
    """Run migrations without a database connection.
    
    Used for generating SQL scripts.
    """
    url = config.get_main_option("sqlalchemy.url")
    context.configure(
        url=url,
        target_metadata=target_metadata,
        literal_binds=True,
        dialect_opts={"paramstyle": "named"},
    )

    with context.begin_transaction():
        context.run_migrations()

def do_run_migrations(connection) -> None:
    """Run migrations with a database connection."""
    context.configure(
        connection=connection,
        target_metadata=target_metadata
    )

    with context.begin_transaction():
        context.run_migrations()

async def run_async_migrations() -> None:
    """Create async engine and run migrations."""
    connectable = async_engine_from_config(
        config.get_section(config.config_ini_section, {}),
        prefix="sqlalchemy.",
        poolclass=pool.NullPool,
    )

    async with connectable.connect() as connection:
        await connection.run_sync(do_run_migrations)

    await connectable.dispose()

def run_migrations_online() -> None:
    """Run migrations in 'online' mode."""
    asyncio.run(run_async_migrations())

if context.is_offline_mode():
    run_migrations_offline()
else:
    run_migrations_online()

# Demonstration output
print("=== env.py Configuration ===")
print("\nKey points:")
print("1. Import ALL your models before setting target_metadata")
print("2. Use async_engine_from_config for async support")
print("3. run_sync() bridges async engine with sync Alembic")
print("4. Set database URL from your app config")
print("\nCommon issues:")
print("- 'No changes detected' = Models not imported")
print("- Connection errors = Wrong database URL")
```
