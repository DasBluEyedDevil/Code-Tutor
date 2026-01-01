---
type: "THEORY"
title: "Railway Deployment Walkthrough"
---

Railway is a modern platform that makes deploying full-stack applications simple. Here is a step-by-step guide to deploying our Task Manager.

Step 1: Create Railway Account
- Go to https://railway.app
- Sign up with GitHub (recommended for easy integration)

Step 2: Create New Project
```bash
# Install Railway CLI
npm install -g @railway/cli

# Login
railway login

# Create new project
railway init
```

Step 3: Add PostgreSQL Database
- In Railway dashboard, click "+ New"
- Select "Database" then "PostgreSQL"
- Railway automatically provisions the database
- Copy the DATABASE_URL from the Variables tab

Step 4: Configure Backend Service
- Click "+ New" then "GitHub Repo"
- Select your repository
- Railway detects Dockerfile and builds automatically

Add environment variables in Railway dashboard:
```
DATABASE_URL=${{Postgres.DATABASE_URL}}
JWT_SECRET=generate-a-secure-random-string
JWT_EXPIRATION=86400000
CORS_ALLOWED_ORIGINS=https://your-frontend.up.railway.app
```

Step 5: Configure railway.json (optional, for explicit config):
```json
{
  "$schema": "https://railway.app/railway.schema.json",
  "build": {
    "builder": "DOCKERFILE",
    "dockerfilePath": "Dockerfile"
  },
  "deploy": {
    "healthcheckPath": "/actuator/health",
    "healthcheckTimeout": 100,
    "restartPolicyType": "ON_FAILURE",
    "restartPolicyMaxRetries": 3
  }
}
```

Step 6: Deploy Frontend
- Create another service in the same project
- Point to frontend directory or separate repo
- Add VITE_API_URL pointing to your backend URL

Step 7: Set Up Custom Domain (Optional)
- Go to Settings > Domains
- Add your custom domain
- Configure DNS with CNAME record pointing to Railway

Step 8: Monitor Deployment
```bash
# View logs
railway logs

# Check status
railway status

# Redeploy
railway up
```

Railway Dashboard Features:
- Real-time logs and metrics
- Automatic SSL certificates
- Environment variable management
- Usage analytics and cost tracking
- One-click rollbacks

Monthly costs (as of 2024):
- Hobby plan: $5/month with $5 credit (enough for small apps)
- Pro plan: Pay for usage (starts around $20/month for active apps)