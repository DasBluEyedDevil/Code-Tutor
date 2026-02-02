---
type: "WARNING"
title: "Common Render Deployment Issues"
---

### Watch out for these common problems:

**1. Free tier cold starts**
```
# Free services spin down after 15 minutes of inactivity
# First request after spin-down takes 30-60 seconds

# Solution: Use a health check service like UptimeRobot
# or upgrade to paid tier for always-on
```

**2. DATABASE_URL format mismatch**
```python
# WRONG - Render uses postgresql://, SQLAlchemy 2.0 needs postgresql+asyncpg://
DATABASE_URL = os.environ["DATABASE_URL"]

# CORRECT - Convert the URL for async SQLAlchemy
db_url = os.environ["DATABASE_URL"]
if db_url.startswith("postgres://"):
    db_url = db_url.replace("postgres://", "postgresql+asyncpg://", 1)
```

**3. Missing health check endpoint**
```python
# WRONG - No health check, Render can't verify app is running
# App may show as "unhealthy" in dashboard

# CORRECT - Add health check endpoint
@app.get("/health")
async def health_check():
    return {"status": "healthy"}
```

**4. Not setting PORT correctly**
```python
# WRONG - Hardcoded port
uvicorn.run(app, port=8000)

# CORRECT - Use PORT environment variable
import os
port = int(os.environ.get("PORT", 8000))
uvicorn.run(app, host="0.0.0.0", port=port)
```

**5. Free database expiration**
```
# Free PostgreSQL expires after 90 days
# Set a reminder to either:
# - Export your data before expiration
# - Upgrade to a paid plan
# - Create a new free database and migrate
```