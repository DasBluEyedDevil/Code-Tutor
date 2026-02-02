---
type: "KEY_POINT"
title: "Service Containers"
---

**Service containers run alongside your job:**

GitHub Actions can spin up Docker containers that your tests can connect to:

**PostgreSQL:**
```yaml
services:
  postgres:
    image: postgres:16
    env:
      POSTGRES_USER: test
      POSTGRES_PASSWORD: test
      POSTGRES_DB: test
    ports:
      - 5432:5432
```

**Redis:**
```yaml
services:
  redis:
    image: redis:7
    ports:
      - 6379:6379
```

**Health checks ensure the service is ready:**
```yaml
options: >-
  --health-cmd pg_isready
  --health-interval 10s
  --health-timeout 5s
  --health-retries 5
```

**Connection from your code:**
- Use `localhost` and the mapped port
- Pass as environment variable
- Service is destroyed after job completes