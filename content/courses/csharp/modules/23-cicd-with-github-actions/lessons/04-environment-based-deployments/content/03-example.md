---
type: "EXAMPLE"
title: "Deploying to Multiple Environments"
---

This workflow uses a matrix strategy to deploy the same application to multiple environments with environment-specific configurations.

```yaml
# ===== .github/workflows/matrix-deploy.yml =====
# Deploy to multiple environments using matrix strategy

name: Multi-Environment Deployment

on:
  workflow_dispatch:
    inputs:
      deploy_prod:
        description: 'Include production deployment'
        required: false
        default: false
        type: boolean

env:
  REGISTRY: ghcr.io
  IMAGE_NAME: shopflow/api

jobs:
  # Build once, deploy everywhere
  build:
    name: Build Image
    runs-on: ubuntu-latest
    outputs:
      image-tag: ${{ steps.meta.outputs.tags }}
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Build and push
        uses: docker/build-push-action@v5
        with:
          push: true
          tags: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}:${{ github.sha }}

  # Matrix deployment to non-production environments
  deploy-matrix:
    name: Deploy to ${{ matrix.environment }}
    needs: build
    runs-on: ubuntu-latest
    strategy:
      matrix:
        environment: [development, staging]
        include:
          - environment: development
            resource-group: shopflow-dev-rg
            container-app: shopflow-api-dev
            url: https://dev.shopflow.example.com
          - environment: staging
            resource-group: shopflow-staging-rg
            container-app: shopflow-api-staging
            url: https://staging.shopflow.example.com
      fail-fast: false    # Continue other deployments if one fails
    
    environment:
      name: ${{ matrix.environment }}
      url: ${{ matrix.url }}
    
    steps:
      - name: Log in to Azure
        uses: azure/login@v2
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      
      - name: Deploy to ${{ matrix.environment }}
        run: |
          az containerapp update \
            --name ${{ matrix.container-app }} \
            --resource-group ${{ matrix.resource-group }} \
            --image ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}:${{ github.sha }}
      
      - name: Verify deployment
        run: |
          for i in {1..30}; do
            if curl -sf ${{ matrix.url }}/healthz/ready; then
              echo "Deployment healthy!"
              exit 0
            fi
            echo "Waiting for deployment... ($i/30)"
            sleep 10
          done
          echo "Deployment health check failed"
          exit 1

  # Production deployment (separate job with approval)
  deploy-production:
    name: Deploy to Production
    needs: [build, deploy-matrix]
    runs-on: ubuntu-latest
    if: github.event.inputs.deploy_prod == 'true'
    environment:
      name: production
      url: https://shopflow.example.com
    
    steps:
      - name: Log in to Azure
        uses: azure/login@v2
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      
      - name: Create deployment record
        run: |
          echo "Deploying ${{ github.sha }} to production"
          echo "Triggered by: ${{ github.actor }}"
          echo "Timestamp: $(date -u +%Y-%m-%dT%H:%M:%SZ)"
      
      - name: Deploy with blue-green strategy
        run: |
          # Create new revision without traffic
          az containerapp revision copy \
            --name shopflow-api-prod \
            --resource-group shopflow-prod-rg \
            --image ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}:${{ github.sha }} \
            --revision-suffix ${{ github.sha }}
          
          # Gradually shift traffic
          for pct in 10 25 50 75 100; do
            echo "Shifting $pct% traffic to new revision"
            az containerapp ingress traffic set \
              --name shopflow-api-prod \
              --resource-group shopflow-prod-rg \
              --revision-weight shopflow-api-prod--${{ github.sha }}=$pct
            
            if [ $pct -lt 100 ]; then
              echo "Waiting 60s before next increment..."
              sleep 60
            fi
          done
      
      - name: Verify production deployment
        run: |
          curl -sf https://shopflow.example.com/healthz/ready
          echo "Production deployment successful!"
```
