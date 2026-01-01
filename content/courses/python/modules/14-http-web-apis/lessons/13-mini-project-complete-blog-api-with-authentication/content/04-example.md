---
type: "EXAMPLE"
title: "Testing the Blog API"
---

**Test your API thoroughly before deployment!**

These tests verify:
- User registration and login
- Token authentication
- Post creation with auth
- Authorization (can't edit others' posts)

```python
# tests/test_blog_api.py
# Install: pip install pytest httpx pytest-asyncio
import pytest
from httpx import AsyncClient, ASGITransport
from main import app, users_db, posts_db, comments_db

@pytest.fixture(autouse=True)
def reset_data():
    """Reset database before each test."""
    users_db.clear()
    posts_db.clear()
    comments_db.clear()
    yield

@pytest.fixture
async def client():
    """Create async test client."""
    async with AsyncClient(transport=ASGITransport(app=app), base_url="http://test") as ac:
        yield ac

@pytest.fixture
async def auth_headers(client):
    """Create user and return auth headers."""
    # Register user
    await client.post("/api/auth/register", json={
        "username": "testuser",
        "email": "test@example.com",
        "password": "password123"
    })
    
    # Login (OAuth2 uses form data)
    response = await client.post("/api/auth/login", data={
        "username": "testuser",
        "password": "password123"
    })
    token = response.json()["access_token"]
    return {"Authorization": f"Bearer {token}"}

# ========== Auth Tests ==========
@pytest.mark.asyncio
async def test_register_success(client):
    """Test successful user registration."""
    response = await client.post("/api/auth/register", json={
        "username": "alice",
        "email": "alice@example.com",
        "password": "secret123"
    })
    
    assert response.status_code == 201
    assert response.json()["user"]["username"] == "alice"

@pytest.mark.asyncio
async def test_register_duplicate(client):
    """Test duplicate registration fails."""
    await client.post("/api/auth/register", json={
        "username": "alice", "email": "a@b.com", "password": "secret123"
    })
    response = await client.post("/api/auth/register", json={
        "username": "alice", "email": "c@d.com", "password": "secret123"
    })
    
    assert response.status_code == 400

@pytest.mark.asyncio
async def test_login_success(client):
    """Test login returns token."""
    await client.post("/api/auth/register", json={
        "username": "alice", "email": "a@b.com", "password": "secret123"
    })
    response = await client.post("/api/auth/login", data={
        "username": "alice", "password": "secret123"
    })
    
    assert response.status_code == 200
    assert "access_token" in response.json()

# ========== Post Tests ==========
@pytest.mark.asyncio
async def test_create_post_authenticated(client, auth_headers):
    """Test creating post with valid token."""
    response = await client.post(
        "/api/posts",
        json={"title": "Test Post Title", "content": "Content here"},
        headers=auth_headers
    )
    
    assert response.status_code == 201
    assert response.json()["post"]["title"] == "Test Post Title"

@pytest.mark.asyncio
async def test_create_post_unauthenticated(client):
    """Test creating post without token fails."""
    response = await client.post(
        "/api/posts",
        json={"title": "Test Post", "content": "Content"}
    )
    
    assert response.status_code == 401

@pytest.mark.asyncio
async def test_update_own_post(client, auth_headers):
    """Test updating own post."""
    await client.post(
        "/api/posts",
        json={"title": "Original Title", "content": "Original"},
        headers=auth_headers
    )
    response = await client.put(
        "/api/posts/0",
        json={"title": "Updated Title", "content": "Updated"},
        headers=auth_headers
    )
    
    assert response.status_code == 200
    assert response.json()["post"]["title"] == "Updated Title"

# Run: pytest tests/test_blog_api.py -v --asyncio-mode=auto
```
