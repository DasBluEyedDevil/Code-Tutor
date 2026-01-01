# fly.toml - Fly.io configuration for Serverpod

app = "my-flutter-backend"
primary_region = "sjc"

[build]
  dockerfile = "Dockerfile"

[env]
  SERVERPOD_ENV = "production"
  PORT = "8080"

[http_service]
  internal_port = 8080
  force_https = true
  auto_stop_machines = true
  auto_start_machines = true
  min_machines_running = 1

[[services.http_checks]]
  interval = 15000
  grace_period = "5s"
  method = "get"
  path = "/health"
  protocol = "http"
  timeout = 2000

[[vm]]
  cpu_kind = "shared"
  cpus = 1
  memory_mb = 512