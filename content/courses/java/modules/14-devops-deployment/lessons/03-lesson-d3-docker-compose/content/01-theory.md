---
type: "THEORY"
title: "The Problem: Multiple Containers"
---

Real applications rarely run alone. Your Spring Boot app needs:

- PostgreSQL database
- Redis for caching
- Maybe Elasticsearch for search
- Maybe a message queue

RUNNING MULTIPLE CONTAINERS MANUALLY:

# Start PostgreSQL
docker run -d --name postgres \
  -e POSTGRES_PASSWORD=secret \
  -p 5432:5432 \
  postgres:16

# Start Redis
docker run -d --name redis \
  -p 6379:6379 \
  redis:7

# Start your app (needs to connect to both)
docker run -d --name myapp \
  -e DB_URL=jdbc:postgresql://postgres:5432/mydb \
  -e REDIS_HOST=redis \
  -p 8080:8080 \
  --link postgres \
  --link redis \
  myapp:latest

PROBLEMS:
- Many commands to remember
- Order matters (database before app)
- Links are deprecated
- No easy way to share this with team
- 'Restart everything' is painful

Docker Compose solves all of this.