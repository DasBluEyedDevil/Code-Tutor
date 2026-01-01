---
type: "WARNING"
title: "Never Commit Passwords to Git"
---

The `passwords.yaml` file contains sensitive credentials and should NEVER be committed to version control.

**What to Do:**

1. Add `passwords.yaml` to your `.gitignore` file (Serverpod does this automatically)
2. Create a `passwords.yaml.template` file with placeholder values for team members
3. Use environment variables or secret management in production

**Example passwords.yaml.template:**

```yaml
# Copy this file to passwords.yaml and fill in real values
database:
  password: 'your_database_password_here'
redis:
  password: ''
```

**Production Credentials:**

In production, use proper secret management:
- AWS Secrets Manager
- Google Cloud Secret Manager
- HashiCorp Vault
- Environment variables from your hosting platform

Never hardcode production passwords in files that could be exposed.

