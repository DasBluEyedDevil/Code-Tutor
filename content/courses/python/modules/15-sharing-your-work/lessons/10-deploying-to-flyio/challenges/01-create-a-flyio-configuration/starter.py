# fly.toml

app = "my-fastapi-app"
primary_region = "____"

[build]
  dockerfile = "____"

[http_service]
  internal_port = ____
  force_https = ____
  auto_stop_machines = true
  auto_start_machines = true
  min_machines_running = ____

[[http_service.checks]]
  method = "GET"
  path = "____"
  grace_period = "____"
  interval = "30s"
  timeout = "5s"

[[vm]]
  size = "____"
  memory = "____"