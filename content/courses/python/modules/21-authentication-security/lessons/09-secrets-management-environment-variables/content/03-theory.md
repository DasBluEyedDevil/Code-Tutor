---
type: "THEORY"
title: "Secret Rotation and Vault Integration"
---

**Secret Rotation** is the practice of regularly changing secrets to limit the damage window if a secret is compromised.

**Why Rotate Secrets?**
- Limits exposure time if secret is leaked
- Compliance requirements (PCI-DSS, SOC2)
- Reduces risk from insider threats
- Forces good secret management practices

**Rotation Strategies:**

**1. Dual-Secret Rotation (Zero Downtime)**
```
Step 1: Add new secret alongside old
        App accepts: [OldSecret, NewSecret]

Step 2: Update all clients to use new secret
        Clients send: NewSecret

Step 3: Remove old secret
        App accepts: [NewSecret]
```

**2. Automatic Rotation with Vault**
```
[Application] <---> [HashiCorp Vault] <---> [Database]
                           |
                    Generates dynamic
                    credentials with TTL
```

**Secret Management Solutions:**

| Solution | Best For | Features |
|----------|----------|----------|
| HashiCorp Vault | Enterprise, multi-cloud | Dynamic secrets, rotation, audit |
| AWS Secrets Manager | AWS workloads | Auto-rotation, RDS integration |
| Azure Key Vault | Azure workloads | HSM-backed, RBAC |
| Google Secret Manager | GCP workloads | Versioning, IAM |
| Doppler | Startups, simplicity | Easy setup, sync to envs |

**Vault Integration Pattern:**
```python
import hvac  # HashiCorp Vault client

class VaultSecretProvider:
    def __init__(self, vault_url: str, token: str):
        self.client = hvac.Client(url=vault_url, token=token)
    
    def get_database_credentials(self):
        # Dynamic credentials with automatic rotation
        secret = self.client.secrets.database.generate_credentials(
            name='finance-tracker-db'
        )
        return {
            'username': secret['data']['username'],
            'password': secret['data']['password'],
            'ttl': secret['lease_duration']  # Credentials expire automatically
        }
```

**Environment-Specific Secrets:**
```
.env.development   # Local dev secrets (git-ignored)
.env.staging       # Staging secrets (CI/CD only)
.env.production    # NEVER in git - use secret manager

# Or better: No .env files in staging/prod
# Secrets injected by orchestrator (K8s, ECS, etc.)
```