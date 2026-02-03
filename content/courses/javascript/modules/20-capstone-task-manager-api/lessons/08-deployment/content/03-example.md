---
type: "EXAMPLE"
title: "Deploy to Railway"
---

Railway is a modern hosting platform perfect for Node/Bun apps. Set up deployment:

1. Install Railway CLI and connect your GitHub repo
2. Set up your production database
3. Configure environment variables

```bash
# Install Railway CLI
npm install -g @railway/cli

# Login to Railway
railway login

# Link to your project
railway init

# Set environment variables
railway variables set JWT_SECRET="your-production-secret-key-32+chars"
railway variables set NODE_ENV=production
railway variables set LOG_LEVEL=warn

# Deploy
railway up

# View logs
railway logs

# View your deployed app
railway open
```
