---
type: "THEORY"
title: "FastAPI Testing with httpx AsyncClient"
---

FastAPI apps need proper async testing. The recommended approach uses `httpx.AsyncClient`:

**Why AsyncClient over TestClient?**
- `TestClient` (from Starlette) runs sync - can't test async dependencies properly
- `AsyncClient` runs truly async - tests lifespan events, async deps, WebSockets
- Matches production behavior more accurately

**Pattern:**
```python
from httpx import AsyncClient, ASGITransport
from app.main import app

async def test_endpoint():
    transport = ASGITransport(app=app)
    async with AsyncClient(transport=transport, base_url="http://test") as client:
        response = await client.get("/api/endpoint")
        assert response.status_code == 200
```

**Finance Tracker Context:** We'll test our transaction API endpoints with proper async fixtures.