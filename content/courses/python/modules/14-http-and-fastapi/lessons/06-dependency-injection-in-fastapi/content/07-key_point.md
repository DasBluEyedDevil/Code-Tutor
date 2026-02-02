---
type: "KEY_POINT"
title: "Background Tasks and Middleware"
---

**Background tasks** - run after response:
```python
from fastapi import BackgroundTasks

def send_email(email: str, message: str):
    # Slow operation
    time.sleep(5)
    print(f"Sent to {email}")

@app.post("/signup/")
def signup(email: str, background: BackgroundTasks):
    create_user(email)
    background.add_task(send_email, email, "Welcome!")
    return {"status": "created"}  # Returns immediately
```

**Middleware** - runs on every request:
```python
from fastapi.middleware.cors import CORSMiddleware

app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:3000"],
    allow_methods=["*"],
    allow_headers=["*"]
)

@app.middleware("http")
async def log_requests(request, call_next):
    start = time.time()
    response = await call_next(request)
    duration = time.time() - start
    print(f"{request.method} {request.url} - {duration:.2f}s")
    return response
```
