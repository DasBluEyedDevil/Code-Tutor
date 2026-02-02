---
type: "EXAMPLE"
title: "Essential Docker Compose Commands"
---

**Commands you'll use daily:**

```bash
# Start all services in foreground (see logs)
docker compose up

# Start in background (detached)
docker compose up -d

# Start and rebuild images
docker compose up --build

# Stop all services
docker compose down

# Stop and remove volumes (WARNING: deletes database data!)
docker compose down -v

# View logs for all services
docker compose logs

# Follow logs in real-time
docker compose logs -f

# View logs for specific service
docker compose logs app
docker compose logs db

# List running services
docker compose ps

# Execute command in running container
docker compose exec app bash
docker compose exec db psql -U financeuser -d financedb

# Run a one-off command (starts new container)
docker compose run app pytest
docker compose run app uv add requests

# Restart a specific service
docker compose restart app

# Scale a service (run multiple instances)
docker compose up -d --scale app=3

# Pull latest images
docker compose pull

# View service configuration
docker compose config
```
