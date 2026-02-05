# Docker Compose Solution
# This file outputs the docker-compose.yml content that students should create

DOCKER_COMPOSE = """services:
  app:
    build: .
    ports:
      - "8000:8000"
    environment:
      - DATABASE_URL=postgresql+asyncpg://user:pass@db:5432/mydb
    depends_on:
      db:
        condition: service_healthy
    volumes:
      - ./src:/app/src
    command: uvicorn src.main:app --host 0.0.0.0 --port 8000 --reload

  db:
    image: postgres:16
    environment:
      POSTGRES_USER: user
      POSTGRES_PASSWORD: pass
      POSTGRES_DB: mydb
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U user -d mydb"]
      interval: 5s
      timeout: 5s
      retries: 5
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
"""

print("=== Docker Compose for Development ===")
print()
print("Save this content to 'docker-compose.yml':")
print()
print(DOCKER_COMPOSE)
print()
print("=== Key Features ===")
print("1. FastAPI app with hot reload (--reload)")
print("2. PostgreSQL 16 with health checks")
print("3. Volume mount for live code changes")
print("4. Database depends_on with health condition")
print("5. Named volume for database persistence")
print()
print("=== Usage ===")
print("docker compose up -d    # Start services")
print("docker compose logs -f  # View logs")
print("docker compose down     # Stop services")
