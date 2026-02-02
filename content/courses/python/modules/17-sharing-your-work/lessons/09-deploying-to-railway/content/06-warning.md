---
type: "WARNING"
title: "Common Railway Deployment Issues"
---

### Watch out for these common problems:

**1. PORT environment variable**
```python
# WRONG - Hardcoded port
uvicorn.run(app, port=8000)

# CORRECT - Railway sets PORT dynamically
import os
port = int(os.environ.get("PORT", 8000))
uvicorn.run(app, host="0.0.0.0", port=port)
```

**2. Missing Procfile or start command**
```
# Railway needs to know how to start your app
# Option 1: Procfile
web: uvicorn src.main:app --host 0.0.0.0 --port $PORT

# Option 2: railway.toml
[deploy]
startCommand = "uvicorn src.main:app --host 0.0.0.0 --port $PORT"

# Option 3: Dockerfile CMD (recommended)
CMD ["uvicorn", "src.main:app", "--host", "0.0.0.0", "--port", "8000"]
```

**3. Build failures with uv**
```dockerfile
# If Railway doesn't have uv, install it in Dockerfile
FROM python:3.13-slim
RUN pip install uv
# ... rest of Dockerfile
```

**4. Database connection refused**
```python
# WRONG - Using localhost
DATABASE_URL = "postgresql://localhost/mydb"

# CORRECT - Use Railway's DATABASE_URL
DATABASE_URL = os.environ["DATABASE_URL"]
# Railway provides the full connection string
```

**5. Running out of free credits**
```
# Railway hobby plan: $5 free credit/month
# Monitor usage in dashboard > Usage tab
# Tips to reduce usage:
# - Use smaller memory limits
# - Stop unused services
# - Delete old deployments
```