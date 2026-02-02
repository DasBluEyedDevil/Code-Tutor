---
type: "THEORY"
title: "What is Render?"
---

**Render = Modern PaaS with excellent free tier**

Render is a modern Platform-as-a-Service (PaaS) that makes deployment simple while offering powerful features:

**Why choose Render?**

1. **Generous free tier**
   - Free web services (with some limits)
   - Free PostgreSQL database (90 days)
   - Free Redis cache
   - Great for learning and side projects

2. **Simple deployment**
   - Connect GitHub/GitLab
   - Automatic deployments on push
   - Zero-downtime deploys

3. **Infrastructure as Code**
   - `render.yaml` blueprint defines everything
   - Version-controlled infrastructure
   - Reproducible environments

4. **Native Docker support**
   - Use your Dockerfile
   - Or let Render detect your app
   - No Docker knowledge required

**Render vs other platforms:**

| Feature | Render | Railway | Fly.io |
|---------|--------|---------|--------|
| Free tier | Yes | Limited | Yes |
| PostgreSQL | Free 90 days | Add-on | Fly Postgres |
| Auto-deploy | Yes | Yes | Optional |
| Docker support | Yes | Yes | Yes |
| Blueprint file | render.yaml | railway.toml | fly.toml |

**For our Personal Finance Tracker:**
- Web service running FastAPI
- PostgreSQL database for data
- Automatic SSL/HTTPS
- Environment variables for secrets