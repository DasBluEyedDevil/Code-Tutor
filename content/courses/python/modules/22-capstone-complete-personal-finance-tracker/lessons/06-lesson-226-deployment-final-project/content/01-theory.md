---
type: "THEORY"
title: "Deploying the Finance Tracker"
---

**Deployment Options:**

| Platform | Best For | Estimated Cost |
|----------|----------|----------------|
| Railway | Quick deployment | $5-20/month |
| Render | Free tier available | Free-$25/month |
| Fly.io | Global distribution | $5-15/month |
| DigitalOcean | Full control | $5-25/month |
| AWS/GCP/Azure | Enterprise scale | Variable |

**Our Stack:**
- **API**: FastAPI on Uvicorn
- **Database**: PostgreSQL (managed service)
- **Container**: Docker

**What we'll cover:**
1. Dockerfile for the application
2. Docker Compose for local development
3. GitHub Actions for CI/CD
4. Environment configuration for production