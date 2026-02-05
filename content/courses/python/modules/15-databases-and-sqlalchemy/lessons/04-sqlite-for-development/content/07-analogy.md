---
type: "ANALOGY"
title: "SQLite as Practice Kitchen"
---

**Understanding SQLite's Role**

**SQLite = Your Home Kitchen**
- Everything you need in one place
- Quick to set up (no restaurant permits)
- Perfect for practice and small meals
- You cook for yourself or a few guests

**PostgreSQL = Commercial Restaurant Kitchen**
- Industrial equipment
- Handles hundreds of orders
- Requires training and setup
- Built for scale

**When to Use Each:**

| Home Kitchen (SQLite) | Restaurant Kitchen (PostgreSQL) |
|-----------------------|--------------------------------|
| Learning recipes (development) | Serving customers (production) |
| Cooking for family (single user) | Catering events (many users) |
| Trying new dishes (prototyping) | Consistent menu (stable app) |
| No staff needed (simple setup) | Professional team (ops support) |

**The Development Workflow:**

```python
# Development: Home kitchen (SQLite)
DATABASE_URL = "sqlite:///./dev.db"

# Testing: Practice run (also SQLite, in-memory)
DATABASE_URL = "sqlite:///:memory:"

# Production: Restaurant kitchen (PostgreSQL)
DATABASE_URL = "postgresql+asyncpg://user:pass@localhost/prod"
```

**Why Start with SQLite:**

1. **Zero setup**: `pip install aiosqlite` and you're done
2. **Single file**: Easy to reset (`rm app.db`)
3. **Fast tests**: In-memory database for speed
4. **Portable**: Database travels with your code

**When to Graduate to PostgreSQL:**

- Multiple users writing simultaneously
- Data integrity is critical
- You need advanced features (JSONB, full-text search)
- Going to production

**The Key Insight:**
SQLite is for cooking practice. PostgreSQL is for feeding the world. Master your recipes in the home kitchen, then scale up.
