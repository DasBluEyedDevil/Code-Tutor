---
type: "EXAMPLE"
title: "Scaling and Multi-Region Deployment"
---

**Deploy globally for low latency worldwide:**

```bash
# Add regions (deploy to multiple locations)
fly regions add lhr sin syd
# lhr = London, sin = Singapore, syd = Sydney

# List current regions
fly regions list

# Scale machines per region
fly scale count 2 --region ord
fly scale count 1 --region lhr
fly scale count 1 --region sin

# Scale memory/CPU
fly scale memory 512  # 512MB RAM
fly scale vm shared-cpu-2x  # More CPU

# View machine status
fly status
fly machines list

# Auto-scaling configuration in fly.toml
[http_service]
  auto_stop_machines = true   # Stop when idle
  auto_start_machines = true  # Start on request
  min_machines_running = 1    # Always keep 1 running
  max_machines_running = 5    # Scale up to 5

# Useful monitoring commands
fly logs                # Live log streaming
fly logs --app finance-db  # Database logs
fly dashboard           # Open web dashboard
fly ssh console         # SSH into running machine
```
