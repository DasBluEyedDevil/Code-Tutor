---
type: "THEORY"
title: "Async Database Fixtures with SQLAlchemy"
---

For real applications, you need async database fixtures. SQLAlchemy 2.0 provides full async support:

**Key Components:**
- `create_async_engine()` - Async database engine
- `AsyncSession` - Async session for queries
- `async_sessionmaker` - Factory for async sessions

**Testing Pattern:**
1. Create in-memory SQLite database for tests
2. Set up schema before each test
3. Provide session fixture to tests
4. Clean up after each test

**Why In-Memory SQLite?**
- Fast (no disk I/O)
- Isolated (fresh for each test)
- No cleanup needed (disappears after test)
- Works with `aiosqlite` driver