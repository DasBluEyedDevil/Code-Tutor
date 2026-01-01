---
type: "KEY_POINT"
title: "Main Application Entry Point"
---

**Bringing it all together in main.py:**

```python
# src/finance_tracker/main.py
from contextlib import asynccontextmanager

from fastapi import FastAPI

from .config import get_settings
from .database import Database
from .api import auth, transactions, categories, budgets


@asynccontextmanager
async def lifespan(app: FastAPI):
    """Manage application lifecycle.
    
    Connect to database on startup, disconnect on shutdown.
    """
    await Database.connect()
    yield
    await Database.disconnect()


def create_app() -> FastAPI:
    """Application factory."""
    settings = get_settings()
    
    app = FastAPI(
        title=settings.app_name,
        description="Personal Finance Tracker API",
        version="1.0.0",
        lifespan=lifespan,
    )
    
    # Include routers
    app.include_router(auth.router)
    app.include_router(transactions.router)
    app.include_router(categories.router)
    app.include_router(budgets.router)
    
    @app.get("/health")
    async def health_check():
        return {"status": "healthy"}
    
    return app


app = create_app()


if __name__ == "__main__":
    import uvicorn
    uvicorn.run("finance_tracker.main:app", reload=True)
```

**Run with:**
```bash
uv run uvicorn finance_tracker.main:app --reload
```