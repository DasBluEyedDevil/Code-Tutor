---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`.WithExternalHttpEndpoints()`**: Marks a service as internet-facing. Azure Container Apps will create a public URL. Without this, service is internal only.

**`azd init`**: Creates azure.yaml and .azure/ folder. Scans your solution, detects Aspire AppHost, generates deployment config. Run once per project.

**`azd up`**: The magic command! Provisions Azure resources, builds containers, deploys everything. First run creates resources, subsequent runs update.

**`azure.yaml`**: Deployment descriptor. Points to AppHost project. azd reads this to understand your app structure.

**`azd env`**: Manage multiple environments (dev, staging, prod). Each environment has separate Azure resources. Switch with 'azd env select'.

**`azd down`**: Cleanup! Deletes ALL Azure resources for the environment. Use carefully - data is lost! Great for dev environments to save costs.