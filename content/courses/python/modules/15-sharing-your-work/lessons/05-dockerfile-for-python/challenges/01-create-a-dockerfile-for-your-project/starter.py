# Stage 1: Builder
FROM ____:____-slim AS builder

# Install uv
COPY --from=ghcr.io/astral-sh/uv:latest ____ /usr/local/bin/uv

WORKDIR /app

# Copy dependency files
COPY pyproject.toml uv.lock ./

# Install production dependencies
RUN uv sync ____ ____

# Stage 2: Runtime
FROM python:3.13-slim

WORKDIR /app

# Copy virtual environment from builder
COPY ____=builder /app/.venv /app/.venv

# Copy source code
COPY src/ ./src/

# Set PATH to use venv
ENV PATH="____:$PATH"

# Create and switch to non-root user
RUN useradd -m appuser && chown -R appuser:appuser /app
USER ____

# Expose port and run
EXPOSE ____
CMD ["uvicorn", "src.main:app", "--host", "0.0.0.0", "--port", "8000"]