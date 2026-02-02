---
type: "THEORY"
title: "Why Never Hardcode Secrets"
---

**Hardcoded secrets are one of the most common and dangerous security vulnerabilities.** Here's why:

**What Are Secrets?**
- Database passwords and connection strings
- API keys (Stripe, AWS, SendGrid, etc.)
- JWT signing keys
- Encryption keys
- OAuth client secrets
- SSH private keys
- TLS certificates

**Why Hardcoding Is Dangerous:**

1. **Version Control Exposure**
```python
# This ends up in git history FOREVER
DATABASE_URL = "postgresql://admin:SuperSecret123@db.example.com/prod"
```
Even if you delete it later, it's in the commit history. Attackers scan public repos for exposed secrets.

2. **Shared Secrets Across Environments**
```python
# Same secret in dev, staging, prod = disaster
JWT_SECRET = "my_jwt_secret"  # Compromised in dev? Prod is compromised too.
```

3. **Deployment Pipeline Exposure**
Secrets in code are visible in:
- CI/CD logs
- Docker image layers
- Build artifacts
- Error messages and stack traces

4. **No Rotation Capability**
Hardcoded secrets require code changes to rotate, meaning:
- Downtime during deployment
- Resistance to rotation ("it's too hard")
- Stale credentials stay in use too long

5. **Shared Access**
Anyone with code access has secret access:
- Contractors
- Former employees
- Compromised developer machines

**Real-World Breaches from Exposed Secrets:**
- Uber (2016): Hardcoded AWS credentials in GitHub led to 57M user records exposed
- Twitter (2020): Internal API keys exposed in mobile app code
- Countless startups: Production database credentials in public repos

**The Solution: Environment Variables and Secret Management**
```python
import os

# Good: Read from environment
DATABASE_URL = os.environ["DATABASE_URL"]

# Better: Use a configuration library with validation
from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    database_url: str
    jwt_secret: str
```