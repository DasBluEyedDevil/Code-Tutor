---
type: "WARNING"
title: "Modern Deployment with Azure Developer CLI"
---

## Azure Developer CLI (azd) - The Modern Way

**Why azd?** One command to provision AND deploy!

```bash
# Install azd
winget install microsoft.azd

# Initialize project (creates azure.yaml)
azd init

# Login to Azure
azd auth login

# Deploy everything!
azd up
```

**azd + .NET Aspire** (best combo!):
- `azd init` detects Aspire AppHost automatically
- Creates Azure Container Apps infrastructure
- Deploys all services with one command
- Sets up networking, secrets, monitoring

**Key azd commands:**
- `azd up` = provision + deploy (full deployment)
- `azd deploy` = deploy only (after infrastructure exists)
- `azd down` = tear down all resources
- `azd monitor` = open Application Insights

**Container Apps vs App Service:**
- Container Apps: Better for microservices, Aspire, containers
- App Service: Simpler, traditional web apps
- azd supports both!