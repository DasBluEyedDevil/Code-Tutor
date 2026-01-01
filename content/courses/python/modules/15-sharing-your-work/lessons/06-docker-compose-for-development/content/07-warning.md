---
type: "WARNING"
title: "Common Docker Compose Mistakes"
---

### Avoid these common pitfalls:

**1. Hardcoding secrets in docker-compose.yml**
```yaml
# WRONG - Secrets in version control!
environment:
  - POSTGRES_PASSWORD=mysecretpassword

# CORRECT - Use .env file (add to .gitignore)
environment:
  - POSTGRES_PASSWORD=${POSTGRES_PASSWORD}
```

**2. Not waiting for database to be ready**
```yaml
# WRONG - App starts before DB is ready
depends_on:
  - db  # Only waits for container to start, not be ready

# CORRECT - Wait for healthcheck
depends_on:
  db:
    condition: service_healthy
```

**3. Using bind mounts for database data**
```yaml
# WRONG - Permission issues on some systems
volumes:
  - ./data:/var/lib/postgresql/data

# CORRECT - Use named volumes
volumes:
  - postgres_data:/var/lib/postgresql/data
```

**4. Forgetting to persist volumes**
```bash
# WRONG - Deletes all data!
docker compose down -v

# CORRECT - Keep volumes
docker compose down
```

**5. Not using networks for isolation**
```yaml
# WRONG - All services on default network
services:
  app:
    ...
  db:
    ports:
      - "5432:5432"  # DB exposed to world!

# CORRECT - Internal network, no exposed ports
services:
  db:
    networks:
      - internal
    # No ports exposed
```