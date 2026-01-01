---
type: "KEY_POINT"
title: "Docker Compose: One File to Rule Them All"
---

Docker Compose lets you define multi-container applications in a single YAML file:

# docker-compose.yml
services:
  app:
    build: .
    ports:
      - "8080:8080"
    depends_on:
      - db
    environment:
      - SPRING_DATASOURCE_URL=jdbc:postgresql://db:5432/mydb

  db:
    image: postgres:16
    environment:
      - POSTGRES_DB=mydb
      - POSTGRES_PASSWORD=secret

NOW, ONE COMMAND:

docker compose up     # Start everything
docker compose down   # Stop everything
docker compose logs   # View all logs

BENEFITS:
- All services defined in one file
- Automatic networking between services
- Dependency ordering (depends_on)
- Easy to version control
- Same file works for entire team
- 'docker compose up' and you're running