---
type: "WARNING"
title: "Common Docker Compose Mistakes"
---

MISTAKE 1: Forgetting volumes (data loss!)

# BAD - data lost when container stops
db:
  image: postgres:16

# GOOD - data persists
db:
  image: postgres:16
  volumes:
    - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:

MISTAKE 2: depends_on without health check

# BAD - app starts before DB is ready
depends_on:
  - db

# GOOD - waits for DB health check
depends_on:
  db:
    condition: service_healthy

MISTAKE 3: Hardcoding secrets

# BAD - secrets in version control
environment:
  - POSTGRES_PASSWORD=supersecretpassword

# BETTER - use .env file (add to .gitignore)
environment:
  - POSTGRES_PASSWORD=${DB_PASSWORD}

# Create .env file:
# DB_PASSWORD=supersecretpassword

MISTAKE 4: Using 'localhost' in container

# BAD - 'localhost' inside container is the container itself
SPRING_DATASOURCE_URL=jdbc:postgresql://localhost:5432/myapp

# GOOD - use service name
SPRING_DATASOURCE_URL=jdbc:postgresql://db:5432/myapp