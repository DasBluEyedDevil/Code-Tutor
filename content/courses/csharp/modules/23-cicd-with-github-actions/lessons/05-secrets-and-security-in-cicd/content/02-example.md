---
type: "EXAMPLE"
title: "Managing Secrets in GitHub Actions"
---

This example demonstrates secure secret management using repository secrets, environment secrets, and OIDC authentication for cloud providers. No long-lived credentials are stored in the repository.

```yaml
# ===== .github/workflows/secure-deploy.yml =====
# Demonstrates secure secret management patterns

name: Secure Deployment

on:
  push:
    branches: [main]

# OIDC: Request token for cloud authentication
permissions:
  id-token: write    # Required for OIDC
  contents: read
  packages: write

env:
  # Non-secret configuration (safe to commit)
  AZURE_RESOURCE_GROUP: shopflow-prod-rg
  AZURE_CONTAINER_APP: shopflow-api
  AZURE_REGION: eastus

jobs:
  deploy:
    name: Deploy with OIDC
    runs-on: ubuntu-latest
    environment: production
    
    steps:
      - uses: actions/checkout@v4
      
      # ==============================================
      # OIDC Authentication (no stored credentials!)
      # ==============================================
      # Azure login using OpenID Connect - no secrets stored in GitHub!
      # Configure: Azure AD App Registration with federated credentials
      - name: Azure Login with OIDC
        uses: azure/login@v2
        with:
          # These are identifiers, not secrets
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
          # OIDC means no client secret needed!
      
      # ==============================================
      # Repository Secrets (available to all workflows)
      # ==============================================
      - name: Build with registry credentials
        run: |
          # GITHUB_TOKEN is automatically available
          echo ${{ secrets.GITHUB_TOKEN }} | docker login ghcr.io -u ${{ github.actor }} --password-stdin
          
          docker build -t ghcr.io/${{ github.repository }}:${{ github.sha }} .
          docker push ghcr.io/${{ github.repository }}:${{ github.sha }}
      
      # ==============================================
      # Environment Secrets (scoped to environment)
      # ==============================================
      - name: Deploy to Azure Container Apps
        run: |
          # DATABASE_URL is only available in 'production' environment
          # It's automatically injected because this job uses 'environment: production'
          
          az containerapp update \
            --name ${{ env.AZURE_CONTAINER_APP }} \
            --resource-group ${{ env.AZURE_RESOURCE_GROUP }} \
            --image ghcr.io/${{ github.repository }}:${{ github.sha }} \
            --set-env-vars \
              "ConnectionStrings__DefaultConnection=secretref:database-url" \
              "ConnectionStrings__Redis=secretref:redis-url"
      
      # ==============================================
      # Secrets from Key Vault (pulled at runtime)
      # ==============================================
      - name: Configure from Key Vault
        run: |
          # Fetch secrets from Key Vault (already authenticated via OIDC)
          STRIPE_KEY=$(az keyvault secret show \
            --vault-name shopflow-prod-kv \
            --name stripe-api-key \
            --query value -o tsv)
          
          # Use immediately, don't store or log
          # The secret is masked in logs automatically
          echo "::add-mask::$STRIPE_KEY"
          
          # Update container app with secret reference
          az containerapp secret set \
            --name ${{ env.AZURE_CONTAINER_APP }} \
            --resource-group ${{ env.AZURE_RESOURCE_GROUP }} \
            --secrets stripe-key="$STRIPE_KEY"

# ===== Separate job for secret rotation =====
  rotate-secrets:
    name: Rotate Secrets (scheduled)
    runs-on: ubuntu-latest
    if: github.event_name == 'schedule'
    environment: production
    
    steps:
      - name: Azure Login
        uses: azure/login@v2
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      
      - name: Rotate database password
        run: |
          # Generate new password
          NEW_PASSWORD=$(openssl rand -base64 32)
          echo "::add-mask::$NEW_PASSWORD"
          
          # Update in PostgreSQL
          az postgres flexible-server execute \
            --name shopflow-prod-pg \
            --admin-user pgadmin \
            --admin-password "${{ secrets.PG_ADMIN_PASSWORD }}" \
            --query "ALTER USER shopflow_app PASSWORD '$NEW_PASSWORD';"
          
          # Update in Key Vault
          az keyvault secret set \
            --vault-name shopflow-prod-kv \
            --name database-password \
            --value "$NEW_PASSWORD"
          
          # Restart app to pick up new connection string
          az containerapp revision restart \
            --name ${{ env.AZURE_CONTAINER_APP }} \
            --resource-group ${{ env.AZURE_RESOURCE_GROUP }}
          
          echo "Database password rotated successfully"
```
