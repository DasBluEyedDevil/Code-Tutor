---
type: "EXAMPLE"
title: "Alternative: Native Python Runtime"
---

**If you prefer not to use Docker, Render can detect Python automatically:**

```yaml
# render.yaml - Using Native Python Runtime
# Render detects Python from requirements.txt or pyproject.toml

services:
  - type: web
    name: finance-api
    runtime: python
    
    # Build command (install dependencies)
    buildCommand: |
      pip install uv
      uv sync --frozen --no-dev
    
    # Start command
    startCommand: uvicorn src.main:app --host 0.0.0.0 --port $PORT
    
    # Health check
    healthCheckPath: /health
    
    envVars:
      - key: DATABASE_URL
        fromDatabase:
          name: finance-db
          property: connectionString
      - key: SECRET_KEY
        generateValue: true
      - key: PYTHON_VERSION
        value: "3.13.0"

databases:
  - name: finance-db
    plan: free
```
