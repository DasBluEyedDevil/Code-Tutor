---
type: "ANALOGY"
title: "The Restaurant Kitchen Analogy"
---

**Understanding Async Request Handling**

Imagine a restaurant kitchen with one chef:

**Synchronous Kitchen (Flask/Django traditional)**
The chef takes an order, cooks the entire meal, serves it, then takes the next order. If the steak takes 10 minutes to cook, the chef waits and does nothing. Other customers wait in line.

**Async Kitchen (FastAPI)**
The chef takes an order, puts the steak on the grill, and immediately takes the next order. While the steak cooks (waiting for I/O), the chef prepares salads, takes more orders, or plates desserts. When the steak is ready, the chef returns to finish it.

**The Key Insight:**
The chef is always busy, never waiting. One async "chef" (server process) can handle many "customers" (requests) simultaneously.

**In Code Terms:**

```python
# Synchronous: Chef waits for steak
@app.get("/steak")
def make_steak():
    steak = cook_steak()  # Blocks for 10 minutes
    return {"meal": steak}

# Async: Chef works on other orders while steak cooks
@app.get("/steak")
async def make_steak():
    steak = await cook_steak()  # Other requests handled here
    return {"meal": steak}
```

**Why This Matters:**
A single FastAPI server with one worker can handle hundreds of concurrent requests because it never waits idle—it switches to other work whenever it hits an `await`.

This is exactly the pattern you mastered in M13 with `asyncio.gather()` and `TaskGroup`. FastAPI just applies it to HTTP requests.
