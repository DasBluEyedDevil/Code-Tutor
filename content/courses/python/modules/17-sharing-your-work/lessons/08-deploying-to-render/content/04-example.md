---
type: "EXAMPLE"
title: "Deploying to Render"
---

**Step-by-step deployment process:**

```bash
# Step 1: Ensure you have these files in your repo
# - Dockerfile (from previous lesson)
# - render.yaml (blueprint)
# - pyproject.toml / uv.lock
# - src/ directory with your FastAPI app

# Step 2: Push to GitHub
git add .
git commit -m "Add Render deployment configuration"
git push origin main

# Step 3: Connect Render to GitHub
# 1. Go to https://render.com
# 2. Sign up / Log in
# 3. Click "New +" > "Blueprint"
# 4. Connect your GitHub repository
# 5. Render detects render.yaml automatically

# Step 4: Deploy via Render Dashboard
# 1. Click "Apply" to create services from blueprint
# 2. Wait for build and deploy (5-10 minutes first time)
# 3. Your app is live at https://finance-api.onrender.com

# Step 5: Verify deployment
curl https://finance-api.onrender.com/health
# Expected: {"status": "healthy"}

# Useful Render CLI commands (optional)
# Install: npm install -g @render-cli/cli
render login
render services list
render logs --service finance-api --tail
```
