---
type: "EXAMPLE"
title: "GitHub Actions + Railway"
---

Automated deployment from GitHub Actions:

```yaml
# .github/workflows/deploy.yml
name: Deploy to Railway

on:
  push:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - uses: actions/setup-java@v4
        with:
          java-version: '21'
          distribution: 'temurin'
          cache: 'maven'
      
      - name: Run tests
        run: ./mvnw verify

  deploy:
    needs: test
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Install Railway CLI
        run: npm install -g @railway/cli
      
      - name: Deploy to Railway
        run: railway up --service ${{ vars.RAILWAY_SERVICE_ID }}
        env:
          RAILWAY_TOKEN: ${{ secrets.RAILWAY_TOKEN }}

# How to get RAILWAY_TOKEN:
# 1. Go to railway.app/account/tokens
# 2. Generate a new token
# 3. Add to GitHub repo secrets as RAILWAY_TOKEN

# How to get RAILWAY_SERVICE_ID:
# 1. In Railway dashboard, click your service
# 2. Go to Settings
# 3. Copy the Service ID
# 4. Add to GitHub repo variables as RAILWAY_SERVICE_ID
```
