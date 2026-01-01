---
type: "EXAMPLE"
title: "Fly Postgres Setup and Management"
---

**Fly Postgres = Managed PostgreSQL on Fly.io:**

```bash
# Create a Postgres cluster
fly postgres create --name finance-db
# Interactive prompts for:
# - Region (pick one close to your app)
# - Configuration (Development, Production)
# - Size (1GB free tier available)

# Attach to your app (adds DATABASE_URL secret)
fly postgres attach finance-db --app finance-tracker

# Connect to database directly
fly postgres connect -a finance-db
# Opens psql session

# Run migrations
fly ssh console -C "alembic upgrade head"

# Or use proxy for local tools
fly proxy 5432 -a finance-db
# In another terminal:
psql postgres://postgres:password@localhost:5432/finance_db

# Check database status
fly status -a finance-db

# Scale Postgres (add read replicas)
fly postgres scale -a finance-db --count 2

# View connection string format
# postgres://user:password@finance-db.internal:5432/finance_db

# For async SQLAlchemy, convert in your config:
def get_database_url() -> str:
    url = os.environ.get("DATABASE_URL", "")
    if url.startswith("postgres://"):
        url = url.replace("postgres://", "postgresql+asyncpg://", 1)
    return url
```
