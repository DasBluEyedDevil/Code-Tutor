---
type: "WARNING"
title: "Common Fly.io Deployment Issues"
---

### Watch out for these common problems:

**1. Health check failures**
```toml
# WRONG - Health check times out during startup
[[http_service.checks]]
  grace_period = "5s"  # Too short for Python startup

# CORRECT - Give app time to start
[[http_service.checks]]
  grace_period = "30s"  # Longer grace period
  interval = "30s"
  timeout = "10s"
```

**2. Port mismatch**
```toml
# fly.toml says internal_port = 8000
[http_service]
  internal_port = 8000

# But Dockerfile uses 8080 - MISMATCH!
# Ensure your app listens on the same port
```

**3. Database connection string format**
```python
# Fly uses postgres://, asyncpg needs postgresql+asyncpg://
url = os.environ["DATABASE_URL"]
if url.startswith("postgres://"):
    url = url.replace("postgres://", "postgresql+asyncpg://", 1)
```

**4. Machines stopping unexpectedly**
```toml
# Free tier machines stop when idle
# Keep at least one running:
[http_service]
  min_machines_running = 1
  auto_stop_machines = false  # Or disable auto-stop
```

**5. Running out of free tier**
```
# Free tier limits:
# - 3 shared-cpu-1x VMs
# - 256MB memory each
# - 1GB persistent storage

# Check usage:
fly billing

# Reduce costs:
fly scale memory 256  # Minimum memory
fly scale count 1     # Single instance
```

**6. Secrets not available during build**
```dockerfile
# WRONG - Trying to use SECRET_KEY during build
RUN python setup.py --secret=$SECRET_KEY

# CORRECT - Secrets are only available at runtime
# Use build args for build-time values:
# fly deploy --build-arg MY_ARG=value
```