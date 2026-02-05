---
type: "THEORY"
title: "Why Async Matters for Web APIs"
---

**Your async skills are about to become superpowers.**

In Module 13, you learned to write concurrent code with `async`/`await`. Now you'll see why this matters enormously for web development.

**The Web is Naturally Async**

When a user requests a webpage, the server often needs to:
- Query a database (wait for response)
- Call another API (wait for response)  
- Read a file (wait for disk)
- Send an email (wait for SMTP server)

Traditional synchronous code handles one request at a time. But async code? It can handle **thousands** of concurrent requests efficiently.

**From Async Functions to Web Handlers**

The jump is smaller than you think:

```python
# What you learned in M13
async def fetch_data():
    async with httpx.AsyncClient() as client:
        response = await client.get("https://api.example.com/data")
        return response.json()

# What you'll write in M14 (FastAPI)
from fastapi import FastAPI

app = FastAPI()

@app.get("/data")
async def get_data():
    # Same async patterns, wrapped in an HTTP endpoint
    async with httpx.AsyncClient() as client:
        response = await client.get("https://api.example.com/data")
        return response.json()
```

**FastAPI is Built on Asyncio**

FastAPI was designed specifically for Python's async capabilities. Every route handler can be `async`, and the framework handles the event loop for you—just like you learned in M13.

**What Changes in M14:**
- You'll add **HTTP routing** (`@app.get()`, `@app.post()`)
- You'll use **Pydantic** for data validation (type hints on steroids)
- You'll learn **dependency injection** (clean, testable code)
- But the **async patterns stay the same**

Your `async def`, `await`, `async with`, and TaskGroup skills transfer directly.
