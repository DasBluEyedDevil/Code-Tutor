---
type: "EXAMPLE"
title: "Class-Based Dependencies"
---

**Use classes for dependencies with state or configuration:**

Classes are useful when:
- Dependencies have configuration options
- You need to reuse with different settings
- Dependencies maintain state across requests

**Pattern: Callable Class**
```python
class MyDependency:
    def __init__(self, config):
        self.config = config
    
    def __call__(self):
        return self.config
```

**Shortcut: Depends() with class**
```python
# FastAPI automatically calls the class
@app.get('/items')
def get_items(pagination: Pagination = Depends()):
    # pagination is an instance of Pagination
```

```python
from fastapi import FastAPI, Depends, Query

app = FastAPI()

class Pagination:
    """Reusable pagination parameters"""
    
    def __init__(
        self,
        skip: int = Query(0, ge=0, description="Items to skip"),
        limit: int = Query(10, ge=1, le=100, description="Max items")
    ):
        self.skip = skip
        self.limit = limit

class DatabaseConfig:
    """Database configuration as dependency"""
    
    def __init__(self, db_name: str = "default"):
        self.db_name = db_name
        self.connection_string = f"sqlite:///{db_name}.db"
    
    def __call__(self):
        """Makes the instance callable"""
        return {
            "db": self.db_name,
            "connection": self.connection_string
        }

# Create instances with different configs
default_db = DatabaseConfig("main")
test_db = DatabaseConfig("test")

@app.get("/items")
async def list_items(pagination: Pagination = Depends()):
    """Pagination is auto-instantiated from query params"""
    return {
        "skip": pagination.skip,
        "limit": pagination.limit,
        "items": [f"item_{i}" for i in range(pagination.skip, pagination.skip + pagination.limit)]
    }

@app.get("/users")
async def list_users(pagination: Pagination = Depends()):
    """Same pagination class, different endpoint"""
    return {
        "skip": pagination.skip,
        "limit": pagination.limit,
        "users": [f"user_{i}" for i in range(pagination.skip, pagination.skip + pagination.limit)]
    }

@app.get("/db-info")
async def get_db_info(db = Depends(default_db)):
    """Uses callable class instance"""
    return db

# Demonstration
print("=== Class-Based Dependencies ===")

print("\n1. Pagination class:")
pagination = Pagination()  # Uses defaults
print(f"   Default: skip={pagination.skip}, limit={pagination.limit}")

print("\n2. DatabaseConfig as callable:")
print(f"   default_db() -> {default_db()}")
print(f"   test_db() -> {test_db()}")

print("\n3. How Depends() works with classes:")
print("   @app.get('/items')")
print("   async def list_items(pagination: Pagination = Depends()):")
print("       # FastAPI creates Pagination instance from query params")
print("       # ?skip=10&limit=20 -> Pagination(skip=10, limit=20)")

print("\n4. Benefits of class-based dependencies:")
print("   - Encapsulate related parameters")
print("   - Add validation in __init__")
print("   - Reuse across multiple endpoints")
print("   - Configure different instances for different contexts")
```
