---
type: "EXAMPLE"
title: "Multi-Stage Dockerfile with uv"
---

**Multi-stage builds = smaller production images**

We use two stages:
1. **Builder stage**: Install dependencies with uv
2. **Runtime stage**: Copy only what's needed to run

This separates build tools from the final image, reducing size and attack surface.

```dockerfile
# Dockerfile for Personal Finance Tracker
# Multi-stage build for smaller, more secure images

# ====================
# Stage 1: Builder
# ====================
FROM python:3.13-slim AS builder

# Install uv - fast Python package manager
COPY --from=ghcr.io/astral-sh/uv:latest /uv /usr/local/bin/uv

# Set working directory
WORKDIR /app

# Copy dependency files first (better layer caching)
COPY pyproject.toml uv.lock ./

# Install dependencies to a virtual environment
# --frozen: use exact versions from uv.lock
# --no-dev: skip development dependencies
RUN uv sync --frozen --no-dev

# ====================
# Stage 2: Runtime
# ====================
FROM python:3.13-slim

# Set working directory
WORKDIR /app

# Copy the virtual environment from builder stage
COPY --from=builder /app/.venv /app/.venv

# Copy application source code
COPY src/ ./src/

# Add .venv/bin to PATH so we can run Python packages directly
ENV PATH="/app/.venv/bin:$PATH"

# Create non-root user for security
RUN useradd --create-home --shell /bin/bash appuser \
    && chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

# Document the port the app uses
EXPOSE 8000

# Health check for container orchestration
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD python -c "import urllib.request; urllib.request.urlopen('http://localhost:8000/health')"

# Run the FastAPI application
CMD ["uvicorn", "src.main:app", "--host", "0.0.0.0", "--port", "8000"]
```
