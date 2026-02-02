---
type: "THEORY"
title: "Understanding Each Dockerfile Instruction"
---

**Let's break down the key instructions:**

**`FROM python:3.13-slim AS builder`**
- Sets the base image
- `AS builder` names this stage for reference

**`COPY --from=ghcr.io/astral-sh/uv:latest /uv /usr/local/bin/uv`**
- Copies uv binary directly from its official image
- No need to install pip or download scripts
- Fastest way to get uv in Docker

**`COPY pyproject.toml uv.lock ./`**
- Copies dependency files first
- Docker caches this layer
- Dependencies only reinstall when these files change

**`RUN uv sync --frozen --no-dev`**
- `--frozen`: Uses exact versions from uv.lock
- `--no-dev`: Skips pytest, black, etc.
- Creates `.venv` directory with all packages

**`COPY --from=builder /app/.venv /app/.venv`**
- Copies virtual environment from builder stage
- Only the installed packages, not build tools
- Dramatically reduces final image size

**`ENV PATH="/app/.venv/bin:$PATH"`**
- Adds venv to PATH
- Python and all installed packages are now available
- Equivalent to "activating" the venv

**`USER appuser`**
- Runs container as non-root user
- Security best practice
- Limits damage if container is compromised

**`HEALTHCHECK`**
- Tells Docker how to check if app is healthy
- Used by orchestration tools (Kubernetes, Docker Compose)
- Enables automatic restarts on failure