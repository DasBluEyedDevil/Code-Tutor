---
type: "EXAMPLE"
title: "Your First FastAPI App"
---

**Getting Started with FastAPI:**

**1. Installation:**
```bash
uv add "fastapi[standard]"
# Or: pip install "fastapi[standard]"
```

**2. Basic app structure:**
```python
from fastapi import FastAPI

app = FastAPI()

@app.get("/")
async def root():
    return {"message": "Hello World"}
```

**3. Running the server:**
```bash
uvicorn main:app --reload
# --reload enables auto-restart on code changes
```

**4. Access your API:**
- API: http://localhost:8000
- Swagger docs: http://localhost:8000/docs
- ReDoc: http://localhost:8000/redoc

**Key Concepts:**
- `FastAPI()` creates the application instance
- `@app.get("/")` decorates route handlers
- `async def` enables async/await (optional but recommended)
- Return dicts/lists - FastAPI converts to JSON automatically

```python
from fastapi import FastAPI
import uvicorn

# Create FastAPI application with metadata
app = FastAPI(
    title="Personal Finance API",
    description="API for managing personal finances",
    version="1.0.0"
)

# Root endpoint
@app.get("/")
async def root():
    """Welcome message and API info."""
    return {
        "message": "Welcome to the Personal Finance API",
        "version": "1.0.0",
        "docs": "/docs"
    }

# Health check endpoint
@app.get("/health")
async def health():
    """Health check for monitoring."""
    return {"status": "healthy"}

# API info endpoint
@app.get("/api/info")
async def api_info():
    """Return API metadata."""
    return {
        "name": app.title,
        "version": app.version,
        "description": app.description,
        "endpoints": [
            {"path": "/", "method": "GET", "description": "Root"},
            {"path": "/health", "method": "GET", "description": "Health check"},
            {"path": "/docs", "method": "GET", "description": "Swagger UI"}
        ]
    }

# Demo: Show what the API returns
print("=== FastAPI Application Demo ===")
print(f"App Title: {app.title}")
print(f"Version: {app.version}")
print("\nEndpoints defined:")
for route in app.routes:
    if hasattr(route, 'methods'):
        methods = ', '.join(route.methods - {'HEAD', 'OPTIONS'})
        print(f"  {methods:6} {route.path}")

print("\n=== To run this app ===")
print("Save as main.py and run:")
print("  uvicorn main:app --reload")
print("\nThen visit:")
print("  http://localhost:8000      - API root")
print("  http://localhost:8000/docs - Swagger UI")

# Uncomment to run directly:
# if __name__ == "__main__":
#     uvicorn.run(app, host="0.0.0.0", port=8000)
```
