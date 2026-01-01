---
type: "THEORY"
title: "Understanding docker-compose.yml Structure"
---

**Key sections explained:**

**`services:`**
Defines each container in your application.

**`build:`**
```yaml
build:
  context: .         # Directory containing Dockerfile
  dockerfile: Dockerfile  # Which Dockerfile to use
```

**`ports:`**
```yaml
ports:
  - "8000:8000"     # host:container
```
Maps container port to your machine.

**`environment:`**
```yaml
environment:
  - DATABASE_URL=postgresql://...
  - DEBUG=true
```
Sets environment variables inside the container.

**`depends_on:`**
```yaml
depends_on:
  db:
    condition: service_healthy
```
- Waits for `db` service to be healthy before starting
- `service_healthy` requires a healthcheck
- Prevents "connection refused" errors

**`volumes:`**
```yaml
volumes:
  - ./src:/app/src   # Bind mount: host path : container path
  - postgres_data:/var/lib/postgresql/data  # Named volume
```
- **Bind mounts**: Sync local files into container (hot reload)
- **Named volumes**: Persist data between restarts

**`healthcheck:`**
```yaml
healthcheck:
  test: ["CMD-SHELL", "pg_isready -U user -d db"]
  interval: 5s    # Check every 5 seconds
  timeout: 5s     # Fail if no response in 5s
  retries: 5      # Mark unhealthy after 5 failures
```
Docker checks if the service is ready.