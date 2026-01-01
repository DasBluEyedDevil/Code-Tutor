# Implement secure deployment with OIDC

name: Secure Deploy

on:
  push:
    branches: [main]

# TODO: Set minimal required permissions including id-token for OIDC

jobs:
  deploy:
    runs-on: ubuntu-latest
    environment: production
    
    steps:
      - uses: actions/checkout@v4
      
      # TODO: Add Azure login with OIDC (no client secret)
      # Use azure/login@v2
      # Hint: Only need client-id, tenant-id, subscription-id
      
      # TODO: Scan for vulnerable packages
      # Use: dotnet list package --vulnerable
      # Fail if vulnerabilities found
      
      # TODO: Fetch secrets from Key Vault
      # Use: az keyvault secret show
      # Mask the secret with add-mask
      
      # TODO: Deploy with fetched secrets
      # Update container app with secret reference
      
      # TODO: Create audit log
      # Log: actor, commit, timestamp, environment