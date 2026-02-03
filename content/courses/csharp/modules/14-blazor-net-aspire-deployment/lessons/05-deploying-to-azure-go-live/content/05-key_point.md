---
type: "KEY_POINT"
title: "Azure Deployment Basics"
---

## Key Takeaways

- **Use Azure App Service for simple deployments** -- `az webapp up` deploys directly from your project folder. Choose B1 tier for production, F1 for free testing.

- **Store secrets in Azure Configuration, not code** -- connection strings and API keys go in the Azure portal's Configuration section, accessed via environment variables. Azure encrypts them at rest.

- **Group related resources** -- use a Resource Group to contain your app, database, and storage together. Deleting the group removes everything, making cleanup easy for development environments.
