---
type: "EXAMPLE"
title: "Running Locally with Docker - Build and Run Commands"
---

Essential Docker commands for development and deployment:

```bash
# Build the Docker image
docker build -t task-manager-api:latest .

# Run with docker-compose (recommended for development)
docker-compose up -d          # Start in background
docker-compose up             # Start in foreground (see logs)
docker-compose logs -f        # Follow logs
docker-compose down           # Stop and remove containers
docker-compose down -v        # Stop and remove containers + volumes

# Run database migrations
docker-compose exec api bunx prisma migrate dev
docker-compose exec api bunx prisma db push

# Access database with psql
docker-compose exec postgres psql -U taskmanager -d taskmanager_dev

# View database with pgAdmin
# Open http://localhost:5050 and connect to postgres:5432

# Check health
curl http://localhost:3000/health/live
curl http://localhost:3000/health/ready

# View container logs
docker logs task-manager-api
docker logs -f task-manager-api  # Follow logs

# Interactive shell in running container
docker exec -it task-manager-api /bin/sh

# Run production image locally
docker run -e DATABASE_URL="..." -e JWT_SECRET="..." -p 3000:3000 task-manager-api:latest

# Push image to registry (for production)
docker tag task-manager-api:latest myregistry.azurecr.io/task-manager-api:latest
docker push myregistry.azurecr.io/task-manager-api:latest
```
