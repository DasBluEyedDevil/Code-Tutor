---
type: "THEORY"
title: "pytest-asyncio 1.3.0: Modern Async Testing"
---

Testing async code requires special handling - coroutines don't run automatically:

```python
async def fetch_data():
    return "data"

# This doesn't work:
def test_fetch():
    result = fetch_data()  # Returns coroutine object, not "data"!
```

**Solution:** `pytest-asyncio` - the standard plugin for async testing in Python.

**Installation:**
```bash
uv add pytest-asyncio httpx aiosqlite sqlalchemy[asyncio]
```

**Key Changes in pytest-asyncio 0.21+ (leading to 1.3.0):**
- **Auto mode** is now the recommended default
- No more `@pytest.mark.asyncio` decorators needed in auto mode
- Better fixture scope handling
- Improved event loop management

**Configure in pyproject.toml:**
```toml
[tool.pytest.ini_options]
asyncio_mode = "auto"  # Auto-detect and run async tests
asyncio_default_fixture_loop_scope = "function"  # Isolate event loops
```