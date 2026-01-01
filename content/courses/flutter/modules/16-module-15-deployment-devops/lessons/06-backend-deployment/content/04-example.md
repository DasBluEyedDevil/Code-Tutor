---
type: "EXAMPLE"
title: "Fly.io Configuration"
---


Deploying a Serverpod backend to Fly.io with multi-region support:



```toml
## Step 1: Install Fly CLI and Login

# macOS
brew install flyctl

# Windows
scoop install flyctl

# Login
fly auth login

## Step 2: Create fly.toml

# In my_app_server/fly.toml
app = "my-serverpod-app"
primary_region = "iad"  # US East

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
  
  [http_service.concurrency]
    type = "requests"
    hard_limit = 250
    soft_limit = 200

[[services]]
  protocol = "tcp"
  internal_port = 8080

  [[services.ports]]
    port = 80
    handlers = ["http"]

  [[services.ports]]
    port = 443
    handlers = ["tls", "http"]

  [[services.http_checks]]
    interval = 10000
    grace_period = "5s"
    method = "get"
    path = "/health"
    protocol = "http"
    timeout = 2000

[[vm]]
  cpu_kind = "shared"
  cpus = 1
  memory_mb = 512

## Step 3: Launch and Configure

# Initialize the app
fly launch --no-deploy

# Create PostgreSQL database
fly postgres create --name my-app-db --region iad

# Attach database to app
fly postgres attach my-app-db

# Set secrets
fly secrets set \
  DATABASE_PASSWORD="your-secure-password" \
  SERVERPOD_PASSWORD="your-serverpod-password"

## Step 4: Deploy

fly deploy

# Check status
fly status
fly logs

## Step 5: Scale to Multiple Regions

# Add machines in other regions
fly scale count 2 --region lhr  # London
fly scale count 2 --region sin  # Singapore

# Scale memory/CPU
fly scale memory 1024
fly scale vm shared-cpu-2x

## Health Check Endpoint

# Add to your Serverpod endpoints
class HealthEndpoint extends Endpoint {
  Future<String> check(Session session) async {
    // Verify database connection
    await session.db.query('SELECT 1');
    return 'ok';
  }
}
```
