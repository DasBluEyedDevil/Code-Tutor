---
type: "EXAMPLE"
title: "Code Example: Preparing for Deployment"
---

**Production deployment steps:**

**1. Configuration:**
- Separate dev/prod configs
- Environment variables for secrets
- Debug mode OFF in production

**2. Dependencies:**
- pyproject.toml for modern projects (uv)
- requirements.txt for compatibility
- Pin versions (fastapi==0.109.0)

**3. Server:**
- Use uvicorn for FastAPI (production-ready ASGI server)
- Multiple workers for concurrency
- Proper error handling

**4. Monitoring:**
- Logging to files
- Error tracking (Sentry)
- Health check endpoints

**5. Security:**
- HTTPS only
- Secure cookies
- Input validation
- Regular updates

```python
import os
from pathlib import Path

print("=== Deployment Preparation ===")

print("\n1. Environment Configuration")

config_example = '''
# config.py - Production-ready configuration for FastAPI

import os
from pydantic_settings import BaseSettings
from functools import lru_cache

class Settings(BaseSettings):
    """Application settings using Pydantic for validation"""
    
    # App settings
    app_name: str = "My API"
    debug: bool = False
    
    # Database
    database_url: str = "sqlite:///./dev.db"
    
    # Security
    secret_key: str = "dev-key-change-in-production"
    access_token_expire_minutes: int = 30
    
    # Environment
    environment: str = "development"
    
    class Config:
        env_file = ".env"

@lru_cache()
def get_settings() -> Settings:
    """Cached settings instance"""
    return Settings()
'''

print(config_example)

print("\n2. Project Configuration (pyproject.toml)")

pyproject_example = '''
# pyproject.toml - Modern Python project configuration
[project]
name = "my-api"
version = "1.0.0"
description = "Production FastAPI application"
requires-python = ">=3.11"

dependencies = [
    "fastapi>=0.109.0",
    "uvicorn[standard]>=0.27.0",
    "sqlalchemy>=2.0.0",
    "pydantic-settings>=2.0.0",
    "python-jose[cryptography]>=3.3.0",
    "passlib[bcrypt]>=1.7.4",
    "asyncpg>=0.29.0",  # PostgreSQL async driver
]

[project.optional-dependencies]
dev = [
    "pytest>=7.4.0",
    "httpx>=0.26.0",
    "black>=24.0.0",
    "ruff>=0.1.0",
    "mypy>=1.8.0",
]

[tool.uv]
dev-dependencies = [
    "pytest>=7.4.0",
    "httpx>=0.26.0",
]
'''

print(pyproject_example)

print("\n3. Procfile (works on Railway, Render)")

procfile_example = '''
# Procfile (works on Railway, Render, Fly.io)
web: uvicorn app.main:app --host 0.0.0.0 --port $PORT

# With multiple workers for production
web: uvicorn app.main:app --host 0.0.0.0 --port $PORT --workers 4
'''

print(procfile_example)

print("\n4. Production-Ready FastAPI App")

app_example = '''
# app/main.py - Production-ready FastAPI app

import os
import logging
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException, Request
from fastapi.responses import JSONResponse
from config import get_settings

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s - %(name)s - %(levelname)s - %(message)s"
)
logger = logging.getLogger(__name__)

settings = get_settings()

@asynccontextmanager
async def lifespan(app: FastAPI):
    """Startup and shutdown events"""
    # Startup: connect to database, etc.
    logger.info("Starting up...")
    yield
    # Shutdown: cleanup resources
    logger.info("Shutting down...")

app = FastAPI(
    title=settings.app_name,
    debug=settings.debug,
    lifespan=lifespan
)

# Global exception handler
@app.exception_handler(Exception)
async def global_exception_handler(request: Request, exc: Exception):
    logger.error(f"Unhandled error: {exc}")
    return JSONResponse(
        status_code=500,
        content={"error": "Internal server error"}
    )

# Health check endpoint
@app.get("/health")
async def health_check():
    return {"status": "healthy"}

# Main routes
@app.get("/")
async def index():
    return {"message": "Welcome to the API"}
'''

print(app_example)

print("\n5. Docker Deployment (Optional)")

dockerfile_example = '''
# Dockerfile
FROM python:3.11-slim

# Set working directory
WORKDIR /app

# Install uv for fast package management
RUN pip install uv

# Copy project files
COPY pyproject.toml .
COPY uv.lock .

# Install dependencies
RUN uv sync --frozen --no-dev

# Copy application
COPY . .

# Expose port
EXPOSE 8000

# Run application
CMD ["uv", "run", "uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8000"]
'''

print(dockerfile_example)

print("\n=== Deployment Workflows ===")

print("\n1. Deploying to Railway (recommended):")
railway_steps = '''
# Deploying to Railway (recommended)
# 1. Install Railway CLI
npm install -g @railway/cli

# 2. Login
railway login

# 3. Initialize project
railway init

# 4. Add PostgreSQL database
railway add postgres

# 5. Deploy
railway up

# 6. Open in browser
railway open
'''

print(railway_steps)

print("\n2. Deploying to Render:")
render_steps = '''
# Deploying to Render
# 1. Push code to GitHub
# 2. Go to render.com and connect repo
# 3. Select "Web Service"
# 4. Set build command: pip install uv && uv sync
# 5. Set start command: uvicorn app.main:app --host 0.0.0.0 --port $PORT
# 6. Deploy automatically on push
'''

print(render_steps)

print("\n3. Deploying to Fly.io:")
flyio_steps = '''
# Deploying to Fly.io
# 1. Install Fly CLI
curl -L https://fly.io/install.sh | sh

# 2. Login
fly auth login

# 3. Launch app (creates fly.toml)
fly launch

# 4. Add PostgreSQL database
fly postgres create
fly postgres attach <db-name>

# 5. Deploy
fly deploy

# 6. Open in browser
fly open
'''

print(flyio_steps)

print("\n4. Using GitHub Actions (CI/CD):")
github_actions = '''
# .github/workflows/deploy.yml

name: Deploy to Production

on:
  push:
    branches: [ main ]

jobs:
  test:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v4
    
    - name: Install uv
      uses: astral-sh/setup-uv@v4
    
    - name: Set up Python
      run: uv python install 3.11
    
    - name: Install dependencies
      run: uv sync --all-extras
    
    - name: Run tests
      run: uv run pytest
    
    - name: Check code style
      run: uv run ruff check .
    
  deploy:
    needs: test
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Deploy to Railway
        uses: bervProject/railway-deploy@main
        with:
          railway_token: ${{ secrets.RAILWAY_TOKEN }}
'''

print(github_actions)
```
