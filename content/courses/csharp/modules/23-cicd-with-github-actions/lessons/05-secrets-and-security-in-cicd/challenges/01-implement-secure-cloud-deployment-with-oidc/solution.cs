# Implement secure deployment with OIDC

name: Secure Deploy

on:
  push:
    branches: [main]

permissions:
  contents: read
  packages: write
  id-token: write

jobs:
  deploy:
    runs-on: ubuntu-latest
    environment: production
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'
      
      - name: Azure login with OIDC
        uses: azure/login@v2
        with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
      
      - name: Scan for vulnerable packages
        run: |
          dotnet restore
          VULN_OUTPUT=$(dotnet list package --vulnerable 2>&1)
          echo "$VULN_OUTPUT"
          
          if echo "$VULN_OUTPUT" | grep -q "has the following vulnerable packages"; then
            echo "::error::Critical vulnerabilities detected!"
            exit 1
          fi
          echo "No vulnerabilities found"
      
      - name: Fetch secrets from Key Vault
        id: secrets
        run: |
          # Fetch database connection string
          DB_SECRET=$(az keyvault secret show \
            --vault-name shopflow-prod-kv \
            --name database-connection \
            --query value -o tsv)
          echo "::add-mask::$DB_SECRET"
          
          # Fetch API key
          API_KEY=$(az keyvault secret show \
            --vault-name shopflow-prod-kv \
            --name stripe-api-key \
            --query value -o tsv)
          echo "::add-mask::$API_KEY"
          
          # Store for next step (masked in logs)
          echo "db-secret=$DB_SECRET" >> $GITHUB_OUTPUT
          echo "api-key=$API_KEY" >> $GITHUB_OUTPUT
      
      - name: Deploy to Container Apps
        run: |
          # Update secrets in container app
          az containerapp secret set \
            --name shopflow-api \
            --resource-group shopflow-prod-rg \
            --secrets \
              database-url="${{ steps.secrets.outputs.db-secret }}" \
              stripe-key="${{ steps.secrets.outputs.api-key }}"
          
          # Deploy new image
          az containerapp update \
            --name shopflow-api \
            --resource-group shopflow-prod-rg \
            --image ghcr.io/${{ github.repository }}:${{ github.sha }}
      
      - name: Create audit log
        run: |
          TIMESTAMP=$(date -u +%Y-%m-%dT%H:%M:%SZ)
          
          echo "========== DEPLOYMENT AUDIT =========="
          echo "Timestamp: $TIMESTAMP"
          echo "Actor: ${{ github.actor }}"
          echo "Repository: ${{ github.repository }}"
          echo "Commit: ${{ github.sha }}"
          echo "Environment: production"
          echo "Trigger: ${{ github.event_name }}"
          echo "======================================="
          
          # In production, send to logging service
          # az monitor log-analytics workspace ...