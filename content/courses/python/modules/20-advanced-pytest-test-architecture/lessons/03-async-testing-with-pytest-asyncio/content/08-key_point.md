---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **`asyncio_mode = "auto"`** in pyproject.toml eliminates `@pytest.mark.asyncio` decorators
- Use **`httpx.AsyncClient`** with `ASGITransport` for testing FastAPI - not TestClient for async deps
- **Async database fixtures** use `create_async_engine()` and `async_sessionmaker`
- **In-memory SQLite** (`sqlite+aiosqlite:///:memory:`) is perfect for fast, isolated tests
- Always **dispose engines** and **rollback sessions** in fixture cleanup
- Match **fixture scopes** - don't mix session-scoped with function-scoped dependencies
- Test **concurrent operations** with `asyncio.gather()` and proper async fixtures