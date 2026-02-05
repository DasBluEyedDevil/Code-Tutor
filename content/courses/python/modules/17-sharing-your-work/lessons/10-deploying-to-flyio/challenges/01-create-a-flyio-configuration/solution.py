# Fly.io Configuration Solution
# This file outputs the fly.toml content that students should create

FLY_TOML = """app = "my-fastapi-app"
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
"""

print("=== Fly.io Configuration (fly.toml) ===")
print()
print("Save this content to 'fly.toml' in your project root:")
print()
print(FLY_TOML)
print()
print("=== Key Features ===")
print("1. Docker-based deployment")
print("2. Auto-start/stop machines (cost-effective)")
print("3. HTTPS enforced")
print("4. Health check on /health endpoint")
print("5. Minimum 1 machine always running")
print()
print("=== Deployment Steps ===")
print("1. Install flyctl: curl -L https://fly.io/install.sh | sh")
print("2. Login: fly auth login")
print("3. Launch: fly launch --copy-config")
print("4. Deploy: fly deploy")
print("5. Open: fly open")
