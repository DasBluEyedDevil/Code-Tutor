---
type: "KEY_POINT"
title: "Railway Basics"
---

Railway is a modern PaaS that makes deployment simple:

KEY FEATURES:
- Deploy from GitHub with auto-deploy on push
- Built-in PostgreSQL, Redis, MySQL
- Environment variables management
- Automatic HTTPS certificates
- Logs and metrics
- Private networking between services

PRICING (as of 2025):
- Free tier: $5/month credit (enough for small apps)
- Hobby: $5/month per service
- Pro: Usage-based pricing

DEPLOYMENT METHODS:

1. GitHub Integration (recommended):
   - Connect GitHub repo
   - Auto-deploy on push to main
   - Automatic rollback on failure

2. Railway CLI:
   - railway login
   - railway up
   - Good for CI/CD pipelines

3. Docker Image:
   - Push to container registry
   - Railway pulls and runs

RAILWAY DETECTS:
- Dockerfile (uses it)
- pom.xml (runs Maven build)
- package.json (runs npm)
- Go, Python, Ruby, etc.