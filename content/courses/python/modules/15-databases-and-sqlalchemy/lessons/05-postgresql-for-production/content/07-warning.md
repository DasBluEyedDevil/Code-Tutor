---
type: "WARNING"
title: "PostgreSQL Production Pitfalls"
---

**Production Database Mistakes to Avoid:**

❌ **Don't expose database ports to the internet**

```yaml
# WRONG: Docker Compose exposing port
services:
  db:
    image: postgres:16
    ports:
      - "5432:5432"  # World can connect!

# RIGHT: Internal network only
services:
  db:
    image: postgres:16
    # No ports exposed - only internal services connect
```

❌ **Don't hardcode connection strings**

```python
# WRONG: Credentials in code
DATABASE_URL = "postgresql+asyncpg://myuser:mypassword@localhost/mydb"

# RIGHT: Environment variables
import os
DATABASE_URL = os.environ["DATABASE_URL"]

# BETTER: Use pydantic-settings
from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    database_url: str
    
    class Config:
        env_file = ".env"
```

❌ **Don't skip connection pooling**

```python
# WRONG: Default pool settings
engine = create_async_engine(DATABASE_URL)

# RIGHT: Configure pool for production
engine = create_async_engine(
    DATABASE_URL,
    pool_size=5,           # Connections to keep open
    max_overflow=10,       # Additional connections if needed
    pool_timeout=30,       # Seconds to wait for connection
    pool_recycle=1800,     # Recycle connections every 30 min
)
```

❌ **Don't forget indexes**

```python
# SLOW: No index on frequently queried column
class User(Base):
    email: Mapped[str]  # WHERE email = ? is slow!

# FAST: Add index
class User(Base):
    email: Mapped[str] = mapped_column(index=True)

# Or composite index
__table_args__ = (
    Index('ix_user_email_active', 'email', 'active'),
)
```

❌ **Don't skip backups**

```bash
# Schedule regular backups!
pg_dump mydb > backup_$(date +%Y%m%d).sql

# Test your backups restore correctly!
psql testdb < backup_20240101.sql
```

**Production Checklist:**
- [ ] Connection string in environment variables
- [ ] Connection pooling configured
- [ ] Indexes on frequently queried columns
- [ ] Regular automated backups
- [ ] Database not exposed to public internet
- [ ] SSL connections enabled
- [ ] Monitoring and alerts configured
