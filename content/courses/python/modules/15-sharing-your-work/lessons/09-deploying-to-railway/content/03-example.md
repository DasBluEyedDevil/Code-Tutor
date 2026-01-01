---
type: "EXAMPLE"
title: "Railway Configuration File"
---

**railway.toml = Fine-tune your deployment**

While Railway auto-detects most settings, railway.toml gives you control:

```toml
# railway.toml - Railway Configuration
# Place in repository root

[build]
# Use Dockerfile for builds
builder = "dockerfile"
dockerfilePath = "./Dockerfile"

# Or use Nixpacks (Railway's buildpack system)
# builder = "nixpacks"

[deploy]
# Number of instances (default: 1)
replicas = 1

# Health check configuration
healthcheckPath = "/health"
healthcheckTimeout = 300

# Restart policy
restartPolicyType = "on_failure"
restartPolicyMaxRetries = 3

# Start command (overrides Dockerfile CMD)
# startCommand = "uvicorn src.main:app --host 0.0.0.0 --port $PORT"

[deploy.resources]
# Memory limit (optional, for resource control)
# memory = "512MB"
# cpu = "0.5"
```
