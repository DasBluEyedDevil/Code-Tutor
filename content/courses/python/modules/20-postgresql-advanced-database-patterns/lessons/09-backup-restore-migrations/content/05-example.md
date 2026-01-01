---
type: "EXAMPLE"
title: "Alembic Migration Example"
---

**Create and apply a migration for the budgets table:**

```python
# migrations/env.py - Configure Alembic for asyncpg
from alembic import context
import asyncio
from sqlalchemy import pool
from sqlalchemy.ext.asyncio import create_async_engine

# Your database URL
DATABASE_URL = "postgresql+asyncpg://finance_user:secure_password@localhost/finance_tracker"

def run_migrations_online():
    """Run migrations in 'online' mode with async support."""
    connectable = create_async_engine(DATABASE_URL, poolclass=pool.NullPool)

    async def do_run_migrations(connection):
        await connection.run_sync(do_run_migrations_sync)

    def do_run_migrations_sync(connection):
        context.configure(connection=connection, target_metadata=None)
        with context.begin_transaction():
            context.run_migrations()

    async def run_async_migrations():
        async with connectable.connect() as connection:
            await do_run_migrations(connection)

    asyncio.run(run_async_migrations())

run_migrations_online()
```
