---
type: "KEY_POINT"
title: "Deploying Aspire to Azure"
---

## Key Takeaways

- **`azd up` deploys everything** -- Azure Developer CLI provisions resources, builds containers, and deploys all services defined in your Aspire AppHost. First run creates; subsequent runs update.

- **`.WithExternalHttpEndpoints()` makes services public** -- without this, services are internal-only within the container environment. Add it to the API or frontend project that needs a public URL.

- **`azd down` cleans up completely** -- deletes all Azure resources for the environment. Use it for development environments to avoid ongoing costs. Be careful with production environments.
