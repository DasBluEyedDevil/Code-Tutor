---
type: "THEORY"
title: "Understanding render.yaml Structure"
---

**Let's break down each section:**

**Services section:**
```yaml
services:
  - type: web       # Web service (receives HTTP traffic)
    name: finance-api  # Service name in dashboard
    runtime: docker    # Use Dockerfile for builds
```

**Supported types:**
- `web` - HTTP services (FastAPI, Flask, Django)
- `worker` - Background workers (Celery, RQ)
- `cron` - Scheduled jobs
- `static` - Static websites

**Environment variables:**
```yaml
envVars:
  # Reference database connection
  - key: DATABASE_URL
    fromDatabase:
      name: finance-db
      property: connectionString
  
  # Auto-generate secure value
  - key: SECRET_KEY
    generateValue: true
  
  # Static value
  - key: DEBUG
    value: "false"
```

**Database section:**
```yaml
databases:
  - name: finance-db
    plan: free       # free, basic, standard, etc.
    databaseName: finance_tracker
    user: finance_user
```

**Important:** The `fromDatabase` reference automatically creates the DATABASE_URL when the database is provisioned.