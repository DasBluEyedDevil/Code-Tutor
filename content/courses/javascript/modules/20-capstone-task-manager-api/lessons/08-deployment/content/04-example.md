---
type: "EXAMPLE"
title: "Deploy to Fly.io (Alternative)"
---

Alternatively, deploy to Fly.io with their serverless platform:

Create a fly.toml configuration file and deploy:

```toml
# fly.toml
app = "task-manager-api"
kill_signal = "SIGINT"
kill_timeout = 5
processes = []

[env]
  NODE_ENV = "production"

[build]
  builder = "patak/buildpacks"
  buildpacks = ["paketo-buildpacks/nodejs-engine"]

[build.args]
  BP_NODE_RUN_SCRIPT = "src/index.ts"
  BP_BUILT_MODULE = "src"

[[services]]
  internal_port = 3000
  processes = ["app"]

  [services.http_checks]
    enabled = true
    uri = "/health"

[[services.ports]]
  port = 80
  handlers = ["http"]
  force_https = true

[[services.ports]]
  port = 443
  handlers = ["tls", "http"]

[checks]
  [checks.status]
    type = "http"
    uri = "/health"
    interval = "30s"
    timeout = "10s"
    grace_period = "5s"
    success_threshold = 1
    failure_threshold = 3
```
