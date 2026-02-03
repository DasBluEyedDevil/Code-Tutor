---
type: "EXAMPLE"
title: "Deploy to Fly.io (Alternative)"
---

Alternatively, deploy to Fly.io with their serverless platform:

Create a fly.toml configuration file and deploy:

```toml
# fly.toml
app = "task-manager-api"
primary_region = "iad"

[env]
  NODE_ENV = "production"

[build]
  # Uses the Dockerfile in the project root
  dockerfile = "Dockerfile"

[http_service]
  internal_port = 3000
  force_https = true
  auto_stop_machines = "stop"
  auto_start_machines = true
  min_machines_running = 0

  [http_service.checks]
    [http_service.checks.status]
      interval = "30s"
      timeout = "10s"
      grace_period = "5s"
      path = "/health/live"
```

```bash
# Deploy with Fly.io CLI
flyctl launch              # First-time setup (creates app + fly.toml)
flyctl secrets set JWT_SECRET="your-production-secret-key"
flyctl secrets set DATABASE_URL="your-production-db-url"
flyctl deploy              # Build and deploy
flyctl logs                # View logs
flyctl status              # Check deployment status
```
