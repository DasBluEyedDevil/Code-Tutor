---
type: "THEORY"
title: "⚠️ Never Commit Secrets to Git!"
---

❌ BAD - Secrets in application.yml:
app:
  database:
    password: mySecretPassword123  # DON'T DO THIS!
  api:
    key: sk_live_abc123xyz  # SECURITY RISK!

✓ GOOD - Use environment variables:

application.yml:
app:
  database:
    password: ${DB_PASSWORD}  # Read from environment
  api:
    key: ${API_KEY}

Set environment variables:
export DB_PASSWORD=mySecretPassword123
export API_KEY=sk_live_abc123xyz

OR use application-local.yml (gitignored):

.gitignore:
application-local.yml

BEST PRACTICES:
✓ Use environment variables for secrets
✓ Use .env files (not committed to git)
✓ Use secret management (AWS Secrets Manager, HashiCorp Vault)
✓ Never commit passwords, API keys, tokens