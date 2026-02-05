---
type: "WARNING"
title: "SQLite Development Gotchas"
---

**SQLite Is Great for Development, But Watch Out:**

❌ **Don't assume SQLite behavior matches PostgreSQL**

```python
# SQLite: This works (columns are basically TEXT)
CREATE TABLE users (age INTEGER);
INSERT INTO users (age) VALUES ('not a number');  # Works!

# PostgreSQL: Strict typing
INSERT INTO users (age) VALUES ('not a number');  # Error!

# SOLUTION: Use strict mode
engine = create_engine("sqlite:///app.db", connect_args={"check_same_thread": False})
# And always test on PostgreSQL before production!
```

❌ **Don't ignore concurrency limitations**

```python
# SQLite: Write lock blocks all writers
# If your app has concurrent writes, you'll see:
# "database is locked" errors

# SOLUTION: Use WAL mode for better concurrency
engine = create_engine(
    "sqlite:///app.db",
    connect_args={"check_same_thread": False},
    execution_options={"isolation_level": "AUTOCOMMIT"}
)
# Or just use PostgreSQL for concurrent apps
```

❌ **Don't commit the database file accidentally**

```bash
# Add to .gitignore:
*.db
*.sqlite
*.sqlite3
```

❌ **Don't use SQLite features missing in PostgreSQL**

```python
# SQLite-only: REPLACE INTO
cursor.execute("REPLACE INTO users (id, name) VALUES (?, ?)", ...)

# Standard SQL (works everywhere): UPSERT
from sqlalchemy.dialects.postgresql import insert

stmt = insert(User).values(id=1, name="Alice")
stmt = stmt.on_conflict_do_update(
    index_elements=['id'],
    set_=dict(name=stmt.excluded.name)
)
```

**Development vs Production Checklist:**

| Development (SQLite) | Production (PostgreSQL) |
|---------------------|------------------------|
| Fast to set up | Requires server |
| Single file | Proper database |
| Limited concurrency | Full concurrency |
| Type flexibility | Strict types |
| Great for tests | Great for production |

**Best Practice:**
Develop with SQLite, test with PostgreSQL, deploy with PostgreSQL.
