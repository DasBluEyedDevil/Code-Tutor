---
type: "EXAMPLE"
title: "Your First Async Function"
---

**Basic async function:**
```python
import asyncio

async def greet(name):
    await asyncio.sleep(1)  # Pause 1 second (non-blocking)
    return f"Hello, {name}!"

# Running the coroutine
result = asyncio.run(greet("Alice"))
print(result)  # Hello, Alice!
```

**Key points:**
- `asyncio.run()` starts the event loop and runs a coroutine
- `await asyncio.sleep()` pauses without blocking
- The function returns normally after awaiting

```python
import asyncio

# Define an async function (coroutine)
async def greet(name):
    """Async greeting with simulated delay"""
    print(f"  Starting to greet {name}...")
    await asyncio.sleep(0.5)  # Non-blocking pause
    print(f"  Finished greeting {name}!")
    return f"Hello, {name}!"

async def fetch_user_data(user_id):
    """Simulates fetching user data from API"""
    print(f"  Fetching user {user_id}...")
    await asyncio.sleep(0.3)  # Simulate network delay
    return {"id": user_id, "name": f"User_{user_id}"}

# The main async function
async def main():
    print("=== Basic Async Function ===")
    
    # Await a single coroutine
    greeting = await greet("Alice")
    print(f"  Result: {greeting}\n")
    
    print("=== Calling Multiple Async Functions ===")
    
    # These run sequentially (one after another)
    user1 = await fetch_user_data(1)
    user2 = await fetch_user_data(2)
    print(f"  User 1: {user1}")
    print(f"  User 2: {user2}\n")
    
    print("=== What NOT to Do ===")
    print("  # This creates a coroutine but doesn't run it:")
    print("  coro = greet('Bob')  # Just creates object")
    print("  # Must use: result = await coro")

# Run the async code
print("Starting async program...\n")
asyncio.run(main())
print("\nAsync program complete!")
```
