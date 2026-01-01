---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Use `python:3.13-slim`** - Best balance of size and compatibility
- **Multi-stage builds** reduce image size by separating build tools from runtime
- **uv in Docker** - Copy binary from `ghcr.io/astral-sh/uv:latest`
- **Layer caching** - Copy dependency files before source code
- **Run as non-root** - Create and switch to a non-privileged user
- **Use .dockerignore** - Exclude .git, .venv, .env from builds
- **Pin versions** - Use specific Python versions, not `latest`
- **Add HEALTHCHECK** - Enables container orchestration features