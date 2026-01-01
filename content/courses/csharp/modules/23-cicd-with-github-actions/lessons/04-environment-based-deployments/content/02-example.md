---
type: "EXAMPLE"
title: "Environment Configuration in GitHub"
---

GitHub Environments provide deployment targets with protection rules, secrets, and approval workflows. This configuration creates development, staging, and production environments with appropriate safeguards.

```yaml
# ===== GitHub Repository Settings =====
# Navigate to: Settings > Environments

# Create these environments in the GitHub UI:

# ========== DEVELOPMENT ENVIRONMENT ==========
# Name: development
# Protection rules: None (fast iteration)
# Secrets:
#   - AZURE_CLIENT_ID: (dev subscription)
#   - DATABASE_URL: postgres://dev-server/shopflow_dev
#   - REDIS_URL: redis://dev-cache:6379

# ========== STAGING ENVIRONMENT ==========  
# Name: staging
# Protection rules:
#   - Required reviewers: (empty - automated)
#   - Wait timer: 0 minutes
#   - Deployment branches: main, release/*
# Secrets:
#   - AZURE_CLIENT_ID: (staging subscription)
#   - DATABASE_URL: postgres://staging-server/shopflow_staging
#   - REDIS_URL: redis://staging-cache:6379

# ========== PRODUCTION ENVIRONMENT ==========
# Name: production  
# Protection rules:
#   - Required reviewers: @shopflow/platform-team
#   - Wait timer: 15 minutes (for metrics observation)
#   - Deployment branches: main
#   - Custom rules: Must pass staging first
# Secrets:
#   - AZURE_CLIENT_ID: (production subscription)
#   - DATABASE_URL: (stored in Key Vault, not here)
#   - REDIS_URL: (stored in Key Vault, not here)

# ===== .github/workflows/deploy.yml =====
# Workflow using these environments

name: Deploy ShopFlow

on:
  push:
    branches: [main]
  workflow_dispatch:
    inputs:
      environment:
        description: 'Target environment'
        required: true
        default: 'staging'
        type: choice
        options:
          - development
          - staging
          - production

jobs:
  deploy-dev:
    name: Deploy to Development
    runs-on: ubuntu-latest
    if: github.event_name == 'push' || github.event.inputs.environment == 'development'
    environment:
      name: development
      url: https://dev.shopflow.example.com
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Log in to Azure
        uses: azure/login@v2
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      
      - name: Deploy to Container Apps
        uses: azure/container-apps-deploy-action@v1
        with:
          containerAppName: shopflow-api-dev
          resourceGroup: shopflow-dev-rg
          imageToDeploy: ghcr.io/shopflow/api:${{ github.sha }}

  deploy-staging:
    name: Deploy to Staging
    runs-on: ubuntu-latest
    needs: deploy-dev
    if: github.ref == 'refs/heads/main'
    environment:
      name: staging
      url: https://staging.shopflow.example.com
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Log in to Azure
        uses: azure/login@v2
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      
      - name: Deploy to Container Apps
        uses: azure/container-apps-deploy-action@v1
        with:
          containerAppName: shopflow-api-staging
          resourceGroup: shopflow-staging-rg
          imageToDeploy: ghcr.io/shopflow/api:${{ github.sha }}
      
      - name: Run smoke tests
        run: |
          # Wait for deployment to stabilize
          sleep 30
          
          # Run smoke tests against staging
          curl -f https://staging.shopflow.example.com/healthz/ready
          curl -f https://staging.shopflow.example.com/api/products | jq '.items | length > 0'

  deploy-production:
    name: Deploy to Production
    runs-on: ubuntu-latest
    needs: deploy-staging
    if: github.ref == 'refs/heads/main'
    environment:
      name: production
      url: https://shopflow.example.com
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Log in to Azure
        uses: azure/login@v2
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      
      # Blue-green deployment with traffic shifting
      - name: Deploy new revision
        uses: azure/container-apps-deploy-action@v1
        with:
          containerAppName: shopflow-api-prod
          resourceGroup: shopflow-prod-rg
          imageToDeploy: ghcr.io/shopflow/api:${{ github.sha }}
      
      - name: Shift traffic gradually
        run: |
          # Start with 10% traffic to new revision
          az containerapp ingress traffic set \
            --name shopflow-api-prod \
            --resource-group shopflow-prod-rg \
            --revision-weight latest=10
          
          # Wait and monitor
          sleep 60
          
          # Increase to 50%
          az containerapp ingress traffic set \
            --name shopflow-api-prod \
            --resource-group shopflow-prod-rg \
            --revision-weight latest=50
          
          # Wait and monitor
          sleep 60
          
          # Full rollout
          az containerapp ingress traffic set \
            --name shopflow-api-prod \
            --resource-group shopflow-prod-rg \
            --revision-weight latest=100
```
