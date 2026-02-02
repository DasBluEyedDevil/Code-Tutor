---
type: "EXAMPLE"
title: "Setting Up PostgreSQL"
---

**Three Ways to Get PostgreSQL Running:**

**Option 1: Docker (Recommended for Development)**
```bash
# Start PostgreSQL container
docker run --name postgres \
  -e POSTGRES_PASSWORD=secret \
  -e POSTGRES_USER=myuser \
  -e POSTGRES_DB=mydb \
  -p 5432:5432 \
  -d postgres:16

# Verify it's running
docker ps

# Connect with psql
docker exec -it postgres psql -U myuser -d mydb
```

**Option 2: Local Installation**
```bash
# macOS
brew install postgresql@16
brew services start postgresql@16

# Ubuntu/Debian
sudo apt update
sudo apt install postgresql postgresql-contrib
sudo systemctl start postgresql

# Windows
# Download installer from postgresql.org
```

**Option 3: Cloud Services (Easiest for Production)**
- **Supabase**: Free tier, instant setup at supabase.com
- **Neon**: Serverless, generous free tier at neon.tech
- **Railway**: Simple deployment at railway.app

All provide a connection string like:
```
postgresql://user:password@host:5432/database
```

```python
# PostgreSQL Setup Options

print("=== PostgreSQL Setup Guide ===")
print("")
print("Option 1: Docker (Recommended for Development)")
print("-" * 50)
print("""docker run --name postgres \\\n  -e POSTGRES_PASSWORD=secret \\\n  -e POSTGRES_USER=myuser \\\n  -e POSTGRES_DB=mydb \\\n  -p 5432:5432 \\\n  -d postgres:16""")
print("")
print("Option 2: Local Installation")
print("-" * 50)
print("macOS:  brew install postgresql@16")
print("Ubuntu: sudo apt install postgresql")
print("")
print("Option 3: Cloud Services")
print("-" * 50)
print("Supabase: https://supabase.com (free tier available)")
print("Neon:     https://neon.tech (serverless PostgreSQL)")
print("Railway:  https://railway.app (one-click deploy)")
print("")
print("All options provide a connection string:")
print("  postgresql://user:password@host:5432/database")
```
