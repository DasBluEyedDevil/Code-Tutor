---
type: "EXAMPLE"
title: "Connecting to Railway PostgreSQL"
---

**Access your database for debugging and migrations:**

```bash
# Connect to PostgreSQL via CLI
railway connect postgresql
# Opens psql session connected to your database

# Run database migrations
railway run alembic upgrade head

# Run any command in Railway environment
railway run python -c "from src.database import engine; print(engine.url)"

# Get connection details
railway variables
# Shows all environment variables including DATABASE_URL

# Example DATABASE_URL format:
# postgresql://user:password@host.railway.internal:5432/railway

# For async SQLAlchemy, convert the URL in your config:
import os

def get_database_url() -> str:
    """Get database URL, converting for async if needed."""
    url = os.environ.get("DATABASE_URL", "")
    
    # Railway uses postgresql://, convert for asyncpg
    if url.startswith("postgresql://"):
        url = url.replace("postgresql://", "postgresql+asyncpg://", 1)
    
    return url
```
