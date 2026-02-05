# Dockerfile Solution
# This file outputs the Dockerfile content that students should create

DOCKERFILE = """# Stage 1: Builder
FROM python:3.13-slim AS builder

# Install uv
COPY --from=ghcr.io/astral-sh/uv:latest /uv /usr/local/bin/uv

WORKDIR /app

# Copy dependency files
COPY pyproject.toml uv.lock ./

# Install production dependencies
RUN uv sync --frozen --no-dev

# Stage 2: Runtime
FROM python:3.13-slim

WORKDIR /app

# Copy virtual environment from builder
COPY --from=builder /app/.venv /app/.venv

# Copy source code
COPY src/ ./src/

# Set PATH to use venv
ENV PATH="/app/.venv/bin:$PATH"

# Create and switch to non-root user
RUN useradd -m appuser && chown -R appuser:appuser /app
USER appuser

# Expose port and run
EXPOSE 8000
CMD ["uvicorn", "src.main:app", "--host", "0.0.0.0", "--port", "8000"]
"""

print("=== Dockerfile for Python with uv ===")
print()
print("Save this content to a file named 'Dockerfile' (no extension):")
print()
print(DOCKERFILE)
print()
print("=== Key Features ===")
print("1. Multi-stage build (smaller final image)")
print("2. Uses uv for fast dependency installation")
print("3. Non-root user for security")
print("4. Frozen dependencies for reproducibility")
