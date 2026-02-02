---
type: "EXAMPLE"
title: "Development vs Production Compose Files"
---

**Override files for different environments:**

Docker Compose automatically merges `docker-compose.yml` with `docker-compose.override.yml` for development.

```yaml
# docker-compose.yml (base configuration)
services:
  app:
    build: .
    ports:
      - "8000:8000"
    environment:
      - DATABASE_URL=postgresql+asyncpg://user:pass@db:5432/finance
    depends_on:
      db:
        condition: service_healthy

  db:
    image: postgres:16
    environment:
      POSTGRES_USER: user
      POSTGRES_PASSWORD: pass
      POSTGRES_DB: finance
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U user -d finance"]
      interval: 5s
      timeout: 5s
      retries: 5
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:

---
# docker-compose.override.yml (development overrides - auto-loaded)
services:
  app:
    volumes:
      - ./src:/app/src  # Hot reload
    command: uvicorn src.main:app --host 0.0.0.0 --port 8000 --reload
    environment:
      - DEBUG=true
      - LOG_LEVEL=DEBUG

  db:
    ports:
      - "5432:5432"  # Expose for local DB tools

---
# docker-compose.prod.yml (production configuration)
# Use with: docker compose -f docker-compose.yml -f docker-compose.prod.yml up
services:
  app:
    # No volume mounts in production
    # No --reload in production
    command: uvicorn src.main:app --host 0.0.0.0 --port 8000 --workers 4
    environment:
      - DEBUG=false
      - LOG_LEVEL=WARNING
    deploy:
      resources:
        limits:
          memory: 512M
        reservations:
          memory: 256M

  db:
    # Don't expose database port in production
    ports: []
```
