---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **PostgreSQL** is the production choice: concurrency, ACID, advanced features
- **Docker** provides the easiest local setup for development
- **asyncpg** is 3-5x faster than psycopg2 and fully async
- **Parameterized queries** use `$1, $2, $3` syntax (not `?` or `%s`)
- **Connection pools** are essential for web applications
- **Records** returned by asyncpg work like both dicts and tuples
- **Always close connections** - use context managers or explicit `close()`