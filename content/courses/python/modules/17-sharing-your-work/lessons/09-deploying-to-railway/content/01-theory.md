---
type: "THEORY"
title: "What is Railway?"
---

**Railway = Developer-focused PaaS with predictable pricing**

Railway is designed for developers who want simplicity without sacrificing power:

**Why choose Railway?**

1. **Developer experience**
   - Instant deploys from GitHub
   - Excellent CLI tool
   - Real-time logs and metrics
   - One-click database provisioning

2. **Predictable pricing**
   - Pay only for resources used
   - $5 free trial credit monthly (hobby)
   - No surprise bills
   - Usage-based, not tier-based

3. **Built-in services**
   - PostgreSQL, MySQL, Redis, MongoDB
   - One-click provisioning
   - Automatic backups
   - Private networking

4. **Flexible configuration**
   - Auto-detect or Dockerfile
   - railway.toml for customization
   - Environment groups
   - Preview environments

**Railway workflow:**
```
Code Push → Auto Build → Deploy → Monitor
    ↓
Pull Request → Preview Environment
```

**For our Personal Finance Tracker:**
- FastAPI app deployed via Railway CLI or GitHub
- PostgreSQL plugin for database
- Environment variables in Railway dashboard
- Automatic HTTPS and custom domains