---
type: "WARNING"
title: "Common Async Testing Pitfalls"
---

### Watch Out For These Mistakes:

**1. Forgetting to Await in Async Tests**
```python
# WRONG - Test passes but doesn't actually run!
async def test_fetch():
    result = fetch_data()  # Missing await!
    assert result == "data"  # Comparing coroutine to string!

# CORRECT - Await the coroutine
async def test_fetch():
    result = await fetch_data()
    assert result == "data"
```

**2. Using TestClient for Async Dependencies**
```python
# WRONG - TestClient runs sync, async deps may not work
from starlette.testclient import TestClient
client = TestClient(app)
response = client.get("/async-endpoint")  # Blocks, may miss async issues

# CORRECT - AsyncClient for async testing
from httpx import AsyncClient, ASGITransport
async def test_endpoint():
    transport = ASGITransport(app=app)
    async with AsyncClient(transport=transport, base_url="http://test") as client:
        response = await client.get("/async-endpoint")
```

**3. Missing asyncio_mode Configuration**
```python
# WRONG - Without config, async tests may not run properly
# pytest.ini or pyproject.toml missing asyncio_mode

# CORRECT - Add to pyproject.toml
# [tool.pytest.ini_options]
# asyncio_mode = "auto"
```

**4. Session Scope Mismatch**
```python
# WRONG - Session-scoped fixture with function-scoped db
@pytest.fixture(scope="session")
async def app_client(db_session):  # db_session is function-scoped!
    ...

# CORRECT - Match fixture scopes
@pytest.fixture(scope="function")  # Same as db_session
async def app_client(db_session):
    ...
```

**5. Not Cleaning Up Async Resources**
```python
# WRONG - Engine never disposed
@pytest.fixture
async def db_engine():
    engine = create_async_engine("sqlite+aiosqlite:///:memory:")
    yield engine
    # Missing: await engine.dispose()

# CORRECT - Always clean up
@pytest.fixture
async def db_engine():
    engine = create_async_engine("sqlite+aiosqlite:///:memory:")
    yield engine
    await engine.dispose()
```

**6. Reusing Event Loops Incorrectly**
```python
# WRONG - Sharing event loop across tests can cause issues
@pytest.fixture(scope="session")
def event_loop():
    return asyncio.new_event_loop()

# CORRECT - Let pytest-asyncio manage loops per-test (default behavior)
# Just use asyncio_mode = "auto" and asyncio_default_fixture_loop_scope = "function"
```