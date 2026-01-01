---
type: "WARNING"
title: "Important Notes"
---

## .NET Aspire Updates (2025)

**Current Version: 9.5** - Key features:
- Self-contained SDK (no workload installation needed)
- Single-file AppHost option
- GenAI Visualizer for AI/LLM debugging
- Mobile-responsive dashboard
- Start/stop individual resources
- Azure Functions preview support
- AWS CDK integration

**Aspire CLI** (GA in 9.4+):
```bash
aspire new starter --name MyApp
aspire run
aspire deploy
```

**Dashboard URL:** http://localhost:15888 (default)

**Production Deployment:**
- Use `azd init` + `azd up` for Azure Container Apps
- Replace AddRedis() with AddAzureRedis() for cloud
- Aspire generates deployment manifests automatically