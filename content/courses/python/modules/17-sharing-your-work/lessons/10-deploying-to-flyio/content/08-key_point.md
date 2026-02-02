---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Fly.io uses VMs** (Firecracker), not containers - more control and persistence
- **`fly launch`** creates app and generates fly.toml configuration
- **`fly postgres create/attach`** provisions and connects databases
- **`fly secrets set`** manages encrypted environment variables
- **Multi-region deployment** reduces latency for global users
- **Auto-stop/auto-start** manages costs for low-traffic apps
- **fly.toml** controls build, deploy, scaling, and health checks
- **Grace period** in health checks - give Python apps time to start