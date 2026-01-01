# fly.toml - Fly.io configuration for Serverpod

app = "___"
primary_region = "___"

[build]
  dockerfile = "Dockerfile"

[env]
  SERVERPOD_ENV = "production"
  PORT = "8080"

# TODO: Configure HTTP service
[http_service]
  internal_port = ___
  force_https = ___
  auto_stop_machines = true
  auto_start_machines = true
  min_machines_running = ___

# TODO: Add health check
[[services.http_checks]]
  interval = ___
  grace_period = "5s"
  method = "get"
  path = "___"
  protocol = "http"
  timeout = 2000

# TODO: Configure VM resources
[[vm]]
  cpu_kind = "___"
  cpus = ___
  memory_mb = ___