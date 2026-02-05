---
type: "EXAMPLE"
title: "Setting Up Your FastAPI Environment"
---

**Before diving into M14, let's set up your environment.**

**Step 1: Create a Virtual Environment**

```bash
# Create a new directory for your FastAPI projects
mkdir fastapi-projects
cd fastapi-projects

# Create virtual environment
python -m venv venv

# Activate it
# On Windows:
venv\Scripts\activate
# On macOS/Linux:
source venv/bin/activate
```

**Step 2: Install FastAPI and Uvicorn**

```bash
pip install fastapi uvicorn
```

**Step 3: Create Your First Async Endpoint**

Create a file named `main.py`:

```python
from fastapi import FastAPI
import asyncio

app = FastAPI()

@app.get("/")
async def root():
    # This is an async function - just like M13!
    await asyncio.sleep(0.1)  # Simulate async work
    return {"message": "Hello from async FastAPI!"}

@app.get("/slow")
async def slow_endpoint():
    # Simulating an async database query
    await asyncio.sleep(1)
    return {"message": "This was slow, but didn't block other requests!"}
```

**Step 4: Run the Server**

```bash
uvicorn main:app --reload
```

Visit `http://localhost:8000` to see your async API in action!

**Step 5: Test Concurrency**

Open two terminal windows and run simultaneously:

```bash
# Terminal 1
curl http://localhost:8000/slow

# Terminal 2 (immediately after)
curl http://localhost:8000/
```

Notice how the second request returns immediately—even while the first is still "sleeping." That's async power!
