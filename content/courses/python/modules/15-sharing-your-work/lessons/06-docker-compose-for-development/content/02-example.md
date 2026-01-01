---
type: "EXAMPLE"
title: "Docker Compose for Personal Finance Tracker"
---

**Complete docker-compose.yml with PostgreSQL**

This configuration sets up:
- Your FastAPI app with hot reload
- PostgreSQL 16 with persistent data
- Automatic service dependencies

```yaml
# docker-compose.yml
# Development environment for Personal Finance Tracker

services:
  # ==================
  # FastAPI Application
  # ==================
  app:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8000:8000"
    environment:
      # Database connection string
      - DATABASE_URL=postgresql+asyncpg://financeuser:financepass@db:5432/financedb
      # Development settings
      - DEBUG=true
      - LOG_LEVEL=DEBUG
    depends_on:
      db:
        condition: service_healthy
    volumes:
      # Mount source code for hot reload
      - ./src:/app/src
    # Override CMD for development with reload
    command: uvicorn src.main:app --host 0.0.0.0 --port 8000 --reload
    networks:
      - finance-network

  # ==================
  # PostgreSQL Database
  # ==================
  db:
    image: postgres:16
    environment:
      POSTGRES_USER: financeuser
      POSTGRES_PASSWORD: financepass
      POSTGRES_DB: financedb
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U financeuser -d financedb"]
      interval: 5s
      timeout: 5s
      retries: 5
    volumes:
      # Persist database data
      - postgres_data:/var/lib/postgresql/data
      # Optional: Run init scripts
      - ./scripts/init-db.sql:/docker-entrypoint-initdb.d/init.sql
    ports:
      # Expose for local tools (DBeaver, pgAdmin)
      - "5432:5432"
    networks:
      - finance-network

# Named volumes persist data between container restarts
volumes:
  postgres_data:

# Custom network for service communication
networks:
  finance-network:
    driver: bridge
```
