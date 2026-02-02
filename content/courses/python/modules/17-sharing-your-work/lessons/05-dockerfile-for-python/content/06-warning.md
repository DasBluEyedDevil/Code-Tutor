---
type: "WARNING"
title: "Common Dockerfile Mistakes"
---

### Watch out for these common issues:

**1. Installing pip packages without caching**
```dockerfile
# WRONG - Downloads packages every build
RUN pip install -r requirements.txt

# CORRECT - Cache dependencies layer
COPY requirements.txt .
RUN pip install -r requirements.txt
COPY . .
```

**2. Running as root**
```dockerfile
# WRONG - Security risk!
CMD ["uvicorn", "main:app"]

# CORRECT - Use non-root user
USER appuser
CMD ["uvicorn", "main:app"]
```

**3. Copying unnecessary files**
```dockerfile
# WRONG - Includes .git, __pycache__, .env
COPY . .

# CORRECT - Use .dockerignore
# .dockerignore:
# .git
# __pycache__
# .env
# *.pyc
# .venv
```

**4. Using `latest` tag in production**
```dockerfile
# WRONG - Not reproducible
FROM python:latest

# CORRECT - Pin specific version
FROM python:3.13-slim
```

**5. Not using multi-stage builds**
```dockerfile
# WRONG - Image contains build tools
FROM python:3.13
RUN pip install uv
RUN uv sync

# CORRECT - Separate build and runtime
FROM python:3.13-slim AS builder
# ... build steps ...

FROM python:3.13-slim
COPY --from=builder /app/.venv /app/.venv
```