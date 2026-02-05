---
type: "WARNING"
title: "API Testing Pitfalls"
---

**Common Mistakes When Testing FastAPI:**

❌ **Don't use `requests` for testing**

```python
# WRONG: requests makes real HTTP calls
import requests

def test_get_users():
    response = requests.get("http://localhost:8000/users")
    assert response.status_code == 200

# RIGHT: Use TestClient (no server needed)
from fastapi.testclient import TestClient

def test_get_users():
    client = TestClient(app)
    response = client.get("/users")
    assert response.status_code == 200
```

❌ **Don't forget to override dependencies**

```python
# WRONG: Tests hit real database!
def test_create_user():
    client = TestClient(app)
    response = client.post("/users", json={"name": "Test"})
    # Creates real user in production DB!

# RIGHT: Override with test dependency
def get_test_db():
    return InMemoryDatabase()

app.dependency_overrides[get_database] = get_test_db

def test_create_user():
    client = TestClient(app)
    response = client.post("/users", json={"name": "Test"})
    # Uses in-memory test database
```

❌ **Don't ignore async in tests**

```python
# WRONG: Sync TestClient with async endpoints (works but limited)
def test_async_endpoint():
    client = TestClient(app)
    response = client.get("/async-data")

# BETTER: Use httpx with pytest-asyncio
import httpx
import pytest

@pytest.mark.anyio
async def test_async_endpoint():
    async with httpx.AsyncClient(app=app, base_url="http://test") as client:
        response = await client.get("/async-data")
```

❌ **Don't test implementation details**

```python
# WRONG: Testing internal structure
def test_user_creation():
    user = create_user("test@example.com")
    assert user._internal_id == 1  # Implementation detail!

# RIGHT: Test behavior
def test_user_creation():
    response = client.post("/users", json={"email": "test@example.com"})
    assert response.status_code == 201
    assert "id" in response.json()
```

**Testing Checklist:**
- [ ] Use `TestClient` (not `requests`)
- [ ] Override database/external dependencies
- [ ] Test happy path AND error cases
- [ ] Test validation (422 responses)
- [ ] Test authentication (401/403 responses)
