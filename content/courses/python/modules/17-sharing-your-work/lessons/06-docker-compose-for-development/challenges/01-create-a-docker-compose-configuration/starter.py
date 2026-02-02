# docker-compose.yml
services:
  app:
    build: .
    ports:
      - "____:8000"
    environment:
      - DATABASE_URL=postgresql+asyncpg://user:pass@____:5432/mydb
    depends_on:
      db:
        condition: ____
    volumes:
      - ____:/app/src
    command: uvicorn src.main:app --host 0.0.0.0 --port 8000 --reload

  db:
    image: postgres:____
    environment:
      POSTGRES_USER: user
      POSTGRES_PASSWORD: pass
      POSTGRES_DB: mydb
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U ____ -d ____"]
      interval: 5s
      timeout: 5s
      retries: 5
    volumes:
      - ____:/var/lib/postgresql/data

volumes:
  postgres_data: