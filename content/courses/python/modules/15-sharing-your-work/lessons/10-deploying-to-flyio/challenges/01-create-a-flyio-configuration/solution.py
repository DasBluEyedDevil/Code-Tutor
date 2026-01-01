# fly.toml

app = "my-fastapi-app"
primary_region = "ord"

[build]
  dockerfile = "Dockerfile"

[http_service]
  internal_port = 8000
  force_https = true
  auto_stop_machines = true
  auto_start_machines = true
  min_machines_running = 1

[[http_service.checks]]
  method = "GET"
  path = "/health"
  grace_period = "30s"
  interval = "30s"
  timeout = "5s"

[[vm]]
  size = "shared-cpu-1x"
  memory = "512mb"