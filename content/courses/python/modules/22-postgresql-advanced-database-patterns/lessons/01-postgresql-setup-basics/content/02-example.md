---
type: "EXAMPLE"
title: "Installation Options"
---

**Three ways to get PostgreSQL running:**

**1. Docker (Recommended for Development)**
```bash
docker run --name postgres-dev \
  -e POSTGRES_USER=finance_user \
  -e POSTGRES_PASSWORD=secure_password \
  -e POSTGRES_DB=finance_tracker \
  -p 5432:5432 \
  -d postgres:16
```

**2. Local Installation**
- **Windows:** Download from postgresql.org, use installer
- **macOS:** `brew install postgresql@16`
- **Linux:** `sudo apt install postgresql postgresql-contrib`

**3. Cloud Services**
- Supabase (free tier)
- Railway, Render, Neon
- AWS RDS, Google Cloud SQL

**psql CLI Basics:**
```bash
# Connect to database
psql -U finance_user -d finance_tracker -h localhost

# Common commands inside psql:
\l          # List databases
\c dbname   # Connect to database
\dt         # List tables
\d table    # Describe table
\q          # Quit
```

```python
# After installation, test connection with Python
import asyncio
import asyncpg

async def test_connection():
    """Verify PostgreSQL connection works."""
    try:
        conn = await asyncpg.connect(
            host='localhost',
            port=5432,
            user='finance_user',
            password='secure_password',
            database='finance_tracker'
        )
        
        version = await conn.fetchval('SELECT version()')
        print(f"Connected to PostgreSQL!")
        print(f"Version: {version[:50]}...")
        
        await conn.close()
        print("Connection closed successfully.")
        
    except Exception as e:
        print(f"Connection failed: {e}")

asyncio.run(test_connection())
```
