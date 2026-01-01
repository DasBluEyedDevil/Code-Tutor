---
type: "CODE"
title: "docker-compose with PostgreSQL"
---

Create a docker-compose.yml for local development with PostgreSQL and volumes:

```yaml
# docker-compose.yml
version: '3.9'

services:
  # PostgreSQL database
  postgres:
    image: postgres:16-alpine
    container_name: task-manager-postgres
    environment:
      POSTGRES_USER: taskmanager
      POSTGRES_PASSWORD: ${DB_PASSWORD:-dev-password}
      POSTGRES_DB: taskmanager_dev
    ports:
      - "5432:5432"
    volumes:
      # Named volume for persistent data
      - postgres_data:/var/lib/postgresql/data
      # Initialize database with seed (optional)
      - ./prisma/init.sql:/docker-entrypoint-initdb.d/init.sql
    networks:
      - task-manager-network
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U taskmanager"]
      interval: 10s
      timeout: 5s
      retries: 5

  # Bun API application
  api:
    build: .
    container_name: task-manager-api
    depends_on:
      postgres:
        condition: service_healthy
    environment:
      DATABASE_URL: postgresql://taskmanager:${DB_PASSWORD:-dev-password}@postgres:5432/taskmanager_dev
      JWT_SECRET: ${JWT_SECRET:-dev-secret-change-in-production}
      PORT: 3000
      NODE_ENV: development
    ports:
      - "3000:3000"
    volumes:
      # Hot reload during development
      - ./src:/app/src
      - ./prisma:/app/prisma
      # Don't sync node_modules
      - /app/node_modules
    networks:
      - task-manager-network
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost:3000/"]
      interval: 30s
      timeout: 3s
      retries: 3
      start_period: 5s
    command: bun run --watch src/index.ts

  # Optional: pgAdmin for database management
  pgadmin:
    image: dpage/pgadmin4:latest
    container_name: task-manager-pgadmin
    environment:
      PGADMIN_DEFAULT_EMAIL: admin@local
      PGADMIN_DEFAULT_PASSWORD: admin
    ports:
      - "5050:80"
    depends_on:
      - postgres
    networks:
      - task-manager-network
    volumes:
      - pgadmin_data:/var/lib/pgadmin

volumes:
  postgres_data:
  pgadmin_data:

networks:
  task-manager-network:
    driver: bridge
```
