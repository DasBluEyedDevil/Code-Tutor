---
type: "EXAMPLE"
title: "Dependencies with Yield (Cleanup)"
---

**Dependencies that need cleanup use `yield`:**

Some resources (database connections, file handles) need to be properly closed after use. Use `yield` instead of `return` to ensure cleanup.

**Pattern:**
```python
def get_resource():
    resource = acquire_resource()  # Setup
    try:
        yield resource              # Provide to endpoint
    finally:
        resource.close()            # Cleanup (always runs)
```

**How It Works:**
1. Code before `yield` runs before the endpoint
2. The yielded value is injected into the endpoint
3. Code after `yield` (in finally) runs after the response
4. Cleanup runs even if the endpoint raises an exception

```python
from fastapi import FastAPI, Depends
from typing import Generator

app = FastAPI()

def get_db() -> Generator[dict, None, None]:
    """Database connection with automatic cleanup"""
    db = {"connection": "open", "queries": 0}
    print("Opening database connection")
    try:
        yield db
    finally:
        print("Closing database connection")
        db["connection"] = "closed"

def get_file_handler() -> Generator[dict, None, None]:
    """File handler with cleanup"""
    file = {"name": "data.txt", "status": "open"}
    print("Opening file")
    try:
        yield file
    finally:
        print("Closing file")
        file["status"] = "closed"

@app.get("/data")
async def get_data(db = Depends(get_db)):
    """Database connection is automatically cleaned up after response"""
    db["queries"] += 1
    return {"status": db["connection"], "queries": db["queries"]}

@app.get("/read-file")
async def read_file(
    db = Depends(get_db),
    file = Depends(get_file_handler)
):
    """Multiple cleanup dependencies"""
    return {
        "db": db["connection"],
        "file": file["status"]
    }

# Demonstration
print("=== Dependencies with Yield ===")
print("\nSimulating request lifecycle:")

# Simulate what FastAPI does
db_gen = get_db()
db = next(db_gen)  # Setup - opens connection
print(f"  Endpoint receives: {db}")
print("  Endpoint does its work...")

# Simulate cleanup after response
try:
    next(db_gen)
except StopIteration:
    pass

print(f"  After cleanup: connection is closed")

print("\nKey benefits:")
print("  - Resources are ALWAYS cleaned up")
print("  - Works even if endpoint raises exception")
print("  - Perfect for DB connections, file handles, locks")
```
