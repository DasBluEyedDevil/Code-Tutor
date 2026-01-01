---
type: "ANALOGY"
title: "Understanding the Concept"
---

You've built your orchestra (Aspire app) and rehearsed locally. Now it's time for the REAL CONCERT - production deployment!

LOCAL DEVELOPMENT:
- Containers run on your machine
- Dashboard at localhost:18888
- Redis/Postgres in Docker

PRODUCTION (Azure Container Apps):
- Containers run in Azure's cloud
- Auto-scaling based on load
- Managed Redis (Azure Cache)
- Managed Postgres (Azure Database)
- Built-in HTTPS, load balancing

AZURE CONTAINER APPS (ACA):
- Serverless containers - pay for what you use
- Automatic scaling (0 to many instances)
- Built-in service discovery (works with Aspire!)
- No Kubernetes complexity

DEPLOYMENT OPTIONS:
1. Azure Developer CLI (azd) - Recommended!
2. Visual Studio Publish
3. CI/CD pipelines (GitHub Actions)

ASPIRE + ACA = Perfect Match:
- Aspire manifest describes your app
- azd reads manifest, creates Azure resources
- Connection strings auto-configured
- Same code, different environment!

Think: 'Aspire is the blueprint, Azure Container Apps is the construction site!'