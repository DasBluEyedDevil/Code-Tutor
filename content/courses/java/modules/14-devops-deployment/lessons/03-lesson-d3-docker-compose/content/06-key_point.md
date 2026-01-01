---
type: "KEY_POINT"
title: "Essential Commands"
---

STARTING SERVICES:

docker compose up              # Start all, attached (see logs)
docker compose up -d           # Start all, detached (background)
docker compose up --build      # Rebuild images before starting
docker compose up app          # Start only 'app' and dependencies

STOPPING SERVICES:

docker compose stop            # Stop containers (keep data)
docker compose down            # Stop and remove containers
docker compose down -v         # Also remove volumes (wipes data!)

VIEWING STATUS:

docker compose ps              # List running services
docker compose logs            # View all logs
docker compose logs -f app     # Follow logs for 'app' service
docker compose top             # Show running processes

DEBUGGING:

docker compose exec app sh     # Shell into running container
docker compose exec db psql -U postgres  # Connect to database

REBUILDING:

docker compose build           # Rebuild all images
docker compose build app       # Rebuild only 'app'
docker compose up --build -d   # Rebuild and restart

CLEANUP:

docker compose down --rmi all  # Remove images too
docker system prune            # Clean unused Docker resources