---
type: "EXAMPLE"
title: "GitHub Integration Setup"
---

**Deploy automatically on every push:**

```bash
# Step 1: Go to railway.app and create new project
# Click "New Project" > "Deploy from GitHub repo"

# Step 2: Select your repository
# Railway will auto-detect Python/Docker and configure build

# Step 3: Add PostgreSQL
# In project dashboard: "New" > "Database" > "PostgreSQL"
# DATABASE_URL is automatically added to your service

# Step 4: Add environment variables
# Go to your service > "Variables" tab
# Add:
#   SECRET_KEY = <generate a secure key>
#   ENVIRONMENT = production
#   DEBUG = false

# Step 5: Configure domain
# Service > "Settings" > "Generate Domain"
# Your app is at: https://your-app.up.railway.app

# Optional: railway.json for project settings
{
  "$schema": "https://railway.app/railway.schema.json",
  "build": {
    "builder": "DOCKERFILE",
    "dockerfilePath": "Dockerfile"
  },
  "deploy": {
    "restartPolicyType": "ON_FAILURE",
    "restartPolicyMaxRetries": 3
  }
}
```
