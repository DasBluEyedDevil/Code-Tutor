---
type: "KEY_POINT"
title: "Docker Compose for Local Development"
---

Instead of installing PostgreSQL locally, use Docker Compose for consistent development environments.

```yaml
# docker-compose.yml (in project root)
version: '3.8'

services:
  postgres:
    image: postgres:16-alpine
    container_name: taskmanager-db
    environment:
      POSTGRES_DB: taskmanager
      POSTGRES_USER: taskuser
      POSTGRES_PASSWORD: localdev123
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U taskuser -d taskmanager"]
      interval: 5s
      timeout: 5s
      retries: 5

volumes:
  postgres_data:
```

Usage Commands:
- docker compose up -d: Start database in background
- docker compose logs -f postgres: View database logs
- docker compose down: Stop database (data preserved)
- docker compose down -v: Stop and delete all data (fresh start)

Benefits:
- One command starts everything needed
- Same environment for all team members
- Easy to reset: down -v, then up -d
- Matches production environment
- No "works on my machine" problems