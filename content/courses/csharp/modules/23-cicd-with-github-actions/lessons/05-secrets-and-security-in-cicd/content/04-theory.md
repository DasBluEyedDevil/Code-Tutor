---
type: "THEORY"
title: "Security Best Practices"
---

## Principle of Least Privilege

Every workflow, job, and step should have only the permissions it absolutely needs. GitHub Actions defaults to read-only permissions when you explicitly set any permission, which is the safest baseline.

```yaml
# Explicitly declare only needed permissions
permissions:
  contents: read     # Read repository code
  packages: write    # Push container images
  id-token: write    # Use OIDC authentication

jobs:
  build:
    runs-on: ubuntu-latest
    permissions:
      contents: read   # Job-level override: even more restrictive
```

For secrets, create separate credentials for each environment. The development database password should not work in production. Even within production, consider separate credentials for read-only operations versus write operations.

## OpenID Connect (OIDC) Authentication

OIDC eliminates long-lived credentials entirely. Instead of storing a service principal password that never expires, your workflow proves its identity to the cloud provider using GitHub's identity token.

**Traditional (risky):** Store Azure service principal password in GitHub Secrets. Password never expires, if leaked, attacker has permanent access.

**OIDC (recommended):** Configure Azure to trust tokens from your specific GitHub repository, branch, and environment. Each workflow run receives a short-lived token valid for only that run.

```yaml
# Azure OIDC configuration
permissions:
  id-token: write  # Required to request OIDC token

- uses: azure/login@v2
  with:
    client-id: ${{ secrets.AZURE_CLIENT_ID }}      # Identifier, not secret
    tenant-id: ${{ secrets.AZURE_TENANT_ID }}      # Identifier, not secret  
    subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}  # Identifier
    # No client-secret needed!
```

## Secret Rotation

Secrets should be rotated regularly:
- API keys: Every 90 days
- Database passwords: Every 90 days
- Service account credentials: Every 30 days
- Emergency rotation: Immediately if compromise suspected

Automate rotation to make it painless:
1. Generate new secret
2. Update in secret store (Key Vault)
3. Restart application to pick up new value
4. Verify application works with new secret
5. Keep old secret valid briefly for in-flight requests
6. Delete old secret

## Audit Logging

Maintain comprehensive logs of secret access and deployment activities:
- Which workflow accessed which secrets
- Who triggered deployments
- What exactly was deployed (commit SHA, image digest)
- Environment and timestamp of every deployment

```yaml
- name: Create audit record
  run: |
    az monitor log-analytics workspace query \
      --workspace ${{ secrets.LOG_ANALYTICS_ID }} \
      --analytics-query "AuditLogs | where TimeGenerated > ago(1h)" \
      --output table
    
    echo "Deployment audit:"
    echo "  Repository: ${{ github.repository }}"
    echo "  Commit: ${{ github.sha }}"
    echo "  Actor: ${{ github.actor }}"
    echo "  Environment: production"
    echo "  Timestamp: $(date -u +%Y-%m-%dT%H:%M:%SZ)"
```

## Environment Isolation

Never share credentials between environments. A compromised development environment should not enable access to production:

| Resource | Development | Staging | Production |
|----------|-------------|---------|------------|
| Database | dev-shopflow.postgres | staging-shopflow.postgres | prod-shopflow.postgres |
| Azure Subscription | Dev Sub | Staging Sub | Prod Sub |
| Key Vault | shopflow-dev-kv | shopflow-staging-kv | shopflow-prod-kv |
| Service Principal | sp-shopflow-dev | sp-shopflow-staging | sp-shopflow-prod |

Use GitHub Environments to scope secrets. Production secrets are only available to jobs that specify `environment: production`, and production environments should require manual approval from authorized reviewers.

## Dependency Security

Lock dependencies to prevent supply chain attacks:
```bash
# Create lock file
dotnet restore --locked-mode

# Audit for vulnerabilities
dotnet list package --vulnerable --include-transitive
```

Use Dependabot for automated security updates and review dependency changes carefully in pull requests.