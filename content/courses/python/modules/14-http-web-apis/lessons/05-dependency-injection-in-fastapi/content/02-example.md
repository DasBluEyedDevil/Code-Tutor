---
type: "EXAMPLE"
title: "Simple Dependencies"
---

**Basic dependency pattern:**

A dependency is just a function that returns something your endpoint needs.

**Key Points:**
- Dependencies can be regular functions or async functions
- They can return any type
- FastAPI automatically calls them and passes the result
- Multiple endpoints can share the same dependency

```python
from fastapi import FastAPI, Depends

app = FastAPI()

def get_db_connection():
    """Simulated database connection"""
    return {"connection": "active", "pool_size": 10}

def get_settings():
    """Application settings"""
    return {
        "app_name": "My API",
        "debug": True,
        "version": "1.0.0"
    }

@app.get("/status")
async def get_status(db = Depends(get_db_connection)):
    """Endpoint receives db connection automatically"""
    return {"db_status": db["connection"]}

@app.get("/info")
async def get_info(
    db = Depends(get_db_connection),
    settings = Depends(get_settings)
):
    """Multiple dependencies in one endpoint"""
    return {
        "app": settings["app_name"],
        "version": settings["version"],
        "db_pool": db["pool_size"]
    }

# Demonstration
print("=== Simple Dependencies ===")
print("\nDependency functions:")
print(f"  get_db_connection() -> {get_db_connection()}")
print(f"  get_settings() -> {get_settings()}")

print("\nEndpoints use Depends() to inject these automatically:")
print("  @app.get('/status')")
print("  async def get_status(db = Depends(get_db_connection)):")
print("      return {'db_status': db['connection']}")

print("\nBenefits:")
print("  - Endpoints don't create their own connections")
print("  - Easy to swap dependencies for testing")
print("  - Shared logic across multiple endpoints")
```
