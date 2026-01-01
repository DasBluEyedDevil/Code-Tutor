---
type: "EXAMPLE"
title: "Docker Compose for Development"
---

Local development with PostgreSQL:

```yaml
# docker-compose.yml
version: '3.8'

services:
  app:
    build: .
    ports:
      - "8000:8000"
    environment:
      - DATABASE_URL=postgresql://postgres:postgres@db:5432/finance_tracker
      - SECRET_KEY=${SECRET_KEY:-development-secret-key}
      - DEBUG=true
    depends_on:
      db:
        condition: service_healthy
    volumes:
      - ./src:/app/src:ro  # Mount source for development
    command: uvicorn finance_tracker.main:app --host 0.0.0.0 --reload

  db:
    image: postgres:16-alpine
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: finance_tracker
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
      - ./migrations:/docker-entrypoint-initdb.d:ro
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U postgres"]
      interval: 5s
      timeout: 5s
      retries: 5

volumes:
  postgres_data:
```
